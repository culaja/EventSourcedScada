using System;
using Autofac;
using AutofacApplicationWrapUp;
using CommandSide.Domain;
using CommandSide.Domain.Queueing;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.Domain.Queueing.Configuring;
using CommandSide.Domain.TicketIssuing;
using CommandSide.Domain.TicketIssuing.Commands;
using Common;
using Common.Messaging;
using static System.DateTime;
using static System.Linq.Enumerable;
using static CommandSide.Domain.TicketIssuing.Commands.TicketNumber;
using static Common.Nothing;
using static Common.Time.TimeOfDay;

namespace LargeEventStoreCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new MainRegistrator());
            var container = containerBuilder.Build();
            var commandBus = container.Resolve<ICommandBus>();

            CreateAndOpenCounters(commandBus, 20);
            ConfigureOpenTimes(commandBus);
            IssueTickets(commandBus, 1000000, 1);
            ServeCustomers(commandBus, 1000000, 20);
        }

        private static void CreateAndOpenCounters(ICommandBus commandBus, int numberOfCounters)
        {
            Execute(commandBus, new SetCounterConfiguration(
                new CounterConfiguration(
                    Range(1, numberOfCounters)
                        .Map(i => new CounterDetails(new CounterId(i), new CounterName($"Counter {i}"))))));

            Range(1, numberOfCounters).Map(i => Execute(commandBus, new OpenCounter(new CounterId(i))));
        }

        private static void ConfigureOpenTimes(ICommandBus commandBus)
        {
            Execute(commandBus, new SetOpenTimes(new OpenTimes(
                Range(1, 1).Map(i => new OpenTime(Now.DayOfWeek, TimeOfDayFromHour(0), TimeOfDayFromHour(24))))));
        }

        private static void IssueTickets(ICommandBus commandBus, int numberOfTickets, int startingFromTicket)
        {
            Range(startingFromTicket, numberOfTickets)
                .Map(i => Execute(commandBus, new IssueATicket(TicketNumberFrom(i))));
        }

        private static void ServeCustomers(ICommandBus commandBus, int numberOfCustomers, int numberOfCounters)
        {
            Range(1, numberOfCustomers)
                .Map(i =>
                    Range(1, (numberOfCustomers % numberOfCounters) + 1)
                        .Map(counter =>
                        {
                            Execute(commandBus, new NextCustomer(new CounterId(counter)));
                            Execute(commandBus, new ReCallCustomer(new CounterId(counter)));
                            return NotAtAll;
                        }));
        }

        private static int _numberOfCommandsExecuted;

        private static Nothing Execute(ICommandBus commandBus, ICommand command)
        {
            var result = commandBus.Execute(command);
            if (result.IsFailure)
            {
                throw new CommandExecutionException(result.Error);
            }

            _numberOfCommandsExecuted++;
            if (_numberOfCommandsExecuted % 1000 == 0) Console.WriteLine(_numberOfCommandsExecuted);

            return NotAtAll;
        }
    }
}