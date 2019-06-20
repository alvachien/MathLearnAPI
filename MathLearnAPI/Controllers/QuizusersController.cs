using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MathLearnAPI.Models;
using Microsoft.AspNet.OData;

namespace MathLearnAPI.Controllers
{
    public class QuizusersController: ODataController
    {
        private readonly acquizdbContext _context;

        public QuizusersController(acquizdbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Quizuser> Get()
        {
            // If you have any security filters you should apply them before returning then from this method.
            return _context.Quizuser;
        }

        [EnableQuery]
        public SingleResult<Quizuser> Get([FromODataUri] string userid)
        {
            return SingleResult.Create(_context.Quizuser.Where(p => String.CompareOrdinal(p.Userid, userid) == 0));
        }

        public async Task<IActionResult> Put([FromODataUri] string userid, [FromBody] Quizuser update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (String.CompareOrdinal(userid, update.Userid) != 0)
            {
                return BadRequest();
            }

            _context.Entry(update).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Quizuser.Any(p => String.CompareOrdinal(p.Userid, userid) == 0))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(update);
        }

        public async Task<IActionResult> Post([FromBody] Quizuser quser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Quizuser.Add(quser);
            await _context.SaveChangesAsync();

            return Created(quser);
        }

        public async Task<IActionResult> Patch([FromODataUri] string userid, [FromBody] Delta<Quizuser> quser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _context.Quizuser.FindAsync(userid);
            if (entity == null)
            {
                return NotFound();
            }

            quser.Patch(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Quizuser.Any(p => String.CompareOrdinal(p.Userid, userid) == 0))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(entity);
        }

        public async Task<IActionResult> Delete([FromODataUri] string userid)
        {
            var quser = await _context.Quizuser.FindAsync(userid);
            if (quser == null)
            {
                return NotFound();
            }

            _context.Quizuser.Remove(quser);
            await _context.SaveChangesAsync();

            return StatusCode(204); // HttpStatusCode.NoContent
        }
    }
}
