using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.Data;
using OrchardCore.Modules;
using Sporty.Sports.Handlers;
using Sporty.Sports.Indexes;
using Sporty.Sports.Migrations;
using Sporty.Sports.Models;
using Sporty.Sports.Services;

namespace Sporty.Sports
{
    public sealed class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IStandingsBuilder, StandingsBuilder>();
            services.AddContentPart<TeamPart>().WithMigration<TeamMigrations>().AddHandler<TeamPartHandler>();
            services.AddContentPart<MatchPart>().WithMigration<MatchMigrations>().AddHandler<MatchPartHandler>();
            services.AddIndexProvider<MatchPartIndexProvider>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
           
        }
    }

}
