using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Viajemos.Web.Data;
using Viajemos.Web.Data.Entities;

namespace Viajemos.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialsController : ControllerBase
    {
        private readonly DataContext _context;

        public EditorialsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Editorials
        [HttpGet]
        public IEnumerable<Editorial> GetEditorials()
        {
            return _context.Editorials;
        }


        // GET: api/Editorials/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditorial([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var editorial = await _context.Editorials.FindAsync(id);

            if (editorial == null)
            {
                return NotFound();
            }

            return Ok(editorial);
        }

        // PUT: api/Editorials/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEditorial([FromRoute] int id, [FromBody] Editorial editorial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != editorial.Id)
            {
                return BadRequest();
            }

            _context.Entry(editorial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EditorialExists(id))
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

        // POST: api/Editorials
        [HttpPost]
        public async Task<IActionResult> PostEditorial([FromBody] Editorial editorial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Editorials.Add(editorial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEditorial", new { id = editorial.Id }, editorial);
        }

        // DELETE: api/Editorials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEditorial([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var editorial = await _context.Editorials.FindAsync(id);
            if (editorial == null)
            {
                return NotFound();
            }

            _context.Editorials.Remove(editorial);
            await _context.SaveChangesAsync();

            return Ok(editorial);
        }

        private bool EditorialExists(int id)
        {
            return _context.Editorials.Any(e => e.Id == id);
        }
    }
}