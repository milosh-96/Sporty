using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using Sporty.Sports.Models;

public class StandingsMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public StandingsMigrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public async Task<int> Create()
    {

       
        return 1;
    }
}