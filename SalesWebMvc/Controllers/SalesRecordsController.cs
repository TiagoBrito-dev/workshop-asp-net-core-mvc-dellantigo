﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesWebMvcContext _context;
        private readonly SalesRecordService _salesRecordService;
        private readonly SellerService _sellerService;

        public SalesRecordsController(SalesWebMvcContext context, SalesRecordService salesRecordService, SellerService sellerService)
        {
            _context = context;
            _salesRecordService = salesRecordService;
            _sellerService = sellerService;
        }

        
        // GET: SalesRecords
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesRecord.ToListAsync());
        }

        public async Task<IActionResult> SimpleSearch(DateTime ? minDate, DateTime ? maxDate)
        {
            if(!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            if(!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }



        // GET: SalesRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _salesRecordService.FindByIdAsync(id.Value);
            var salesRecord = await _context.Seller.FirstOrDefaultAsync(m => m.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // GET: SalesRecords/Create
        public async Task<IActionResult> Create()
        {
            var sellers = await _sellerService.FindAllAsync();
            var viewModel = new SalesRecordFormViewModel { Sellers = sellers };
            return View(viewModel);
        }

        // POST: SalesRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesRecord salesRecord)
        {
            if (!ModelState.IsValid)
            {
                var sellers = await _sellerService.FindAllAsync();
                var viewModel = new SalesRecordFormViewModel(salesRecord, sellers);
                return View(viewModel);
            }

            await _salesRecordService.InsertAsync(salesRecord);
            return RedirectToAction(nameof(Index));
        }


        // GET: SalesRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesRecord = await _context.SalesRecord.FindAsync(id);
            if (salesRecord == null)
            {
                return NotFound();
            }
            return View(salesRecord);
        }

        // POST: SalesRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Amount,Status")] SalesRecord salesRecord)
        {
            if (id != salesRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesRecordExists(salesRecord.Id))
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
            return View(salesRecord);
        }

        // GET: SalesRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesRecord = await _context.SalesRecord
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesRecord == null)
            {
                return NotFound();
            }

            return View(salesRecord);
        }

        // POST: SalesRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesRecord = await _context.SalesRecord.FindAsync(id);
            _context.SalesRecord.Remove(salesRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesRecordExists(int id)
        {
            return _context.SalesRecord.Any(e => e.Id == id);
        }
    }
}
