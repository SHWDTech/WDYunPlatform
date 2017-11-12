using System;
using System.Linq;
using System.Linq.Expressions;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class DataStatisticsProcess : ProcessBase
    {
        public void StoreDataStatistic(DataStatistics data)
        {
            using (var repo = Repo<DataStatisticsRepository>())
            {
                repo.AddOrUpdateDoCommit(data);
            }
        }

        public DateTime GetLastUpdateDataDate(Expression<Func<DataStatistics, bool>> exp)
        {
            using (var repo = Repo<DataStatisticsRepository>())
            {
                var data = repo.GetModels(exp)
                    .OrderByDescending(item => item.UpdateTime)
                    .FirstOrDefault();

                return data?.UpdateTime ?? DateTime.MinValue;
            }
        }

        public double GetDayAverage(Expression<Func<DataStatistics, bool>> exp)
        {
            using (var repo = Repo<DataStatisticsRepository>())
            {
                var data = repo.GetModels(exp).Average(d => d.DoubleValue);

                return data.Value;
            }
        }

        public IQueryable<DataStatistics> GetDataStaitsticsRepo()
        {
            return Repo<DataStatisticsRepository>().GetAllModels();
        }
    }
}
