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
    public class KnowledgesController : ODataController
    {
        private readonly acquizdbContext _context;

        public KnowledgesController(acquizdbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds support for getting knowledges, for example:
        /// 
        /// GET /Knowledges
        /// GET /Knowledges?$filter=Name eq 'Windows 95'
        /// GET /Knowledges?
        /// 
        /// <remarks>
        /// Support for $filter, $orderby, $top and $skip is provided by the [EnableQuery] attribute.
        /// </remarks>
        /// </summary>
        /// <returns>An IQueryable with all the products you want it to be possible for clients to reach.</returns>
        [EnableQuery]
        public IQueryable<Knowledge> Get()
        {
            // If you have any security filters you should apply them before returning then from this method.
            return _context.Knowledge;
        }

        // GET: api/Knowledges/5
        /// <summary>
        /// Adds support for getting a knowledge by key, for example:
        /// 
        /// GET /Knowledges(1)
        /// </summary>
        /// <param name="key">The key of the Knowledge required</param>
        /// <returns>The Knowledge</returns>
        [EnableQuery]
        public SingleResult<Knowledge> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_context.Knowledge.Where(p => p.Id == key));
        }

        // PUT: api/Knowledges/5
        /// <summary>
        /// Support for updating Knowledges
        /// </summary>
        public async Task<IActionResult> Put([FromODataUri] int key, Knowledge update)
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
                if (!_context.Knowledge.Any(p => p.Id == key))
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

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutKnowledge([FromRoute] int id, [FromBody] Knowledge knowledge)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != knowledge.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(knowledge).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!KnowledgeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Knowledges
        /// <summary>
        /// Support for creating knowledge
        /// </summary>
        public async Task<IActionResult> Post(Knowledge knowledge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Knowledge.Add(knowledge);
            await _context.SaveChangesAsync();

            return Created(knowledge);
        }

        //[HttpPost]
        //public async Task<IActionResult> PostKnowledge([FromBody] Knowledge knowledge)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Knowledge.Add(knowledge);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetKnowledge", new { id = knowledge.Id }, knowledge);
        //}

        /// <summary>
        /// Support for partial updates of knowledges
        /// </summary>
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Knowledge> knowledge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _context.Knowledge.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            knowledge.Patch(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Knowledge.Any(p => p.Id == key))
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

        // DELETE: api/Knowledges/5
        /// <summary>
        /// Support for deleting knowledge by key.
        /// </summary>
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var knowledge = await _context.Knowledge.FindAsync(key);
            if (knowledge == null)
            {
                return NotFound();
            }

            _context.Knowledge.Remove(knowledge);
            await _context.SaveChangesAsync();

            return StatusCode(204); // HttpStatusCode.NoContent
        }

        /// <summary>
        /// Adds support for getting the linkages from a Knowledge, for example:
        /// 
        /// GET /Knowledges(11)/Qblink
        /// </summary>
        /// <param name="key">The id of the Product</param>
        /// <returns>The related link</returns>
        public async Task<IActionResult> GetQbklink([FromODataUri] int key)
        {
            var links = _context.Knowledge.Where(p => p.Id == key).Select(p => p.Qbklink);
            return Ok(links);
        }

        ///// <summary>
        ///// Support for creating links between entities in this entity set and other entities
        ///// using the specified navigation property.
        ///// </summary>
        ///// <remarks>
        ///// In this example Product only has a Product.Family relationship, which is a singleton, soon only PUT
        ///// support is required, if there was a Product.Orders relationship - a collection - then this would need 
        ///// to respond to POST requests too.
        ///// </remarks>
        ///// <param name="key">The key of the Entity in this EntitySet</param>
        ///// <param name="navigationProperty">The navigation property of the Entity in this EntitySet that should be modified</param>
        ///// <param name="link">The url to the other entity that should be related via the navigationProperty</param>
        //[AcceptVerbs("POST", "PUT")]
        //public async Task<IActionResult> CreateRef([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        //{
        //    var product = await _db.Products.FindAsync(key);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    switch (navigationProperty)
        //    {
        //        case "Family":
        //            // The utility method uses routing (ODataRoutes.GetById should match) to get the value of {id} parameter 
        //            // which is the id of the ProductFamily.
        //            var relatedKey = Request.GetKeyValue<int>(link);
        //            var family = await _db.ProductFamilies.SingleOrDefaultAsync(f => f.Id == relatedKey);
        //            product.Family = family;
        //            break;

        //        default:
        //            // return Content(HttpStatusCode.NotImplemented, ODataErrors.CreatingLinkNotSupported(navigationProperty));
        //            return StatusCode(501, ODataErrors.CreatingLinkNotSupported(navigationProperty));
        //    }
        //    await _db.SaveChangesAsync();
        //    return StatusCode(204); // HttpStatusCode.NoContent
        //}

        ///// <summary>
        ///// Support for removing links between resources
        ///// </summary>
        ///// <param name="key">The key of the entity with the navigation property</param>
        ///// <param name="navigationProperty">The navigation property on the entity to be modified</param>
        ///// <param name="link">The url to the other entity that should no longer be linked to the entity via the navigation property</param>
        //public async Task<IActionResult> DeleteRef([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        //{
        //    var product = await _db.Products.FindAsync(key);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    switch (navigationProperty)
        //    {
        //        case "Family":
        //            product.Family = null;
        //            break;

        //        default:
        //            // return Content(HttpStatusCode.NotImplemented, ODataErrors.DeletingLinkNotSupported(navigationProperty));
        //            return StatusCode(501, ODataErrors.DeletingLinkNotSupported(navigationProperty));
        //    }
        //    await _db.SaveChangesAsync();

        //    return StatusCode(204); // HttpStatusCode.NoContent
        //}

        private bool KnowledgeExists(int id)
        {
            return _context.Knowledge.Any(e => e.Id == id);
        }
    }
}