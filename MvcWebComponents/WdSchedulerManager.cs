namespace MvcWebComponents
{
    public class WdSchedulerManager
    {
        public static void Register(IWdScheduler scheduler)
        {
            scheduler.Start();
        }

        public static void UnRegister(IWdScheduler scheduler)
        {
            scheduler.Stop();
        }
    }
}
