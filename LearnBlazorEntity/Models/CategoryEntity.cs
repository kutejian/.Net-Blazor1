using SqlSugar;
using System.Security.Principal;

namespace LearnBlazorEntity.Models
{
    public class CategoryEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int CategoryId { get; set; } = 0;

        public string CategoryName { get; set; } = "";
        public int ParentId { set; get; } = 0;

        /// <summary>
        /// 分类路径，存储所有父级ID，如：,1,2,3,
        /// </summary>
        public string CategoryPath { set; get; } = "";

        /// <summary>
        /// 排序字段,从小到大
        /// </summary>
        public int Sort { set; get; } = 0;

        [SugarColumn(IsIgnore = true)]
        public List<CategoryEntity> Items { set; get; } = new List<CategoryEntity>();
    }
}