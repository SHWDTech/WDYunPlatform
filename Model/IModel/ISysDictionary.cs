namespace SHWDTech.Platform.Model.IModel
{
    public interface ISysDictionary : ISysModel
    {
        string ItemName { get; set; }

        string ItemKey { get; set; }

        string ItemValue { get; set; }
    }
}
