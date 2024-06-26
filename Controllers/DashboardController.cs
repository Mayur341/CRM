﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CRM.Controllers
{
    public class DashboardController : Controller
    {
        private readonly CRMContext _context;

        public DashboardController(CRMContext context)
        {
            _context = context;
        }






        // GET: SALES SUMMARY 
        // GET: SALES SUMMARY 
        public async Task<IActionResult> Summary()
        {

            var clientEvents = await _context.ClientActivities
                .Include(c => c.Client)
                .Where(c => c.Activity_Type == "event" && c.Activity_Date >= DateTime.Today)
                .OrderBy(c => c.Activity_Date)
                .Take(10) // Assuming you want to show only the next 5 events
                .ToListAsync();

            // Store the upcoming client events in ViewBag
            ViewBag.ClientEvents = clientEvents;

            foreach (var clientEvent in clientEvents)
            {
                Console.WriteLine($"Event Name: {clientEvent.Activity_Type}, Client Name: {clientEvent.Activity_Description}, Date: {clientEvent.Activity_Date}");
            }




            // Fetch total numbers of leads, deals, and clients
            var totalClients = await _context.Clients.CountAsync();
            var totalDeals = await _context.Deals.CountAsync();
            var totalLeads = await _context.Leads.CountAsync();
            var AmountPending = await _context.Deals.SumAsync(d => d.pending_amount);

            // Fetch transactions and calculate total revenue generated
            var revenueGenerated = await _context.Transactions.SumAsync(t => t.Amount);

            // Calculate clients to lead conversion rate
            var leadsConvertedFromClients = await _context.Leads.Where(l => l.ClientID != null).CountAsync();
            var clientsToLeadsConversionRate = Math.Round((double)leadsConvertedFromClients / totalClients * 100, 2);

            var clientsByStatus = await _context.Clients
            .GroupBy(c => c.ClientStatus) // Assuming ClientStatusId is the foreign key to ClientStages table
            .Select(g => new {
             Status = g.FirstOrDefault().ClientStatus.StageName, // Access the StageName property from the joined ClientStage entity
             Count = g.Count()
             })
    .ToListAsync();


            foreach (var status in clientsByStatus)
            {
                Console.WriteLine($"Status: {status.Status}, Count: {status.Count}");
            }

            // Store the values in ViewBag
            ViewBag.TotalClients = totalClients;
            ViewBag.TotalDeals = totalDeals;
            ViewBag.TotalLeads = totalLeads;
            ViewBag.RevenueGenerated = revenueGenerated;
            ViewBag.AmountPending = AmountPending;
            ViewBag.ClientsToLeadsConversionRate = clientsToLeadsConversionRate;
            ViewBag.ClientsByStatus = clientsByStatus;
            ViewBag.ProspectByStages = clientsByStatus;

            return View();
        }

    }
}
