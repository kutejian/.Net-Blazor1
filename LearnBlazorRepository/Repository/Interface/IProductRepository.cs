using LearnBlazorDto.Models;

namespace LearnBlazorRepository.Repository.Interface
{
    public interface IProductRepository
    {
        void Add(Product model);

        void Delete(int id);

        void Update(Product model);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="caId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<Product> GetListPage(string searchKey = "", int caId = 0, int pageSize = 8, int pageIndex = 1);

        Product GetModel(int id);

        int CalcCount(int caid);

        int CalcCountPage(string searchKey = "", int caId = 0);
    }
}