using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Globalization;
using Diet_Diary.Models;
using Diet_Diary.Services;

namespace zpDiet_Diaryp2.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        private readonly ProductsService _productsService;
        private readonly MealsService _mealsService;

        public MealController(ProductsService productsService, MealsService mealsService)
        {
            _productsService = productsService;
            _mealsService = mealsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSuggestions(string searchText)
        {
            var suggestions = _productsService.GetSuggestionsByMetaScore(searchText);

            return PartialView("_GetSuggestions", suggestions);
        }

        [HttpPost]
        public ActionResult Create(string name, Dictionary<string, int> productQuantities, string calc)
        {
            var meal = new Meal
            {
                Name = name,
                Username = User.Identity.Name,
                Products = productQuantities,
                Date = DateTime.Now,
                Calories = double.Parse(calc, CultureInfo.InvariantCulture)
            };

            _mealsService.Create(meal);

            return RedirectToAction("Index", "Home");
        }


        public ActionResult SavedMeals() 
        {
            var currentUser = User.Identity.Name;
            var showmeal = _mealsService.GetByUsername(currentUser);
            return View(showmeal); 
        }

    }
}
