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

        private static int ProcessMatchResults(List<MatchPart> results, string teamId, Func<int, int, int> action)
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
            return CalculateScoredGoals(results, teamId) - CalculateConcededGaols(results, teamId);
        }
        public int CalculateScoredGoals(List<MatchPart> results, string teamId)
        {
            return ProcessMatchResults(results, teamId, CalculateScoredGoalsForTeamPerMatch);

        }
        public int CalculateConcededGaols(List<MatchPart> results, string teamId)
        {
            return ProcessMatchResults(results, teamId, CalculateConcededGoalsForTeamPerMatch);
        }
        public int CalculatePoints(List<MatchPart> results, string teamId)
        {
            return ProcessMatchResults(results, teamId, CalculatePointsForTeamPerMatch);
        }

        public int CalculateWins(List<MatchPart> results, string teamId)
        {
            return ProcessMatchResults(results, teamId, CalculateWinsForTeamPerMatch);
        }

        public int CalculateDraws(List<MatchPart> results, string teamId)
        {
            return ProcessMatchResults(results, teamId, CalculateDrawsForTeamPerMatch);
        }

        public int CalculateLosses(List<MatchPart> results, string teamId)
        {
            return ProcessMatchResults(results, teamId, CalculateLossesForTeamPerMatch);
        }

        public int CalculateGoalDifferenceForTeamPerMatch(int teamScore, int opponentScore)
        {
            return teamScore - opponentScore;
        }

        public int CalculatePointsForTeamPerMatch(int teamScore, int opponentScore)
        {
            if (teamScore > opponentScore)
            {
                return FootballDefaultValues.POINTS_FOR_WIN;
            }
            else if (teamScore == opponentScore)
            {
                return FootballDefaultValues.POINTS_FOR_DRAW;
            }

            return FootballDefaultValues.POINTS_FOR_LOSS;
        }

        public int CalculateWinsForTeamPerMatch(int teamScore, int opponentScore)
        {
            if (teamScore > opponentScore)
            {
                return 1;
            }
            return 0;
        }

        public int CalculateDrawsForTeamPerMatch(int teamScore, int opponentScore)
        {
            if (teamScore == opponentScore)
            {
                return 1;
            }
            return 0;
        }

        public int CalculateLossesForTeamPerMatch(int teamScore, int opponentScore)
        {
            if (teamScore < opponentScore)
            {
                return 1;
            }
            return 0;
        }

        public int CalculateMatchesPlayed(List<MatchPart> results, string teamId)
        {
            int sum = 0;
            foreach (MatchPart part in results)
            {
                if (part.TeamA.ContentItemIds.Contains(teamId) || part.TeamB.ContentItemIds.Contains(teamId))
                {
                    sum += 1;
                }
            }
            return sum;
        }

        public int CalculateScoredGoalsForTeamPerMatch(int teamScore, int opponentScore)
        {
            return teamScore;
        }

        public int CalculateConcededGoalsForTeamPerMatch(int teamScore, int opponentScore)
        {
            return opponentScore;
        }
    }

}