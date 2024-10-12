using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.Modules;
using Sporty.Sports.Handlers;
using Sporty.Sports.Migrations;
using Sporty.Sports.Models;

namespace Sporty.Sports
{
    public sealed class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<TeamPart>().WithMigration<TeamMigrations>().AddHandler<TeamPartHandler>();
            services.AddContentPart<MatchPart>().WithMigration<MatchMigrations>().AddHandler<MatchPartHandler>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
           
        }
    }

}
