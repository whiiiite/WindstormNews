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
using NewsApp.Repositories.NewsArticles;
using NewsApp.Services.NewsArticles;

namespace NewsApp.Controllers
{
    public class NewsArticlesController : Controller
    {
        private readonly INewsArticlesService newsArticlesService;
        private readonly NewsAppContext _context;
        private const int pageSize = 25;

        public NewsArticlesController(NewsAppContext context, INewsArticlesService newsArticlesService)
        { 
            _context = context;
            this.newsArticlesService = newsArticlesService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.ArticlesCount = await newsArticlesService.CountArticlesAsync(predicate: null);

            return View(await newsArticlesService.GetArticlesAsync(pageSize, page));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NewsArticle == null)
            {
                return NotFound();
            }

            var newsArticle = await newsArticlesService.GetArticleByIdAsync(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }
    }
}
