using Diet_Diary.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Diet_Diary.Services;
using System.Globalization;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Diet_Diary.Controllers
{
	[Authorize]
	public class MealChartController : Controller
	{
		private readonly MealsService _mealsService;
		private readonly ServedMealsService _servedMealsService;
		public MealChartController(MealsService mealsService, ServedMealsService servedMealsService)
		{
			_mealsService = mealsService;
			_servedMealsService = servedMealsService;
		}

		public ActionResult Index()
		{
			var userMeals = _mealsService.GetByUsername(User.Identity.Name);

			List<ServedMeal> servedMeals = new List<ServedMeal>();
			List<double> calories = new List<double>();
            List<DataPoint> dataPoints = new List<DataPoint>();

			DateTime date = DateTime.Now.Date.AddDays(-6);

            double[] caloriesPerDays = new double[7];

            foreach (var document in userMeals )
			{
				var b = _servedMealsService.GetByMealID(document.Id);

				foreach (var item in b)
				{
					if(item.Date.Date >= DateTime.Now.Date.AddDays(-6) && item.Date.Date <= DateTime.Now.Date) // past week
					{
                        servedMeals.Add(item);
                        calories.Add(document.Calories);
                    }
				}
			}

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < servedMeals.Count; j++)
				{
					if (servedMeals[j].Date.Date == date)
						caloriesPerDays[i] += servedMeals[j].Coeff * calories[j];
				}

                date = date.AddDays(1);
            }

            date = DateTime.Now.Date.AddDays(-6);

            for (int i = 0; i < caloriesPerDays.Length; i++)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(date.Day);
				sb.Append(".");
				sb.Append(date.Month);
				sb.Append(".");
				sb.Append(date.Year);
                var Date = sb.ToString();
                var Value = caloriesPerDays[i];

                dataPoints.Add(new DataPoint(Date, Value));
                date = date.AddDays(1);
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
		}
	}
}