using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace TestProj.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EmailModel> Emails { get; set; }
        public DbSet<AttributeModel> Attributes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //    builder.Entity<AttributeModel>(
            //eb =>
            //{
            //    eb.HasNoKey();
            //  //  eb.ToView("View_BlogPostCounts");
            //    eb.Property(v => v.EmailKey).HasColumnName("EmailKey");
            //});
            //}
        }
    }
}
