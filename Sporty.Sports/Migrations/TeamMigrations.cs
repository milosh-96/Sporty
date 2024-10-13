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
    internal class TeamMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public TeamMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(TeamPart),part =>
            {
                part.WithField(nameof(TeamPart.Name), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithPosition("1")
                        ;
                }); 
                part.WithField(nameof(TeamPart.ShortName), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithDescription("This is a shorter team name. Use something 'Real' for 'Real Madrid' or 'City' for 'Manchester City'.")
                        .WithPosition("2")
                        ;
                });
                part.WithField(nameof(TeamPart.Abbreviation), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithDescription("This is a team abbreviation that will be used in places where full name or even short name won't be useful for layouts. Use something like 'INT' for 'Inter' or 'RMA' for 'Real Madrid'.")
                        .WithPosition("3")
                        ;
                });

                part.WithField(nameof(TeamPart.Description), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithEditor(nameof(TextFieldEditors.TextArea))
                        .WithDescription("Write briefly (or extended) the history of the team.")
                        .WithPosition("4")
                        ;
                }); 
                
                part.WithField(nameof(TeamPart.Logo), field =>
                {
                    field
                        .OfType(nameof(MediaField))
                        .WithSettings(new MediaFieldSettings() { Multiple = false, Hint = "Main team logo (consider transparent background)" })
                        .WithPosition ("5")
                        ;
                });
            });

            await _contentDefinitionManager.AlterTypeDefinitionAsync("Team", type =>
            {
                type.Listable().Creatable().Draftable().Versionable().WithPart(nameof(TeamPart));
            });

            return 5;
        } 
        public async Task<int> UpdateFrom1Async()
        {
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Team", type =>
            {
                type.Creatable().Draftable().Versionable().WithPart(nameof(TeamPart));
            });

            return 2;
        }
        
        public async Task<int> UpdateFrom2Async()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(TeamPart), part =>
            {
                part.WithField(nameof(TeamPart.Name), field =>
                {
                    field
                        .OfType(nameof(TextField));
                });
                part.WithField(nameof(TeamPart.ShortName), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithDescription("This is a shorter team name. Use something 'Real' for 'Real Madrid' or 'City' for 'Manchester City'.")
                        ;
                });
                part.WithField(nameof(TeamPart.Abbreviation), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithDescription("This is a team abbreviation that will be used in places where full name or even short name won't be useful for layouts. Use something like 'INT' for 'Inter' or 'RMA' for 'Real Madrid'.")
                        ;
                });

                part.WithField(nameof(TeamPart.Description), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithEditor(nameof(TextFieldEditors.TextArea))
                        .WithDescription("Write briefly (or extended) the history of the team.");
                });

                part.WithField(nameof(TeamPart.Logo), field =>
                {
                    field
                        .OfType(nameof(MediaField))
                        .WithSettings(new MediaFieldSettings() { Multiple = false, Hint = "Main team logo (consider transparent background)" });
                });
            });

            return 3;
        }
        
        public async Task<int> UpdateFrom3Async()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(TeamPart), part =>
            {
                part.WithField(nameof(TeamPart.Name), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithPosition("1")
                        ;
                });
                part.WithField(nameof(TeamPart.ShortName), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithDescription("This is a shorter team name. Use something 'Real' for 'Real Madrid' or 'City' for 'Manchester City'.")
                        .WithPosition("2")
                        ;
                });
                part.WithField(nameof(TeamPart.Abbreviation), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithDescription("This is a team abbreviation that will be used in places where full name or even short name won't be useful for layouts. Use something like 'INT' for 'Inter' or 'RMA' for 'Real Madrid'.")
                        .WithPosition("3")
                        ;
                });

                part.WithField(nameof(TeamPart.Description), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithEditor(nameof(TextFieldEditors.TextArea))
                        .WithDescription("Write briefly (or extended) the history of the team.")
                        .WithPosition("4")
                        ;
                });

                part.WithField(nameof(TeamPart.Logo), field =>
                {
                    field
                        .OfType(nameof(MediaField))
                        .WithSettings(new MediaFieldSettings() { Multiple = false, Hint = "Main team logo (consider transparent background)" })
                        .WithPosition("5")
                        ;
                });
            });

            return 4;
        } 
        public async Task<int> UpdateFrom4Async()
        {
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Team", type =>
            {
                type.Listable().Creatable().Draftable().Versionable().WithPart(nameof(TeamPart));
            });

            return 5;
        }
    }
}
