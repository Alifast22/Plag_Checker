using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Logging;
using System.Web.Mvc;
using PlagChecker.Data;
using PlagChecker.Models;
using PlagChecker;


namespace PlagChecker.Controllers
{
    public class StudentController : Controller
    {
        YourDbContext listofcodes = new YourDbContext();
        private static List<Student> TempCodeSubmissions = new List<Student>();

        List<Result> results = new List<Result>();
        
         private readonly ILogger<StudentController> _logger;
        // GET: Student
        public StudentController()
        {

        }
        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }
        public ActionResult Index()
        {
            var ListofStudent = listofcodes.CodeSubmissions.ToList();
            return View(ListofStudent);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student model)
        {
            try
            {
                TempCodeSubmissions.Add(model);
                //var allCodes =listofcodes.CodeSubmissions.ToList();
               // var comparisonResults = new List<object>();

               // foreach (var obj in allCodes)
               // {
               //     Console.WriteLine(obj.Code);
               //     var similarityPercentage=CalculateLevenshteinDistance(obj.Code, model.Code);
                    //comparisonResults.Add(new { obj.Code, similarityPercentage });
               //     results.Add(new Result { Studen_RollNo = obj.Roll_Number, Plag_RollNo = model.Roll_Number, PlagPercentage = similarityPercentage });
               // }
              //  _logger.LogInformation(string.Join(", ", allCodes));
                //listofcodes.CodeSubmissions.Add(model);
                //listofcodes.SaveChanges();
              //  Console.WriteLine(allCodes);
              //  ViewBag.ComparisonResults = results;
                ViewBag.Message = "Data inserted Successfully";
                //return Json(allCodes);
                //return RedirectToAction("Index");
                return View();
            }
            catch
            {
                return View();
            }
        }
        static int CalculateLevenshteinDistance(string a, string b)
        {
            int[,] distance = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++)
                distance[i, 0] = i;

            for (int j = 0; j <= b.Length; j++)
                distance[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = (b[j - 1] == a[i - 1]) ? 0 : 1;
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            int maxLength = Math.Max(a.Length, b.Length);
            int levenshteinDistance = distance[a.Length, b.Length];

            int similarity = (int)(((double)(maxLength - levenshteinDistance) / maxLength) * 100);

            return similarity;
        }
        public ActionResult Process()
        {
            var results = new List<Result>();
            for (int i = 0; i < TempCodeSubmissions.Count; i++)
            {
                for (int j = i + 1; j < TempCodeSubmissions.Count; j++)
                {
                    int similarityPercentage = CalculateLevenshteinDistance(TempCodeSubmissions[i].Code, TempCodeSubmissions[j].Code);
                    if (similarityPercentage >= 80)
                    {
                        results.Add(new Result { Studen_RollNo = TempCodeSubmissions[i].Roll_Number, Plag_RollNo = TempCodeSubmissions[j].Roll_Number, PlagPercentage = similarityPercentage });
                    }
                }
            }

            ViewBag.ComparisonResults = results;
            TempCodeSubmissions.Clear(); // Clearing the temporary storage after processing
            return View("Process", results);
        }
        /**
        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }**/
    }
}
