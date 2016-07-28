using System;

namespace WebViewModels.ViewDataModel
{
    public class CleanessStatics
    {
        public Guid AreaGuid { get; set; }

        public string AreaName { get; set; }

        public long FaildRunningTimeTicks { get; set; }

        public TimeSpan FaildRunningTime 
            => new TimeSpan(FaildRunningTimeTicks);

        public long WorseRunningTimeTicks { get; set; }

        public TimeSpan WorseRunningTime
            => new TimeSpan(WorseRunningTimeTicks);

        public long QualifiedRunningTimeTicks { get; set; }

        public TimeSpan QualifiedRunningTime
            => new TimeSpan(QualifiedRunningTimeTicks);

        public long GoodRunningTimeTicks { get; set; }

        public TimeSpan GoodRunningTime
            => new TimeSpan(GoodRunningTimeTicks);

        public long FanRunningTimeTicks { get; set; }

        public TimeSpan FanRunningTime
            => new TimeSpan(FanRunningTimeTicks);

        public double OverRate
        {
            get
            {
                var total = FaildRunningTimeTicks + WorseRunningTimeTicks + QualifiedRunningTimeTicks +
                            GoodRunningTimeTicks;
                if (total == 0) return 0.0;

                return Math.Round((FaildRunningTimeTicks*1.0 + WorseRunningTimeTicks)/total);
            } 
        }
    }
}
