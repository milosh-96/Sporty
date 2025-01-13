using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.Sports.Models
{
    public class StandingsItem
    {
        public TeamPart Team { get; set; } = new TeamPart();

        public int Points { get; set; } = 0;
        public int Played { get; set; } = 0;
        public int Wins { get; set; } = 0;
        public int Draws { get; set; } = 0;
        public int Losses { get; set; } = 0;
        public int GoalsScored { get; set; } = 0;
        public int GoalsConceded { get; set; } = 0;
        public int GoalDifference { get; set; } = 0;
    }
}
