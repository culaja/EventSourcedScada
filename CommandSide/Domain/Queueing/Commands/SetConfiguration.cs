using CommandSide.Domain.Queueing.Configuring;
using Common.Messaging;

namespace CommandSide.Domain.Queueing.Commands
{
    public sealed class SetConfiguration : ICommand
    {
        public Configuration Configuration { get; }

        public SetConfiguration(Configuration configuration)
        {
            Configuration = configuration;
        }
    }
}