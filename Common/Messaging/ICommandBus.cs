namespace Common.Messaging
{
    public interface ICommandBus
    {
        Result Execute(ICommand c);

        Nothing ScheduleOneWayCommand(ICommand c);
    }
}