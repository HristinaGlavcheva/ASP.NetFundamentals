using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoardApp.Data.Models;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Data.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder
                .HasOne(t => t.Board)
                .WithMany(b => b.Tasks)
                .HasForeignKey(t => t.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(SeedTasks());
        }

        private IEnumerable<Task> SeedTasks()
        {
            return new Task[]
            {
                new Task()
                {
                    Id = 1,
                    Title = "Improve CSS styles",
                    Descripion = "Implement better styling for all public pages",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = ConfigurationHelper.TestUser.Id,
                    BoardId = ConfigurationHelper.OpenBoard.Id
                },
                new Task()
                {
                    Id = 2,
                    Title = "Android Client App",
                    Descripion = "Create Android client app for the TaskBoard RESTful API",
                    CreatedOn = DateTime.Now.AddDays(-5),
                    OwnerId = ConfigurationHelper.TestUser.Id,
                    BoardId = ConfigurationHelper.OpenBoard.Id
                },
                 new Task()
                {
                    Id = 3,
                    Title = "Desktop Client App",
                    Descripion = "Create Windows Forms desktop app client for the TaskBoard RESTful API",
                    CreatedOn = DateTime.Now.AddDays(-1),
                    OwnerId = ConfigurationHelper.TestUser.Id,
                    BoardId = ConfigurationHelper.InProgressBoard.Id
                },
                  new Task()
                {
                    Id = 4,
                    Title = "Create Tasks",
                    Descripion = "Implement [Create Task] page for adding new tasks",
                    CreatedOn = DateTime.Now.AddDays(-1),
                    OwnerId = ConfigurationHelper.TestUser.Id,
                    BoardId = ConfigurationHelper.DoneBoard.Id
                },
            };
        }
    }
}
