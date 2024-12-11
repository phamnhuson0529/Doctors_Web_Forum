using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors_Web_Forum.DAL.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public string? QuestionText { get; set; }
        public DateTime PostDate { get; set; }
        public bool Status { get; set; }
    }
}
