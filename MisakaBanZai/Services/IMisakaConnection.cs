namespace MisakaBanZai.Services
{
    public interface IMisakaConnection
    {
        /// <summary>
        /// 连接名称
        /// </summary>
        string ConnectionName { get; set; }

        /// <summary>
        /// 连接类型
        /// </summary>
        string ConnectionType { get; set; }

        /// <summary>
        /// 连接对象
        /// </summary>
        object ConnObject { get; }
    }
}
