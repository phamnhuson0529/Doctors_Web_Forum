using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors_Web_Forum.DAL.Models
{
    [Table("DoctorProfiles")]
    public class DoctorProfile
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Profession { get; set; } // Nghề nghiệp 
        public string? Experience { get; set; } // Kinh nghiệm 
        public bool Status { get; set; } // Trạng thái profile
    }
}
