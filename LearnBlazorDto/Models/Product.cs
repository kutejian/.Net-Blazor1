using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace LearnBlazorDto.Models
{
    public class Product
    {
        public int ProductId { set; get; } = 0;

        [Required]
        [Display(Name = "商品名称")]
        public string ProductName { set; get; } = "";

        [Display(Name = "商品图片")]
        public string ThumbnailImage { set; get; } = "";

        public int CategoryId { set; get; } = 0;

        public Category Category { get; set; }

        public List<ProductImage> Images { get; set; }//注意禁止给books手动赋值

        /// <summary>
        /// 商品条码
        /// </summary>
        public string Code { set; get; }

        /// <summary>
        /// 商品品牌
        /// </summary>
        public string BrandName { set; get; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { set; get; }

        /// <summary>
        /// 单价
        /// </summary>
        public double UnitPrice { set; get; } = 0;

        /// <summary>
        /// 使用感受
        /// </summary>
        public string? UseFeel { set; get; }

        /// <summary>
        /// 新增时间
        /// </summary>
        public DateTime CreatedTime { set; get; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LastUpdateTime { set; get; } = DateTime.Now;
    }
}