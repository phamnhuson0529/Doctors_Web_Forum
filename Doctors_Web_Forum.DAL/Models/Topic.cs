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

        [Column("topic_name")]
        public string? TopicName { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}
