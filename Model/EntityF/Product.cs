namespace Model.EntityF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã sản phẩm hiển thị")]
        public string Code { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string Decription { get; set; }

        [StringLength(250)]
        [Display(Name = "Ảnh")]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        [Display(Name = "Ảnh số nhiều")]
        public string MoreImages { get; set; }
        [Display(Name = "Giá ")]
        public decimal? Price { get; set; }
        [Display(Name = "Giá Khuyến mãi")]
        public decimal? PromotionPrice { get; set; }
        [Display(Name = "Có thuế VAT")]
        public bool? IncludeVAT { get; set; }
        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }
        [Display(Name = "Loại")]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }
        [Display(Name = "Bảo hành")]
        public int? Warranty { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Người thêm")]
        public string CreatedBy { get; set; }
        [Display(Name = "Ngày sửa")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Người sửa")]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeyword { get; set; }

        [StringLength(250)]
        public string MetaDecriptions { get; set; }
        [Display(Name = "Trạng thái")]
        public bool? Status { get; set; }

        public DateTime? TopHot { get; set; }
        [Display(Name = "Lượt xem")]
        public int? ViewCount { get; set; }
    }
}
