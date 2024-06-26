﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM.Models;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Authorization;

namespace CRM.Controllers
{
    
    public class ClientController : Controller
    {
        private readonly CRMContext _context;

        public ClientController(CRMContext context)
        {
            _context = context;
        }

        //export to pdf
      





        // GET: Client
        public async Task<IActionResult> Index()
        {
            var cRMContext = _context.Clients.Include(c => c.ClientStatus).Include(c => c.Source).Include(c => c.Address).Include(c => c.FinancialDetails).Include(c => c.Deal);

            // Fetch distinct ClientIDs from Addresses table
            var distinctClientIDs = await _context.Addresses.Select(a => a.ClientID).Distinct().ToListAsync();

            // Add distinct ClientIDs to ViewData
            ViewData["ClientID"] = distinctClientIDs;

            return View(await cRMContext.ToListAsync());
        }



        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.ClientStatus)
                .Include(c => c.Source)
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            Console.WriteLine("create calling");
            ViewData["ClientStatusID"] = new SelectList(_context.ClientStages, "ClientStatusID", "StageName");
            ViewData["SourceID"] = new SelectList(_context.Sources, "SourceID", "SourceName");
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientID,ClientName,Date,Subject,OrganizationName,MobileNumber,Email,SourceID,ClientStatusID")] Client client)
        {
            Console.WriteLine("create submitted");
            string clientInfo = $"ClientID: {client.ClientID}, " +
                        $"ClientName: {client.ClientName}, " +
                        $"Date: {client.Date}, " +
                        $"Subject: {client.Subject}, " +
                        $"OrganizationName: {client.OrganizationName}, " +
                        $"MobileNumber: {client.MobileNumber}, " +
                        $"Email: {client.Email}, " +
                        $"SourceID: {client.SourceID}, " +
                        $"ClientStatusID: {client.ClientStatusID}";

            // Print client information to console
            Console.WriteLine(clientInfo);
            
            
                Console.WriteLine("yeah its valid");
                Console.WriteLine(client);
            client.ClientStatusID = 1;
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
           /// ViewData["ClientStatusID"] = new SelectList(_context.ClientStages, "ClientStatusID", "ClientStatusID", client.ClientStatusID);
           /// ViewData["SourceID"] = new SelectList(_context.Sources, "SourceID", "SourceID", client.SourceID);
           /// return View(client);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["ClientStatusID"] = new SelectList(_context.ClientStages, "ClientStatusID", "StageName", client.ClientStatusID);
            ViewData["SourceID"] = new SelectList(_context.Sources, "SourceID", "SourceName", client.SourceID);
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientID,ClientName,Date,Subject,OrganizationName,MobileNumber,Email,SourceID,ClientStatusID")] Client client)
        {
            if (id != client.ClientID)
            {
                return NotFound();
            }

   
            
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["ClientStatusID"] = new SelectList(_context.ClientStages, "ClientStatusID", "ClientStatusID", client.ClientStatusID);
            ViewData["SourceID"] = new SelectList(_context.Sources, "SourceID", "SourceID", client.SourceID);
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Console.WriteLine("my name is khan");
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.ClientStatus)
                .Include(c => c.Source)
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            Console.WriteLine("Client Information Before Saving:");
            Console.WriteLine($"Client ID: {client.ClientID}");
            Console.WriteLine($"Client Name: {client.ClientName}");
            Console.WriteLine($"Date: {client.Date}");
            Console.WriteLine($"Subject: {client.Subject}");
            Console.WriteLine($"Organization Name: {client.OrganizationName}");
            Console.WriteLine($"Mobile Number: {client.MobileNumber}");
            Console.WriteLine($"Email: {client.Email}");
            Console.WriteLine($"Source ID: {client.SourceID}");
            Console.WriteLine($"Client Status ID: {client.ClientStatusID}");
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientID == id);
        }
    }
}
