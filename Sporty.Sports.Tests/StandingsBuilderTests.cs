using OrchardCore.ContentFields.Fields;
using Sporty.Sports.Models;
using Sporty.Sports.Services;
using Sporty.Sports.Tests.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.Sports.Tests
{
    public class StandingsBuilderTests
    {
        private readonly IStandingsBuilder _builder;

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
                },new MatchPart()
        {
            TeamA = new ContentPickerField() { ContentItemIds = ["4"] },
                    TeamB = new ContentPickerField() { ContentItemIds = ["2"] },
                    TeamAScore = new NumericField() { Value = 4 },
                    TeamBScore = new NumericField() { Value = 2 },
                }
        };

        public StandingsBuilderTests()
        {
            _builder = new StandingsBuilderDemo();
        }

        [Fact]
        public void CorrectNumberOfTeamsIsHandled()
        {
            int expected = 4;
            int actual = _builder.Build(_results).Count();
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TeamHasValidNumberOfPoints()
        {
            int expected = 4;
            List<StandingsItem> standingsItem = _builder.Build(_results).ToList();
            Assert.Equal(expected, standingsItem.Where(x => x.Team.ContentItem.ContentItemId == "4").First().Points);
        }

    }
}
