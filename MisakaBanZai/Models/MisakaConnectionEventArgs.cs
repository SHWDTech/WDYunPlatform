using System;
using MisakaBanZai.Services;

namespace MisakaBanZai.Models
{
    public class MisakaConnectionEventArgs : EventArgs
    {
        public IMisakaConnection Connection { get; set; }
    }
}
