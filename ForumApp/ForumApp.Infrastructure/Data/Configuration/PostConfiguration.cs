using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Infrastructure.Data.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        private Post[] initialPosts = new Post[]
        {
            new Post() { Id=1, Title = "My First Post", Content = "First Post Content"},
            new Post() { Id=2, Title = "My Second Post", Content = "Second Post Content"},
            new Post() { Id=3, Title = "My Third Post", Content = "Third Post Content"},
        };

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(initialPosts);
        }
    }
}
