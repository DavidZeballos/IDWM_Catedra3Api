using Cat3_API.Src.Data;
using Cat3_API.Src.DTOs;
using Cat3_API.Src.Models;
using Cat3_API.Src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cat3_API.Src.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CloudinaryService _cloudinaryService;

        public PostsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, CloudinaryService cloudinaryService)
        {
            _context = context;
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromForm] PostDto postDto)
        {
            var userId = _userManager.GetUserId(User) ?? throw new UnauthorizedAccessException("Usuario no autenticado.");

            if (postDto.Image.Length > 5 * 1024 * 1024)
                return BadRequest(new { Message = "El tamaño de la imagen no debe exceder los 5MB." });

            var allowedFormats = new[] { "image/jpeg", "image/png" };
            if (!allowedFormats.Contains(postDto.Image.ContentType))
                return BadRequest(new { Message = "El formato de la imagen debe ser JPG o PNG." });

            var imageUrl = await _cloudinaryService.UploadImageAsync(postDto.Image);

            var post = new Post
            {
                Title = postDto.Title,
                ImageUrl = imageUrl,
                PublishDate = DateTime.Now,
                UserId = userId
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Post creado exitosamente.", ImageUrl = imageUrl });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _context.Posts
                .Select(p => new
                {
                    p.Title,
                    p.ImageUrl,
                    PublishDate = p.PublishDate.ToString("dd/MM/yyyy HH:mm")
                })
                .ToListAsync();

            if (!posts.Any())
                return Ok(new { Message = "No hay posts disponibles." });

            return Ok(posts);
        }
    }
}
