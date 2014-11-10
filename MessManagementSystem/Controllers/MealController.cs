using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MessManagementSystem.Models;

namespace MessManagementSystem.Controllers
{
    public class MealController : Controller
    {
        private MessDbContext db = new MessDbContext();

        // GET: /Meal/
        public ActionResult Index()
        {
            var members = db.Members;
            var meals = db.Meals.Include(m => m.Member);
            ViewBag.TotalMeal = TotalMeal();
            ViewBag.Members = members.ToList();
            return View(meals.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int memberId)
        {
            var members = db.Members;
            double? totalMeal;
            var memberName = db.Members.Where(m => m.MemberId == memberId).Select(m => m.Name);
            var meals = db.Meals.Where(m=>m.MemberId==memberId);
            totalMeal = 0;
            if (meals.Any())
            {
                totalMeal = db.Meals.Where(m => m.MemberId == memberId).Sum(a => a.NoOfMeal);
            }
            
           
            ViewBag.Members = members.ToList();
            ViewBag.MemberName = memberName;
            ViewBag.TotalMeal = totalMeal;
           
            return View(meals.ToList());
        }

        // GET: /Meal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // GET: /Meal/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name");
            return View();
        }

        // POST: /Meal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MealId,MealDate,MemberId,NoOfMeal")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                db.Meals.Add(meal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", meal.MemberId);
            return View(meal);
        }

        // GET: /Meal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", meal.MemberId);
            return View(meal);
        }

        // POST: /Meal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MealId,MealDate,MemberId,NoOfMeal")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", meal.MemberId);
            return View(meal);
        }

        // GET: /Meal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: /Meal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meal meal = db.Meals.Find(id);
            db.Meals.Remove(meal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult MealRate()
        {
            var totalMeal = db.Meals.Sum(m => m.NoOfMeal);
            var totalBazar = db.Bazars.Sum(b => b.BazarAmount);
            double mealRate = totalBazar / totalMeal;
            ViewBag.MealRate = mealRate;
            return View();
        }

        public double MealRates()
        {
            var totalMeal = db.Meals.Sum(m => m.NoOfMeal);
            var totalBazar = db.Bazars.Sum(b => b.BazarAmount);
            double mealRate = totalBazar / totalMeal;
            return mealRate;
        }

        public int TotalMeal()
        {
            int totalMeal = Convert.ToInt32(db.Meals.Sum(m => m.NoOfMeal));
            return totalMeal;
        }
    }
}
