using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Entities.Models;

namespace NewsApp.Controllers
{
    public class NewsArticlesController : Controller
    {
        private readonly NewsAppContext _context;
        private const int pageSize = 25;

        public NewsArticlesController(NewsAppContext context)
        {
            _context = context;
        }

        // GET: NewsArticles
        public async Task<IActionResult> Index(int page = 1)
        {
            var newsAppContext = _context.NewsArticle.Include(n => n.User);

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.ArticlesCount = await _context.NewsArticle.CountAsync();

            return View(await _context.NewsArticle.
                           Where(x => x.IsDeleted == false)
                          .OrderByDescending(x => x.CreatedDate)
                          .Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .ToListAsync());
        }

        // GET: NewsArticles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NewsArticle == null)
            {
                return NotFound();
            }

            var newsArticle = await _context.NewsArticle
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }
    }
}
