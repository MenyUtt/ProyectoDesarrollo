﻿using Microsoft.AspNetCore.Mvc;
using ProyectoDesarrollo.Data;
using Microsoft.EntityFrameworkCore;
using ProyectoDesarrollo.Models;
namespace ProyectoDesarrollo.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }




        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var products = _context.product_categories.OrderBy(c => c.CATEGORY_ID);

            var paginatedCustomers = products.Skip((pageNumber - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();

            int totalCustomers = _context.product_categories.Count();
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

            var product = _context.product_categories.FirstOrDefault(c => c.CATEGORY_ID == id);
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
        public ActionResult Create(Product_Categories product_Categories)
        {
            if (ModelState.IsValid)
            {
                _context.product_categories.Add(product_Categories);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product_Categories);
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var cust = _context.product_categories.Find(id);
            return View(cust);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product_Categories product_Categories)
        {
            if (ModelState.IsValid)
            {
                _context.product_categories.Update(product_Categories);
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

            var customer = _context.product_categories.FirstOrDefault(c => c.CATEGORY_ID == id);
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
            var product = _context.product_categories.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.product_categories.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
