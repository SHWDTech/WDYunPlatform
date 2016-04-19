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
                ComponentName = "Head",
                ComponentIndex = 0,
                ComponentDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SingleByte
            };

            var cmdtype = new ProtocolStructure
            {
                Id = Guid.Parse("dcdb914f-62ec-42dc-bece-04124cfd61fa"),
                ProtocolId = classic.Id,
                ComponentName = "CmdType",
                ComponentIndex = 1,
                ComponentDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SingleByte
            };

            var cmdbyte = new ProtocolStructure
            {
                Id = Guid.Parse("fa009ac1-8a08-4243-a143-eb7cb8942660"),
                ProtocolId = classic.Id,
                ComponentName = "CmdByte",
                ComponentIndex = 2,
                ComponentDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SingleByte
            };

            var password = new ProtocolStructure()
            {
                Id = Guid.Parse("eddaa794-088a-4cc0-8c8e-b09635ba249a"),
                ProtocolId = classic.Id,
                ComponentName = "Password",
                ComponentIndex = 3,
                ComponentDataLength = 8,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.NumPassword
            };

            var nodeid = new ProtocolStructure()
            {
                Id = Guid.Parse("ff20af2c-8f93-4755-889f-e8943c023e7d"),
                ProtocolId = classic.Id,
                ComponentName = "NodeId",
                ComponentIndex = 4,
                ComponentDataLength = 4,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.NodeId
            };

            var descrip = new ProtocolStructure()
            {
                Id = Guid.Parse("92006a07-3726-4cdc-99a4-7b3fdf7ef03d"),
                ProtocolId = classic.Id,
                ComponentName = "Description",
                ComponentIndex = 5,
                ComponentDataLength = 12,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Description
            };

            var sourceaddr = new ProtocolStructure()
            {
                Id = Guid.Parse("9ccdffd7-7d22-43d1-9185-a58541ce87f8"),
                ProtocolId = classic.Id,
                ComponentName = "SourceAddr",
                ComponentIndex = 6,
                ComponentDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SourceAddr
            };

            var destination = new ProtocolStructure()
            {
                Id = Guid.Parse("20ddc634-6ce6-47cc-82e1-c3df8f68abaa"),
                ProtocolId = classic.Id,
                ComponentName = "DestinationAddr",
                ComponentIndex = 7,
                ComponentDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Destination
            };

            var datalength = new ProtocolStructure()
            {
                Id = Guid.Parse("ad465018-24d3-400d-b7d5-712abf54ceeb"),
                ProtocolId = classic.Id,
                ComponentName = "PayhloadLength",
                ComponentIndex = 8,
                ComponentDataLength = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.DataLength
            };

            var data = new ProtocolStructure()
            {
                Id = Guid.Parse("4cde87be-468c-46cf-a1f9-0172f21761ca"),
                ProtocolId = classic.Id,
                ComponentName = "Data",
                ComponentIndex = 9,
                ComponentDataLength = 0,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Data
            };

            var crc = new ProtocolStructure()
            {
                Id = Guid.Parse("1bd5725a-0408-4c4a-b7ad-f2c92dd830e2"),
                ProtocolId = classic.Id,
                ComponentName = "CrcValue",
                ComponentIndex = 10,
                ComponentDataLength = 2,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Crc
            };

            var tail = new ProtocolStructure()
            {
                Id = Guid.Parse("6aea3080-bb97-4e50-8140-7c31728b1637"),
                ProtocolId = classic.Id,
                ComponentName = "Tail",
                ComponentIndex = 11,
                ComponentDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SingleByte
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

            dbContext.SysConfigs.Add(commandReply);

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

            dbContext.ProtocolCommands.Add(command);

            var device = new Device
            {
                Id = Guid.Parse("ba0ca1dc-0331-4d5a-96e5-49ac20665a13"),
                DeviceTypeId = deviceType.Id,
                DeviceCode = "wohao",
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