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
    public class CommunicationsController : Controller
    {
        private readonly CRMContext _context;

        public CommunicationsController(CRMContext context)
        {
            _context = context;
        }







        // GET: Communications
        public async Task<IActionResult> getById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communications = await _context.Communications
                .Include(c => c.Lead)
                .Where(c => c.Lead.LeadID == id)
                .ToListAsync();

            return View(communications);
        }

        /// customized controller for getting communications by leadid



        // GET: Communications
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Communications.Include(c => c.Lead);
            return View(await cRMContext.ToListAsync());
        }

        // GET: Communications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communication = await _context.Communications
                .Include(c => c.Lead)
                .FirstOrDefaultAsync(m => m.CommunicationID == id);
            if (communication == null)
            {
                return NotFound();
            }

            return View(communication);
        }

        // GET: Communications/Create
        public async Task<IActionResult> CreateAsync(int? id)
        {
            var lead = await _context.Leads.FindAsync(id);

            // If the client is found, pass its ID to the view
            if (lead != null)
            // Set the ID in the ViewBag
            {
                ViewBag.LeadID = id;
            }


         
            return View();
        }

        // POST: Communications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommunicationID,Details,LeadID,Date")] Communication communication)
        {
           
                _context.Add(communication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["LeadID"] = new SelectList(_context.Leads, "LeadID", "LeadID", communication.LeadID);
            return View(communication);
        }

        // GET: Communications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communication = await _context.Communications.FindAsync(id);
            if (communication == null)
            {
                return NotFound();
            }
            var lead = communication.Lead.LeadID;
            ViewBag.LeadID = lead;
            return View(communication);
        }

        // POST: Communications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommunicationID,Details,LeadID,Date")] Communication communication)
        {
            if (id != communication.CommunicationID)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(communication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunicationExists(communication.CommunicationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["LeadID"] = new SelectList(_context.Leads, "LeadID", "LeadID", communication.LeadID);
            return View(communication);
        }

        // GET: Communications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communication = await _context.Communications
                .Include(c => c.Lead)
                .FirstOrDefaultAsync(m => m.CommunicationID == id);
            if (communication == null)
            {
                return NotFound();
            }

            return View(communication);
        }

        // POST: Communications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var communication = await _context.Communications.FindAsync(id);
            if (communication != null)
            {
                _context.Communications.Remove(communication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommunicationExists(int id)
        {
            return _context.Communications.Any(e => e.CommunicationID == id);
        }
    }
}
