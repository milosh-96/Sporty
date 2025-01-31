using Sporty.Sports.Services;
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

        public StandingsBuilderTests()
        {
            _builder = new StandingsBuilder();
        }

        [Fact]
        public void CorrectNumberOfTeamsIsHandled()
        {
            int expected = 4;
            int actual = _builder.Build().Count();
            Assert.Equal(expected, actual);
        }

    }
}
