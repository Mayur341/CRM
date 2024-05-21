using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM.Models;

namespace CRM.Controllers
{
    public class AddressesController : Controller
    {
        private readonly CRMContext _context;

        public AddressesController(CRMContext context)
        {
            _context = context;
        }

        //GET ADDRESS BY CLIENT ID
        // GET: Addresses
        public async Task<IActionResult> GetAddressesByClientId(int? clientId)
        {
            if (clientId == null)
            {
                return NotFound();
            }

            var addresses = await _context.Addresses
                .Where(a => a.Client.ClientID == clientId)
                .ToListAsync();

            return View(addresses);
        }




        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Addresses.Include(a => a.Client);
            return View(await cRMContext.ToListAsync());
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.Client)
                .FirstOrDefaultAsync(m => m.AddressID == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Addresses/Create/id
        public async Task<IActionResult> CreateAsync(int? id)
        {

            var client = await _context.Clients.FindAsync(id);

            // If the client is found, pass its ID to the view
            if (client != null)
            // Set the ID in the ViewBag
            {
                ViewBag.ClientID = id;
            }

            return View();

        }


        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressID,ClientID,AddressLine1,AddressLine2,City,State,Country,Pincode")] Address address)
        {
            
            
                _context.Add(address);
                await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Client");

            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", address.ClientID);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            //
            var Cid = address.ClientID;


            var client = await _context.Clients.FindAsync(Cid);

          
            if (client != null)
            
            {
                ViewBag.ClientID = Cid;
            }
           
       
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressID,ClientID,AddressLine1,AddressLine2,City,State,Country,Pincode")] Address address)
        {
            if (id != address.AddressID)
            {
                Console.WriteLine("bhenchod");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressID))
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
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", address.ClientID);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.Client)
                .FirstOrDefaultAsync(m => m.AddressID == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address != null)
            {
                _context.Addresses.Remove(address);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.AddressID == id);
        }
    }
}
