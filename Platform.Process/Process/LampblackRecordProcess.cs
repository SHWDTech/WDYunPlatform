using System.Linq;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class LampblackRecordProcess : ProcessBase
    {
        public IQueryable<LampblackRecord> GetRecordRepo() => Repo<LampblackRecordRepository>().GetAllModels();
    }
}
