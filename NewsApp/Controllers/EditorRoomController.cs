using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Entities.Models;
using NewsApp.Entities.ViewModels;
using NewsApp.Extentions;
using NewsApp.Shared;
using NewsApp.Shared.FileSystem;

namespace NewsApp.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, 
        Roles = "Editor,ChiefEditor,Owner")]
    public class EditorRoomController : Controller
    {
        private readonly NewsAppContext _context;
        private const int pageSize = 25;

        public EditorRoomController(NewsAppContext context)
        {
            _context = context;
        }

        // GET: NewsArticles
        public async Task<IActionResult> Index(int page = 1)
        {
            var segments = HttpContext.Request.Path.Value.Split('/');

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.ArticlesCount = await _context.NewsArticle.CountAsync();

            return _context.NewsArticle != null ? 
                          View(await _context.NewsArticle
                          .OrderByDescending(x=>x.CreatedDate)
                          .Skip((page - 1) * pageSize) 
                          .Take(pageSize)
                          .ToListAsync()) :
                          Problem("Entity set 'NewsAppContext.NewsArticle'  is null.");
        }

        // GET: NewsArticles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NewsArticle == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // GET: NewsArticles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]
            NewsArticleCreateViewModel newsArticleVM)
        {
            try
            {
                IFormFile headerImage = newsArticleVM.HeaderImage;
                string saveImagePath = Path.Combine(Defaults.ArticleHeaderImagesPath, headerImage.FileName);
                await FileHelper.CopyFileAsync(newsArticleVM.HeaderImage, saveImagePath);

                NewsArticle newsArticle = new NewsArticle()
                {
                    UserId = (await _context.GetUserAsync(User.Identity)).Id,
                    Title = newsArticleVM.Title,
                    TextData = newsArticleVM.TextData,
                    HeadImagePath = saveImagePath,
                    CreatedDate = DateTimeOffset.UtcNow,
                    EditDate = DateTimeOffset.UtcNow,
                    IsDeleted = false
                };

                if (ModelState.IsValid)
                {
                    _context.Add(newsArticle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(newsArticle);
            }
            catch(Exception ex) 
            {
                return NotFound(ex.ToString());
            }
        }

        // GET: NewsArticles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.NewsArticle == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticle.FindAsync(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            var newsArticleViewModel = new NewsArticleEditViewModel()
            {
                Title = newsArticle.Title,
                TextData = newsArticle.TextData,
                ImagePath = newsArticle.HeadImagePath,
                IsDeleted = newsArticle.IsDeleted
            };
            return View(newsArticleViewModel);
        }

        // POST: NewsArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, 
            [Bind("Title, TextData, Image, IsDeleted")] NewsArticleEditViewModel newsArticleVM)
        {
            var newsArticle = await _context.NewsArticle.FindAsync(id);
            if(newsArticle == null)
            {
                return NotFound();
            }

            newsArticle.Title = newsArticleVM.Title;
            newsArticle.TextData = newsArticleVM.TextData;
            newsArticle.EditDate = DateTimeOffset.UtcNow;
            newsArticle.IsDeleted = newsArticleVM.IsDeleted;

            IFormFile? headerImage = newsArticleVM.Image;
            if(headerImage != null)
            {
                string saveImagePath = Path.Combine(Defaults.ArticleHeaderImagesPath, headerImage.FileName);
                await FileHelper.CopyFileAsync(headerImage, saveImagePath);
                newsArticle.HeadImagePath = saveImagePath;  
            }

            if (id != newsArticle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsArticleExists(newsArticle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newsArticleVM);
        }

        // GET: NewsArticles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.NewsArticle == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // POST: NewsArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.NewsArticle == null)
            {
                return Problem("Entity set 'NewsAppContext.NewsArticle'  is null.");
            }
            var newsArticle = await _context.NewsArticle.FindAsync(id);
            if (newsArticle != null)
            {
                _context.NewsArticle.Remove(newsArticle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsArticleExists(string id)
        {
          return (_context.NewsArticle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
