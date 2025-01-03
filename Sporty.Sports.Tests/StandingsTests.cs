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
        public void GoalDifferenceIsInvalid()
        {
            var actual = _calculator.CalculateGoalDifference(_results, "1");
            var expected = 0;

            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void GoalDifferenceIsValid()
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
        public void GoalDifferenceForMatchIsInvalid()
        {
            var actual = _calculator.CalculateGoalDifferenceForTeamPerMatch(3, 3);
            var expected = 0;
            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void GoalDifferenceForMatchIsValid()
        {
            var actual = _calculator.CalculateGoalDifferenceForTeamPerMatch(3, 3);
            var expected = 0;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GoalDifferenceForMatchIsNegative()
        {
            var actual = _calculator.CalculateGoalDifferenceForTeamPerMatch(1, 3);
            var expected = -2;
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GoalDifferenceForMatchIsPositive()
        {
            var actual = _calculator.CalculateGoalDifferenceForTeamPerMatch(3, 1);
            var expected = 2;
            Assert.Equal(expected, actual);
        }

    }
}