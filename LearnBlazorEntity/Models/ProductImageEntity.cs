using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SqlSugar;
using System.Security.Principal;

namespace LearnBlazorEntity.Models
{
    /// <summary>
    /// 商品图片
    /// </summary>
    public class ProductImageEntity
    {
        /// <summary>
        /// 主键自增
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ImageId { set; get; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductId { set; get; }
        /// <summary>
        /// 图片名称 
        /// </summary>
        public string?  Title { set; get; }
    }
}
