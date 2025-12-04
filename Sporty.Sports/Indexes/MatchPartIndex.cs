using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using Sporty.Sports.Constants;
using Sporty.Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesSql.Indexes;

namespace Sporty.Sports.Indexes
{
    public class MatchPartIndex : MapIndex
    {
        public string? ContentItemId { get; set; }
        public string TeamA { get; set; } = "";
        public string TeamB { get; set; } = "";

        public int TeamAScore { get; set; } = 0;
        public int TeamBScore { get; set; } = 0;
        public string EventStatus { get; set; } = EventStatuses.Scheduled;
    }

    public class MatchPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context) =>
           
            context.For<MatchPartIndex>()
                .When(contentItem => contentItem.Has<MatchPart>())
                .Map(contentItem =>
                {
                    var matchPart = contentItem.As<MatchPart>();
                    return new MatchPartIndex()
                    {
                        ContentItemId = contentItem.ContentItemId,
                        TeamA = matchPart.TeamA.ContentItemIds[0],
                        TeamAScore = matchPart.TeamAScore.Value != null ? (int)matchPart.TeamAScore.Value : 0,
                        TeamB = matchPart.TeamB.ContentItemIds[0],
                        TeamBScore = matchPart.TeamBScore.Value != null ? (int)matchPart.TeamBScore.Value : 0,
                        EventStatus = matchPart.EventStatus.Text,
                    };
                });
    }
}
