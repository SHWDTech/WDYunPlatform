using System.Data.Entity;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    public class ProtocolContext : DbContext
    {
        public ProtocolContext() : base("ProtocolContext")
        {
            
        }

        public ProtocolContext(string connStringOrName) : base(connStringOrName)
        {
            
        }

        /// <summary>
        /// 固件集表
        /// </summary>
        public virtual DbSet<FirmwareSet> FirmwareSets { get; set; }

        /// <summary>
        /// 固件表
        /// </summary>
        public virtual DbSet<Firmware> Firmwares { get; set; }

        /// <summary>
        /// 协议表
        /// </summary>
        public virtual DbSet<Protocol> Protocols { get; set; }

        /// <summary>
        /// 协议结构表
        /// </summary>
        public virtual DbSet<ProtocolStructure> ProtocolStructures { get; set; }

        /// <summary>
        /// 协议指令表
        /// </summary>
        public virtual DbSet<ProtocolCommand> ProtocolCommands { get; set; }

        /// <summary>
        /// 指令数据表
        /// </summary>
        public virtual DbSet<CommandData> CommandDatas { get; set; }

        /// <summary>
        /// 指令定义表
        /// </summary>
        public virtual DbSet<CommandDefinition> CommandDefinitions { get; set; }

        /// <summary>
        /// 协议数据记录
        /// </summary>
        public virtual DbSet<ProtocolData> ProtocolDatas { get; set; }
    }
}
