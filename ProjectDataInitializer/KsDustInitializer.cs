using System;
using System.Collections.Generic;
using System.Data.Entity;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.ProjectDataInitializer
{
    public class KsDustInitializer : DropCreateDatabaseAlways<RepositoryDbContext>
    {
        protected override void Seed(RepositoryDbContext dbContext)
        {
            var domain = new Domain
            {
                Id = Guid.Parse("DB07AB5E-A23F-4238-94CE-D52411199C18"),
                DomainName = "Root",
                DomianType = DomainType.RootDomain,
                CreateDateTime = DateTime.Now,
                DomainStatus = DomainStatus.Enabled,
                IsEnabled = true
            };

            var user = new WdUser
            {
                Id = Guid.Parse("77F05B52-AE62-4225-9F09-153C5634031F"),
                UserName = "Root",
                LoginName = "Root",
                Password = "b5ede2dc220e9c28362d5454d4f6bbd4",
                Email = "shweidongtech@126.com",
                Telephone = "18679361687",
                Status = UserStatus.Enabled,
                CreateDateTime = DateTime.Now,
                DomainId = domain.Id,
                Roles = new List<WdRole>(),
                IsEnabled = true
            };

            var commUser = new WdUser()
            {
                Id = Guid.Parse("c8c95a88-5d5d-4e80-a2d6-3ff32d472bde"),
                UserName = "CommnicationServer",
                LoginName = "CommnicationServer",
                Password = "b5ede2dc220e9c28362d5454d4f6bbd4",
                Email = "shweidongtech@126.com",
                Telephone = "18679361687",
                Status = UserStatus.Enabled,
                CreateDateTime = DateTime.Now,
                DomainId = domain.Id,
                Roles = new List<WdRole>(),
                IsEnabled = true
            };

            var role = new WdRole
            {
                Id = Guid.Parse("650BFB4C-7277-484A-812E-6052F6DB71C7"),
                RoleName = "Root",
                Users = new List<WdUser>(),
                CreateDateTime = DateTime.Now,
                Status = RoleStatus.Enabled,
                CreateUserId = user.Id,
                DomainId = domain.Id,
                IsEnabled = true
            };

            var adminRole = new WdRole()
            {
                Id = Guid.Parse("fce321ca-8761-44c4-8204-546dfa6134e1"),
                RoleName = "Admin",
                Users = new List<WdUser>(),
                CreateDateTime = DateTime.Now,
                Status = RoleStatus.Enabled,
                CreateUserId = user.Id,
                DomainId = domain.Id,
                IsEnabled = true
            };

            var serverRole = new WdRole()
            {
                Id = Guid.Parse("a45ae4cd-d0ad-4cea-b666-6787a42b2b4d"),
                RoleName = "Server",
                Users = new List<WdUser>(),
                CreateDateTime = DateTime.Now,
                Status = RoleStatus.Enabled,
                CreateUserId = user.Id,
                DomainId = domain.Id,
                IsEnabled = true
            };

            user.CreateUserId = user.Id;
            user.LastUpdateUserId = user.Id;
            user.LastUpdateDateTime = DateTime.Now;
            user.LastLoginDateTime = DateTime.Now;
            commUser.CreateUserId = user.Id;
            commUser.LastUpdateUserId = user.Id;
            commUser.LastUpdateDateTime = DateTime.Now;
            commUser.LastLoginDateTime = DateTime.Now;
            domain.CreateUserId = user.Id;
            domain.LastUpdateUserId = user.Id;
            domain.LastUpdateDateTime = DateTime.Now;
            role.CreateUserId = user.Id;
            role.LastUpdateUserId = user.Id;
            role.LastUpdateDateTime = DateTime.Now;
            adminRole.CreateUserId = user.Id;
            adminRole.LastUpdateUserId = user.Id;
            adminRole.LastUpdateDateTime = DateTime.Now;
            serverRole.CreateUserId = user.Id;
            serverRole.LastUpdateUserId = user.Id;
            serverRole.LastUpdateDateTime = DateTime.Now;
            user.Roles.Add(role);
            role.Users.Add(user);
            commUser.Roles.Add(serverRole);
            serverRole.Users.Add(commUser);

            dbContext.Users.Add(user);
            dbContext.Users.Add(commUser);
            dbContext.Roles.Add(role);
            dbContext.Roles.Add(adminRole);
            dbContext.Roles.Add(serverRole);
            dbContext.SysDomains.Add(domain);

            var field = new SysDictionary
            {
                Id = Guid.Parse("7402cdb5-1e1e-4633-a7e9-7d6d15634fc0"),
                ItemName = "Field",
                ItemKey = "7E0384B37CFJ",
                ItemValue = "GpsCommunication",
                ItemLevel = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true
            };

            var subfield = new SysDictionary
            {
                Id = Guid.Parse("24ae6ee7-a024-4052-a1de-3cc0d542d908"),
                ParentDictionaryId = field.Id,
                ItemName = "Subield",
                ItemKey = "7E0384B37CFL",
                ItemValue = "GeneralFunction",
                ItemLevel = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true
            };

            dbContext.SysDictionaries.Add(field);
            dbContext.SysDictionaries.Add(subfield);

            var deviceType = new DeviceType
            {
                Id = Guid.Parse("31b9ae77-6f5b-4c70-b180-d80ecb7df12b"),
                Field = field,
                SubField = subfield,
                Version = "2016-03-17",
                ReleaseDateTime = DateTime.Now,
                DeviceTypeCode = "WD_YC_Classic",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true
            };

            var firma = new Firmware
            {
                Id = Guid.Parse("648892a9-bf37-4490-a947-e1c0a529bba1"),
                FirmwareName = "firma",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                Protocols = new List<Protocol>()
            };

            dbContext.Firmwares.Add(firma);

            var firmSet = new FirmwareSet
            {
                Id = Guid.Parse("6c36fddf-d3d9-416b-84df-cd849006eef1"),
                FirmwareSetName = "扬尘第三版",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true
            };

            firmSet.Firmwares.Add(firma);
            dbContext.FirmwareSets.Add(firmSet);

            dbContext.DeviceTypes.Add(deviceType);

            var nep = new Protocol
            {
                Id = Guid.Parse("f59022bc-6f8c-4ced-954f-3a6d7dd29335"),
                FieldId = field.Id,
                SubFieldId = subfield.Id,
                CustomerInfo = "1",
                ProtocolName = "Nep",
                ProtocolModule = "Nep",
                Version = "HT212",
                ReleaseDateTime = DateTime.Now,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                CheckType = ProtocolCheckType.CrcModBus,
                Head = new byte[] { 0x23, 0x23 },
                Tail = new byte[] { 0x0D, 0x0A }
            };

            firma.Protocols.Add(nep);

            var head = new ProtocolStructure
            {
                Id = Guid.Parse("2bb54221-f036-48ff-babc-22358bdf50e2"),
                ProtocolId = nep.Id,
                StructureName = "Head",
                StructureIndex = 0,
                StructureDataLength = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.AsciiString,
                DefaultBytes = new byte[] { 0x23, 0x23 }
            };

            var contentLength = new ProtocolStructure
            {
                Id = Guid.Parse("277211ee-4ee9-4ef3-ab95-c43fce4c395b"),
                ProtocolId = nep.Id,
                StructureName = "ContentLength",
                StructureIndex = 1,
                StructureDataLength = 4,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.AsciiString,
                DefaultBytes = new byte[0]
            };

            var requestNumber = new ProtocolStructure
            {
                Id = Guid.Parse("e64862bb-1427-45e6-9dda-ec7925963573"),
                ProtocolId = nep.Id,
                StructureName = "RequestNumber",
                StructureIndex = 2,
                StructureDataLength = 21,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.AsciiString,
                DefaultBytes = new byte[0]
            };

            var cmdtype = new ProtocolStructure
            {
                Id = Guid.Parse("dcdb914f-62ec-42dc-bece-04124cfd61fa"),
                ProtocolId = nep.Id,
                StructureName = "CmdType",
                StructureIndex = 3,
                StructureDataLength = 6,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.AsciiString,
                DefaultBytes = new byte[0]
            };

            var cmdbyte = new ProtocolStructure
            {
                Id = Guid.Parse("fa009ac1-8a08-4243-a143-eb7cb8942660"),
                ProtocolId = nep.Id,
                StructureName = "CmdByte",
                StructureIndex = 4,
                StructureDataLength = 8,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.AsciiString,
                DefaultBytes = new byte[0]
            };

            var password = new ProtocolStructure()
            {
                Id = Guid.Parse("eddaa794-088a-4cc0-8c8e-b09635ba249a"),
                ProtocolId = nep.Id,
                StructureName = "Password",
                StructureIndex = 5,
                StructureDataLength = 10,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.AsciiString,
                DefaultBytes = new byte[] { 0x50, 0x57, 0x3D, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x3B }
            };

            var nodeid = new ProtocolStructure()
            {
                Id = Guid.Parse("ff20af2c-8f93-4755-889f-e8943c023e7d"),
                ProtocolId = nep.Id,
                StructureName = "NodeId",
                StructureIndex = 6,
                StructureDataLength = 18,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.AsciiString,
                DefaultBytes = new byte[0]
            };

            var data = new ProtocolStructure()
            {
                Id = Guid.Parse("4cde87be-468c-46cf-a1f9-0172f21761ca"),
                ProtocolId = nep.Id,
                StructureName = "Data",
                StructureIndex = 7,
                StructureDataLength = 0,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Data,
                DefaultBytes = new byte[0]
            };

            var crcModBus = new ProtocolStructure()
            {
                Id = Guid.Parse("1bd5725a-0408-4c4a-b7ad-f2c92dd830e2"),
                ProtocolId = nep.Id,
                StructureName = "CrcModBus",
                StructureIndex = 8,
                StructureDataLength = 4,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.CrcModBus,
                DefaultBytes = new byte[] { 0x00, 0x00, 0x00, 0x00 }
            };

            var tail = new ProtocolStructure()
            {
                Id = Guid.Parse("6aea3080-bb97-4e50-8140-7c31728b1637"),
                ProtocolId = nep.Id,
                StructureName = "Tail",
                StructureIndex = 9,
                StructureDataLength = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.AsciiString,
                DefaultBytes = new byte[] { 0x0D, 0x0A }
            };

            dbContext.ProtocolStructures.Add(head);
            dbContext.ProtocolStructures.Add(cmdtype);
            dbContext.ProtocolStructures.Add(cmdbyte);
            dbContext.ProtocolStructures.Add(password);
            dbContext.ProtocolStructures.Add(nodeid);
            dbContext.ProtocolStructures.Add(contentLength);
            dbContext.ProtocolStructures.Add(requestNumber);
            dbContext.ProtocolStructures.Add(data);
            dbContext.ProtocolStructures.Add(crcModBus);
            dbContext.ProtocolStructures.Add(tail);

            var commandReplyA = new SysConfig()
            {
                Id = Guid.Parse("b06c49c1-f9f7-4e69-92df-04fa55f95045"),
                SysConfigName = ProtocolDeliveryParam.StoreData,
                SysConfigType = SysConfigType.ProtocolDeliveryParam,
                SysConfigValue = ProtocolDeliveryParam.StoreData,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            dbContext.SysConfigs.Add(commandReplyA);

            var command = new ProtocolCommand
            {
                Id = Guid.Parse("d2ccf8b0-ed68-48ef-b185-f94b504944ca"),
                CommandTypeCode = new byte[] { 0x32, 0x32 },
                CommandCode = new byte[] { 0x32, 0x30, 0x31, 0x31 },
                ReceiveBytesLength = 0,
                CommandCategory = CommandCategory.TimingAutoReport,
                DataOrderType = DataOrderType.Random,
                ProtocolId = nep.Id,
                CommandDeliverParamConfigs = new List<SysConfig> { commandReplyA },
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandA = new ProtocolCommand
            {
                Id = Guid.Parse("ec4260ba-b36d-40ab-b357-15345590f516"),
                CommandTypeCode = new byte[] { 0x32, 0x32 },
                CommandCode = new byte[] { 0x32, 0x30, 0x31, 0x31 },
                ReceiveBytesLength = 0,
                CommandCategory = CommandCategory.TimingAutoReport,
                DataOrderType = DataOrderType.Random,
                ProtocolId = nep.Id,
                CommandDeliverParamConfigs = new List<SysConfig> { commandReplyA },
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandB = new ProtocolCommand
            {
                Id = Guid.Parse("9fbc1163-abf1-4f49-9bb6-64bec7946670"),
                CommandTypeCode = new byte[] { 0x32, 0x32 },
                CommandCode = new byte[] { 0x32, 0x30, 0x31, 0x31 },
                ReceiveBytesLength = 0,
                CommandCategory = CommandCategory.TimingAutoReport,
                DataOrderType = DataOrderType.Random,
                ProtocolId = nep.Id,
                CommandDeliverParamConfigs = new List<SysConfig> { commandReplyA },
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var tsp = new CommandData
            {
                Id = Guid.NewGuid(),
                DataIndex = 0,
                DataLength = 0,
                DataName = "a34001-Avg",
                DataDisplayName = "粉尘",
                DataConvertType = "DoubleString",
                DataValueType = DataValueType.Double,
                DataFlag = 0x00,
                ValidFlagIndex = 0,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var pm25 = new CommandData
            {
                Id = Guid.NewGuid(),
                DataIndex = 0,
                DataLength = 0,
                DataName = "a34004-Avg",
                DataDisplayName = "PM2.5",
                DataConvertType = "DoubleString",
                DataValueType = DataValueType.Double,
                DataFlag = 0x00,
                ValidFlagIndex = 0,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var pm100 = new CommandData
            {
                Id = Guid.NewGuid(),
                DataIndex = 0,
                DataLength = 0,
                DataName = "a34005-Avg",
                DataDisplayName = "PM10",
                DataConvertType = "DoubleString",
                DataValueType = DataValueType.Double,
                DataFlag = 0x00,
                ValidFlagIndex = 0,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var noise = new CommandData
            {
                Id = Guid.NewGuid(),
                DataIndex = 0,
                DataLength = 0,
                DataName = "a50001-Avg",
                DataDisplayName = "噪音",
                DataConvertType = "DoubleString",
                DataValueType = DataValueType.Double,
                DataFlag = 0x00,
                ValidFlagIndex = 0,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var temp = new CommandData
            {
                Id = Guid.NewGuid(),
                DataIndex = 0,
                DataLength = 0,
                DataName = "a01001-Avg",
                DataDisplayName = "温度",
                DataConvertType = "DoubleString",
                DataValueType = DataValueType.Double,
                DataFlag = 0x00,
                ValidFlagIndex = 0,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var huma = new CommandData
            {
                Id = Guid.NewGuid(),
                DataIndex = 0,
                DataLength = 0,
                DataName = "a01002-Avg",
                DataDisplayName = "湿度",
                DataConvertType = "DoubleString",
                DataValueType = DataValueType.Double,
                DataFlag = 0x00,
                ValidFlagIndex = 0,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var windspeed = new CommandData
            {
                Id = Guid.NewGuid(),
                DataIndex = 0,
                DataLength = 0,
                DataName = "a01007-Avg",
                DataDisplayName = "风速",
                DataConvertType = "DoubleString",
                DataValueType = DataValueType.Double,
                DataFlag = 0x00,
                ValidFlagIndex = 0,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var winddir = new CommandData
            {
                Id = Guid.NewGuid(),
                DataIndex = 0,
                DataLength = 0,
                DataName = "a01008-Avg",
                DataDisplayName = "风向",
                DataConvertType = "DoubleString",
                DataValueType = DataValueType.Double,
                DataFlag = 0x00,
                ValidFlagIndex = 0,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            command.CommandDatas = new List<CommandData> {tsp, pm25, pm100, noise, temp, huma, windspeed, winddir};
            commandA.CommandDatas = new List<CommandData> { tsp, pm25, pm100, noise, temp, huma, windspeed, winddir };
            commandB.CommandDatas = new List<CommandData> { tsp, pm25, pm100, noise, temp, huma, windspeed, winddir };
            dbContext.ProtocolCommands.Add(command);
            dbContext.ProtocolCommands.Add(commandA);
            dbContext.ProtocolCommands.Add(commandB);

            var dev = new Device()
            {
                Id = Guid.NewGuid(),
                DeviceTypeId = deviceType.Id,
                DeviceCode = "扬尘硬件第三版测试一号",
                DevicePassword = string.Empty,
                DeviceModuleGuid = Guid.NewGuid(),
                DeviceNodeId = "KSHBZBWCOM0001",
                FirmwareSetId = firmSet.Id,
                StartTime = DateTime.Now,
                PreEndTime = DateTime.Now,
                EndTime = DateTime.Now,
                Status = DeviceStatus.Enabled,
                DomainId = domain.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                IsEnabled = true
            };

            dbContext.Devices.Add(dev);
        }
    }

    public class KsDustDevelopInitializer : DropCreateDatabaseIfModelChanges<RepositoryDbContext>
    {
        protected override void Seed(RepositoryDbContext dbContext)
        {
            base.Seed(dbContext);
        }
    }
}
