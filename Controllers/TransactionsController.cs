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
    public class TransactionsController : Controller
    {
        private readonly CRMContext _context;

        public TransactionsController(CRMContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Transactions.Include(t => t.Deal);
            return View(await cRMContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Deal)
                .FirstOrDefaultAsync(m => m.Transaction_id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public async Task<IActionResult> CreateAsync(int? id)
        {

            var deal = await _context.Deals.FindAsync(id);

            // If the client is found, pass its ID to the view
            if (deal != null)
            // Set the ID in the ViewBag
            {
                ViewBag.deal_id = id;
            }

            return View();

        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Transaction_id,Amount,deal_id,Transaction_Date")] Transaction transaction)
        {

            _context.Add(transaction);
            await _context.SaveChangesAsync();

            int dealid = transaction.deal_id;
            var totalAmountReceived = _context.Transactions
                                    .Where(t => t.deal_id == dealid)
                                         .Sum(t => t.Amount);

            Deal deal=_context.Deals.Find(dealid);
            if (deal != null)
            {
                deal.amount_received = totalAmountReceived;
                deal.pending_amount = deal.Total_Amount - totalAmountReceived;

                _context.Update(deal);
                await _context.SaveChangesAsync();
            }

            Console.WriteLine(deal.deal_name);
            Console.WriteLine(deal.Total_Amount);
            Console.WriteLine(deal.amount_received);
            

            return RedirectToAction(nameof(Index));

            ViewData["deal_id"] = new SelectList(_context.Deals, "deal_id", "deal_id", transaction.deal_id);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["deal_id"] = new SelectList(_context.Deals, "deal_id", "deal_id", transaction.deal_id);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Transaction_id,Amount,deal_id,Transaction_Date")] Transaction transaction)
        {
            if (id != transaction.Transaction_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Transaction_id))
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
            ViewData["deal_id"] = new SelectList(_context.Deals, "deal_id", "deal_id", transaction.deal_id);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Deal)
                .FirstOrDefaultAsync(m => m.Transaction_id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Transaction_id == id);
        }
    }
}
