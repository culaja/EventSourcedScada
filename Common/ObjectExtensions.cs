namespace Common
{
    public static class ObjectExtensions
    {
        public static Nothing ToNothing(this object o) => Nothing.NotAtAll;
    }
}