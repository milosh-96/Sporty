using OrchardCore.ContentFields.Fields;
using Sporty.Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.Sports.Services
{
    public class StandingsBuilder : IStandingsBuilder
    {
        private readonly StandingsCalculator _calculator;
        private List<MatchPart> _results;
        public StandingsBuilder()
        {
            _calculator = new StandingsCalculator();
        }

        public IEnumerable<StandingsItem> Build(List<MatchPart> results)
        {
            _results = results;
            List<StandingsItem> standings = new List<StandingsItem>();

            foreach (var item in results)
            {
                if (!standings.Any(
                    standingsItem => standingsItem.Team.ContentItem.ContentItemId == item.TeamA.ContentItemIds.First()))
                {
                    standings.Add(ProcessTeam(item.TeamA.ContentItemIds.First()));
                }

                if (!standings.Any(
                    standingsItem => standingsItem.Team.ContentItem.ContentItemId == item.TeamB.ContentItemIds.First()))
                {
                    standings.Add(ProcessTeam(item.TeamB.ContentItemIds.First()));
                }
            }
            return standings;
        }

        public StandingsItem ProcessTeam(string id)
        {
            var team = new StandingsItem() {  Team = new TeamPart() { ContentItem = new OrchardCore.ContentManagement.ContentItem { ContentItemId = id} } };

            team.Points = _calculator.CalculatePoints(_results, team.Team.ContentItem.ContentItemId);
            team.GoalsConceded = _calculator.CalculateConcededGoals(_results, team.Team.ContentItem.ContentItemId);
            team.GoalsScored = _calculator.CalculateScoredGoals(_results, team.Team.ContentItem.ContentItemId);
            team.GoalDifference = _calculator.CalculateGoalDifference(_results, team.Team.ContentItem.ContentItemId);
            team.Wins = _calculator.CalculateWins(_results, team.Team.ContentItem.ContentItemId);

            return team;
        }
    }
}
