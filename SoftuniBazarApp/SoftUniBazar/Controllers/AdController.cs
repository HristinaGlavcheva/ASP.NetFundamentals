﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.ViewModels;
using System.Security.Claims;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly BazarDbContext dbContext;

        public AdController(BazarDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await dbContext.Ads
                .AsNoTracking()
                .Select(a => new AdInfoViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Category = a.Category.Name,
                    ImageUrl = a.ImageUrl,
                    Price = a.Price.ToString(),
                    Owner = a.Owner.UserName,
                    CreatedOn = a.CreatedOn.ToString(DataConstants.DateTimeFormat),
                }).ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AdFormViewModel();
            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            var ad = new Ad
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                OwnerId = GetUserId(),
                CreatedOn = DateTime.UtcNow
            };

            await dbContext.Ads.AddAsync(ad);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ad = await dbContext.Ads
                .FindAsync(id);

            if (ad == null)
            {
                return BadRequest();
            }

            if (ad.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new AdFormViewModel
            {
                Name = ad.Name,
                Description = ad.Description,
                ImageUrl = ad.ImageUrl,
                Price = ad.Price,
                CategoryId = ad.CategoryId
            };

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdFormViewModel model)
        {
            var ad = await dbContext.Ads
                .FindAsync(id);

            if (ad == null)
            {
                return BadRequest();
            }

            if (ad.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            ad.Name = model.Name;
            ad.Description = model.Description;
            ad.ImageUrl = model.ImageUrl;
            ad.CategoryId = model.CategoryId;
            ad.Price = model.Price;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var ad = await dbContext.Ads
                .FindAsync(id);

            if (ad == null)
            {
                return BadRequest();
            }

            var adBuyer = new AdBuyer
            {
                AdId = ad.Id,
                BuyerId = GetUserId()
            };

            if (!await dbContext.AdBuyers.ContainsAsync(adBuyer))
            {
                await dbContext.AdBuyers.AddAsync(adBuyer);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Cart", "Ad");
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var ad = await dbContext.Ads
                .FindAsync(id);

            if (ad == null)
            {
                return BadRequest();
            }

            var currentUserCart = await dbContext.AdBuyers
                .Where(ab => ab.BuyerId == GetUserId())
                .ToListAsync();

            var adBuyerToRemove = await dbContext.AdBuyers
                .FirstOrDefaultAsync(ab => ab.BuyerId == GetUserId() && ab.AdId == ad.Id);

            if (adBuyerToRemove == null)
            {
                return BadRequest();
            }

            dbContext.AdBuyers.Remove(adBuyerToRemove);

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var model = await dbContext.AdBuyers
                .Where(x => x.BuyerId == GetUserId())
                .AsNoTracking()
                .Select(ab => new AdInfoViewModel
                {
                    Id = ab.AdId,
                    Name = ab.Ad.Name,
                    Description = ab.Ad.Description,
                    CreatedOn = ab.Ad.CreatedOn.ToString(DataConstants.DateTimeFormat),
                    Price = ab.Ad.Price.ToString(),
                    Owner = ab.Ad.Owner.UserName,
                    Category = ab.Ad.Category.Name,
                    ImageUrl = ab.Ad.ImageUrl
                })
                .ToListAsync();

            return View(model);
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return await dbContext.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel
                {
                    Name = c.Name,
                    Id = c.Id
                }).ToListAsync();
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
