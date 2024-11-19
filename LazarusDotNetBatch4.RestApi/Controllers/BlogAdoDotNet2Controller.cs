using LazarusDotNetBatch4.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
//using System.Data.SqlClient;
using LazarusDotNetBatch4.Shared;

namespace LazarusDotNetBatch4.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog";
           
            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult EditBlogs(int id)
        {
            string query = "select * from Tbl_Blog where BlogId = @BlogId";

            
            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id)); 
            if (item is null )
            {
                return NotFound("No Data Found");
            }

            
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlogs (BlogModel blog)
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
            

            int result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent));

            string message = result > 0 ? "Saving Successful" : "Unsuccessful.";
            Console.WriteLine(message);
            return Ok(message);

            //return StatusCode(300, message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            connection.Open();


            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result);

            connection.Close();

            

            string message = result > 0 ? "Updating Successful." : "Updating Failed";
            Console.WriteLine(message);
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogModel blog)
        {
            // Initialize query and list to hold update clauses
            string query = "UPDATE [dbo].[Tbl_Blog] SET ";
            List<string> updateFields = new List<string>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            // Dynamically add non-null or non-empty fields to the update query
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                updateFields.Add("[BlogTitle] = @BlogTitle");
                parameters.Add(new SqlParameter("@BlogTitle", blog.BlogTitle));
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                updateFields.Add("[BlogAuthor] = @BlogAuthor");
                parameters.Add(new SqlParameter("@BlogAuthor", blog.BlogAuthor));
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                updateFields.Add("[BlogContent] = @BlogContent");
                parameters.Add(new SqlParameter("@BlogContent", blog.BlogContent));
            }

            // Validate that there is at least one field to update
            if (updateFields.Count == 0)
            {
                return NotFound("No Data Found.");
            }

            // Combine update fields and append where clause
            query += string.Join(", ", updateFields) + " WHERE BlogId = @BlogId";
            parameters.Add(new SqlParameter("@BlogId", id));

            // ADO.NET connection and execution
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open(); // Open the connection

            SqlCommand command = new SqlCommand(query, connection);

            // Add parameters to the command
            foreach (var param in parameters)
            {
                command.Parameters.Add(param);
            }

            int result = command.ExecuteNonQuery(); // Execute the query

            connection.Close(); // Close the connection

            // Check if any rows were updated
            string message = result > 0 ? "Patch Successful." : "Patch Failed. Blog ID not found.";
            return Ok(message);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs (int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            connection.Open();


            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();

            Console.WriteLine("result value is " + result);

            connection.Close();

            string message = result > 0 ? "Delete Successful" : "Delete Unsuccessful";
            Console.WriteLine(message);
            return Ok(message);
        }
    }
}
