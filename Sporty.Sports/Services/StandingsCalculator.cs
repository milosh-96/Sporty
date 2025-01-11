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

        public int ProcessMatchResult(List<MatchPart> results, string teamId, Func<int, int, int> action)
        {
            int result = 0;
            foreach (MatchPart part in results)
            {
                if (part.TeamA.ContentItemIds.Contains(teamId))
                {
                    result += action((int)part.TeamAScore.Value, (int)part.TeamBScore.Value);
                }
                if (part.TeamB.ContentItemIds.Contains(teamId))
                {
                    result += action((int)part.TeamBScore.Value, (int)part.TeamAScore.Value);
                }
            }

            return result;
        }
        public int CalculateGoalDifference(List<MatchPart> results, string teamId)
        {
            return ProcessMatchResult(results, teamId, CalculateGoalDifferenceForTeamPerMatch);
        }

        public int CalculateGoalDifferenceForTeamPerMatch(int teamScore, int opponentScore)
        {
            return teamScore - opponentScore;
        }

        public int CalculatePoints(List<MatchPart> results, string teamId)
        {
            return ProcessMatchResult(results, teamId, CalculatePointsForTeamPerMatch);
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
