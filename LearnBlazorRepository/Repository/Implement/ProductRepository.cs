using AntDesign;
using AutoMapper;
using LearnBlazorDto.Models;
using LearnBlazorEntity.Models;
using LearnBlazorRepository.Repository;
using LearnBlazorRepository.Repository.Interface;
using SqlSugar;
using System.Drawing.Printing;

namespace Niunan.LearnBlazor.WebServer.Repository.Implement
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICategoryRepository _category;
        private readonly SqlSugarClient _SqlSugarDb;
        private readonly IMapper _mapper;

        public ProductRepository(ICategoryRepository category, SqlSugarHelper SqlSugarHelper, IMapper mapper)
        {
            _category = category;
            _SqlSugarDb = SqlSugarHelper.SqlSugarDb();
            _mapper = mapper;
        }

        //添加产品
        public void Add(Product model)
        {
            var modelEneity = _mapper.Map<ProductEntity>(model);

            List<ProductEntity> templist = new List<ProductEntity>();
            templist.Add(modelEneity);

            _SqlSugarDb.InsertNav(templist).Include(a => a.Images).ExecuteCommand();
            //  SqlSugarHelper.Db.Insertable(model).ExecuteCommand();
        }

        //产品总和
        public int CalcCount(int caid)
        {
            if (caid == 0)
            {
                //查询全部
                return _SqlSugarDb.Queryable<ProductEntity>().Count();
            }
            Category ca = _category.GetModel(caid);
            string temp = "";
            if (ca.ParentId == 0)
            {
                //第一级分类 ,1,
                temp = $",{ca.CategoryId},";
            }
            else
            {
                temp = ca.CategoryPath + ca.CategoryId + ",";
            }

            //下级分类的商品数
            int a = _SqlSugarDb.Queryable<ProductEntity>().Where(it => it.Category.CategoryPath.StartsWith(temp)).Count();
            //本级分类的商品数
            int b = _SqlSugarDb.Queryable<ProductEntity>().Where(it => it.CategoryId == caid).Count();
            return a + b;
        }

        //产品总和分页
        public int CalcCountPage(string searchKey = "", int caId = 0)
        {
            var q = _SqlSugarDb.Queryable<ProductEntity>().Where(a => a.ProductName.Contains(searchKey));
            if (caId != 0)
            {
                Category ca = _category.GetModel(caId);
                string temp = "";
                if (ca.ParentId == 0)
                {
                    //第一级分类 ,1,
                    temp = $",{ca.CategoryId},";
                }
                else
                {
                    temp = ca.CategoryPath + ca.CategoryId + ",";
                }
                q = q.Where(pro => pro.CategoryId == caId || pro.Category.CategoryPath.StartsWith(temp));
            }
            return q.Count();
        }

        //删除产品
        public void Delete(int id)
        {
            _SqlSugarDb.Deleteable<ProductEntity>().Where(it => it.ProductId == id).ExecuteCommand();
        }

        //获取产品分页

        public List<Product> GetListPage(string searchKey = "", int caId = 0, int pageSize = 8, int pageIndex = 1)
        {
            var q = _SqlSugarDb.Queryable<ProductEntity>().Where(a => a.ProductName.Contains(searchKey));
            if (caId != 0)
            {
                Category ca = _category.GetModel(caId);
                string temp = "";
                if (ca.ParentId == 0)
                {
                    //第一级分类 ,1,
                    temp = $",{ca.CategoryId},";
                }
                else
                {
                    temp = ca.CategoryPath + ca.CategoryId + ",";
                }
                q = q.Where(pro => pro.CategoryId == caId || pro.Category.CategoryPath.StartsWith(temp));
            }
            q = q.OrderByDescending(pro => pro.LastUpdateTime);
            var listProductEntity = q.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            var ListProduct = _mapper.Map<List<Product>>(listProductEntity);
            return ListProduct;
        }

        //获取单个产品
        public Product GetModel(int id)
        {
            var listProductEntity = _SqlSugarDb.Queryable<ProductEntity>()
                .Includes(a => a.Category)
                .Includes(a => a.Images)
                .Single(it => it.ProductId == id);

            var ListProduct = _mapper.Map<Product>(listProductEntity);
            return ListProduct;
        }

        //修改产品
        public void Update(Product model)
        {
            var modelEneity = _mapper.Map<ProductEntity>(model);

            List<ProductEntity> templist = new List<ProductEntity>();

            templist.Add(modelEneity);
            _SqlSugarDb.UpdateNav<ProductEntity>(templist).Include(a => a.Images).ExecuteCommand();
            // SqlSugarHelper.Db.Updateable(model).ExecuteCommand();
        }
    }
}