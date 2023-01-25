using EmpPayRollMVCAjax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPayRollMVCAjax.Controllers
{
    public class EmpController : Controller
    {
        private readonly EmpDbContext _context;
        public EmpController(EmpDbContext context)
        {
            _context = context;
        }
        //Get EmployeeAjax
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmpAjax.ToListAsync());
        }

        // GET: Employee/Create
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new EmpModel());
            else
            {
                var emp = await _context.EmpAjax.FindAsync(id);
                if (emp == null)
                {
                    return NotFound();
                }
                return View(emp);
            }
        }

        // POST: Employee/AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id,[Bind("EmpId,Name,Gender,Department,Profileimage,StartDate,Salary,Notes")] EmpModel empModel)
        {
            

            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _context.Add(empModel);
                    await _context.SaveChangesAsync();
                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(empModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeModelExists(empModel.EmpId))
                        {
                            return NotFound(); 
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.EmpAjax.ToList()) });
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", empModel) });

        }


        // GET: Employee/DeleteEmployee
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var empModel = await _context.EmpAjax
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (empModel == null)
            {
                return NotFound();
            }
            return View(empModel);
        }


        // POST: Employee/DeleteEmployee
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empModel = await _context.EmpAjax.FindAsync(id);
            _context.EmpAjax.Remove(empModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.EmpAjax.ToList()) });
        }

        private bool EmployeeModelExists(int id)
        {
            return _context.EmpAjax.Any(e => e.EmpId == id);
        }
        
    }
}
