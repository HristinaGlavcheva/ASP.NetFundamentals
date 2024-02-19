using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.ViewModels;
using System.Runtime.Serialization;
using System.Security.Claims;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext dbContext;

        public SeminarController(SeminarHubDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new SeminarFormModel();
            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarFormModel model)
        {
            var categories = await GetCategories();

            if (!ModelState.IsValid)
            {
                model.Categories = categories;

                return View(model);
            }

            var seminar = new Seminar
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                DateAndTime = model.DateAndTime,
                Duration = model.Duration,
                CategoryId = model.CategoryId,
                OrganizerId = GetUserId()
            };

            await dbContext.Seminars.AddAsync(seminar);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var seminar = await dbContext.Seminars
                .FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new SeminarFormModel
            {
                Topic = seminar.Topic,
                Lecturer = seminar.Lecturer,
                Details = seminar.Details,
                DateAndTime = seminar.DateAndTime,
                Duration = seminar.Duration,
                CategoryId = seminar.CategoryId
            };

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeminarFormModel model, int id)
        {
            var seminar = await dbContext.Seminars
                .FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            seminar.Topic = model.Topic;
            seminar.Lecturer = model.Lecturer;
            seminar.Details = model.Details;
            seminar.DateAndTime = model.DateAndTime;
            seminar.Duration = model.Duration;
            seminar.CategoryId = model.CategoryId;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await dbContext.Seminars
                .AsNoTracking()
                .Select(s => new SeminarInfoViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    DateAndTime = s.DateAndTime.ToString(DateTimeFormatString),
                    Category = s.Category.Name,
                    Organizer = s.Organizer.UserName
                }).ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var seminar = await dbContext.Seminars
                .FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            var seminarParticipant = new SeminarParticipant
            {
                SeminarId = seminar.Id,
                ParticipantId = GetUserId()
            };

            if (!await dbContext.SeminarParticipants.ContainsAsync(seminarParticipant))
            {
                await dbContext.SeminarParticipants.AddAsync(seminarParticipant);
                await dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Joined));
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await dbContext.SeminarParticipants
                .Where(sp => sp.ParticipantId == userId)
                .AsNoTracking()
                .Select(sp => new SeminarInfoViewModel
                {
                    Id = sp.SeminarId,
                    Topic = sp.Seminar.Topic,
                    Lecturer = sp.Seminar.Lecturer,
                    DateAndTime = sp.Seminar.DateAndTime.ToString(DateTimeFormatString),
                    Organizer = sp.Seminar.Organizer.UserName
                })
                .ToListAsync();


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var seminar = await dbContext.Seminars
                .FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            var seminarParticipantToRemove = await dbContext.SeminarParticipants
                .FirstOrDefaultAsync(sp => sp.ParticipantId == GetUserId() && sp.SeminarId == seminar.Id);

            if (seminarParticipantToRemove == null)
            {
                return BadRequest();
            }

            dbContext.SeminarParticipants.Remove(seminarParticipantToRemove);

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await dbContext.Seminars
                .Where(s => s.Id == id)
                .AsNoTracking()
                .Select(s => new SeminarDetailsViewModel
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    DateAndTime = s.DateAndTime.ToString(DateTimeFormatString),
                    Duration = s.Duration.ToString(),
                    Category = s.Category.Name,
                    Details = s.Details,
                    Organizer = s.Organizer.UserName
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var seminar = await dbContext.Seminars.FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new SeminarInfoViewModel
            {
                Id= seminar.Id,
                Topic = seminar.Topic,
                DateAndTime = seminar.DateAndTime.ToString(DateTimeFormatString)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(SeminarInfoViewModel model, int id)
        {
            var seminar = await dbContext.Seminars.FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var seminarsParticipants = await dbContext.SeminarParticipants
                .Where(sp => sp.SeminarId == id).ToListAsync();

            dbContext.SeminarParticipants.RemoveRange(seminarsParticipants);
            dbContext.Seminars.Remove(seminar);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            var categories = await dbContext.Categories
                .AsNoTracking()
                .Select(t => new CategoryViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToListAsync();

            return categories;
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
