using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;

namespace CRM.Controllers
{
    [Authorize]    
    
    public class LeadsController : Controller
    {
        private readonly CRMContext _context;

        public LeadsController(CRMContext context)
        {
            _context = context;
        }

        // GET: Leads
        public async Task<IActionResult> Index()
        {
            

            var cRMContext = _context.Leads.Include(l => l.Client).Include(l => l.LeadStage);
            var leadStageNames = await _context.LeadStages
                .Select(ls => ls.LeadStageName)
                .Distinct()
                .ToListAsync();

            // Create variables to store LeadStageNames
            string leadStage1 = null;
            string leadStage2 = null;
            string leadStage3 = null;
            string leadStage4 = null;
            // Add more variables if needed

            // Assign LeadStageNames to variables
            for (int i = 0; i < leadStageNames.Count; i++)
            {
                if (i == 0)
                {
                    leadStage4 = leadStageNames[i];
                }
                else if (i == 1)
                {
                    leadStage2 = leadStageNames[i];
                }
                else if (i == 2)
                {
                    leadStage3 = leadStageNames[i];
                }
                else
                {
                    leadStage1= leadStageNames[i];
                }
                // Add more conditions if needed for additional variables
            }

            // Pass the variables to the view
            ViewBag.LeadStage1 = leadStage1;
            ViewBag.LeadStage2 = leadStage2;
            ViewBag.LeadStage3 = leadStage3;
            ViewBag.LeadStage4 = leadStage4;
            // Add more ViewBag assignments if needed for additional variables
           
            return View(await cRMContext.ToListAsync());
        }


        // GET: Leads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Leads
                .Include(l => l.Client)
                .Include(l => l.LeadStage)
                .FirstOrDefaultAsync(m => m.LeadID == id);
            if (lead == null)
            {
                return NotFound();
            }

            return View(lead);
        }

        // GET: Leads/Create
        public async Task<IActionResult> CreateAsync(int? id)
        {
            //checking for deal if exists
            Console.WriteLine("leads creation page calling ");

            var dealExists = await _context.Deals.AnyAsync(d => d.ClientID == id);

            // If no deal exists for the client, show an alert and redirect back
            if (!dealExists)
            {
                // Set an alert message
                TempData["AlertMessage"] = "No deal is created for this client. Please create a deal first.";

                // Redirect to the previous page or any other appropriate page
                string refererUrl = Request.Headers["Referer"].ToString();
                if (string.IsNullOrEmpty(refererUrl))
                {
                    refererUrl = Url.Action("Index"); // Fallback to a default page if Referer is not available
                }
                return Redirect(refererUrl);
            }



            var client = await _context.Clients.FindAsync(id);

            // If the client is found, pass its ID to the view
            if (client != null)
            // Set the ID in the ViewBag
            {
                ViewBag.ClientID = id;
            }

            
            ViewData["LeadStageID"] = new SelectList(_context.LeadStages, "LeadStageID", "LeadStageName");
            return View();
        }

        // POST: Leads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeadID,ClientID,LeadName,LeadDate,LeadDescription,LeadStageID")] Lead lead)
        {
            
                _context.Add(lead);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", lead.ClientID);
            ViewData["LeadStageID"] = new SelectList(_context.LeadStages, "LeadStageID", "LeadStageID", lead.LeadStageID);
            return View(lead);
        }

        // GET: Leads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Leads.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }

            var Cid = lead.ClientID;
            var client = await _context.Clients.FindAsync(Cid);


            if (client != null)

            {
                ViewBag.ClientID = Cid;
            }


            ViewData["LeadStageID"] = new SelectList(_context.LeadStages, "LeadStageID", "LeadStageID", lead.LeadStageID);
            return View(lead);
        }

        // POST: Leads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeadID,ClientID,LeadName,LeadDate,LeadDescription,LeadStageID")] Lead lead)
        {
            if (id != lead.LeadID)
            {
                return NotFound();
            }

          
                try
                {
                    _context.Update(lead);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadExists(lead.LeadID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", lead.ClientID);
            ViewData["LeadStageID"] = new SelectList(_context.LeadStages, "LeadStageID", "LeadStageID", lead.LeadStageID);
            return View(lead);
        }

        // GET: Leads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Leads
                .Include(l => l.Client)
                .Include(l => l.LeadStage)
                .FirstOrDefaultAsync(m => m.LeadID == id);
            if (lead == null)
            {
                return NotFound();
            }

            return View(lead);
        }

        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead != null)
            {
                _context.Leads.Remove(lead);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeadExists(int id)
        {
            return _context.Leads.Any(e => e.LeadID == id);
        }
    }
}
