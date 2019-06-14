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
    public class PermusersController: ODataController
    {
        private readonly acquizdbContext _context;

        public PermusersController(acquizdbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Permuser> Get()
        {
            // If you have any security filters you should apply them before returning then from this method.
            return _context.Permuser;
        }

        [EnableQuery]
        public SingleResult<Permuser> Get([FromODataUri] string userid, [FromODataUri] string monitor)
        {
            return SingleResult.Create(_context.Permuser.Where(p => String.CompareOrdinal(p.Userid, userid) == 0 && String.CompareOrdinal(p.Monitor, monitor) == 0));
        }

        public async Task<IActionResult> Post(Permuser puser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Permuser.Add(puser);
            await _context.SaveChangesAsync();

            return Created(puser);
        }

        public async Task<IActionResult> Delete([FromODataUri] string userid, [FromODataUri] string monitor)
        {
            var puser = await _context.Permuser.FindAsync(userid, monitor);
            if (puser == null)
            {
                return NotFound();
            }

            _context.Permuser.Remove(puser);
            await _context.SaveChangesAsync();

            return StatusCode(204); // HttpStatusCode.NoContent
        }

    }
}
