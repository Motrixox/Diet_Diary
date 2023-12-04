using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml.Linq;
using Diet_Diary.Models;
using Diet_Diary.Services;

namespace Diet_Diary.Controllers
{
    [Authorize]
    public class ServedMealController : Controller
    {
        private readonly MealsService _mealsService;
        private readonly ServedMealsService _servedMealsService;

        public ServedMealController(MealsService mealsService, ServedMealsService service)
        {
            _mealsService = mealsService;
            _servedMealsService = service;
        }

        // GET: ServedMealsController
        public ActionResult Index()
        {
            string? currentUser = User.Identity?.Name;

            List<Meal> mealListOfUser = new List<Meal>();

            if (currentUser != null)
                mealListOfUser = _mealsService.GetByUsername(currentUser);

            return View(mealListOfUser);
        }

        // GET: ServedMealsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServedMealsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string id, IFormCollection collection)
        {
            var meal = new ServedMeal
            {
                MealId = id,
                Coeff = double.Parse(collection["coeff"], CultureInfo.InvariantCulture),
                Date = DateTime.Parse(collection["Date"]) 
            };
            _servedMealsService.Create(meal);


            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
