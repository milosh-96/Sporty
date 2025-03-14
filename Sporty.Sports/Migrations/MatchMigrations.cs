using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Media.Fields;
using OrchardCore.Media.Settings;
using Sporty.Sports.Constants;
using Sporty.Sports.Indexes;
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
            await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(MatchPart), part =>
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
                part.WithField(nameof(MatchPart.TeamAScore), field =>
                {
                    field
                        .OfType(nameof(NumericField))
                        .WithSettings(new NumericFieldSettings()
                        {
                            Required = true,
                            DefaultValue = "0",
                        })
                        .WithPosition("2")
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
                        .WithPosition("3")
                        ;
                });
                part.WithField(nameof(MatchPart.TeamBScore), field =>
                {
                    field
                        .OfType(nameof(NumericField))
                        .WithSettings(new NumericFieldSettings()
                        {
                            Required = true,
                            DefaultValue = "0",
                        })
                        .WithPosition("4")
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
                        .WithPosition("5")
                        ;
                });
                part.WithField(nameof(MatchPart.EventStatus), field =>
                {
                    field
                        .OfType(nameof(TextField))
                        .WithEditor(nameof(TextFieldEditors.PredefinedList))
                        .WithDisplayName("Event Status")
                        .WithSettings(
                            new TextFieldPredefinedListEditorSettings
                            {
                                Options = [
                                    FinishedValueOption,
                                    ScheduledValueOption,
                                    PostponedValueOption,
                                    CancelledValueOption
                                ],
                                DefaultValue = EventStatuses.Scheduled,
                                Editor = EditorOption.Dropdown,
                            });
                });
            });

            await _contentDefinitionManager.AlterTypeDefinitionAsync("Match", type =>
            {
                type.Listable().Creatable().Draftable().WithPart(nameof(MatchPart));
            });

            await SchemaBuilder.CreateMapIndexTableAsync<MatchPartIndex>(table =>
            {
                table.Column<string>(nameof(MatchPartIndex.ContenttemId));
                table.Column<string>(nameof(MatchPartIndex.TeamA));
                table.Column<string>(nameof(MatchPartIndex.TeamAScore));
                table.Column<string>(nameof(MatchPartIndex.TeamB));
                table.Column<string>(nameof(MatchPartIndex.TeamBScore));
                table.Column<string>(nameof(MatchPartIndex.EventStatus));
            });

            await SchemaBuilder.AlterTableAsync(nameof(MatchPartIndex), table => table
                .CreateIndex($"IDX_{nameof(MatchPartIndex)}_{nameof(MatchPartIndex.ContenttemId)}", nameof(MatchPartIndex.ContenttemId))
            );
            await SchemaBuilder.AlterTableAsync(nameof(MatchPartIndex), table => table
                .CreateIndex($"IDX_{nameof(MatchPartIndex)}_{nameof(MatchPartIndex.TeamA)}", nameof(MatchPartIndex.TeamA))
            );
            await SchemaBuilder.AlterTableAsync(nameof(MatchPartIndex), table => table
                .CreateIndex($"IDX_{nameof(MatchPartIndex)}_{nameof(MatchPartIndex.TeamAScore)}", nameof(MatchPartIndex.TeamAScore))
            );
            await SchemaBuilder.AlterTableAsync(nameof(MatchPartIndex), table => table
                .CreateIndex($"IDX_{nameof(MatchPartIndex)}_{nameof(MatchPartIndex.TeamB)}", nameof(MatchPartIndex.TeamB))
            );
            await SchemaBuilder.AlterTableAsync(nameof(MatchPartIndex), table => table
                .CreateIndex($"IDX_{nameof(MatchPartIndex)}_{nameof(MatchPartIndex.TeamBScore)}", nameof(MatchPartIndex.TeamBScore))
            );
            await SchemaBuilder.AlterTableAsync(nameof(MatchPartIndex), table => table
                .CreateIndex($"IDX_{nameof(MatchPartIndex)}_{nameof(MatchPartIndex.EventStatus)}", nameof(MatchPartIndex.EventStatus))
            );

            return 1;
        }

        private static readonly ListValueOption FinishedValueOption = new()
        {
            Name = nameof(EventStatuses.Finished),
            Value = EventStatuses.Finished,
        };
        private static readonly ListValueOption ScheduledValueOption = new()
        {
            Name = nameof(EventStatuses.Scheduled),
            Value = EventStatuses.Scheduled,
        };
        private static readonly ListValueOption PostponedValueOption = new()
        {
            Name = nameof(EventStatuses.Postponed),
            Value = EventStatuses.Postponed,
        };
        private static readonly ListValueOption CancelledValueOption = new()
        {
            Name = nameof(EventStatuses.Cancelled),
            Value = EventStatuses.Cancelled,
        };

    }
}
