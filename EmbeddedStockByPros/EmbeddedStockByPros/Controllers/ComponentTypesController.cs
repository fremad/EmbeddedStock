﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmbeddedStockByPros.Models;
using EmbeddedStockByPros.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace EmbeddedStockByPros.Controllers
{
    public class ComponentTypesController : Controller
    {
        private readonly DatabaseContext _context;

        public ComponentTypesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ComponentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComponentTypes.ToListAsync());
        }

        // GET: ComponentTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // GET: ComponentTypes/Create
        public IActionResult Create()
        {

            //TODO make this god forsaken code into a viewmodel
            ViewBag.categories = _context.Categories.Select(data => data.Name).ToList();

            return View();
        }

        // POST: ComponentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComponenttypeVM componentType)
        {


            var catlist = componentType.Categories.Split(",");

                var tmp = new ComponentType();

                tmp.ComponentName = componentType.ComponentName;
                tmp.AdminComment = componentType.AdminComment;
                tmp.Datasheet = componentType.Datasheet;
                tmp.ComponentInfo = componentType.ComponentInfo;
                tmp.ImageUrl = componentType.ImageUrl;
                tmp.Location = componentType.Location;
                tmp.Status = componentType.Status;
                tmp.Manufacturer = componentType.Manufacturer;
                tmp.WikiLink = componentType.WikiLink;

            _context.Add(tmp);
            await _context.SaveChangesAsync();

            //long Id = tmp.ComponentTypeId;

            ICollection<Category> categories = new List<Category>();

            foreach (var item in catlist)
            {
                var cool = _context.Categories.Select(a => a).Where(a => a.Name == item).ToList();

                if (!cool.Any())
                {
                    var aCat = new Category
                    {
                        Name = item,
                    };

                    categories.Add(aCat);
                    _context.Add(aCat);
                }
            }
            await _context.SaveChangesAsync();


            var free = new CategoryComponenttypebinding()
            {
                CategoryId = categories.First().CategoryId,
                ComponentTypeId = tmp.ComponentTypeId
            };

            tmp.CategoryComponenttypebindings.Add(free);


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            
            //return View(componentType);
        }

        // GET: ComponentTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes.SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            if (componentType == null)
            {
                return NotFound();
            }
            return View(componentType);
        }

        // POST: ComponentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ComponentTypeId,ComponentName,ComponentInfo,Location,Status,Datasheet,ImageUrl,Manufacturer,WikiLink,AdminComment")] ComponentType componentType)
        {
            if (id != componentType.ComponentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(componentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentTypeExists(componentType.ComponentTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(componentType);
        }

        // GET: ComponentTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // POST: ComponentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var componentType = await _context.ComponentTypes.SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            _context.ComponentTypes.Remove(componentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentTypeExists(long id)
        {
            return _context.ComponentTypes.Any(e => e.ComponentTypeId == id);
        }
    }
}
