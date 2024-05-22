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
    public class DealsController : Controller
    {
        private readonly CRMContext _context;

        public DealsController(CRMContext context)
        {
            _context = context;
        }

        // GET: Deals
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("getting the deals");
            var cRMContext = _context.Deals.Include(d => d.Client);
            return View(await cRMContext.ToListAsync());
        }

        // GET: Deals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deals
                .Include(d => d.Client)
                .FirstOrDefaultAsync(m => m.deal_id == id);
            if (deal == null)
            {
                return NotFound();
            }

            return View(deal);
        }

        // GET: Deals/Create
       
         public async Task<IActionResult> CreateAsync(int? id)
        {
            Console.WriteLine("Got the deal request dashboard");
            Console.WriteLine("cline id:"+id);

            var client = await _context.Clients.FindAsync(id);

            // If the client is found, pass its ID to the view
            if (client != null)
            // Set the ID in the ViewBag
            {
                ViewBag.ClientID = id;
            }

            return View();

        }





        // POST: Deals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("deal_id,ClientID,deal_name,deal_description,deal_date,Total_Amount,amount_received,pending_amount,closing_date,description,product_name")] Deal deal)
        {
            Console.WriteLine("got the deal");
            _context.Add(deal);
            await _context.SaveChangesAsync();

            //saving the first amount received in transaction table
            var transaction = new Transaction
            {
                deal_id = deal.deal_id,
                Amount = deal.amount_received,
                Transaction_Date = DateTime.Now // or any appropriate date
            };

            // Add the new transaction to the context and save changes
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();





            return RedirectToAction("Index", "Client");

            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", deal.ClientID);
            return View(deal);
        }

        // GET: Deals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deals.FindAsync(id);
            if (deal == null)
            {
                return NotFound();
            }
            var deals = await _context.Deals.FindAsync(id);

            var Cid = deals.ClientID;


            var client = await _context.Clients.FindAsync(Cid);


            if (client != null)

            {
                ViewBag.ClientID = Cid;
            }
            return View(deals);

        }

        // POST: Deals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("deal_id,ClientID,deal_name,deal_description,deal_date,Total_Amount,amount_received,pending_amount,closing_date,description,product_name")] Deal deal)
        {
            if (id != deal.deal_id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(deal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealExists(deal.deal_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", deal.ClientID);
            return View(deal);
        }

        // GET: Deals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deals
                .Include(d => d.Client)
                .FirstOrDefaultAsync(m => m.deal_id == id);
            if (deal == null)
            {
                return NotFound();
            }

            return View(deal);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deal = await _context.Deals.FindAsync(id);
            if (deal != null)
            {
                _context.Deals.Remove(deal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DealExists(int id)
        {
            return _context.Deals.Any(e => e.deal_id == id);
        }
    }
}
