using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors_Web_Forum.DAL.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public DateTime RegDate { get; set; } = DateTime.Now; // ngày tạo 
        public DateTime? LastLogin { get; set; } // thời gian login gần nhất 
        public bool Status { get; set; } // trạng thái tài khoản 
        public string? Role { get; set; } // Quyền 
    }
}
