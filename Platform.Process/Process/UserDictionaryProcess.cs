using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Platform.Process.Enums;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;
using SqlComponents.SqlExcute;

namespace Platform.Process.Process
{
    /// <summary>
    /// 用户词典处理程序
    /// </summary>
    public class UserDictionaryProcess : ProcessBase, IUserDictionaryProcess
    {
        public object AddArea(string areaName, int areaLevel, Guid parentNode)
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                UserDictionary parentArea = null;
                if (areaLevel != 0)
                {
                    parentArea = repo.GetModel(obj => obj.Id == parentNode);
                    if (parentArea == null) return null;
                }

                if (repo.IsExists(obj => obj.ItemValue == areaName && obj.ItemLevel == areaLevel && obj.ParentDictionary.Id == parentNode))
                {
                    return null;
                }

                var dictItem = repo.CreateDefaultModel();
                dictItem.ItemKey = Globals.NewIdentityCode();
                dictItem.ItemLevel = areaLevel;
                dictItem.ItemValue = areaName;
                dictItem.ItemName = UserDictionaryType.Area;
                dictItem.ParentDictionary = parentArea;

                repo.AddOrUpdateDoCommit(dictItem);

                return new
                {
                    dictItem.Id,
                    dictItem.ItemKey,
                    dictItem.ItemLevel,
                    dictItem.ItemValue,
                    ParentNode = dictItem.ParentDictionary?.Id.ToString() ?? string.Empty
                };
            }
        }

        public object EditArea(Guid itemId, string itemValue)
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                var item = repo.GetModel(obj => obj.Id == itemId);
                if (item == null)
                {
                    return false;
                }

                item.ItemValue = itemValue;

                repo.AddOrUpdateDoCommit(item);
                return new
                {
                    item.Id,
                    item.ItemKey,
                    item.ItemLevel,
                    item.ItemValue,
                    ParentNode = item.ParentDictionary?.Id.ToString() ?? string.Empty
                };
            }
        }

        public object GetAreaInfo()
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                var areas = repo.GetModels(obj => obj.ItemName == UserDictionaryType.Area).Select(item => new
                {
                    item.Id,
                    item.ItemKey,
                    item.ItemLevel,
                    item.ItemValue,
                    ParentNode = item.ParentDictionary == null ? string.Empty : item.ParentDictionary.Id.ToString()
                }).ToList();

                var areaInfo = areas.Select(obj => new
                {
                    obj.Id,
                    obj.ItemKey,
                    obj.ItemLevel,
                    obj.ItemValue,
                    obj.ParentNode,
                    Parent = areas.FirstOrDefault(item => item.Id.ToString() == obj.ParentNode)
                })
                .OrderBy(item => item.ItemKey);

                return areaInfo;
            }
        }

        public List<UserDictionary> GetDictionaries(string name, int level)
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                return repo.GetModels(obj => obj.ItemName == name && obj.ItemLevel == level).ToList();
            }
        }

        public SqlExcuteResult DeleteArea(Guid itemId)
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                var sqlResult = new SqlExcuteResult() {Success = false};
                var item = repo.GetModel(obj => obj.Id == itemId);
                if (item == null) return sqlResult;

                var children = repo.GetModels(obj => obj.ParentDictionaryId == item.Id || obj.ParentDictionary.ParentDictionaryId == item.Id).ToList();

                children.Add(item);

                try
                {
                    repo.DeleteDoCommit(children);
                }
                catch (Exception ex)
                {
                    var innerEx = ex;
                    while (innerEx != null)
                    {
                        var sqlEx = innerEx as SqlException;
                        if (sqlEx != null)
                        {
                            sqlResult.Exception = sqlEx;
                            sqlResult.ErrorNumber = sqlEx.Number;

                            return sqlResult;
                        }

                        innerEx = innerEx.InnerException;
                    }

                    throw;
                }

                sqlResult.Success = true;
                return sqlResult;
            }
        }

        public Dictionary<Guid, string> GetDistrictSelectList()
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                if (RepositoryContext.UserContext.ContainsKey("district"))
                {
                    var userDistrict =
                        RepositoryContext.UserContext.Where(obj => obj.Key == "district")
                            .Select(item => Guid.Parse(item.Value.ToString().ToUpper()))
                            .ToList();
                    return repo.GetModels(obj => obj.ItemName == UserDictionaryType.Area && obj.ItemLevel == 0 && userDistrict.Contains(obj.Id))
                    .ToDictionary(key => key.Id, value => value.ItemValue);
                }
                return repo.GetModels(obj => obj.ItemName == UserDictionaryType.Area && obj.ItemLevel == 0)
                    .ToDictionary(key => key.Id, value => value.ItemValue);
            }
        }

        public Dictionary<Guid, string> GetStreetSelectList(Guid district)
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                return repo.GetModels(obj => obj.ItemName == UserDictionaryType.Area && obj.ItemLevel == 1 && obj.ParentDictionaryId == district)
                    .ToDictionary(key => key.Id, value => value.ItemValue);
            }
        }

        public Dictionary<Guid, string> GetAddressSelectList(Guid street)
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                return repo.GetModels(obj => obj.ItemName == UserDictionaryType.Area && obj.ItemLevel == 2 && obj.ParentDictionaryId == street)
                    .ToDictionary(key => key.Id, value => value.ItemValue);
            }
        }

        public Dictionary<Guid, string> GetChildDistrict(Guid id)
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                return repo.GetModels(obj => obj.ParentDictionary != null && obj.ParentDictionary.Id == id)
                    .ToDictionary(key => key.Id, value => value.ItemValue);
            }
        }

        public Dictionary<Guid, string> GetUserDistricts(List<Guid> userDistricts)
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                var query = repo.GetModels(obj => obj.ItemName == "Area" && obj.ItemLevel == 0);
                if (userDistricts != null)
                {
                    query = query.Where(obj => userDistricts.Contains(obj.Id));
                }
                return query.ToDictionary(key => key.Id, value => value.ItemValue);
            }
        }

        public UserDictionary GetDistrict(Guid districtGuid)
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                return repo.GetModelById(districtGuid);
            }
        }

        public UserDictionary GetAreaByName(string name)
        {
            using (var repo = Repo<UserDictionaryRepository>())
            {
                return repo.GetModel(d => d.ItemName == "Area" && d.ItemValue == name);
            }
        }
    }
}
