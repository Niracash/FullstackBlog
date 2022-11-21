using Blog.API.Data;
using Blog.API.Models.DTO;
using Blog.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly BlogDbContext db;
        // inject dbcontext
        public PostController(BlogDbContext db)
        {
           this.db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var posts = await db.BlogPosts.ToListAsync();
            return Ok(posts);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetPostById")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await db.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if(post != null)
            {
                return Ok(post);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDTO createPostDTO)
        {
            // convert DTO to Entity
            var post = new BlogPost()
            {
                BlogTitle = createPostDTO.BlogTitle,
                BlogContent = createPostDTO.BlogContent,
                BlogAuthor = createPostDTO.BlogAuthor,
                BlogImageUrl = createPostDTO.BlogImageUrl,
                PublishDate = createPostDTO.PublishDate,
                EditedDate = createPostDTO.EditedDate,
                BlogSummary = createPostDTO.BlogSummary,
                UrlHandle = createPostDTO.UrlHandle,
                Visible = createPostDTO.Visible
            };

            post.Id = Guid.NewGuid();
            await db.BlogPosts.AddAsync(post);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> EditPost([FromRoute] Guid id, EditPostDTO editPostDTO)
        {
            // DTO to Entity
            var post = new BlogPost()
            {
                BlogTitle = editPostDTO.BlogTitle,
                BlogContent = editPostDTO.BlogContent,
                BlogAuthor = editPostDTO.BlogAuthor,
                BlogImageUrl = editPostDTO.BlogImageUrl,
                PublishDate = editPostDTO.PublishDate,
                EditedDate = editPostDTO.EditedDate,
                BlogSummary = editPostDTO.BlogSummary,
                UrlHandle = editPostDTO.UrlHandle,
                Visible = editPostDTO.Visible
            };

            //Check if post exists
            var findPost = await db.BlogPosts.FindAsync(id);
            if (findPost != null)
            {
                findPost.BlogTitle = editPostDTO.BlogTitle;
                findPost.BlogContent = editPostDTO.BlogContent;
                findPost.BlogAuthor = editPostDTO.BlogAuthor;
                findPost.BlogImageUrl = editPostDTO.BlogImageUrl;
                findPost.PublishDate = editPostDTO.PublishDate;
                findPost.EditedDate = editPostDTO.EditedDate;
                findPost.BlogSummary = editPostDTO.BlogSummary;
                findPost.UrlHandle = editPostDTO.UrlHandle;
                findPost.Visible = editPostDTO.Visible;
                await db.SaveChangesAsync();
                return Ok(findPost);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var findPost = await db.BlogPosts.FindAsync(id);
            if(findPost != null)
            {
                db.Remove(findPost);
                await db.SaveChangesAsync();
                return Ok(findPost);
            }
            return NotFound();
        }

    }
}
