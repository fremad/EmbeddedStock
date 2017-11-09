using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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

            //todo rename kjal
            var kjal = new ESImage();

            if (componentType.Image.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(componentType.Image.ContentDisposition).FileName.Trim('"');

                using (var reader = new StreamReader(componentType.Image.OpenReadStream()))
                {
                    string contentAsString = reader.ReadToEnd();
                    byte[] bytes = new byte[contentAsString.Length * sizeof(char)];
                    System.Buffer.BlockCopy(contentAsString.ToCharArray(), 0, bytes, 0, bytes.Length);

                    kjal.ImageData = bytes;
                }

            }
            
            kjal.ImageMimeType = "image/png";



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
                tmp.Image = kjal;

            _context.Add(tmp);
            await _context.SaveChangesAsync();

            //long Id = tmp.ComponentTypeId;

            ICollection<Category> categories = new List<Category>();

            List<Category> cool = new List<Category>();

            foreach (var item in catlist)
            {
                cool = _context.Categories.Select(a => a).Where(a => a.Name == item).ToList();

                if (!cool.Any())
                {
                    var aCat = new Category
                    {
                        Name = item,
                    };

                    categories.Add(aCat);
                    _context.Add(aCat);
                }
                else
                {
                    //TODO lol :) hacks
                    categories.Add(cool.First());
                }

            }
            await _context.SaveChangesAsync();

            foreach (var item in categories)
            {
                var free = new CategoryComponenttypebinding()
                {
                    CategoryId = item.CategoryId,
                    ComponentTypeId = tmp.ComponentTypeId,
                    Category = item,
                    ComponentType = tmp
                };

                _context.Add(free);

                tmp.CategoryComponenttypebindings.Add(free);
                item.CategoryComponenttypebindings.Add(free);

                //TODO check if it works without
                _context.Entry(item).State = EntityState.Modified;
                _context.Update(item);
            }

            _context.Entry(tmp).Collection("CategoryComponenttypebindings").IsModified = true;
     

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            
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

        
        public async Task<IActionResult> listComponentTypeFromCategory(string name)
        {
            var nameList = _context.Categories.Select(data => data.Name).ToList();
            List<ComponentType> returnList = new List<ComponentType>();
           
            foreach (var dbname in nameList)
            {
                if (dbname == name)
                {
                    returnList = _context.CategoryComponenttypebindings.Select(data => data.ComponentType).ToList();
                    break;
                }
            }
            if (returnList.Count != 0) 
            {
                return View(returnList);
            }

            return View(returnList);



            //ViewData["categoryName"] = String.IsNullOrEmpty(name) ? "Arduino" : "";
            //var items = from Category in _context.Categories select name;

            //var viewlist = new List<ComponentType>();

            //    var hewoui = _context.CategoryComponenttypebindings.ToList();

            //    var tmp = _context.ComponentTypes
            //        .Include(data => data.CategoryComponenttypebindings)
            //        .ThenInclude(data => data.Category)
            //        .ToList();

            //    foreach (var item in tmp)
            //    {
            //        foreach (var item2 in item.CategoryComponenttypebindings)
            //        {
            //            if (item2.Category.Name == name)
            //            {
            //                viewlist.Add(item);
            //            }
            //        }
            //    }

            //return View(viewlist);

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
