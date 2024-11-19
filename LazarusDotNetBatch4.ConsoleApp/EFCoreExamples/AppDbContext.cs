using LazarusDotNetBatch4.ConsoleApp.Dtos;
using LazarusDotNetBatch4.ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazarusDotNetBatch4.ConsoleApp.EFCoreExamples
{
    internal class AppDbContext : DbContext // Microsoft.EFCore
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<BlogDto> Blogs { get; set; }
    }
}
