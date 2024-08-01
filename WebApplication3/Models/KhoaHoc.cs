using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class KhoaHoc
    {
        [Key]
        [Required]
        public string MaKhoa { get; set; }
        [Required]
        public string TenKhoa { get; set; }
        [Required]
        public int NamHoc { get; set; }
        public List<HocVien>? HocViens { get; set; }
    }
}
