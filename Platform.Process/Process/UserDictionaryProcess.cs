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
        public UserDictionary AddArea(string areaName, int areaLevel, string parentNode)
        {
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
            {

                if (repo.IsExists(obj => obj.ItemValue == areaName && obj.ItemLevel == areaLevel))
                {
                    return null;
                }
                var dictItem = UserDictionaryRepository.CreateDefaultModel();
                dictItem.ItemKey = Globals.NewIdentityCode();
                dictItem.ItemLevel = areaLevel;
                dictItem.ItemValue = areaName;
                dictItem.ItemName = UserDictionaryType.Area;

                if (areaLevel != 0)
                {
                    var parentArea = repo.GetModel(obj => obj.ItemKey == parentNode);
                    if (parentArea == null) return null;
                    dictItem.ParentDictionary = parentArea;
                }
                repo.AddOrUpdate(dictItem);

                return dictItem;
            }
        }

        public object GetAreaInfo()
        {
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
            {
                var areaInfo = repo.GetModels(obj => obj.ItemName == UserDictionaryType.Area).Select(item => new
                {
                    item.ItemKey,
                    item.ItemLevel,
                    item.ItemValue,
                    ParentNode = string.IsNullOrWhiteSpace(item.ParentDictionary.ItemKey) ? string.Empty : item.ParentDictionary.ItemKey
                })
                    .ToList();

                return areaInfo;
            }
        }

        public bool DeleteArea(string itemKey)
        {
            using (var repo = DbRepository.Repo<UserDictionaryRepository>())
            {
                var item = repo.GetModel(obj => obj.ItemKey == itemKey);
                if (item == null) return false;

                return repo.Delete(item);
            }
        }
    }
}
