using OrchardCore.ContentManagement.Handlers;
using Sporty.Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.Sports.Handlers
{
    public class TeamPartHandler : ContentPartHandler<TeamPart>
    {
        public override Task UpdatedAsync(UpdateContentContext context, TeamPart part)
        {
            context.ContentItem.DisplayText = part.Name.Text;
            return Task.CompletedTask;
        }
    }
}
