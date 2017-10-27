using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        IBookRepository repository;
        public NavController(IBookRepository repo)
        {
            this.repository = repo;
        }
        public PartialViewResult Menu(string genre, bool horizontalNav=false)
        {
            ViewBag.SelectedGenre = genre;
            IEnumerable<string> genres = repository.Books
                .Select(book => book.Genre)
                .Distinct()
                .OrderBy(x => x);
            string viewName = horizontalNav == true ? "MenuHorizontal" : "Menu";
            return PartialView(viewName,genres);
        }

    }
}