namespace SHWDTech.Platform.ProtocolCoding
{
    /// <summary>
    /// 数据有效性验证集
    /// </summary>
    public class DataValidFlag : IDataVallidFlag
    {
        /// <summary>
        /// 创建有效性验证集的实例，有效性验证集包含flagLength个有效性标志
        /// </summary>
        /// <param name="flagLength">有效性标志的数量</param>
        public DataValidFlag(int flagLength)
        {
            _dataFlagsList = new bool[flagLength];
        }

        public void AddFlag(int index, bool flag)
        {
            if (index >= _dataFlagsList.Length) return;

            _dataFlagsList[index] = flag;
        }

        public bool this[int index]
        {
            get
            {
                if (index == -1 || index >= _dataFlagsList.Length) return false;

                return _dataFlagsList[index];
            }

        } 

        /// <summary>
        /// 有效性标志位集合
        /// </summary>
        private readonly bool[] _dataFlagsList; 
    }
}
