using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SHWD.Platform.Repository.Entities
{
    public class InitProjectInitializer : DropCreateDatabaseAlways<RepositoryDbContext>
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
                FirmwareSetName = "扬尘第一版",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true
            };

            firmSet.Firmwares.Add(firma);
            dbContext.FirmwareSets.Add(firmSet);

            dbContext.DeviceTypes.Add(deviceType);

            var classic = new Protocol
            {
                Id = Guid.Parse("f59022bc-6f8c-4ced-954f-3a6d7dd29335"),
                FieldId = field.Id,
                SubFieldId = subfield.Id,
                CustomerInfo = "1",
                ProtocolName = "Classic",
                ProtocolModule = "Classic",
                Version = "Dust Protocol Brief V0017",
                ReleaseDateTime = DateTime.Now,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                CheckType = ProtocolCheckType.Crc16,
                Head = new byte[] { 0xAA },
                Tail = new byte[] { 0xDD }
            };

            firma.Protocols.Add(classic);

            var head = new ProtocolStructure
            {
                Id = Guid.Parse("2bb54221-f036-48ff-babc-22358bdf50e2"),
                ProtocolId = classic.Id,
                StructureName = "Head",
                StructureIndex = 0,
                StructureDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SingleByte,
                DefaultBytes = new byte[] { 0xAA }
            };

            var cmdtype = new ProtocolStructure
            {
                Id = Guid.Parse("dcdb914f-62ec-42dc-bece-04124cfd61fa"),
                ProtocolId = classic.Id,
                StructureName = "CmdType",
                StructureIndex = 1,
                StructureDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SingleByte,
                DefaultBytes = new byte[0]
            };

            var cmdbyte = new ProtocolStructure
            {
                Id = Guid.Parse("fa009ac1-8a08-4243-a143-eb7cb8942660"),
                ProtocolId = classic.Id,
                StructureName = "CmdByte",
                StructureIndex = 2,
                StructureDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SingleByte,
                DefaultBytes = new byte[0]
            };

            var password = new ProtocolStructure()
            {
                Id = Guid.Parse("eddaa794-088a-4cc0-8c8e-b09635ba249a"),
                ProtocolId = classic.Id,
                StructureName = "Password",
                StructureIndex = 3,
                StructureDataLength = 8,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.NumPassword,
                DefaultBytes = new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38 }
            };

            var nodeid = new ProtocolStructure()
            {
                Id = Guid.Parse("ff20af2c-8f93-4755-889f-e8943c023e7d"),
                ProtocolId = classic.Id,
                StructureName = "NodeId",
                StructureIndex = 4,
                StructureDataLength = 4,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.NodeId,
                DefaultBytes = new byte[0]
            };

            var descrip = new ProtocolStructure()
            {
                Id = Guid.Parse("92006a07-3726-4cdc-99a4-7b3fdf7ef03d"),
                ProtocolId = classic.Id,
                StructureName = "Description",
                StructureIndex = 5,
                StructureDataLength = 12,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Description,
                DefaultBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }
            };

            var sourceaddr = new ProtocolStructure()
            {
                Id = Guid.Parse("9ccdffd7-7d22-43d1-9185-a58541ce87f8"),
                ProtocolId = classic.Id,
                StructureName = "SourceAddr",
                StructureIndex = 6,
                StructureDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SourceAddr,
                DefaultBytes = new byte[] { 0x01 }
            };

            var destination = new ProtocolStructure()
            {
                Id = Guid.Parse("20ddc634-6ce6-47cc-82e1-c3df8f68abaa"),
                ProtocolId = classic.Id,
                StructureName = "DestinationAddr",
                StructureIndex = 7,
                StructureDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Destination,
                DefaultBytes = new byte[] { 0x01 }
            };

            var datalength = new ProtocolStructure()
            {
                Id = Guid.Parse("ad465018-24d3-400d-b7d5-712abf54ceeb"),
                ProtocolId = classic.Id,
                StructureName = "PayhloadLength",
                StructureIndex = 8,
                StructureDataLength = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.DataLength,
                DefaultBytes = new byte[] { 0x00, 0x00 }
            };

            var data = new ProtocolStructure()
            {
                Id = Guid.Parse("4cde87be-468c-46cf-a1f9-0172f21761ca"),
                ProtocolId = classic.Id,
                StructureName = "Data",
                StructureIndex = 9,
                StructureDataLength = 0,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Data,
                DefaultBytes = new byte[0]
            };

            var crc = new ProtocolStructure()
            {
                Id = Guid.Parse("1bd5725a-0408-4c4a-b7ad-f2c92dd830e2"),
                ProtocolId = classic.Id,
                StructureName = "CrcValue",
                StructureIndex = 10,
                StructureDataLength = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Crc,
                DefaultBytes = new byte[] { 0x00, 0x00 }
            };

            var tail = new ProtocolStructure()
            {
                Id = Guid.Parse("6aea3080-bb97-4e50-8140-7c31728b1637"),
                ProtocolId = classic.Id,
                StructureName = "Tail",
                StructureIndex = 11,
                StructureDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SingleByte,
                DefaultBytes = new byte[] { 0xDD }
            };

            dbContext.ProtocolStructures.Add(head);
            dbContext.ProtocolStructures.Add(cmdtype);
            dbContext.ProtocolStructures.Add(cmdbyte);
            dbContext.ProtocolStructures.Add(password);
            dbContext.ProtocolStructures.Add(nodeid);
            dbContext.ProtocolStructures.Add(descrip);
            dbContext.ProtocolStructures.Add(sourceaddr);
            dbContext.ProtocolStructures.Add(destination);
            dbContext.ProtocolStructures.Add(datalength);
            dbContext.ProtocolStructures.Add(data);
            dbContext.ProtocolStructures.Add(crc);
            dbContext.ProtocolStructures.Add(tail);

            var commandReply = new SysConfig()
            {
                Id = Guid.Parse("6ca98eba-47df-45c3-9bfb-1b56865b0a11"),
                SysConfigName = ProtocolDeliveryParam.ReplyOriginal,
                SysConfigType = SysConfigType.ProtocolDeliveryParam,
                SysConfigValue = ProtocolDeliveryParam.ReplyOriginal,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };


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

            dbContext.SysConfigs.Add(commandReply);
            dbContext.SysConfigs.Add(commandReplyA);

            var commandDataA = new CommandData()
            {
                Id = Guid.Parse("46225dc9-ffe2-43af-bba4-7b45bbb55af2"),
                DataIndex = 0,
                DataLength = 2,
                DataName = ProtocolDataName.DataValidFlag,
                DataType = ProtocolDataType.TwoBytesToUShort,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandDataB = new CommandData()
            {
                Id = Guid.Parse("489287c6-e179-4864-bd02-7f8962d5e81c"),
                DataIndex = 1,
                DataLength = 4,
                DataName = "PM2.5",
                DataType = ProtocolDataType.FourBytesToUInt32,
                ValidFlagIndex = 1,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandDataC = new CommandData()
            {
                Id = Guid.Parse("b69c75a5-c813-42a6-a3a6-5eeef561c068"),
                DataIndex = 2,
                DataLength = 4,
                DataName = "PM10",
                DataType = ProtocolDataType.FourBytesToUInt32,
                ValidFlagIndex = 1,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandDataD = new CommandData()
            {
                Id = Guid.Parse("36b4d5eb-b141-4cfd-9df5-d4508103fbbf"),
                DataIndex = 3,
                DataLength = 4,
                DataName = "CPM",
                DataType = ProtocolDataType.FourBytesToUInt32,
                ValidFlagIndex = 2,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandDataE = new CommandData()
            {
                Id = Guid.Parse("57874a4a-9a91-42dd-86de-e8940ad92c10"),
                DataIndex = 4,
                DataLength = 2,
                DataName = "噪音值",
                DataType = ProtocolDataType.TwoBytesToDoubleSeparate,
                ValidFlagIndex = 3,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandDataF = new CommandData()
            {
                Id = Guid.Parse("7e175d64-be72-48ed-baf2-549f80c27319"),
                DataIndex = 5,
                DataLength = 3,
                DataName = "风向",
                DataType = ProtocolDataType.ThreeBytesToUShort,
                ValidFlagIndex = 4,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandDataG = new CommandData()
            {
                Id = Guid.Parse("0c9124b1-168d-4510-bd8b-1acd183895a3"),
                DataIndex = 6,
                DataLength = 3,
                DataName = "风速",
                DataType = ProtocolDataType.ThreeBytesToUShort,
                ValidFlagIndex = 5,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandDataH = new CommandData()
            {
                Id = Guid.Parse("dc8815aa-0203-4c82-a09a-73521bcf208b"),
                DataIndex = 7,
                DataLength = 2,
                DataName = "温度",
                DataType = ProtocolDataType.TwoBytesToDoubleSeparate,
                ValidFlagIndex = 6,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandDataI = new CommandData()
            {
                Id = Guid.Parse("1ff2c99a-e0af-4419-80f6-53e69574d2c4"),
                DataIndex = 8,
                DataLength = 2,
                DataName = "湿度",
                DataType = ProtocolDataType.TwoBytesToDoubleSeparate,
                ValidFlagIndex = 6,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandDataJ = new CommandData()
            {
                Id = Guid.Parse("d24a7c0b-05f1-4a3f-b82c-6a5615ccdfc5"),
                DataIndex = 9,
                DataLength = 4,
                DataName = "挥发性有机物",
                DataType = ProtocolDataType.FourBytesToUInt32,
                ValidFlagIndex = 7,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var command = new ProtocolCommand
            {
                Id = Guid.Parse("d2ccf8b0-ed68-48ef-b185-f94b504944ca"),
                CommandTypeCode = new byte[] { 0xF9 },
                CommandCode = new byte[] { 0x1F },
                CommandBytesLength = 0,
                CommandCategory = CommandCategory.HeartBeat,
                ProtocolId = classic.Id,
                CommandDeliverParamConfigs = new List<SysConfig> { commandReply },
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var commandA = new ProtocolCommand
            {
                Id = Guid.Parse("29b2f9c1-a79b-4598-bc33-2b4c20488159"),
                CommandTypeCode = new byte[] { 0xFD },
                CommandCode = new byte[] { 0x27 },
                CommandBytesLength = 30,
                CommandCategory = CommandCategory.TimingAutoReport,
                ProtocolId = classic.Id,
                CommandDeliverParamConfigs = new List<SysConfig> { commandReplyA },
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            commandA.CommandDatas.Add(commandDataA);
            commandA.CommandDatas.Add(commandDataB);
            commandA.CommandDatas.Add(commandDataC);
            commandA.CommandDatas.Add(commandDataD);
            commandA.CommandDatas.Add(commandDataE);
            commandA.CommandDatas.Add(commandDataF);
            commandA.CommandDatas.Add(commandDataG);
            commandA.CommandDatas.Add(commandDataH);
            commandA.CommandDatas.Add(commandDataI);
            commandA.CommandDatas.Add(commandDataJ);

            dbContext.ProtocolCommands.Add(command);
            dbContext.ProtocolCommands.Add(commandA);

            var device = new Device
            {
                Id = Guid.Parse("ba0ca1dc-0331-4d5a-96e5-49ac20665a13"),
                DeviceTypeId = deviceType.Id,
                DeviceCode = "扬尘硬件第三版测试一号",
                DevicePassword = string.Empty,
                DeviceModuleGuid = Guid.Parse("024849d6-2538-48a0-8f0d-5a289e27f955"),
                DeviceNodeId = "00001F1F",
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

            dbContext.Devices.Add(device);
        }
    }

    public class DevelopInitializer : DropCreateDatabaseIfModelChanges<RepositoryDbContext>
    {
        protected override void Seed(RepositoryDbContext dbContext)
        {
            base.Seed(dbContext);
        }
    }

    public class ProductionInitializer : CreateDatabaseIfNotExists<RepositoryDbContext>
    {
        protected override void Seed(RepositoryDbContext dbContext)
        {
            base.Seed(dbContext);
        }
    }
}