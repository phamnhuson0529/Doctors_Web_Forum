using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors_Web_Forum.DAL.Models
{
    [Table("Topics")]
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        public string? TopicName { get; set; } // tên chủ đề cần thảo luận và đăng bài 
        public string? Description { get; set; } // mô tả về chủ đề 
        public bool Status { get; set; } // trạng thái của chủ đề 
    }
}
