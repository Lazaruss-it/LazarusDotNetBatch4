using LazarusDotNetBatch4.RestApi.Db;
using LazarusDotNetBatch4.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LazarusDotNetBatch4.RestApi.Controllers
{
    // https://localhost:3000 => domain url
    // api/blog => endpoint
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly AppDbContext _context;

        public BlogController() // constractor
        {
            _context = new AppDbContext();
        }


        [HttpGet]
        public IActionResult Read()
        {
            var lst = _context.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id) // Search id user
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if ( item is null )
            {
                return NotFound("No Data Found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _context.Blogs.Add(blog);
            var result = _context.SaveChanges();

            string message = result > 0 ? "Completely Created." : "Failed.";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if ( item is null)
            {
                return NotFound("No Data Found.");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            var result = _context.SaveChanges();

            string message = result > 0 ? "Saving Successful" : "Saving Failed";

            
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {

            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }

            var result = _context.SaveChanges();

            string message = result > 0 ? "Saving Successful" : "Saving Failed";


            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if ( item is null)
            {
                return NotFound("No Data Found.");
            }

            _context.Blogs.Remove(item);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Delete Succesful" : "Failed.";
            return Ok(message); 
        }

        
    }
}
