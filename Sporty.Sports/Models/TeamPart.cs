using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Media;
using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.Sports.Models
{
    public class TeamPart : ContentPart
    {
        public TextField Name { get; set; } = new();
        public TextField ShortName { get; set; } = new();
        public TextField Abbreviation { get; set; } = new();
        public TextField Description { get; set; } = new();

        public MediaField Logo { get; set; } = new();
    }
}
