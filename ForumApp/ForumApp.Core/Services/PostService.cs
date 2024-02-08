using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data;
using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ForumApp.Core.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext dbContext;

        private readonly ILogger logger;

        public PostService(ForumDbContext _dbContext, ILogger<PostService> _logger)
        {
            dbContext = _dbContext;
            logger = _logger;
        }

        public async Task AddAsync(PostModel model)
        {
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content
            };

            try
            {
                await dbContext.Posts.AddAsync(post);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PostService.AddAsync");

                throw new ApplicationException("Operation failed. Please, try again");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var post = await dbContext.Posts.FindAsync(id);

            if (post == null)
            {
                throw new ApplicationException("Invalid post");
            }

            dbContext.Remove(post);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(PostModel model)
        {
            var post = await dbContext.Posts.FindAsync(model.Id);

            if (post == null)
            {
                throw new ApplicationException("Invalid post");
            }

            post.Title = model.Title;
            post.Content = model.Content;

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostModel>> GetAllAsync()
        {
            var model = await dbContext
                .Posts
                .AsNoTracking()
                .Select(p => new PostModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                }).ToListAsync();

            return model;
        }

        public async Task<PostModel?> GetByIdAsync(int id)
        {
            return await dbContext.Posts
                .Where(p => p.Id == id)
                .Select(p => new PostModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
