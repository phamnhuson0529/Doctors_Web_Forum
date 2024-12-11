using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors_Web_Forum.DAL.Models
{
    [Table("Profiles")]
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Picture { get; set; } // Ảnh cá nhân 
        public string? Contact { get; set; } // liên hệ
        public bool Status { get; set; } // trạng thái profile 
    }
}
