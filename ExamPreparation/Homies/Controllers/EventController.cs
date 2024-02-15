using Homies.Data;
using Homies.Data.Models;
using Homies.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Homies.Data.DataConstants;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext dbContext;

        public EventController(HomiesDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await dbContext.Events
                .AsNoTracking()
                .Select(e => new EventsAllViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Type = e.Type.Name,
                    Start = e.Start.ToString(DateTimeFormat),
                    Organiser = e.Organiser.UserName
                }).ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var e = await dbContext.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!e.EventsParticipants.Any(p => p.HelperId == userId))
            {
                var eventParticipant = new EventParticipant
                {
                    EventId = e.Id,
                    HelperId = userId
                };

                e.EventsParticipants.Add(eventParticipant);

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await dbContext.EventParticipants
                .Where(ep => ep.HelperId == userId)
                .AsNoTracking()
                .Select(ep => new EventsAllViewModel
                {
                    Id = ep.EventId,
                    Name = ep.Event.Name,
                    Type = ep.Event.Type.Name,
                    Start = ep.Event.Start.ToString(DateTimeFormat),
                    Organiser = ep.Event.Organiser.UserName
                })
                .ToListAsync();


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var e = await dbContext.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var ep = e.EventsParticipants.FirstOrDefault(ep => ep.HelperId == userId);

            if (ep == null)
            {
                return BadRequest();
            }

            e.EventsParticipants.Remove(ep);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventFormViewModel();
            model.Types = await GetTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            var types = await GetTypes();

            if (!ModelState.IsValid)
            {
                model.Types = types;
                
                return View(model);
            }

            var userId = GetUserId();

            var e = new Event
            {
                CreatedOn = DateTime.UtcNow,
                Name = model.Name,
                Description = model.Description,
                Start = model.Start,
                End = model.End,
                OrganiserId = userId,
                TypeId = model.TypeId,
            };

            await dbContext.Events.AddAsync(e);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = await dbContext.Events
                .FindAsync(id);

            if(e == null)
            {
                return BadRequest();
            }

            if(e.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new EventFormViewModel
            {
                Name = e.Name,
                Description = e.Description,
                Start = e.Start,
                End = e.End,
                TypeId = e.TypeId
            };

            model.Types = await GetTypes();
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormViewModel model, int id)
        {
            var e = await dbContext.Events
                .FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Types = await GetTypes();

                return View(model);
            }

            e.Name = model.Name;
            e.Description = model.Description;
            e.Start = model.Start;
            e.End = model.End;
            e.TypeId = model.TypeId;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await dbContext.Events
                .Where(e => e.Id == id)
                .AsNoTracking()
                .Select(e => new DetailsViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start.ToString(DateTimeFormat),
                    End = e.End.ToString(DateTimeFormat),
                    CreatedOn = e.CreatedOn.ToString(DateTimeFormat),
                    Organiser = e.Organiser.UserName,
                    Type = e.Type.Name
                })
                .FirstOrDefaultAsync();

            if(model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<IEnumerable<TypeViewModel>> GetTypes()
        {
            var types = await dbContext.Types
                .AsNoTracking()
                .Select(t => new TypeViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToListAsync();

            return types;
        }
    }
}
