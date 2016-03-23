using System;
using MisakaBanZai.Services;

namespace MisakaBanZai.Models
{
    public class MisakaConnectionEventArgs : EventArgs
    {
        /// <summary>
        /// 通信连接对象
        /// </summary>
        public IMisakaConnection Connection { get; set; }
    }
}
