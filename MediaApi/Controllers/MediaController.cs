using MediaApi.Domain;
using MediaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaApi.Controllers
{
    public class MediaController : ControllerBase
    {
        MediaDataContext Context;

        public MediaController(MediaDataContext context)
        {
            Context = context;
        }

        // GET /media?kind=game
        [HttpGet("media")]
        public async Task<IActionResult> GetAllMedia([FromQuery] string kind = "All")
        {
            var query = Context.MediaItems
                .Where(m => m.Removed == false)
                .Select(m => new MediaResponsItem
                {
                    Id = m.Id,
                    Title = m.Title,
                    Consumed = m.Consumed,
                    DateConsumed = m.DateConsumed,
                    Kind = m.Kind,
                    RecommendedBy = m.RecommendedBy
                });
            if(kind != "All")
            {
                query = query.Where(q => q.Kind == kind);
            }
            var response = new GetMediaResponse
            {
                Data = await query.ToListAsync(),
                FilteredBy = kind
            };
            return Ok(response);
            // 2. This should be an asynchronous call. We are using up a valuable
            //    thred of execution just waiting for the datbase query to run.
        }

    }
}
