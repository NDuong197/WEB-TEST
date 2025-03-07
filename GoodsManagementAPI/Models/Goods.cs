using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodsManagementAPI.Models
{
    public class Goods
    {
        [Key]
        [StringLength(9, MinimumLength = 9)]
        public string MaHangHoa { get; set; }

        [Required]
        [MaxLength(255)]
        public string TenHangHoa { get; set; }

        public int SoLuong { get; set; }
        [Column("ghi_chu")] 
        public string GhiChu { get; set; }
    }
}
