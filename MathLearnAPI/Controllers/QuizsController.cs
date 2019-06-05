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
    [Route("api/[controller]")]
    [ApiController]
    public class QuizsController : ODataController
    {
        private readonly acquizdbContext _context;

        public QuizsController(acquizdbContext context)
        {
            _context = context;
        }

        // GET: api/Quizs
        /// <summary>
        /// Adds support for getting quizs, for example:
        /// 
        /// GET /Quizs
        /// GET /Quizs?$filter=Name eq 'Windows 95'
        /// GET /Quizs?
        /// 
        /// <remarks>
        /// Support for $filter, $orderby, $top and $skip is provided by the [EnableQuery] attribute.
        /// </remarks>
        /// </summary>
        /// <returns>An IQueryable with all the products you want it to be possible for clients to reach.</returns>
        [EnableQuery]
        public IQueryable<Quiz> Get()
        {
            // If you have any security filters you should apply them before returning then from this method.
            return _context.Quiz;
        }

        // GET: api/Quizs/5
        /// <summary>
        /// Adds support for getting a product by key, for example:
        /// 
        /// GET /Quizs(1)
        /// </summary>
        /// <param name="key">The key of the Quizs required</param>
        /// <returns>The Quiz</returns>
        [EnableQuery]
        public SingleResult<Quiz> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_context.Quiz.Where(p => p.Quizid == key));
        }

        // PUT: api/Quizs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz([FromRoute] int id, [FromBody] Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quiz.Quizid)
            {
                return BadRequest();
            }

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
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

        // POST: api/Quizs
        [HttpPost]
        public async Task<IActionResult> PostQuiz([FromBody] Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Quiz.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.Quizid }, quiz);
        }

        // DELETE: api/Quizs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quiz = await _context.Quiz.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quiz.Remove(quiz);
            await _context.SaveChangesAsync();

            return Ok(quiz);
        }

        private bool QuizExists(int id)
        {
            return _context.Quiz.Any(e => e.Quizid == id);
        }
    }
}