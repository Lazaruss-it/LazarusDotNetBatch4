﻿using Dapper;
using LazarusDotNetBatch4.RestApi.Models;
using LazarusDotNetBatch4.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace LazarusDotNetBatch4.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DapperService _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

        private BlogModel FindMyId(int id)
        {
            string query = "select * from Tbl_Blog where BlogId = @BlogId";
            
            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id });
            return item;
        }

        //Read
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            var lst = _dapperService.Query<BlogModel>(query);
            return Ok(lst);                              
        }

        //Edit
        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            var item = FindMyId(id);
            if (item is null)
            {
                return NotFound("Data Not Found.");
            }
            return Ok(item);
        }

        //Create
        [HttpPost]
        public IActionResult CreateBlogs(BlogModel blog)
        {
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
            

            int result = _dapperService.Execute(query, blog);

           string message = result > 0 ? "Created Successful" : "Created Failed.";
            return Ok(message);
        }

        //Update All
        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogModel blog)
        {
            var item = FindMyId(id);
            if ( item is null)
            {
                return NotFound("Data Not Found");
            }

            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";


            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Updated Successful" : "Updated Failed";
            return Ok();
        }

        //Update seperate
        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogModel blog)
        {
            var item = FindMyId(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.BlogId = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, blog);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = FindMyId(id);
            if (item is null)
            {
                return NotFound("Data Not Found");
            }

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new BlogModel { BlogId = id });

            string message = result > 0 ? "Delete Successful" : "Delete Failed.";
            return Ok(message);
        }

        
    }
}