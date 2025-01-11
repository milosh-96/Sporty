using Sporty.Sports.Constants;
using Sporty.Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.Sports.Services
{
    public class StandingsCalculator
    {

        private int ProcessMatchResults(List<MatchPart> results, string teamId, Func<int, int, int> action)
        {
            int sum = 0;
            foreach (MatchPart part in results)
            {
                int teamAscore = (int)part.TeamAScore.Value;
                int teamBscore = (int)part.TeamBScore.Value;
                if (part.TeamA.ContentItemIds.Contains(teamId))
                {
                    sum += action(teamAscore, teamBscore);
                }
                if (part.TeamB.ContentItemIds.Contains(teamId))
                {
                    sum += action(teamBscore, teamAscore);
                }
            }

            return sum;
        }
        public int CalculateGoalDifference(List<MatchPart> results, string teamId)
        {
            return ProcessMatchResults(results, teamId, CalculateGoalDifferenceForTeamPerMatch);
        }

        public int CalculateGoalDifferenceForTeamPerMatch(int teamScore, int opponentScore)
        {
            return teamScore - opponentScore;
        }

        public int CalculatePoints(List<MatchPart> results, string teamId)
        {
            return ProcessMatchResults(results, teamId, CalculatePointsForTeamPerMatch);
        }

        public int CalculatePointsForTeamPerMatch(int teamScore, int opponentScore)
        {
            if (teamScore > opponentScore)
            {
                return FootballDefaultValues.POINTS_FOR_WIN;
            }
            else if(teamScore == opponentScore)
            {
                return FootballDefaultValues.POINTS_FOR_DRAW;
            }

            return FootballDefaultValues.POINTS_FOR_LOSS;
        }
    }
}
