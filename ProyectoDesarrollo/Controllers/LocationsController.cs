using Microsoft.AspNetCore.Mvc;
using ProyectoDesarrollo.Data;
using ProyectoDesarrollo.Models;

namespace ProyectoDesarrollo.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }




        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var locations = _context.locations.OrderBy(c => c.LOCATION_ID);

            var paginatedCustomers = locations.Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();

            int totalCustomers = _context.locations.Count();
            int totalPages = (int)Math.Ceiling((double)totalCustomers / pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(paginatedCustomers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.locations.FirstOrDefault(c => c.LOCATION_ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Locations locations)
        {
            if (ModelState.IsValid)
            {
                _context.locations.Add(locations);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(locations);
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var cust = _context.locations.Find(id);
            return View(cust);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Locations locations)
        {
            if (ModelState.IsValid)
            {
                _context.locations.Update(locations);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.locations.FirstOrDefault(c => c.LOCATION_ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var location = _context.locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.locations.Remove(location);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
