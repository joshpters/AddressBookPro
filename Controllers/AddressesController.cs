using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AddressBookPro.Data;
using AddressBookPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace AddressBookPro.Controllers
{
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ABPUser> _userManager;
        public AddressesController(ApplicationDbContext context, UserManager<ABPUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            //get all addresses in DB for current user
            var userId = _userManager.GetUserId(User);
            var currentUserAddresses = _context.Addresses.Where(a => a.UserId == userId).Include(a => a.Label);

            ViewData["LabelId"] = new SelectList(_context.Labels, "Id", "Name");
            return View(await currentUserAddresses.ToListAsync());
        }


        // GET: Addresses/Create
        public IActionResult Create()
        {
            ViewData["LabelId"] = new SelectList(_context.Labels, "Id", "Id");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LabelId,FirstName,LastName,Email,Address1,Address2,City,State,ZipCode,PhoneNumber,Notes")] Address address, IFormFile avatar)
        {
            
            if (ModelState.IsValid) {
            
                if (avatar != null)
                {
                    address.SetAvatar(avatar);
                }
                string userId = _userManager.GetUserId(User).ToString();
                address.DateAdded = DateTime.Now;
                address.UserId = userId;
                _context.Add(address);
                await _context.SaveChangesAsync();
                TempData["success"] = "Contact created!";
                return RedirectToAction(nameof(Index));
            }
            if (address.FirstName == null || address.LastName == null)
            {
                TempData["error"] = "An error occurred creating the new contact. Please include a first and last name.";
            }
            else
            {
                TempData["error"] = "An error occurred creating the new contact.";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,LabelId,FirstName,LastName,Email,Avatar,Address1,Address2,City,State,ZipCode,PhoneNumber,Notes,DateAdded")] Address address, IFormFile Avatar)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Avatar != null)
                    {
                        address.SetAvatar(Avatar);
                    }
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Changes saved!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
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

            return RedirectToAction(nameof(Index));
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            //TempData["success"] = "Changes saved!";
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
