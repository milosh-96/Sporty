using OrchardCore.ContentManagement;
using Sporty.Sports.Models;

namespace Sporty.Sports.Services
{
    public class StandingsBuilder : IStandingsBuilder
    {
        private readonly StandingsCalculator _calculator;
        private List<MatchPart> _results;
        private readonly IContentManager _contentManager;
        public StandingsBuilder(IContentManager contentManager)
        {
            _calculator = new StandingsCalculator();
            _contentManager = contentManager;
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
            var team = new StandingsItem() {  Team = _contentManager.GetAsync(id).Result.As<TeamPart>() };

            team.Points = _calculator.CalculatePoints(_results, team.Team.ContentItem.ContentItemId);
            team.GoalsConceded = _calculator.CalculateConcededGoals(_results, team.Team.ContentItem.ContentItemId);
            team.GoalsScored = _calculator.CalculateScoredGoals(_results, team.Team.ContentItem.ContentItemId);
            team.GoalDifference = _calculator.CalculateGoalDifference(_results, team.Team.ContentItem.ContentItemId);
            team.Played = _calculator.CalculateMatchesPlayed(_results, team.Team.ContentItem.ContentItemId);
            team.Wins = _calculator.CalculateWins(_results, team.Team.ContentItem.ContentItemId);
            team.Draws = _calculator.CalculateDraws(_results, team.Team.ContentItem.ContentItemId);
            team.Losses = _calculator.CalculateLosses(_results, team.Team.ContentItem.ContentItemId);

            return team;
        }
    }
}
