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
        private readonly List<MatchPart> _results = new List<MatchPart>()
            {
                new MatchPart()
        {
            TeamA = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["2"] },
                    TeamAScore = new NumericField() { Value = 1 },
                    TeamBScore = new NumericField() { Value = 3 },
                },
                new MatchPart()
        {
            TeamA = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["3"] },
                    TeamAScore = new NumericField() { Value = 4 },
                    TeamBScore = new NumericField() { Value = 2 },
                },
                new MatchPart()
        {
            TeamA = new ContentPickerField() { ContentItemIds = ["4"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamAScore = new NumericField() { Value = 2 },
                    TeamBScore = new NumericField() { Value = 2 },
                }
        };

        public StandingsBuilder()
        {
            _calculator = new StandingsCalculator();
        }

        public IEnumerable<StandingsItem> Build()
        {
            List<StandingsItem> standings = new List<StandingsItem>();

            foreach (var item in _results)
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
