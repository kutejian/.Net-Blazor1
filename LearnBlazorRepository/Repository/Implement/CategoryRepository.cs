using AutoMapper;
using LearnBlazorDto.Models;
using LearnBlazorEntity.Models;
using LearnBlazorRepository.Repository;
using LearnBlazorRepository.Repository.Interface;
using SqlSugar;

namespace Niunan.LearnBlazor.WebServer.Repository.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlSugarClient _SqlSugarDb;
        private IMapper _mapper;

        public CategoryRepository(SqlSugarHelper SqlSugarHelper, IMapper mapper)
        {
            _SqlSugarDb = SqlSugarHelper.SqlSugarDb();
            _mapper = mapper;
        }

        //添加分类
        public void Add(Category model)
        {
            var modelEneity = _mapper.Map<CategoryEntity>(model);
            _SqlSugarDb.Insertable<CategoryEntity>(modelEneity).ExecuteCommand();
        }

        //删除分类
        public void Delete(int id)
        {
            //有下级时不能删除
            int xjcount = _SqlSugarDb.Queryable<CategoryEntity>().Where(a => a.ParentId == id).Count();
            if (xjcount > 0)
            {
                throw new Exception("该分类下还有下级,不可删除!");
            }
            //有商品时不能删除
            int procount = _SqlSugarDb.Queryable<ProductEntity>().Where(p => p.CategoryId == id).Count();
            if (procount > 0)
            {
                throw new Exception("该分类下还有商品,不可删除!");
            }
            _SqlSugarDb.Deleteable<CategoryEntity>(a => a.CategoryId == id).ExecuteCommand();
        }

        //好像是获取分类
        public List<string> GetMBXList(int caid)
        {
            if (caid == 0)
            {
                return new List<string>() { "全部商品" };
            }
            List<string> list = new List<string>();
            Category ca = GetModel(caid);
            string[] caids = ca.CategoryPath.Split(',');
            foreach (var item in caids)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                Category temp = GetModel(int.Parse(item));
                list.Add(temp.CategoryName);
            }
            list.Add(ca.CategoryName);
            return list;
        }

        //获取单个分类
        public Category GetModel(int caid)
        {
            var modelEneity = _SqlSugarDb.Queryable<CategoryEntity>().Single(ca => ca.CategoryId == caid);
            var Category = _mapper.Map<Category>(modelEneity);
            return Category;
        }

        //获取树状图分类
        public List<Category> GetTreeModel()
        {
            var ListCategory = _mapper.Map<List<Category>>(_SqlSugarDb.Queryable<CategoryEntity>().ToList());

            List<Category> categories = ListCategory;
            List<Category> list = new List<Category>();
            var top = categories.Where(ca => ca.ParentId == 0).OrderBy(a => a.Sort).ToList();
            foreach (var oneca in top)
            {
                oneca.Items.Clear();
                DiGui(oneca, categories);
                list.Add(oneca);
            }
            return list;
        }

        //修改分类
        public void Update(Category model)
        {
            var modelEneity = _mapper.Map<CategoryEntity>(model);
            _SqlSugarDb.Updateable<CategoryEntity>(modelEneity).ExecuteCommand();
        }

        /// <summary>
        /// 递归添加下级节点
        /// </summary>
        /// <param name="oneca"></param>
        private void DiGui(Category oneca, List<Category> categories)
        {
            var sub = categories.Where(ca => ca.ParentId == oneca.CategoryId).OrderBy(a => a.Sort).ToList();
            foreach (var oneca2 in sub)
            {
                oneca2.Items.Clear();
                DiGui(oneca2, categories);
                oneca.Items.Add(oneca2);
            }
        }
    }
}