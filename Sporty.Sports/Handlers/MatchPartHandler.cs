using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement.Records;
using Sporty.Sports.Models;
using YesSql;

namespace Sporty.Sports.Handlers
{
    public class MatchPartHandler : ContentPartHandler<MatchPart>
    {
        private readonly ISession _session;

        public MatchPartHandler(ISession session)
        {
            _session = session;
        }

        public async override Task UpdatedAsync(UpdateContentContext context, MatchPart part)
        {
            context.ContentItem.DisplayText = await BuildMatchTitle(part);
        }

        private async Task<string> BuildMatchTitle(MatchPart part)
        {
            var teamA = await _session.Query<ContentItem, ContentItemIndex>(
                    team => team.ContentItemId == part.TeamA.ContentItemIds.FirstOrDefault() && team.Latest == true
                ).FirstOrDefaultAsync();

            var teamB = await _session.Query<ContentItem, ContentItemIndex>(
                    team => team.ContentItemId == part.TeamB.ContentItemIds.FirstOrDefault()
                    && team.Latest == true
                ).FirstOrDefaultAsync();

            return string.Format("{0}-{1}", teamA.DisplayText, teamB.DisplayText);
        }
    }
}
