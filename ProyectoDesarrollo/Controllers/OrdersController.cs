using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDesarrollo.Data;
using ProyectoDesarrollo.Models;

namespace ProyectoDesarrollo.Controllers
{
    public class OrdersController : Controller
    {

        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }




        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var orders = _context.orders
                               .Include(o => o.Customer)
                               .OrderBy(c => c.ORDER_ID);

            var paginatedorders = orders.Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();

            int totalorders = _context.orders.Count();
            int totalPages = (int)Math.Ceiling((double)totalorders / pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(paginatedorders);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _context.orders.FirstOrDefault(c => c.ORDER_ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            ViewBag.Customers = _context.customers
                .Select(c => new SelectListItem
                {
                    Value = c.CUSTOMER_ID.ToString(),
                    Text = c.NAME
                })
                .ToList();

            return View(new Orders());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Orders orders)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    _context.orders.Add(orders);
                    _context.SaveChanges();
                    Console.WriteLine("MODEL VALID ENABLED");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            ViewBag.Customers = _context.customers
                .Select(c => new SelectListItem
                {
                    Value = c.CUSTOMER_ID.ToString(),
                    Text = c.NAME
                })
                .ToList();
            return View(orders);
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            ViewBag.Customers = _context.customers
                .Select(c => new SelectListItem
                {
                    Value = c.CUSTOMER_ID.ToString(),
                    Text = c.NAME
                })
                .ToList();
            var cust = _context.orders.Find(id);
            return View(cust);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Orders order)
        {
            if (!ModelState.IsValid)
            {
                // Recargar la lista de clientes
                ViewBag.Customers = _context.customers
                    .Select(c => new SelectListItem
                    {
                        Value = c.CUSTOMER_ID.ToString(),
                        Text = c.NAME
                    })
                    .ToList();
                _context.orders.Update(order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _context.orders.FirstOrDefault(c => c.ORDER_ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var contact = _context.orders.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.orders.Remove(contact);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
