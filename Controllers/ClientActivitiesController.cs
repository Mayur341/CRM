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
    public class ClientActivitiesController : Controller
    {
        private readonly CRMContext _context;

        public ClientActivitiesController(CRMContext context)
        {
            _context = context;
        }

        ///method for getting activity based on activity id


       













        public async Task<IActionResult> ActivityByClientId(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Console.WriteLine("call accepting by client");

            var ClientActivity = await _context.ClientActivities
                .Include(c => c.Client)
                .Where(c => c.Client.ClientID == id)
                .ToListAsync();

            ViewData["id"] = id;

            return View(ClientActivity);
        }

        /// customized method for getting client activities  by clientId














        // GET: ClientActivities
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.ClientActivities.Include(c => c.Client);
            return View(await cRMContext.ToListAsync());
        }

        // GET: ClientActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientActivity = await _context.ClientActivities
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.clientActivityID == id);
            if (clientActivity == null)
            {
                return NotFound();
            }

            return View(clientActivity);
        }

        // GET: ClientActivities/Create
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
        // POST: ClientActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("clientActivityID,ClientID,Activity_Type,Activity_Description,Activity_Date")] ClientActivity clientActivity)
        {
            
                _context.Add(clientActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", clientActivity.ClientID);
            return View(clientActivity);
        }

        // GET: ClientActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientActivity = await _context.ClientActivities.FindAsync(id);
            if (clientActivity == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", clientActivity.ClientID);
            return View(clientActivity);
        }

        // POST: ClientActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("clientActivityID,ClientID,Activity_Type,Activity_Description,Activity_Date")] ClientActivity clientActivity)
        {
            if (id != clientActivity.clientActivityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientActivityExists(clientActivity.clientActivityID))
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
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", clientActivity.ClientID);
            return View(clientActivity);
        }

        // GET: ClientActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientActivity = await _context.ClientActivities
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.clientActivityID == id);
            if (clientActivity == null)
            {
                return NotFound();
            }

            return View(clientActivity);
        }

        // POST: ClientActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientActivity = await _context.ClientActivities.FindAsync(id);
            if (clientActivity != null)
            {
                _context.ClientActivities.Remove(clientActivity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientActivityExists(int id)
        {
            return _context.ClientActivities.Any(e => e.clientActivityID == id);
        }
    }
}
