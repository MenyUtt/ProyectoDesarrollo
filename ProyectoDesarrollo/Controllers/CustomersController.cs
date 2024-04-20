using Microsoft.AspNetCore.Mvc;
using ProyectoDesarrollo.Data;
using ProyectoDesarrollo.Models;

namespace ProyectoDesarrollo.Controllers
{
    public class CustomersController : Controller  
    {

        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }




        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var customers = _context.customers.OrderBy(c => c.CUSTOMER_ID);

            var paginatedCustomers = customers.Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();

            int totalCustomers = _context.customers.Count();
            int totalPages = (int)Math.Ceiling((double)totalCustomers / pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.Customers = _context.customers.ToList();

            return View(paginatedCustomers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.customers.FirstOrDefault(c => c.CUSTOMER_ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customers customers)
        {
            if (ModelState.IsValid)
            {
                _context.customers.Add(customers);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(customers);
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var cust = _context.customers.Find(id);
            return View(cust);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customers customer)
        {
            if (ModelState.IsValid)
            {
                _context.customers.Update(customer);
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

            var customer = _context.customers.FirstOrDefault(c => c.CUSTOMER_ID == id);
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
            var customer = _context.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
