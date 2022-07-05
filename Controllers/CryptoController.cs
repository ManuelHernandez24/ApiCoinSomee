using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crypto.Models;

namespace CryptoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CryptoContext _context;

        public CryptoController(CryptoContext context)
        {
            _context = context;
        }

        // GET: api/Crypto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CryptoItem>>> GetCryptoItems()
        {
            if (_context.CryptoItems == null)
            {
                return NotFound();
            }
            return await _context.CryptoItems.ToListAsync();
        }

        // GET: api/Crypto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CryptoItem>> GetCryptoItem(int id)
        {
            if (_context.CryptoItems == null)
            {
                return NotFound();
            }
            var cryptoItem = await _context.CryptoItems.FindAsync(id);

            if (cryptoItem == null)
            {
                return NotFound();
            }

            return cryptoItem;
        }

        // PUT: api/Crypto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCryptoItem(int id, CryptoItem cryptoItem)
        {
            if (id != cryptoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(cryptoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CryptoItemExists(id))
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

        // POST: api/Crypto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CryptoItem>> PostCryptoItem(CryptoItem cryptoItem)
        {
            if (_context.CryptoItems == null)
            {
                return Problem("Entity set 'CryptoContext.CryptoItems'  is null.");
            }
            _context.CryptoItems.Add(cryptoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCryptoItem", new { id = cryptoItem.Id }, cryptoItem);
        }

        [HttpPost]
        [Route("All")]
        public async Task<ActionResult<CryptoItems>> PostCryptoItems(CryptoItems cryptoItems)
        {
            if (_context.CryptoItems == null)
            {
                return Problem("Entity set 'CryptoContext.CryptoItems'  is null.");
            }
            _context.CryptoItems.AddRangeAsync(cryptoItems.ListCryptoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCryptoItem", new { }, cryptoItems.ListCryptoItem);
        }

        // DELETE: api/Crypto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCryptoItem(int id)
        {
            if (_context.CryptoItems == null)
            {
                return NotFound();
            }
            var cryptoItem = await _context.CryptoItems.FindAsync(id);
            if (cryptoItem == null)
            {
                return NotFound();
            }

            _context.CryptoItems.Remove(cryptoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CryptoItemExists(int id)
        {
            return (_context.CryptoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
