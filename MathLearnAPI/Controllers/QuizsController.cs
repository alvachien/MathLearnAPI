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
    public class QuizsController : ODataController
    {
        private readonly acquizdbContext _context;

        public QuizsController(acquizdbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Quiz> Get()
        {
            // If you have any security filters you should apply them before returning then from this method.
            return _context.Quiz;
        }

        [EnableQuery]
        public SingleResult<Quiz> Get([FromODataUri] int qid)
        {
            return SingleResult.Create(_context.Quiz.Where(p => p.Quizid == qid));
        }

        public async Task<IActionResult> Put([FromODataUri] int qid, [FromBody] Quiz update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (qid == update.Quizid)
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
                if (!_context.Quiz.Any(p => p.Quizid == qid))
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

        public async Task<IActionResult> Post([FromBody] Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Quiz.Add(quiz);
            await _context.SaveChangesAsync();

            return Created(quiz);
        }

        public async Task<IActionResult> Patch([FromODataUri] int qid, [FromBody] Delta<Quiz> quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _context.Quiz.FindAsync(qid);
            if (entity == null)
            {
                return NotFound();
            }

            quiz.Patch(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Quiz.Any(p => p.Quizid == qid))
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

        public async Task<IActionResult> Delete([FromODataUri] int qid)
        {
            var quiz = await _context.Quiz.FindAsync(qid);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quiz.Remove(quiz);
            await _context.SaveChangesAsync();

            return StatusCode(204); // HttpStatusCode.NoContent
        }
    }
}
