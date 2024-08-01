using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class HocVien
    {
        public Guid ID { get; set; }
        [Required]
        public string HoTen { get; set; }
        [Required]
        public int Tuoi { get; set; }
        [Required]
        public string ChuyenNganh { get; set; }
        public KhoaHoc? KhoaHoc { get; set; }
    }
}
