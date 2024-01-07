using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SCOREgrp05.Data;
using SCOREgrp05.Hubs;
using SCOREgrp05.Models;

namespace SCOREgrp05.Controllers
{
    public class ButsController : Controller
    {
        private readonly MBContext _context;
        private readonly IHubContext<ButHub> _hubContext;

        public ButsController(MBContext context, IHubContext<ButHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: Buts
        public async Task<IActionResult> Index()
        {
            var mBContext = _context.But.Include(b => b.match);
            return View(await mBContext.ToListAsync());
        }

        // GET: Buts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var but = await _context.But
                .Include(b => b.match)
                .FirstOrDefaultAsync(m => m.butId == id);
            if (but == null)
            {
                return NotFound();
            }

            return View(but);
        }

        // GET: Buts/Create
        public IActionResult Create()
        {
            ViewData["matchId"] = new SelectList(_context.Match, "matchId", "matchId");
            return View();
        }

        // POST: Buts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("butId,matchId,score,temps,joueur")] But but)
        {
            if (ModelState.IsValid)
            {
                var match = _context.Match.Find(but.matchId);
                match.score = but.score;
                match.temps = but.temps;
                 //match.temps+= but.temps;
                _context.Add(but);
                await _context.SaveChangesAsync();

                await _hubContext.Clients.All.SendAsync("NewBut");

                return RedirectToAction(nameof(Index));
            }
            ViewData["matchId"] = new SelectList(_context.Match, "matchId", "matchId", but.matchId);
            return View(but);
        }

        // GET: Buts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var but = await _context.But.FindAsync(id);
            if (but == null)
            {
                return NotFound();
            }
            ViewData["matchId"] = new SelectList(_context.Match, "matchId", "matchId", but.matchId);
            return View(but);
        }

        // POST: Buts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("butId,matchId,score,temps,joueur")] But but)
        {
            if (id != but.butId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(but);
                    await _context.SaveChangesAsync();
                    await _hubContext.Clients.All.SendAsync("NewBut");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ButExists(but.butId))
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
            ViewData["matchId"] = new SelectList(_context.Match, "matchId", "matchId", but.matchId);
            return View(but);
        }

        // GET: Buts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var but = await _context.But
                .Include(b => b.match)
                .FirstOrDefaultAsync(m => m.butId == id);
            if (but == null)
            {
                return NotFound();
            }

            return View(but);
        }

        // POST: Buts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var but = await _context.But.FindAsync(id);
            if (but != null)
            {
                _context.But.Remove(but);
            }

            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("NewBut");
            return RedirectToAction(nameof(Index));
        }

        private bool ButExists(int id)
        {
            return _context.But.Any(e => e.butId == id);
        }
    }
}
