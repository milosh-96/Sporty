using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using Sporty.Sports.Indexes;
using Sporty.Sports.Models;
using Sporty.Sports.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesSql;

namespace Sporty.Sports.Controllers
{
    public class StandingsController : Controller
    {
        private readonly IStandingsBuilder builder;
        private readonly ISession session;
        private readonly IContentManager contentManager;

        public StandingsController(ISession session, IContentManager contentManager, IStandingsBuilder standingsBuilder)
        {
            this.builder = standingsBuilder;
            this.session = session;
            this.contentManager = contentManager;
        }

        [Route("GetStandings")]
        public async Task<IActionResult> GetStandings()
        {
            var matches = (await session.QueryIndex<MatchPartIndex>().ListAsync()).Select(
               index => contentManager.GetAsync(index.ContenttemId).Result.As<MatchPart>()).ToList();
            return new JsonResult(builder.Build(matches));
        }
    }
}
