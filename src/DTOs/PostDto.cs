using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Cat3_API.Src.DTOs
{
    public class PostDto
    {
        [MinLength(5, ErrorMessage = "El título debe tener al menos 5 caracteres.")]
        public required string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe proporcionar una imagen.")]
        public required IFormFile Image { get; set; }
    }
}
