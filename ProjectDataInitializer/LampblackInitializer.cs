using System;
using System.Collections.Generic;
using System.Data.Entity;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.ProjectDataInitializer
{
    public class LampBlackInitProjectInitializer : DropCreateDatabaseAlways<RepositoryDbContext>
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
                StructureName = "PayloadLength",
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
                StructureName = "CRCValue",
                StructureIndex = 10,
                StructureDataLength = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.CRC,
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

            var messageSource = new SysConfig
            {
                Id = Guid.Parse("34EDC4EC-F046-418E-A9DC-9EA3EA284F84"),
                SysConfigName = "CommandMessageQueueName",
                SysConfigType = SysConfigType.ProtocolAdminTools,
                SysConfigValue = @"FormatName:Direct=TCP:121.40.49.97\private$\protocolcommand",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            dbContext.SysConfigs.Add(commandReply);
            dbContext.SysConfigs.Add(commandReplyA);
            dbContext.SysConfigs.Add(messageSource);

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

            var commandB = new ProtocolCommand
            {
                Id = Guid.Parse("52FD2857-1607-4AD6-86C9-AC6B2B75BBB6"),
                CommandTypeCode = new byte[] { 0xFC },
                CommandCode = new byte[] { 0x1F },
                CommandBytesLength = 0,
                CommandCategory = CommandCategory.DeviceControl,
                ProtocolId = classic.Id,
                CommandDeliverParamConfigs = new List<SysConfig>(),
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
            dbContext.ProtocolCommands.Add(commandB);

            var lampblack = new Protocol
            {
                Id = Guid.Parse("ea38e48c-1df2-4bfb-8917-7f736795bdc3"),
                FieldId = field.Id,
                SubFieldId = subfield.Id,
                CustomerInfo = "1",
                ProtocolName = "Lampblack",
                ProtocolModule = "Lampblack",
                Version = "Lampblack_Protocol_V0001",
                ReleaseDateTime = DateTime.Now,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                CheckType = ProtocolCheckType.Lrc,
                Head = new byte[] { 0x3A },
                Tail = new byte[] { 0x0D, 0x0A }
            };

            dbContext.Protocols.Add(lampblack);

            var lbHead = new ProtocolStructure()
            {
                Id = Guid.Parse("a47ffc76-30ed-4d94-84d7-74540c51d9c4"),
                ProtocolId = lampblack.Id,
                StructureName = "Head",
                StructureIndex = 0,
                StructureDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SingleByte,
                DefaultBytes = new byte[] { 0x3A }
            };

            var lbFunctionCode = new ProtocolStructure()
            {
                Id = Guid.Parse("b9c7a5ef-d6ec-43e8-bdec-edcddbc8d4bb"),
                ProtocolId = lampblack.Id,
                StructureName = "FunctionCode",
                StructureIndex = 1,
                StructureDataLength = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.FunctionCode,
                DefaultBytes = new byte[0]
            };

            var lbDeviceNodeId = new ProtocolStructure()
            {
                Id = Guid.Parse("a98f5e06-5093-4ab9-9762-5dacc4bdaf73"),
                ProtocolId = lampblack.Id,
                StructureName = "DeviceNodeId",
                StructureIndex = 2,
                StructureDataLength = 7,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.NodeId,
                DefaultBytes = new byte[0]
            };

            var lbData = new ProtocolStructure()
            {
                Id = Guid.Parse("c3ec8746-7c23-43bd-9cf8-a0db19c29655"),
                ProtocolId = lampblack.Id,
                StructureName = "Data",
                StructureIndex = 3,
                StructureDataLength = 0,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Data,
                DefaultBytes = new byte[0]
            };

            var lbAscTime = new ProtocolStructure()
            {
                Id = Guid.Parse("d9c5172a-233b-4ea8-b064-297bc9c8fb75"),
                ProtocolId = lampblack.Id,
                StructureName = "ASCTime",
                StructureIndex = 4,
                StructureDataLength = 14,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.ASCTime,
                DefaultBytes = new byte[0]
            };

            var lbLrc = new ProtocolStructure()
            {
                Id = Guid.Parse("3e8b6176-19b0-4b0b-8635-a9a995799da4"),
                ProtocolId = lampblack.Id,
                StructureName = "LRCValue",
                StructureIndex = 5,
                StructureDataLength = 3,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.LRC,
                DefaultBytes = new byte[0]
            };

            var lbTail = new ProtocolStructure()
            {
                Id = Guid.Parse("56d4150a-7a22-4c74-8a22-9bc6b3bb70f7"),
                ProtocolId = lampblack.Id,
                StructureName = "Tail",
                StructureIndex = 6,
                StructureDataLength = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Bytes,
                DefaultBytes = new byte[] { 0x0D, 0x0A }
            };

            dbContext.ProtocolStructures.Add(lbHead);
            dbContext.ProtocolStructures.Add(lbFunctionCode);
            dbContext.ProtocolStructures.Add(lbDeviceNodeId);
            dbContext.ProtocolStructures.Add(lbData);
            dbContext.ProtocolStructures.Add(lbAscTime);
            dbContext.ProtocolStructures.Add(lbLrc);
            dbContext.ProtocolStructures.Add(lbTail);

            var userdomain = new Domain
            {
                Id = Guid.Parse("DB07AB5E-A23F-4238-94CE-D52411199C18"),
                DomainName = "乾铎环科",
                DomianType = DomainType.UserDomain,
                CreateDateTime = DateTime.Now,
                DomainStatus = DomainStatus.Enabled,
                IsEnabled = true
            };

            dbContext.SysDomains.Add(userdomain);

            var menu1 = new Menu()
            {
                Id = Guid.Parse("38f7dd0c-9210-4103-92ef-0a32a61e44ee"),
                DomainId = userdomain.Id,
                MenuName = "在线监测",
                Controller = "Monitor",
                Action = string.Empty,
                MenuLevel = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(menu1);

            var submenu1 = new Menu()
            {
                Id = Guid.Parse("7bd18c1a-c73c-4626-ab70-84ce8ac59673"),
                DomainId = userdomain.Id,
                ParentMenuId = menu1.Id,
                MenuName = "地图监测",
                Controller = "Monitor",
                Action = "Map",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu1);

            var submenu2 = new Menu()
            {
                Id = Guid.Parse("fc7753a0-6ef0-45cf-98fd-81e209acca0f"),
                DomainId = userdomain.Id,
                ParentMenuId = menu1.Id,
                MenuName = "实时数据监测",
                Controller = "Monitor",
                Action = "Actual",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu2);

            var menu2 = new Menu()
            {
                Id = Guid.Parse("31c6b62e-c6b8-443a-a082-0f7f1fe3a8b4"),
                DomainId = userdomain.Id,
                MenuName = "数据查询",
                Controller = "Query",
                Action = string.Empty,
                MenuLevel = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(menu2);

            var submenu3 = new Menu()
            {
                Id = Guid.Parse("aad53b14-45f3-49de-af05-542f9f7471fb"),
                DomainId = userdomain.Id,
                ParentMenuId = menu2.Id,
                MenuName = "清洁度查询",
                Controller = "Query",
                Action = "CleanRate",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu3);

            var submenu4 = new Menu()
            {
                Id = Guid.Parse("8fb76e4d-6ef2-48cb-8a9b-690c420b4526"),
                DomainId = userdomain.Id,
                ParentMenuId = menu2.Id,
                MenuName = "联动比查询",
                Controller = "Query",
                Action = "LinkageRate",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu4);

            var submenu5 = new Menu()
            {
                Id = Guid.Parse("36998c60-817b-489d-a3c1-bef5b5117a8a"),
                DomainId = userdomain.Id,
                ParentMenuId = menu2.Id,
                MenuName = "去除率查询",
                Controller = "Query",
                Action = "RemovalRate",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu5);

            var submenu6 = new Menu()
            {
                Id = Guid.Parse("85a9b291-9a1d-4d83-afdf-553369b5594d"),
                DomainId = userdomain.Id,
                ParentMenuId = menu2.Id,
                MenuName = "报警查询",
                Controller = "Query",
                Action = "Alarm",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu6);

            var submenu7 = new Menu()
            {
                Id = Guid.Parse("c8cfccf0-b872-4c4b-99b2-a6b871f1cf36"),
                DomainId = userdomain.Id,
                ParentMenuId = menu2.Id,
                MenuName = "历史数据查询",
                Controller = "Query",
                Action = "HistoryData",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu7);

            var submenu8 = new Menu()
            {
                Id = Guid.Parse("d0bd6836-d0c3-4d63-b18b-95da6ffb1785"),
                DomainId = userdomain.Id,
                ParentMenuId = menu2.Id,
                MenuName = "运行时间",
                Controller = "Query",
                Action = "RunningTime",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu8);

            var menu3 = new Menu()
            {
                Id = Guid.Parse("4b1b7f18-3b4c-487c-b99a-f1119ab77c2d"),
                DomainId = userdomain.Id,
                MenuName = "统计分析",
                Controller = "Analysis",
                Action = string.Empty,
                MenuLevel = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(menu3);

            var submenu9 = new Menu()
            {
                Id = Guid.Parse("baa4cfc2-ca73-44f3-a3ca-e7fd4cd64b3e"),
                DomainId = userdomain.Id,
                ParentMenuId = menu3.Id,
                MenuName = "异常数据统计",
                Controller = "Analysis",
                Action = "ExcetionData",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu9);

            var submenu10 = new Menu()
            {
                Id = Guid.Parse("baa4cfc2-ca73-44f3-a3ca-e7fd4cd64b3e"),
                DomainId = userdomain.Id,
                ParentMenuId = menu3.Id,
                MenuName = "运行状态统计",
                Controller = "Analysis",
                Action = "RuningStatus",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu10);

            var submenu11 = new Menu()
            {
                Id = Guid.Parse("aff54122-5f3d-4b82-b474-14c1f0be8798"),
                DomainId = userdomain.Id,
                ParentMenuId = menu3.Id,
                MenuName = "综合报告",
                Controller = "Analysis",
                Action = "GeneralReport",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu11);

            var submenu12 = new Menu()
            {
                Id = Guid.Parse("59654562-ee10-466d-898d-62e02c139799"),
                DomainId = userdomain.Id,
                ParentMenuId = menu3.Id,
                MenuName = "综合对比",
                Controller = "Analysis",
                Action = "GeneralComparison",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu12);

            var submenu13 = new Menu()
            {
                Id = Guid.Parse("e1e0bdf8-4961-47fb-87e9-eb1b4dd4ed9e"),
                DomainId = userdomain.Id,
                ParentMenuId = menu3.Id,
                MenuName = "趋势分析",
                Controller = "Analysis",
                Action = "TrendAnalysis",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu13);

            var submenu14 = new Menu()
            {
                Id = Guid.Parse("396604ce-53ec-4f91-a03b-15ec2282cbeb"),
                DomainId = userdomain.Id,
                ParentMenuId = menu3.Id,
                MenuName = "清洁度分类统计",
                Controller = "Analysis",
                Action = "CleanlinessStatistics",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu14);

            var menu4 = new Menu()
            {
                Id = Guid.Parse("2e6a7a8d-aea2-42c2-b109-ab7dfd4c9b77"),
                DomainId = userdomain.Id,
                MenuName = "设备维护",
                Controller = "Management",
                Action = string.Empty,
                MenuLevel = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(menu4);

            var submenu15 = new Menu()
            {
                Id = Guid.Parse("3be1c2ec-9651-47d3-8790-745ac0f50d3d"),
                DomainId = userdomain.Id,
                ParentMenuId = menu4.Id,
                MenuName = "检修管理",
                Controller = "Management",
                Action = "DeviceMaintenance",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu15);

            var menu5 = new Menu()
            {
                Id = Guid.Parse("67d9a3a7-00f5-468d-9d3f-2cee2e610d3f"),
                DomainId = userdomain.Id,
                MenuName = "基础资料",
                Controller = "Management",
                Action = string.Empty,
                MenuLevel = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(menu5);

            var submenu16 = new Menu()
            {
                Id = Guid.Parse("01623cfa-9217-4965-a208-eaeeccb2ac9a"),
                DomainId = userdomain.Id,
                ParentMenuId = menu5.Id,
                MenuName = "区域管理",
                Controller = "Management",
                Action = "Area",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu16);

            var submenu17 = new Menu()
            {
                Id = Guid.Parse("3ac94c2b-0b3f-4322-b677-4055f1a7cd2b"),
                DomainId = userdomain.Id,
                ParentMenuId = menu5.Id,
                MenuName = "餐饮企业管理",
                Controller = "Management",
                Action = "CateringEnterprise",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu17);

            var submenu18 = new Menu()
            {
                Id = Guid.Parse("11dc58a6-212f-4ec4-bb23-b3f64be3e4fb"),
                DomainId = userdomain.Id,
                ParentMenuId = menu5.Id,
                MenuName = "酒店管理",
                Controller = "Management",
                Action = "Hotel",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu18);

            var submenu19 = new Menu()
            {
                Id = Guid.Parse("cf898c95-e226-4f18-84d9-4bde6bf9f88d"),
                DomainId = userdomain.Id,
                ParentMenuId = menu5.Id,
                MenuName = "设备管理",
                Controller = "Management",
                Action = "Device",
                MenuLevel = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Menus.Add(submenu19);
        }
    }

    public class LampBlackDevelopInitializer : DropCreateDatabaseIfModelChanges<RepositoryDbContext>
    {
        protected override void Seed(RepositoryDbContext dbContext)
        {
            base.Seed(dbContext);
        }
    }

    public class LampBlackProductionInitializer : CreateDatabaseIfNotExists<RepositoryDbContext>
    {
        protected override void Seed(RepositoryDbContext dbContext)
        {
            base.Seed(dbContext);
        }
    }
}
