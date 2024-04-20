using Microsoft.AspNetCore.Mvc;
using ProyectoDesarrollo.Data;
using ProyectoDesarrollo.Models;
using Microsoft.EntityFrameworkCore;


namespace ProyectoDesarrollo.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }




        public ActionResult Index(int? page)
        {

            int pageSize = 5;
            int pageNumber = page ?? 1;
            var countries = _context.countries
                .Include(o => o.Regions)
                .OrderBy(c => c.REGION_ID);

            var paginatedCustomers = countries.Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();

            int totalCustomers = _context.countries.Count();
            int totalPages = (int)Math.Ceiling((double)totalCustomers / pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(paginatedCustomers);

        }

        // GET: CustomerController/Details/5
        public ActionResult Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _context.countries
                    .Include(o => o.Regions)
                    .FirstOrDefault(c => c.COUNTRY_ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Countries countries)
        {
            if (ModelState.IsValid)
            {
                _context.countries.Add(countries);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(countries);
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(string id)
        {
            if (id.Equals(""))
            {
                return NotFound();
            }
            var cust = _context.countries.Find(id);
            return View(cust);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Countries countries)
        {
            if (!ModelState.IsValid)
            {
                _context.countries.Update(countries);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(string id)
        {
            Console.WriteLine(">>>>>>>>>>" + id);
            if (id.Equals(""))
            {
                return NotFound();
            }

            var contact = _context.countries.FirstOrDefault(c => c.COUNTRY_ID.Equals(id));
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirm(string? id)
        {
            var contact = _context.countries.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.countries.Remove(contact);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
