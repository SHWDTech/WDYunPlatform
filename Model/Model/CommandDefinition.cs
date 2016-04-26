using System;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 指令定义模型
    /// </summary>
    public class CommandDefinition : SysModelBase, ICommandDefinition
    {
        public Guid CommandGuid { get; set; }

        public ProtocolCommand Command { get; set; }

        public string StructureName { get; set; }

        public byte[] ContentBytes { get; set; }
    }
}
