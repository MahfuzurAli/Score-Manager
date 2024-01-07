using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCOREgrp05.Data;
using SCOREgrp05.Models;

namespace SCOREgrp05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ButsAPIController : ControllerBase
    {
        private readonly MBContext _context;

        public ButsAPIController(MBContext context)
        {
            _context = context;
        }

        // GET: api/ButsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<But>>> GetBut()
        {
            return await _context.But.ToListAsync();
        }

        // GET: api/ButsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<But>> GetBut(int id)
        {
            var but = await _context.But.FindAsync(id);

            if (but == null)
            {
                return NotFound();
            }

            return but;
        }

        // PUT: api/ButsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBut(int id, But but)
        {
            if (id != but.butId)
            {
                return BadRequest();
            }

            _context.Entry(but).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ButExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ButsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<But>> PostBut(But but)
        {
            _context.But.Add(but);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBut", new { id = but.butId }, but);
        }

        // DELETE: api/ButsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBut(int id)
        {
            var but = await _context.But.FindAsync(id);
            if (but == null)
            {
                return NotFound();
            }

            _context.But.Remove(but);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ButExists(int id)
        {
            return _context.But.Any(e => e.butId == id);
        }
    }
}
