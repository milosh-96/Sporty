using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using Sporty.Sports.Constants;

namespace Sporty.Sports.Models
{
    public class MatchPart : ContentPart
    {
        public ContentPickerField TeamA { get; set; } = new();
        public NumericField TeamAScore { get; set; }  = new();
        public ContentPickerField TeamB { get; set;} = new();
        public NumericField TeamBScore { get; set; } = new();

        public DateTimeField StartDate { get; set; } = new();

        public TextField EventStatus { get; set; } = new ();

    }
}
