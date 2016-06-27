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
        public object AddArea(string areaName, int areaLevel, string parentNode)
        {
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
            {
                UserDictionary parentArea = null;
                if (areaLevel != 0)
                {
                    parentArea = repo.GetModel(obj => obj.ItemKey == parentNode);
                    if (parentArea == null) return null;
                }

                if (repo.IsExists(obj => obj.ItemValue == areaName && obj.ItemLevel == areaLevel && obj.ParentDictionary.ItemKey == parentNode))
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
                    dictItem.ItemKey,
                    dictItem.ItemLevel,
                    dictItem.ItemValue,
                    ParentNode = dictItem.ParentDictionary == null ? string.Empty : dictItem.ParentDictionary.ItemKey
                };
            }
        }

        public object GetAreaInfo()
        {
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
            {
                var areas = repo.GetModels(obj => obj.ItemName == UserDictionaryType.Area).Select(item => new
                {
                    item.ItemKey,
                    item.ItemLevel,
                    item.ItemValue,
                    ParentNode = item.ParentDictionary == null ? string.Empty : item.ParentDictionary.ItemKey
                }).ToList();

                var areaInfo = areas.Select(obj => new
                {
                    obj.ItemKey,
                    obj.ItemLevel,
                    obj.ItemValue,
                    obj.ParentNode,
                    Parent = areas.FirstOrDefault(item => item.ItemKey == obj.ParentNode)
                });

                return areaInfo;
            }
        }

        public bool DeleteArea(string itemKey)
        {
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
            {
                var item = repo.GetModel(obj => obj.ItemKey == itemKey);
                if (item == null) return false;

                var children = repo.GetModels(obj => obj.ParentDictionaryId == item.Id || obj.ParentDictionary.ParentDictionaryId == item.Id).ToList();

                children.Add(item);

                return repo.Delete(children) > 0;
            }
        }
    }
}
