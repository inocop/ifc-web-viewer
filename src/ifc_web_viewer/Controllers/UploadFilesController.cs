using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ifc_web_viewer.Data;
using ifc_web_viewer.Models;
using Microsoft.AspNetCore.Authorization;
using ifc_web_viewer.Models.UploadFilesViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace ifc_web_viewer.Controllers
{
    [Authorize]
    public class UploadFilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public UploadFilesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UploadFiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UploadFiles.Include(u => u.ApplicationUser);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UploadFiles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadFile = await _context.UploadFiles
                .Include(u => u.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (uploadFile == null)
            {
                return NotFound();
            }

            return View(uploadFile);
        }

        // GET: UploadFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UploadFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FilePath,FileName,FileSize,UploadDate,DeleteFlg")] UploadFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uploadFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uploadFile);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(UploadFilesViewModel files)
        {
            //long size = files.Sum(f => f.UploadFile.Length);

            // full path to file in temp location
            //var filePath = Path.GetTempFileName();


            var applicationUser = await _userManager.GetUserAsync(User);

            var ifcConvert = new IFCConvertModel();

            string save_directory = "/opt/dotnet/src/ifc_web_viewer/wwwroot/sample/";
            string file_name = DateTime.Now.Ticks.ToString() + ".ifc";
            string file_path = save_directory + file_name;


            foreach (var formFile in files.UploadFiles)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(file_path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        await ifcConvert.ConvertIfcToObjAsync(file_path);

                        var uploadFile = new UploadFile
                        {
                            FileName = file_name,
                            FilePath = save_directory,
                            FileSize = formFile.Length,
                            UploadDate = DateTime.Now,
                            ApplicationUser = applicationUser
                        };
                        _context.UploadFiles.Add(uploadFile);
                        _context.SaveChanges();
                    }
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return RedirectToAction("Index", "UploadFiles");
        }


        // GET: UploadFiles/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadFile = await _context.UploadFiles.SingleOrDefaultAsync(m => m.Id == id);
            if (uploadFile == null)
            {
                return NotFound();
            }
            return View(uploadFile);
        }

        // POST: UploadFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FilePath,FileName,FileSize,UploadDate,DeleteFlg")] UploadFile uploadFile)
        {
            if (id != uploadFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uploadFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UploadFileExists(uploadFile.Id))
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
            return View(uploadFile);
        }

        // GET: UploadFiles/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uploadFile = await _context.UploadFiles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (uploadFile == null)
            {
                return NotFound();
            }

            return View(uploadFile);
        }

        // POST: UploadFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var uploadFile = await _context.UploadFiles.SingleOrDefaultAsync(m => m.Id == id);
            _context.UploadFiles.Remove(uploadFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: UploadFiles/Preview/5
        public async Task<ActionResult> Preview(long? id)
        {
            var uploadFile = await _context.UploadFiles.SingleOrDefaultAsync(m => m.Id == id);

            return View(uploadFile);
        }


        private bool UploadFileExists(long id)
        {
            return _context.UploadFiles.Any(e => e.Id == id);
        }
    }
}
