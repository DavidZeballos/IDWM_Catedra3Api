using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cat3_API.Src.Models;

namespace Cat3_API.Src.Data
{
    public static class Seeder
    {
        public static async Task SeedData(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            // Migrar la base de datos
            await context.Database.MigrateAsync();

            // Crear un usuario de prueba
            if (!userManager.Users.Any())
            {
                var testUser = new IdentityUser
                {
                    UserName = "testuser@example.com",
                    Email = "testuser@example.com",
                };
                await userManager.CreateAsync(testUser, "Test123!");
            }

            // Crear posts de prueba
            if (!context.Posts.Any())
            {
                context.Posts.Add(new Post
                {
                    Title = "Primer post",
                    ImageUrl = "https://via.placeholder.com/300",
                    PublishDate = DateTime.Now,
                    UserId = userManager.Users.First().Id,
                });

                context.Posts.Add(new Post
                {
                    Title = "Segundo post",
                    ImageUrl = "https://via.placeholder.com/300",
                    PublishDate = DateTime.Now,
                    UserId = userManager.Users.First().Id,
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
