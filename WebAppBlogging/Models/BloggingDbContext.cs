using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppBlogging.Models
{
    public class BloggingDbContext:DbContext
    {
        public BloggingDbContext(DbContextOptions<BloggingDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Entities> Entities { get; set; }
    }
}
