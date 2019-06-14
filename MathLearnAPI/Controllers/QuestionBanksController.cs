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
    public class QuestionBanksController : ODataController
    {
        private readonly acquizdbContext _context;

        public QuestionBanksController(acquizdbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds support for getting question banks, for example:
        /// 
        /// GET /QuestionBanks
        /// GET /QuestionBanks?$filter=Id eq '1'
        /// GET /QuestionBanks?
        /// 
        /// <remarks>
        /// Support for $filter, $orderby, $top and $skip is provided by the [EnableQuery] attribute.
        /// </remarks>
        /// </summary>
        /// <returns>An IQueryable with all the questionbanks you want it to be possible for clients to reach.</returns>
        [EnableQuery]
        public IQueryable<Questionbank> Get()
        {
            // If you have any security filters you should apply them before returning then from this method.
            return _context.Questionbank;
        }

        // GET: api/QuestionBanks/5
        /// <summary>
        /// Adds support for getting a question bank by key, for example:
        /// 
        /// GET /QuestionBanks(1)
        /// </summary>
        /// <param name="key">The key of the question bank required</param>
        /// <returns>The Question Bank</returns>
        [EnableQuery]
        public SingleResult<Questionbank> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_context.Questionbank.Where(p => p.Id == key));
        }

        // PUT: api/QuestionBanks/5
        /// <summary>
        /// Support for updating question bank
        /// </summary>
        public async Task<IActionResult> Put([FromODataUri] int key, Questionbank update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != update.Id)
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
                if (!_context.Questionbank.Any(p => p.Id == key))
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

        // POST: api/QuestionBanks
        /// <summary>
        /// Support for creating question bank
        /// </summary>
        public async Task<IActionResult> Post(Questionbank qbank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Questionbank.Add(qbank);
            await _context.SaveChangesAsync();

            return Created(qbank);
        }

        /// <summary>
        /// Support for partial updates of question bank
        /// </summary>
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Questionbank> qbank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _context.Questionbank.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            qbank.Patch(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Questionbank.Any(p => p.Id == key))
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

        // DELETE: api/QuestionBanks/5
        /// <summary>
        /// Support for deleting question bank by key.
        /// </summary>
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var qbank = await _context.Questionbank.FindAsync(key);
            if (qbank == null)
            {
                return NotFound();
            }

            _context.Questionbank.Remove(qbank);
            await _context.SaveChangesAsync();

            return StatusCode(204); // HttpStatusCode.NoContent
        }

        ///// <summary>
        ///// Adds support for getting a ProductFamily from a Product, for example:
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

        private bool QuestionbankExists(int id)
        {
            return _context.Questionbank.Any(e => e.Id == id);
        }

    }
}