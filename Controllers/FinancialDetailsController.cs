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
    public class FinancialDetailsController : Controller
    {
        private readonly CRMContext _context;

        public FinancialDetailsController(CRMContext context)
        {
            _context = context;
        }

        // GET: FinancialDetails
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.FinancialDetails.Include(f => f.Client);
            return View(await cRMContext.ToListAsync());
        }

        // GET: FinancialDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialDetails = await _context.FinancialDetails
                .Include(f => f.Client)
                .FirstOrDefaultAsync(m => m.FID == id);
            if (financialDetails == null)
            {
                return NotFound();
            }

            return View(financialDetails);
        }

        // GET: FinancialDetails/Create
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

        // POST: FinancialDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FID,ClientID,AnnualIncome,IncomeSource,Occupation")] FinancialDetails financialDetails)
        {
            
                _context.Add(financialDetails);
                await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Client");

            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", financialDetails.ClientID);
            return View(financialDetails);
        }

        // GET: FinancialDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialDetails = await _context.FinancialDetails.FindAsync(id);
            if (financialDetails == null)
            {
                return NotFound();
            }
            var Fd = await _context.FinancialDetails.FindAsync(id);

            var Cid = Fd.ClientID;


            var client = await _context.Clients.FindAsync(Cid);


            if (client != null)

            {
                ViewBag.ClientID = Cid;
            }
            return View(financialDetails);
            
        }

        // POST: FinancialDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FID,ClientID,AnnualIncome,IncomeSource,Occupation")] FinancialDetails financialDetails)
        {
            if (id != financialDetails.FID)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(financialDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinancialDetailsExists(financialDetails.FID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", financialDetails.ClientID);
            return View(financialDetails);
        }

        // GET: FinancialDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialDetails = await _context.FinancialDetails
                .Include(f => f.Client)
                .FirstOrDefaultAsync(m => m.FID == id);
            if (financialDetails == null)
            {
                return NotFound();
            }

            return View(financialDetails);
        }

        // POST: FinancialDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financialDetails = await _context.FinancialDetails.FindAsync(id);
            if (financialDetails != null)
            {
                _context.FinancialDetails.Remove(financialDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinancialDetailsExists(int id)
        {
            return _context.FinancialDetails.Any(e => e.FID == id);
        }
    }
}
