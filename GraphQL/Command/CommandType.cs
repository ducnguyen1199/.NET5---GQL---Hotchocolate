using HotChocolate.Types;
using CommanderGQL.Models;
using System.Linq;
using HotChocolate;
using CommanderGQL.Data;

namespace CommanderGQL.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Represents any sofware excutive command");

            descriptor
                .Field(c => c.Platform)
                .ResolveWith<Resolves>(c => c.GetPlatform(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("this is the platform to which the command belongs");
        }

        private class Resolves
        {
            public Platform GetPlatform([Parent] Command Command, [ScopedService] AppDbContext context)
            {
                return context.Platforms.FirstOrDefault(p => p.Id == Command.PlatformId);
            }
        }
    }
}