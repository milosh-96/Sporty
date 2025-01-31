using Sporty.Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.Sports.Services
{
    public interface IStandingsBuilder
    {
        public StandingsItem ProcessTeam(string id);
        public IEnumerable<StandingsItem> Build();
    }
}
