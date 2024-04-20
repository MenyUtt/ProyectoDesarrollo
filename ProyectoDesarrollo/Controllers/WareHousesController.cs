using Microsoft.AspNetCore.Mvc;
using ProyectoDesarrollo.Data;
using ProyectoDesarrollo.Models;

namespace ProyectoDesarrollo.Controllers
{
    public class WareHousesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WareHousesController(ApplicationDbContext context)
        {
            _context = context;
        }




        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var warehouses = _context.warehouses.OrderBy(c => c.WAREHOUSE_ID);

            var paginatedCustomers = warehouses.Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();

            int totalCustomers = _context.warehouses.Count();
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

            var product = _context.warehouses.FirstOrDefault(c => c.WAREHOUSE_ID == id);
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
        public ActionResult Create(Warehouses warehouses)
        {
            if (ModelState.IsValid)
            {
                _context.warehouses.Add(warehouses);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouses);
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var cust = _context.warehouses.Find(id);
            return View(cust);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Warehouses warehouses)
        {
            if (ModelState.IsValid)
            {
                _context.warehouses.Update(warehouses);
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

            var customer = _context.warehouses.FirstOrDefault(c => c.WAREHOUSE_ID == id);
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
            var product = _context.warehouses.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.warehouses.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
