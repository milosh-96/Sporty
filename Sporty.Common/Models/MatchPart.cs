using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporty.Sports.Models
{
    public class MatchPart : ContentPart
    {
        public ContentPickerField TeamA { get; set; } = new();
        public ContentPickerField TeamB { get; set;} = new();
        public DateTimeField StartDate { get; set; } = new();
    }
}
