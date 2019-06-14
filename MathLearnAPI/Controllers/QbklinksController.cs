using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MathLearnAPI.Models;
using Microsoft.AspNet.OData;

namespace MathLearnAPI.Controllers
{
    public class QbklinksController: ODataController
    {
        private readonly acquizdbContext _context;

        public QbklinksController(acquizdbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Qbklink> Get()
        {
            // If you have any security filters you should apply them before returning then from this method.
            return _context.Qbklink;
        }

        [EnableQuery]
        public SingleResult<Qbklink> Get([FromODataUri] int qbid, [FromODataUri] int kwgid)
        {
            return SingleResult.Create(_context.Qbklink.Where(p => p.Qbid == qbid && p.Kwgid == kwgid));
        }

        public async Task<IActionResult> Post(Qbklink link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Qbklink.Add(link);
            await _context.SaveChangesAsync();

            return Created(link);
        }

        public async Task<IActionResult> Delete([FromODataUri] int qbid, [FromODataUri] int kwgid)
        {
            var link = await _context.Qbklink.FindAsync(qbid, kwgid);
            if (link == null)
            {
                return NotFound();
            }

            _context.Qbklink.Remove(link);
            await _context.SaveChangesAsync();

            return StatusCode(204); // HttpStatusCode.NoContent
        }

        ///// <summary>
        ///// Adds support for getting from a Product, for example:
        ///// 
        ///// GET /Products(11)/Family
        ///// </summary>
        ///// <param name="key">The id of the Product</param>
        ///// <returns>The related ProductFamily</returns>
        //public async Task<IActionResult> GetFamily([FromODataUri] int key)
        //{
        //    var family = await _db.Products.Where(p => p.Id == key).Select(p => p.Family).SingleOrDefaultAsync();
        //    return Ok(family);
        //}
    }
}
