using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MessManagementSystem.Models;
using MessManagementSystem.ViewModel;

namespace MessManagementSystem.Controllers
{
    public class BazarController : Controller
    {
        private MessDbContext db = new MessDbContext();

        // GET: /Bazar/
        public ActionResult Index()
        {
            var members = db.Members;
            var bazars = db.Bazars.Include(b => b.Member);
            ViewBag.Members = members.ToList();
            ViewBag.TotalBazar = TotalBazar();
            return View(bazars.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? memberId)
        {
            if (memberId != null)
            {
                var members = db.Members;
                var memberName = db.Members.Where(m => m.MemberId == memberId).Select(m => m.Name);
                var bazars = db.Bazars.Where(m => m.MemberId == memberId);
                ViewBag.TotalBazar = 0;
                if (bazars.Any())
                {
                    ViewBag.TotalBazar = db.Bazars.Where(m => m.MemberId == memberId).Sum(m => m.BazarAmount);
                }


                ViewBag.Members = members.ToList();
                ViewBag.MemberName = memberName;
                return View(bazars.ToList());
            }
            ViewBag.Members = db.Members.ToList();
            ViewBag.TotalBazar = TotalBazar();
            return View(db.Bazars.ToList());
        }

        // GET: /Bazar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bazar bazar = db.Bazars.Find(id);
            if (bazar == null)
            {
                return HttpNotFound();
            }
            return View(bazar);
        }

        // GET: /Bazar/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name");
            return View();
        }

        // POST: /Bazar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BazarId,BazarDate,MemberId,BazarAmount")] Bazar bazar)
        {
            if (ModelState.IsValid)
            {
                db.Bazars.Add(bazar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", bazar.MemberId);
            return View(bazar);
        }

        // GET: /Bazar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bazar bazar = db.Bazars.Find(id);
            if (bazar == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", bazar.MemberId);
            return View(bazar);
        }

        // POST: /Bazar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BazarId,BazarDate,MemberId,BazarAmount")] Bazar bazar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bazar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", bazar.MemberId);
            return View(bazar);
        }

        // GET: /Bazar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bazar bazar = db.Bazars.Find(id);
            if (bazar == null)
            {
                return HttpNotFound();
            }
            return View(bazar);
        }

        // POST: /Bazar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bazar bazar = db.Bazars.Find(id);
            db.Bazars.Remove(bazar);
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

        public int TotalBazar()
        {
            int totalBazar = Convert.ToInt32(db.Bazars.Sum(b => b.BazarAmount));
            return totalBazar;
        }

        public ActionResult ShowBill()
        {
            MealController meal = new MealController();
            double mealNo;
            double totalBazar;
            double bill;
            string status;
           
            var membersBill = new List<ShowBillViewModel>();
            foreach (var member in db.Members)
            {
                mealNo = member.Meals.Where(a => a.MemberId == member.MemberId).Sum(a => a.NoOfMeal);
                totalBazar = member.Bazars.Where(n => n.MemberId == member.MemberId).Sum(n => n.BazarAmount);
                bill = Math.Round(meal.MealRates()*mealNo);
                status = string.Empty;
                if (totalBazar > bill)
                {
                    status =(totalBazar-bill)+"(Receive)";
                }
                else
                {
                    status = (bill-totalBazar)+"(Due)";
                }
                membersBill.Add(new ShowBillViewModel()
                {
                    MemberName = member.Name,
                    TotalBazar = totalBazar,
                    TotalMeal = mealNo,
                    Bill = bill,
                    Status = status

                });
            }
   
            ViewBag.Members = db.Members.ToList();
            ViewBag.MembersBill = membersBill.ToList();
            return View();
        }

       
    }
}
