using OrchardCore.ContentFields.Fields;
using Sporty.Sports.Models;
using Sporty.Sports.Services;

namespace Sporty.Sports.Tests
{
    public class StandingsTests
    {
        private readonly StandingsCalculator _calculator;
        private readonly List<MatchPart> _results;
        public StandingsTests()
        {
            _calculator = new StandingsCalculator();
            _results = new List<MatchPart>()
            {
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds =["1"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["2"] },
                    TeamAScore = new NumericField() { Value = 1},
                    TeamBScore = new NumericField() {Value = 3},
                },
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds =["1"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["3"] },
                    TeamAScore = new NumericField() { Value = 4},
                    TeamBScore = new NumericField() {Value = 2},
                },
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds =["4"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamAScore = new NumericField() { Value = 2},
                    TeamBScore = new NumericField() {Value = 2},
                }
            };
        }

        [Fact]
        public void PointsAreCorrect()
        {
            var actual = _calculator.CalculatePoints(_results, "1");
            var expected = 4;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GoalDifferenceIsCorrect()
        {
            var actual = _calculator.CalculateGoalDifference(_results, "1");
            var expected = 0;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GoalDifferenceIsNegative()
        {
            _results.Add(
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["2"] },
                    TeamAScore = new NumericField() { Value = 0 },
                    TeamBScore = new NumericField() { Value = 6 },
                });
            var actual = _calculator.CalculateGoalDifference(
                _results,
                "1");
            var expected = -6;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GoalDifferenceIsPositive()
        {
            _results.Add(
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds = ["2"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamAScore = new NumericField() { Value = 0 },
                    TeamBScore = new NumericField() { Value = 5 },
                });
            var actual = _calculator.CalculateGoalDifference(
                _results,
                "1");
            var expected = 5;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ScoredGoalsIsCorrect()
        {
            _results.Add(
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds = ["2"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamAScore = new NumericField() { Value = 0 },
                    TeamBScore = new NumericField() { Value = 5 },
                });

            var actual = _calculator.CalculateScoredGoals(_results, "1");
            var expected = 12;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConcededGoalsIsCorrect()
        {
            _results.Add(
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds = ["2"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamAScore = new NumericField() { Value = 0 },
                    TeamBScore = new NumericField() { Value = 5 },
                });

            var actual = _calculator.CalculateConcededGaols(_results, "1");
            var expected = 7;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WinsIsCorrect()
        {
            _results.Add(
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds = ["2"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamAScore = new NumericField() { Value = 0 },
                    TeamBScore = new NumericField() { Value = 5 },
                });

            var actual = _calculator.CalculateWins(_results, "1");
            var expected = 2;

            Assert.Equal(expected, actual);
        } 
       
        
        
        [Fact]
        public void DrawsIsCorrect()
        {
            _results.Add(
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds = ["2"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamAScore = new NumericField() { Value = 0 },
                    TeamBScore = new NumericField() { Value = 5 },
                });

            var actual = _calculator.CalculateDraws(_results, "1");
            var expected = 1;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void LossessIsCorrect()
        {
            _results.Add(
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds = ["2"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamAScore = new NumericField() { Value = 0 },
                    TeamBScore = new NumericField() { Value = 5 },
                });

            var actual = _calculator.CalculateLosses(_results, "1");
            var expected = 1;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void MatchesPlayedIsCorrect()
        {
            _results.Add(
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds = ["2"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamAScore = new NumericField() { Value = 0 },
                    TeamBScore = new NumericField() { Value = 5 },
                });
            
            _results.Add(
                new MatchPart()
                {
                    TeamA = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["1"] },
                    TeamAScore = new NumericField() { Value = 0 },
                    TeamBScore = new NumericField() { Value = 5 },
                });

            var actual = _calculator.CalculateMatchesPlayed(_results, "1");
            var expected = 5;

            Assert.Equal(expected, actual);
        }



        [Theory]
        [InlineData(1,4,-3)]
        [InlineData(0,6,-6)]
        [InlineData(1,3,-2)]
        [InlineData(1,2,-1)]
        [InlineData(5,1,4)]
        [InlineData(3,1,2)]
        [InlineData(10,1,9)]
        [InlineData(1,1,0)]
        [InlineData(2,2,0)]
        [InlineData(0,0,0)]
        public void GoalDifferenceForMatchIsCorrect(int teamScore, int opponentScore, int expected)
        {
            var actual = _calculator.CalculateGoalDifferenceForTeamPerMatch(teamScore, opponentScore);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 4, 1)]
        [InlineData(0, 6, 0)]
        [InlineData(1, 3, 1)]
        [InlineData(1, 2, 1)]
        [InlineData(5, 1, 5)]
        [InlineData(3, 1, 3)]
        [InlineData(10, 1, 10)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 2)]
        [InlineData(0, 0, 0)]
        public void ScoredGaolsForMatchIsCorrect(int teamScore, int opponentScore, int expected)
        {
            var actual = _calculator.CalculateScoredGoalsForTeamPerMatch(teamScore, opponentScore);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 4, 4)]
        [InlineData(0, 6, 6)]
        [InlineData(1, 3, 3)]
        [InlineData(1, 2, 2)]
        [InlineData(5, 1, 1)]
        [InlineData(3, 1, 1)]
        [InlineData(10, 1, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 2)]
        [InlineData(0, 0, 0)]
        public void ConcededGoalsForMatchIsCorrect(int teamScore, int opponentScore, int expected)
        {
            var actual = _calculator.CalculateConcededGoalsForTeamPerMatch(teamScore, opponentScore);
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(1,4,0)]
        [InlineData(0,6,0)]
        [InlineData(1,3,0)]
        [InlineData(1,2,0)]
        [InlineData(5,1,3)]
        [InlineData(3,1,3)]
        [InlineData(10,1,3)]
        [InlineData(1,1,1)]
        [InlineData(2,2,1)]
        [InlineData(0,0,1)]
        public void PointsForMatchIsCorrect(int teamScore, int opponentScore, int expected)
        {
            var actual = _calculator.CalculatePointsForTeamPerMatch(teamScore, opponentScore);
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(1,4,0)]
        [InlineData(0,6,0)]
        [InlineData(1,3,0)]
        [InlineData(1,2,0)]
        [InlineData(5,1,1)]
        [InlineData(3,1,1)]
        [InlineData(10,1,1)]
        [InlineData(1,1,0)]
        [InlineData(2,2,0)]
        [InlineData(0,0,0)]
        public void WinsForMatchIsCorrect(int teamScore, int opponentScore, int expected)
        {
            var actual = _calculator.CalculateWinsForTeamPerMatch(teamScore, opponentScore);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 4, 0)]
        [InlineData(0, 6, 0)]
        [InlineData(1, 3, 0)]
        [InlineData(1, 2, 0)]
        [InlineData(5, 1, 0)]
        [InlineData(3, 1, 0)]
        [InlineData(10, 1, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 1)]
        [InlineData(0, 0, 1)]
        public void DrawsForMatchIsCorrect(int teamScore, int opponentScore, int expected)
        {
            var actual = _calculator.CalculateDrawsForTeamPerMatch(teamScore, opponentScore);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1,4,1)]
        [InlineData(0,6,1)]
        [InlineData(1,3,1)]
        [InlineData(1,2,1)]
        [InlineData(5,1,0)]
        [InlineData(3,1,0)]
        [InlineData(10,1,0)]
        [InlineData(1,1,0)]
        [InlineData(2,2,0)]
        [InlineData(0,0,0)]
        public void LosssesForMatchIsCorrect(int teamScore, int opponentScore, int expected)
        {
            var actual = _calculator.CalculateLossesForTeamPerMatch(teamScore, opponentScore);
            Assert.Equal(expected, actual);
        }
    }
}