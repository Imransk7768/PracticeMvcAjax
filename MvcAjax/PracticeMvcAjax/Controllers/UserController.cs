using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeMvcAjax.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeMvcAjax.Controllers
{
    public class UserController : Controller
    {
        public readonly UserContext _context;
        public UserController(UserContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserTbl.ToListAsync());
        }
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new UserModel());
            else
            {
                var user = await _context.UserTbl.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,WebSite,UserId,Email,Password,Mobile,AltEmail")] UserModel userModel)
        {


            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _context.Add(userModel);
                    await _context.SaveChangesAsync();
                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(userModel);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserModelExists(userModel.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.UserTbl.ToList()) });
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", userModel) });

        }
        private bool UserModelExists(int id)
        {
            return _context.UserTbl.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userModel = await _context.UserTbl
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userModel = await _context.UserTbl.FindAsync(id);
            _context.UserTbl.Remove(userModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.UserTbl.ToList()) });
        }

        private bool EmployeeModelExists(int id)
        {
            return _context.UserTbl.Any(e => e.Id == id);
        }

    }
}

