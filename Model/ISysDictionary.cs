using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Platform.Model
{
    public interface ISysDictionary : ISysModel
    {
        string Key { get; set; }

        string Code { get; set; }
    }
}
