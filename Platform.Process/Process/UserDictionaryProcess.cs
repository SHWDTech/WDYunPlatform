using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process.Enums;
using Platform.Process.IProcess;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;

namespace Platform.Process.Process
{
    /// <summary>
    /// 用户词典处理程序
    /// </summary>
    public class UserDictionaryProcess : IUserDictionaryProcess
    {
        public object AddArea(string areaName, int areaLevel, Guid parentNode)
        {
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
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

                var dictItem = UserDictionaryRepository.CreateDefaultModel();
                dictItem.ItemKey = Globals.NewIdentityCode();
                dictItem.ItemLevel = areaLevel;
                dictItem.ItemValue = areaName;
                dictItem.ItemName = UserDictionaryType.Area;
                dictItem.ParentDictionary = parentArea;

                repo.AddOrUpdate(dictItem);

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
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
            {
                var item = repo.GetModel(obj => obj.Id == itemId);
                if (item == null)
                {
                    return false;
                }

                item.ItemValue = itemValue;

                repo.AddOrUpdate(item);
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
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
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

        public bool DeleteArea(Guid itemId)
        {
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
            {
                var item = repo.GetModel(obj => obj.Id == itemId);
                if (item == null) return false;

                var children = repo.GetModels(obj => obj.ParentDictionaryId == item.Id || obj.ParentDictionary.ParentDictionaryId == item.Id).ToList();

                children.Add(item);

                return repo.Delete(children) > 0;
            }
        }

        public Dictionary<string, string> GetDistrictSelectList()
        {
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
            {
                return repo.GetModels(obj => obj.ItemName == UserDictionaryType.Area && obj.ItemLevel == 0)
                    .ToDictionary(key => key.Id.ToString(), value => value.ItemValue);
            }
        }

        public Dictionary<string, string> GetChildDistrict(Guid id)
        {
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
            {
                return repo.GetModels(obj => obj.ParentDictionary != null && obj.ParentDictionary.Id == id)
                    .ToDictionary(key => key.Id.ToString(), value => value.ItemValue);
            }
        }
    }
}
