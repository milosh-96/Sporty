using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Media.Fields;
using OrchardCore.Media.Settings;
using Sporty.Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesSql.Sql;
using static Lombiq.HelpfulLibraries.OrchardCore.Contents.ContentFieldEditorEnums;

namespace Sporty.Sports.Migrations
{
    internal class MatchMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public MatchMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(MatchPart),part =>
            {
                part.WithField(nameof(MatchPart.TeamA), field =>
                {
                    field
                        .OfType(nameof(ContentPickerField))
                        .WithSettings(new ContentPickerFieldSettings()
                        {
                            Multiple = false,
                            DisplayedContentTypes = new[] { "Team" },
                            Required = true
                        })
                        .WithPosition("1")
                        ;
                });
                part.WithField(nameof(MatchPart.TeamB), field =>
                {
                    field
                        .OfType(nameof(ContentPickerField))
                        .WithSettings(new ContentPickerFieldSettings()
                        {
                            Multiple = false,
                            DisplayedContentTypes = new[] { "Team" },
                            Required = true
                        })
                        .WithPosition("2")
                        ;
                }); 
                part.WithField(nameof(MatchPart.StartDate), field =>
                {
                    field
                        .OfType(nameof(DateTimeField))
                        .WithSettings(new DateTimeFieldSettings()
                        { 
                            Required = true
                        })
                        .WithPosition("3")
                        ;
                });
                
            });

            await _contentDefinitionManager.AlterTypeDefinitionAsync("Match", type =>
            {
                type.Listable().Creatable().Draftable().Versionable().WithPart(nameof(MatchPart));
            });

            return 1;
        } 
       
    }
}
