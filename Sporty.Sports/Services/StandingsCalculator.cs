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
        public int CalculateGoalDifference(List<MatchPart> results, string teamId)
        {
            int goalDifference = 0;
            foreach (MatchPart part in results)
            {
                if (part.TeamA.ContentItemIds.Contains(teamId))
                {
                    goalDifference += CalculateGoalDifferenceForTeamPerMatch((int)part.TeamAScore.Value, (int)part.TeamBScore.Value);
                }
                if (part.TeamB.ContentItemIds.Contains(teamId))
                {
                    goalDifference += CalculateGoalDifferenceForTeamPerMatch((int)part.TeamBScore.Value, (int)part.TeamAScore.Value);
                }
            }

            return goalDifference;
        }

        public int CalculateGoalDifferenceForTeamPerMatch(int teamScore, int opponentScore)
        {
            return teamScore - opponentScore;
        }
    }

    
}
