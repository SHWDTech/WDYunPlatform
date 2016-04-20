namespace SHWDTech.Platform.ProtocolCoding
{
    /// <summary>
    /// 数据有效性接口
    /// </summary>
    public interface IDataVallidFlag
    {
        /// <summary>
        /// 添加标志位
        /// </summary>
        /// <param name="index"></param>
        /// <param name="flag"></param>
        void AddFlag(int index, bool flag);

        /// <summary>
        /// 获取指定索引数据的标志位
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool this[int index] { get; }
    }
}
