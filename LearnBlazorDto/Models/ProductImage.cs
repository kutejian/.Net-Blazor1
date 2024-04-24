using SqlSugar;
using System.Security.Principal;

namespace LearnBlazorDto.Models
{
    /// <summary>
    /// 商品图片
    /// </summary>
    public class ProductImage
    {
        public int ImageId { set; get; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductId { set; get; }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string Title { set; get; }
    }
}