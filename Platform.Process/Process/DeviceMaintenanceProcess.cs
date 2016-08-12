using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using PagedList;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class DeviceMaintenanceProcess : ProcessBase, IDeviceMaintenanceProcess
    {
        public IPagedList<DeviceMaintenance> GetPagedDeviceMaintenance(int page, int pageSize, string queryName, out int count)
        {
            using (var repo = Repo<DeviceMaintenanceRepository>())
            {
                var query = repo.GetAllModels();
                count = query.Count();

                return query.OrderBy(obj => obj.CreateDateTime).ToPagedList(page, pageSize);
            }
        }

        public DbEntityValidationException AddOrUpdateDeviceMaintenance(DeviceMaintenance model, List<string> propertyNames)
        {
            using (var repo = Repo<DeviceMaintenanceRepository>())
            {
                try
                {
                    if (model.Id == Guid.Empty)
                    {
                        var dbModel = DeviceMaintenanceRepository.CreateDefaultModel();
                        foreach (var propertyName in propertyNames)
                        {
                            dbModel.GetType().GetProperty(propertyName).SetValue(dbModel, model.GetType().GetProperty(propertyName).GetValue(model));
                        }
                        repo.AddOrUpdateDoCommit(dbModel);
                    }
                    else
                    {
                        repo.PartialUpdateDoCommit(model, propertyNames);
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return ex;
                }
            }

            return null;
        }

        public DeviceMaintenance GetDeviceMaintenance(Guid guid)
        {
            using (var repo = Repo<DeviceMaintenanceRepository>())
            {
                return repo.GetModelById(guid);
            }
        }
    }
}
