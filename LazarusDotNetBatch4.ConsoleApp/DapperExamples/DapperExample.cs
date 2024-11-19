using Dapper;
using LazarusDotNetBatch4.ConsoleApp.Dtos;
using LazarusDotNetBatch4.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazarusDotNetBatch4.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {

        public void Run()
        {
            // Read();
            // Edit(1);
            // Edit(20);
            // Create("test", "ORM", "This is my C# .Net.");
            // Update(12, "dapper", "C#", "ORM");
            Delete(13);
        }

        public void Read()
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = db.Query<BlogDto>("select * from Tbl_Blog").ToList();

            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("------");
            }

        }

        public void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDto>("select * from Tbl_Blog where BlogId = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No data Found.");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("------");
        }

        public void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (
			@BlogTitle,
			@BlogAuthor,
			@BlogContent
		   )";
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);
            if (result == 1)
            {
                Console.WriteLine("Completed Created.");
            }
            else
            {
                Console.WriteLine("You need to create one.");
            }

        }

        public void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";


            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            int result = db.Execute(query, item);

            string message = result > 0 ? "Updated Successful" : "Updated Failed";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {

            var item = new BlogDto
            {
                BlogId = id,
            };

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Delete Successful" : "Delete Failed.";
            Console.WriteLine(message);
        }
    }
}
