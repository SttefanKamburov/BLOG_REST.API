using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BLOG_API.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace BLOG_API.DB
{
    public class BlogDbContext : DbContext
    {
        private readonly string connectionString;

        public BlogDbContext() {

            this.connectionString = "Server=localhost;Database=Blog;Uid=test;Pwd=test;";
        }

        public BlogDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //   modelBuilder.Entity<Blog>().has
            
        //}


        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(this.connectionString))
            {
                optionsBuilder.UseSqlServer(this.connectionString);
            }
            else
            {
                throw new ArgumentNullException("ConnectionString is empty!");
            }
        }

    }
}
