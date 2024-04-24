using LearnBlazorDto.Models;

namespace LearnBlazorRepository.Repository.Interface
{
    public interface ICategoryRepository
    {
        List<Category> GetTreeModel();

        List<string> GetMBXList(int caid);

        Category GetModel(int caid);

        void Add(Category model);

        void Delete(int id);

        void Update(Category model);
    }
}