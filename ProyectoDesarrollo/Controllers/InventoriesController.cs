using Microsoft.AspNetCore.Mvc;
using ProyectoDesarrollo.Data;
using ProyectoDesarrollo.Models;

namespace ProyectoDesarrollo.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoriesController(ApplicationDbContext context)
        {
            _context = context;
        }




        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var query = from i in _context.inventories
                        join p in _context.products on i.PRODUCT_ID equals p.PRODUCT_ID
                        join w in _context.warehouses on i.WAREHOUSE_ID equals w.WAREHOUSE_ID
                        orderby i.PRODUCT_ID
                        select new Inventories // Cambia aquí
                        {
                            ProductName = p.PRODUCT_NAME,
                            WarehouseName = w.WAREHOUSE_NAME,
                            QUANTITY = i.QUANTITY
                        };

            var paginatedInventories = query.Skip((pageNumber - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            int totalInventories = _context.inventories.Count();
            int totalPages = (int)Math.Ceiling((double)totalInventories / pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(paginatedInventories);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.inventories.FirstOrDefault(c => c.PRODUCT_ID == id);
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
        public ActionResult Create(Inventories inventories)
        {
            if (ModelState.IsValid)
            {
                _context.inventories.Add(inventories);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(inventories);
        }
    }
}
