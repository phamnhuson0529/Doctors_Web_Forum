using Doctors_Web_Forum.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors_Web_Forum.DAL.Data
{
    public class DataDBContext : DbContext
    {
        public DataDBContext(DbContextOptions<DataDBContext> options) : base(options) { }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<DoctorProfile> DoctorProfiles { get; set; }

        public DbSet<LoginAttempt> LoginAttempts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<AdminLog> AdminLogs { get; set; }  
 
    }
}
