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
    public class QuizsectionsController : ODataController
    {
        private readonly acquizdbContext _context;

        public QuizsectionsController(acquizdbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Quizsection> Get()
        {
            // If you have any security filters you should apply them before returning then from this method.
            return _context.Quizsection;
        }

        [EnableQuery]
        public SingleResult<Quizsection> Get([FromODataUri] int quizid, [FromODataUri] int section)
        {
            return SingleResult.Create(_context.Quizsection.Where(p => p.Quizid == quizid && p.Section == section));
        }
    }
}
