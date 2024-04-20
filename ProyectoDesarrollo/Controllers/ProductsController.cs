using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using ProyectoDesarrollo.Data;
using Microsoft.EntityFrameworkCore;
using ProyectoDesarrollo.Models;
namespace ProyectoDesarrollo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }




        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var products = _context.products
                            .Include(o => o.Categories)
                            .OrderBy(c => c.PRODUCT_ID);

            var paginatedProducts = products.Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();

            int totalProducts = _context.products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(paginatedProducts);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.products.FirstOrDefault(c => c.PRODUCT_ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            ViewBag.Categories = _context.product_categories
                .Select(c => new SelectListItem
                {
                    Value = c.CATEGORY_ID.ToString(),
                    Text = c.CATEGORY_NAME
                })
                .ToList();
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products)
        {
            if (!ModelState.IsValid)
            {
                _context.products.Add(products);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.product_categories
                .Select(c => new SelectListItem
                {
                    Value = c.CATEGORY_ID.ToString(),
                    Text = c.CATEGORY_NAME
                })
                .ToList();
            return View(products);
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            ViewBag.Categories = _context.product_categories
                .Select(c => new SelectListItem
                {
                    Value = c.CATEGORY_ID.ToString(),
                    Text = c.CATEGORY_NAME
                })
                .ToList();
            var cust = _context.products.Find(id);
            return View(cust);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products product)
        {
            if (!ModelState.IsValid)
            {
                // Recargar la lista de clientes
                ViewBag.Categories = _context.product_categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CATEGORY_ID.ToString(),
                        Text = c.CATEGORY_NAME
                    })
                    .ToList();
                _context.products.Update(product);
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

            var customer = _context.products.FirstOrDefault(c => c.PRODUCT_ID == id);
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
            var product = _context.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
