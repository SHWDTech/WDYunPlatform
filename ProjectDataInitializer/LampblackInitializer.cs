﻿using System;
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
                RoleName = "SuperAdmin",
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
            commUser.CreateUserId = user.Id;
            commUser.LastUpdateUserId = user.Id;
            commUser.LastUpdateDateTime = DateTime.Now;
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

            var subfieldLampblack = new SysDictionary
            {
                Id = Guid.Parse("cc5729db-167d-4915-bf3c-af4868ea7864"),
                ParentDictionaryId = field.Id,
                ItemName = "Subield",
                ItemKey = "7E0384B37CFM",
                ItemValue = "Lampblack",
                ItemLevel = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true
            };

            dbContext.SysDictionaries.Add(field);
            dbContext.SysDictionaries.Add(subfield);
            dbContext.SysDictionaries.Add(subfieldLampblack);

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

            var lampblackDevicetype = new DeviceType
            {
                Id = Guid.Parse("67c410a8-dfab-477e-9988-c9ff0167ceea"),
                Field = field,
                SubField = subfield,
                Version = "2016-03-17",
                ReleaseDateTime = DateTime.Now,
                DeviceTypeCode = "WD_Lampblack",
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

            var firmlampblack = new Firmware
            {
                Id = Guid.Parse("6cd93a64-c033-44a9-828d-f0f0699b8f12"),
                FirmwareName = "Lampblack_Frim_Version_1.0",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                Protocols = new List<Protocol>()
            };

            dbContext.Firmwares.Add(firma);
            dbContext.Firmwares.Add(firmlampblack);

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

            var lampblackfirmset = new FirmwareSet
            {
                Id = Guid.Parse("234730b6-4c32-467a-9a82-f60e9546c3da"),
                FirmwareSetName = "油烟协议第一版",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true
            };

            firmSet.Firmwares.Add(firma);
            lampblackfirmset.Firmwares.Add(firmlampblack);
            dbContext.FirmwareSets.Add(firmSet);
            dbContext.FirmwareSets.Add(lampblackfirmset);

            dbContext.DeviceTypes.Add(deviceType);
            dbContext.DeviceTypes.Add(lampblackDevicetype);

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

            var lampblackProtocol = new Protocol
            {
                Id = Guid.Parse("969a4897-265c-4d86-b000-e70987817524"),
                FieldId = field.Id,
                SubFieldId = subfield.Id,
                CustomerInfo = "1",
                ProtocolName = "Lampblack",
                ProtocolModule = "Lampblack",
                Version = "Lampblack_Protocol_V1.0.0.0",
                ReleaseDateTime = DateTime.Now,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                CheckType = ProtocolCheckType.Crc16,
                Head = new byte[] {0xAC},
                Tail = new byte[] {0xB1}
            };

            firmlampblack.Protocols.Add(lampblackProtocol);

            var lbhead = new ProtocolStructure
            {
                Id = Guid.Parse("9c547924-c395-4da8-be07-2535b44fb1e2"),
                ProtocolId = lampblackProtocol.Id,
                StructureName = "Head",
                StructureIndex = 0,
                StructureDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SingleByte,
                DefaultBytes = new byte[] { 0xAC }
            };

            var lbcmdtype = new ProtocolStructure
            {
                Id = Guid.Parse("9118bb81-3316-4127-9d91-4d4e37134c6f"),
                ProtocolId = lampblackProtocol.Id,
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

            var lbcmdbyte = new ProtocolStructure
            {
                Id = Guid.Parse("2fb764f2-d088-41f0-b8c5-10d6086d02be"),
                ProtocolId = lampblackProtocol.Id,
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

            var lbpassword = new ProtocolStructure()
            {
                Id = Guid.Parse("417b4fdd-2130-4739-b751-077b0eb040b1"),
                ProtocolId = lampblackProtocol.Id,
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

            var lbnodeid = new ProtocolStructure()
            {
                Id = Guid.Parse("7304e821-3e9b-402c-8e1b-75bdbb51b95a"),
                ProtocolId = lampblackProtocol.Id,
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

            var lbdescrip = new ProtocolStructure()
            {
                Id = Guid.Parse("021ba57c-8987-4a4b-8e1a-376cad575ef8"),
                ProtocolId = lampblackProtocol.Id,
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

            var lbdatasource = new ProtocolStructure()
            {
                Id = Guid.Parse("6b829782-8602-4464-a4ad-1ea53d45426f"),
                ProtocolId = lampblackProtocol.Id,
                StructureName = "DataSource",
                StructureIndex = 6,
                StructureDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.DataSource,
                DefaultBytes = new byte[] { 0x01 }
            };

            var reserved = new ProtocolStructure()
            {
                Id = Guid.Parse("28af7dfb-72aa-4f39-b8e2-60f8d7e75532"),
                ProtocolId = lampblackProtocol.Id,
                StructureName = "Reserved",
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

            var lbdatalength = new ProtocolStructure()
            {
                Id = Guid.Parse("4181699e-5106-4cf8-a32b-947b72b60197"),
                ProtocolId = lampblackProtocol.Id,
                StructureName = "DataLength",
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

            var lbdata = new ProtocolStructure()
            {
                Id = Guid.Parse("afcc3ffc-db70-4841-aa91-ac28aa4b701a"),
                ProtocolId = lampblackProtocol.Id,
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

            var lbcrc = new ProtocolStructure()
            {
                Id = Guid.Parse("dee88fe7-0285-4e36-96c2-4c17ce007a6f"),
                ProtocolId = lampblackProtocol.Id,
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

            var lbtail = new ProtocolStructure()
            {
                Id = Guid.Parse("c2478dc2-5736-4bbc-8f51-d396fab81454"),
                ProtocolId = lampblackProtocol.Id,
                StructureName = "Tail",
                StructureIndex = 11,
                StructureDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.SingleByte,
                DefaultBytes = new byte[] { 0xB1 }
            };

            dbContext.ProtocolStructures.Add(lbhead);
            dbContext.ProtocolStructures.Add(lbcmdtype);
            dbContext.ProtocolStructures.Add(lbcmdbyte);
            dbContext.ProtocolStructures.Add(lbpassword);
            dbContext.ProtocolStructures.Add(lbnodeid);
            dbContext.ProtocolStructures.Add(lbdescrip);
            dbContext.ProtocolStructures.Add(lbdatasource);
            dbContext.ProtocolStructures.Add(reserved);
            dbContext.ProtocolStructures.Add(lbdatalength);
            dbContext.ProtocolStructures.Add(lbdata);
            dbContext.ProtocolStructures.Add(lbcrc);
            dbContext.ProtocolStructures.Add(lbtail);

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

            var commandReplyLampblack = new SysConfig()
            {
                Id = Guid.Parse("80918814-68f6-4c4e-932a-69277eb8ce9e"),
                SysConfigName = ProtocolDeliveryParam.Lampblack,
                SysConfigType = SysConfigType.ProtocolDeliveryParam,
                SysConfigValue = ProtocolDeliveryParam.Lampblack,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var lampblackAlarm = new SysConfig()
            {
                Id = Guid.Parse("a6f21730-45c7-49d5-8757-b36e972a7b5c"),
                SysConfigName = ProtocolDeliveryParam.LampblackAlram,
                SysConfigType = SysConfigType.ProtocolDeliveryParam,
                SysConfigValue = ProtocolDeliveryParam.LampblackAlram,
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
            dbContext.SysConfigs.Add(commandReplyLampblack);
            dbContext.SysConfigs.Add(lampblackAlarm);

            var commandDataA = new CommandData()
            {
                Id = Guid.Parse("46225dc9-ffe2-43af-bba4-7b45bbb55af2"),
                DataIndex = 0,
                DataLength = 2,
                DataName = ProtocolDataName.DataValidFlag,
                DataConvertType = ProtocolDataType.TwoBytesToUShort,
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
                DataName = ProtocolDataName.Pm25,
                DataConvertType = ProtocolDataType.FourBytesToUInt32,
                DataValueType = DataValueType.Double,
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
                DataName = ProtocolDataName.Pm100,
                DataConvertType = ProtocolDataType.FourBytesToUInt32,
                DataValueType = DataValueType.Double,
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
                DataName = ProtocolDataName.Cpm,
                DataConvertType = ProtocolDataType.FourBytesToUInt32,
                DataValueType = DataValueType.Double,
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
                DataName = ProtocolDataName.Noise,
                DataConvertType = ProtocolDataType.TwoBytesToDoubleSeparate,
                DataValueType = DataValueType.Double,
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
                DataName = ProtocolDataName.WindDirection,
                DataConvertType = ProtocolDataType.ThreeBytesToUShort,
                DataValueType = DataValueType.Double,
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
                DataName = ProtocolDataName.WindSpeed,
                DataConvertType = ProtocolDataType.ThreeBytesToUShort,
                DataValueType = DataValueType.Double,
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
                DataName = ProtocolDataName.Temperature,
                DataConvertType = ProtocolDataType.TwoBytesToDoubleSeparate,
                DataValueType = DataValueType.Double,
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
                DataName = ProtocolDataName.Humidity,
                DataConvertType = ProtocolDataType.TwoBytesToDoubleSeparate,
                DataValueType = DataValueType.Double,
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
                DataName = ProtocolDataName.Voc,
                DataConvertType = ProtocolDataType.FourBytesToUInt32,
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
                SendBytesLength = 0,
                ReceiveBytesLength = 0,
                ReceiceMaxBytesLength = 0,
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
                SendBytesLength = 0,
                ReceiveBytesLength = 30,
                ReceiceMaxBytesLength = 30,
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
                SendBytesLength = 0,
                ReceiveBytesLength = 0,
                ReceiceMaxBytesLength = 0,
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
                ProtocolName = "LampblackBestLand",
                ProtocolModule = "LampblackBestLand",
                Version = "Lampblack_BestLand_Protocol_V0001",
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

            var lbHreatBeat = new ProtocolCommand
            {
                Id = Guid.Parse("b1bfa04d-531a-40ee-a14c-098977a17f23"),
                CommandTypeCode = new byte[] { 0xF1 },
                CommandCode = new byte[] { 0x01 },
                SendBytesLength = 0,
                ReceiveBytesLength = 0,
                ReceiceMaxBytesLength = 0,
                CommandCategory = CommandCategory.SystemCommunication,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig> { commandReply },
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var lbReadVersion = new ProtocolCommand
            {
                Id = Guid.Parse("5835a807-c425-4332-aae4-94243b6dbe87"),
                CommandTypeCode = new byte[] { 0xF1 },
                CommandCode = new byte[] { 0x02 },
                SendBytesLength = 0,
                ReceiveBytesLength = 8,
                ReceiceMaxBytesLength = 8,
                CommandCategory = CommandCategory.SystemCommunication,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadNode = new ProtocolCommand
            {
                Id = Guid.Parse("5875a293-202c-4cd6-9796-d3528588625f"),
                CommandTypeCode = new byte[] { 0xF1 },
                CommandCode = new byte[] { 0x03 },
                SendBytesLength = 0,
                ReceiveBytesLength = 16,
                ReceiceMaxBytesLength = 16,
                CommandCategory = CommandCategory.SystemCommunication,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReset = new ProtocolCommand
            {
                Id = Guid.Parse("7e2ab3fb-e1b9-4d6f-b482-87b341ab8d46"),
                CommandTypeCode = new byte[] { 0xF1 },
                CommandCode = new byte[] { 0x04 },
                SendBytesLength = 16,
                ReceiveBytesLength = 0,
                ReceiceMaxBytesLength = 0,
                CommandCategory = CommandCategory.SystemCommunication,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbTimeAdjust = new ProtocolCommand
            {
                Id = Guid.Parse("aa5538eb-860a-4d99-aae7-38691a2f77a0"),
                CommandTypeCode = new byte[] { 0xF1 },
                CommandCode = new byte[] { 0x05 },
                SendBytesLength = 8,
                ReceiveBytesLength = 8,
                ReceiceMaxBytesLength = 8,
                CommandCategory = CommandCategory.SystemCommunication,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbSetDescribe = new ProtocolCommand
            {
                Id = Guid.Parse("7437661f-7866-449a-9163-599fea928425"),
                CommandTypeCode = new byte[] { 0xF2 },
                CommandCode = new byte[] { 0x01 },
                SendBytesLength = 32,
                ReceiveBytesLength = 32,
                ReceiceMaxBytesLength = 32,
                CommandCategory = CommandCategory.ModuleConfig,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadDescribe = new ProtocolCommand
            {
                Id = Guid.Parse("6ef01e11-d8c7-418b-8be7-9acd9831f2e7"),
                CommandTypeCode = new byte[] { 0xF2 },
                CommandCode = new byte[] { 0x02 },
                SendBytesLength = 32,
                ReceiveBytesLength = 32,
                ReceiceMaxBytesLength = 32,
                CommandCategory = CommandCategory.ModuleConfig,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbSetNodeId = new ProtocolCommand
            {
                Id = Guid.Parse("631fe519-26aa-4f1e-b305-8409f028e3d7"),
                CommandTypeCode = new byte[] { 0xF2 },
                CommandCode = new byte[] { 0x03 },
                SendBytesLength = 8,
                ReceiveBytesLength = 8,
                ReceiceMaxBytesLength = 8,
                CommandCategory = CommandCategory.ModuleConfig,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadNodeId = new ProtocolCommand
            {
                Id = Guid.Parse("d6116b59-966f-4e10-8989-3e09347b49a2"),
                CommandTypeCode = new byte[] { 0xF2 },
                CommandCode = new byte[] { 0x04 },
                SendBytesLength = 8,
                ReceiveBytesLength = 8,
                ReceiceMaxBytesLength = 8,
                CommandCategory = CommandCategory.ModuleConfig,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbSetConnAddr = new ProtocolCommand
            {
                Id = Guid.Parse("4ff1a46e-4367-46f2-b563-19de7f34bac7"),
                CommandTypeCode = new byte[] { 0xF2 },
                CommandCode = new byte[] { 0x05 },
                SendBytesLength = 0,
                ReceiveBytesLength = 0,
                ReceiceMaxBytesLength = 0,
                CommandCategory = CommandCategory.ModuleConfig,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadConnAddr = new ProtocolCommand
            {
                Id = Guid.Parse("d3bfd547-42c3-42eb-b725-690659e42cc0"),
                CommandTypeCode = new byte[] { 0xF2 },
                CommandCode = new byte[] { 0x06 },
                SendBytesLength = 1,
                ReceiveBytesLength = 0,
                ReceiceMaxBytesLength = 131,
                CommandCategory = CommandCategory.ModuleConfig,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbSetAutoReport = new ProtocolCommand
            {
                Id = Guid.Parse("03d26500-a746-43eb-99d0-2a2caf7420c7"),
                CommandTypeCode = new byte[] { 0xF2 },
                CommandCode = new byte[] { 0x07 },
                SendBytesLength = 4,
                ReceiveBytesLength = 4,
                ReceiceMaxBytesLength = 4,
                CommandCategory = CommandCategory.ModuleConfig,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadAutoReport = new ProtocolCommand
            {
                Id = Guid.Parse("5ca75e4e-b0d8-484d-8ca1-2ea4c7c8ede6"),
                CommandTypeCode = new byte[] { 0xF2 },
                CommandCode = new byte[] { 0x08 },
                SendBytesLength = 0,
                ReceiveBytesLength = 4,
                ReceiceMaxBytesLength = 4,
                CommandCategory = CommandCategory.ModuleConfig,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadCleanerStatus = new ProtocolCommand
            {
                Id = Guid.Parse("bbf23570-3df9-4e3d-9b1b-dacfdce13d78"),
                CommandTypeCode = new byte[] { 0xF3 },
                CommandCode = new byte[] { 0x02 },
                SendBytesLength = 1,
                ReceiveBytesLength = 2,
                ReceiceMaxBytesLength = 2,
                CommandCategory = CommandCategory.DeviceControl,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadCleanerCurrent = new ProtocolCommand
            {
                Id = Guid.Parse("c40e2ccf-5db4-433a-a63c-1bdcdb5533c9"),
                CommandTypeCode = new byte[] { 0xF3 },
                CommandCode = new byte[] { 0x04 },
                SendBytesLength = 1,
                ReceiveBytesLength = 5,
                ReceiceMaxBytesLength = 5,
                CommandCategory = CommandCategory.DeviceControl,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadFanStatus = new ProtocolCommand
            {
                Id = Guid.Parse("97e347bf-971d-4865-8835-83e30d3c9cd1"),
                CommandTypeCode = new byte[] { 0xF3 },
                CommandCode = new byte[] { 0x06 },
                SendBytesLength = 1,
                ReceiveBytesLength = 2,
                ReceiceMaxBytesLength = 2,
                CommandCategory = CommandCategory.DeviceControl,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadFanCurrent = new ProtocolCommand
            {
                Id = Guid.Parse("c16ae7dd-0c46-4364-9a51-6b06cf00d396"),
                CommandTypeCode = new byte[] { 0xF3 },
                CommandCode = new byte[] { 0x08 },
                SendBytesLength = 1,
                ReceiveBytesLength = 5,
                ReceiceMaxBytesLength = 5,
                CommandCategory = CommandCategory.DeviceControl,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadInLampblack = new ProtocolCommand
            {
                Id = Guid.Parse("ef12e6e2-1305-4160-88fe-629474b9bbe6"),
                CommandTypeCode = new byte[] { 0xF3 },
                CommandCode = new byte[] { 0x0A },
                SendBytesLength = 1,
                ReceiveBytesLength = 5,
                ReceiceMaxBytesLength = 5,
                CommandCategory = CommandCategory.DeviceControl,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadOutLampblack = new ProtocolCommand
            {
                Id = Guid.Parse("64183d55-9c5a-4036-8c01-3245a4c6330e"),
                CommandTypeCode = new byte[] { 0xF3 },
                CommandCode = new byte[] { 0x0C },
                SendBytesLength = 1,
                ReceiveBytesLength = 5,
                ReceiceMaxBytesLength = 5,
                CommandCategory = CommandCategory.DeviceControl,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadAlarm = new ProtocolCommand
            {
                Id = Guid.Parse("7fb4417b-829e-45df-bf27-3a62446b59bb"),
                CommandTypeCode = new byte[] { 0xF3 },
                CommandCode = new byte[] { 0x0E },
                SendBytesLength = 1,
                ReceiveBytesLength = 3,
                ReceiceMaxBytesLength = 3,
                CommandCategory = CommandCategory.DeviceControl,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbReadDeviceStatus = new ProtocolCommand
            {
                Id = Guid.Parse("5797e815-cf6a-4fc0-b9c8-3eaf53422827"),
                CommandTypeCode = new byte[] { 0xF3 },
                CommandCode = new byte[] { 0x0E },
                SendBytesLength = 0,
                ReceiveBytesLength = 2,
                ReceiceMaxBytesLength = 2,
                CommandCategory = CommandCategory.DeviceControl,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Order,
                CommandDeliverParamConfigs = new List<SysConfig>(),
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            var lbAutoReport = new ProtocolCommand
            {
                Id = Guid.Parse("320fc79d-ff52-4076-a121-e0c71fcff471"),
                CommandTypeCode = new byte[] { 0xF4 },
                CommandCode = new byte[] { 0x02 },
                SendBytesLength = 0,
                ReceiveBytesLength = 0,
                ReceiceMaxBytesLength = 1024,
                CommandCategory = CommandCategory.TimingAutoReport,
                ProtocolId = lampblackProtocol.Id,
                DataOrderType = DataOrderType.Random,
                CommandDeliverParamConfigs = new List<SysConfig>() { commandReplyLampblack, lampblackAlarm },
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                CommandDatas = new List<CommandData>()
            };

            dbContext.ProtocolCommands.Add(lbHreatBeat);
            dbContext.ProtocolCommands.Add(lbReadVersion);
            dbContext.ProtocolCommands.Add(lbReadNode);
            dbContext.ProtocolCommands.Add(lbReset);
            dbContext.ProtocolCommands.Add(lbTimeAdjust);
            dbContext.ProtocolCommands.Add(lbSetDescribe);
            dbContext.ProtocolCommands.Add(lbReadDescribe);
            dbContext.ProtocolCommands.Add(lbSetNodeId);
            dbContext.ProtocolCommands.Add(lbReadNodeId);
            dbContext.ProtocolCommands.Add(lbSetConnAddr);
            dbContext.ProtocolCommands.Add(lbReadConnAddr);
            dbContext.ProtocolCommands.Add(lbSetAutoReport);
            dbContext.ProtocolCommands.Add(lbReadAutoReport);
            dbContext.ProtocolCommands.Add(lbReadCleanerStatus);
            dbContext.ProtocolCommands.Add(lbReadCleanerCurrent);
            dbContext.ProtocolCommands.Add(lbReadFanStatus);
            dbContext.ProtocolCommands.Add(lbReadFanCurrent);
            dbContext.ProtocolCommands.Add(lbReadInLampblack);
            dbContext.ProtocolCommands.Add(lbReadOutLampblack);
            dbContext.ProtocolCommands.Add(lbReadAlarm);
            dbContext.ProtocolCommands.Add(lbReadDeviceStatus);
            dbContext.ProtocolCommands.Add(lbAutoReport);

            var lmCommandA = new CommandData()
            {
                Id = Guid.Parse("15802959-d25b-42ad-be50-5b48dce4039a"),
                DataIndex = 0,
                DataLength = 1,
                DataName = ProtocolDataName.CleanerSwitch,
                DataConvertType = ProtocolDataType.ByteToBoolean,
                DataValueType = DataValueType.Boolean,
                DataFlag = 0x01,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var lmCommandB = new CommandData()
            {
                Id = Guid.Parse("eee9ec55-7e84-4176-bb90-c13962352bc2"),
                DataIndex = 0,
                DataLength = 4,
                DataName = ProtocolDataName.CleanerCurrent,
                DataConvertType = ProtocolDataType.FourBytesToUInt32,
                DataValueType = DataValueType.Double,
                DataFlag = 0x02,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var lmCommandC = new CommandData()
            {
                Id = Guid.Parse("adce87e7-aef2-4548-aa1e-fb435b72834f"),
                DataIndex = 0,
                DataLength = 1,
                DataName = ProtocolDataName.FanSwitch,
                DataConvertType = ProtocolDataType.ByteToBoolean,
                DataValueType = DataValueType.Boolean,
                DataFlag = 0x03,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var lmCommandD = new CommandData()
            {
                Id = Guid.Parse("f15b955e-af42-44a5-a547-e1e2e7cdac1d"),
                DataIndex = 0,
                DataLength = 4,
                DataName = ProtocolDataName.LampblackInCon,
                DataConvertType = ProtocolDataType.FourBytesToUInt32,
                DataValueType = DataValueType.Double,
                DataFlag = 0x05,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var lmCommandE = new CommandData()
            {
                Id = Guid.Parse("d0e478ae-836a-45eb-ba93-32fdf1cbee61"),
                DataIndex = 0,
                DataLength = 4,
                DataName = ProtocolDataName.LampblackOutCon,
                DataConvertType = ProtocolDataType.FourBytesToUInt32,
                DataValueType = DataValueType.Double,
                DataFlag = 0x06,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var lmCommandF = new CommandData()
            {
                Id = Guid.Parse("01323f2c-70c9-4073-a58c-77f10c819f9c"),
                DataIndex = 0,
                DataLength = 4,
                DataName = ProtocolDataName.FanCurrent,
                DataConvertType = ProtocolDataType.FourBytesToUInt32,
                DataValueType = DataValueType.Double,
                DataFlag = 0x04,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var lmCommandG = new CommandData()
            {
                Id = Guid.Parse("4b2d3a2e-6392-4503-bec2-f60f2457135c"),
                DataIndex = 0,
                DataLength = 2,
                DataName = ProtocolDataName.LampblackException,
                DataConvertType = ProtocolDataType.TwoBytesToUShort,
                DataValueType = DataValueType.Integer,
                CreateDateTime = DateTime.Now,
                DataFlag = 0x07,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            var lmCommandH = new CommandData()
            {
                Id = Guid.Parse("b80862b9-e70a-4d00-aaa7-abd27651566a"),
                DataIndex = 0,
                DataLength = 2,
                DataName = ProtocolDataName.DeviceStatus,
                DataConvertType = ProtocolDataType.TwoBytesToUShort,
                DataValueType = DataValueType.Integer,
                DataFlag = 0x08,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            lbAutoReport.CommandDatas.Add(lmCommandA);
            lbAutoReport.CommandDatas.Add(lmCommandB);
            lbAutoReport.CommandDatas.Add(lmCommandC);
            lbAutoReport.CommandDatas.Add(lmCommandD);
            lbAutoReport.CommandDatas.Add(lmCommandE);
            lbAutoReport.CommandDatas.Add(lmCommandF);
            lbAutoReport.CommandDatas.Add(lmCommandG);
            lbAutoReport.CommandDatas.Add(lmCommandH);

            var userdomain = new Domain
            {
                Id = Guid.Parse("9560e11b-1a70-456b-b201-6d0fa412bad2"),
                DomainName = "上海驿鼎机电工程有限公司",
                DomianType = DomainType.UserDomain,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                DomainStatus = DomainStatus.Enabled,
                IsEnabled = true
            };

            var qd = new WdUser
            {
                Id = Guid.Parse("828ac8fb-5692-499b-b893-18d529740cb4"),
                UserName = "Admin",
                LoginName = "Admin",
                Password = "bced6fd149cfcdb85741768da12e41c6", //admin
                Email = "shweidongtech@126.com",
                Telephone = "18679361687",
                Status = UserStatus.Enabled,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                DomainId = userdomain.Id,
                Roles = new List<WdRole>(),
                IsEnabled = true
            };

            adminRole.Users.Add(qd);

            dbContext.SysDomains.Add(userdomain);
            dbContext.Users.Add(qd);

            var menuPermission1 = new Permission()
            {
                Id = Guid.Parse("c3ac6551-2d34-445d-9bd0-89983518e1ac"),
                PermissionName = "Monitor",
                PermissionDisplayName = "在线监测",
                DomainId = userdomain.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(menuPermission1);

            var menu1 = new Module()
            {
                Id = Guid.Parse("38f7dd0c-9210-4103-92ef-0a32a61e44ee"),
                DomainId = userdomain.Id,
                ModuleName = "在线监测",
                Controller = "Monitor",
                IconString = "glyphicon glyphicon-facetime-video",
                Action = string.Empty,
                ModuleLevel = 1,
                ModuleIndex = 1000,
                IsMenu = true,
                PermissionId = menuPermission1.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(menu1);

            var submenuPermission1 = new Permission()
            {
                Id = Guid.Parse("7ce6e9d1-4a10-4f8f-bde0-49ac1d325c7f"),
                DomainId = userdomain.Id,
                PermissionName = "Map",
                PermissionDisplayName = "地图监测",
                ParentPermissionId = menuPermission1.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission1);

            var submenu1 = new Module()
            {
                Id = Guid.Parse("7bd18c1a-c73c-4626-ab70-84ce8ac59673"),
                DomainId = userdomain.Id,
                ParentModuleId = menu1.Id,
                ModuleName = "地图监测",
                Controller = "Monitor",
                Action = "Map",
                ModuleLevel = 2,
                ModuleIndex = 2000,
                IsMenu = true,
                PermissionId = submenuPermission1.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu1);

            var submenuPermission2 = new Permission()
            {
                Id = Guid.Parse("20920372-3c41-4b94-9038-49a53fbd85bf"),
                DomainId = userdomain.Id,
                PermissionName = "Actual",
                PermissionDisplayName = "实时数据监测",
                ParentPermissionId = menuPermission1.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission2);

            var submenu2 = new Module()
            {
                Id = Guid.Parse("fc7753a0-6ef0-45cf-98fd-81e209acca0f"),
                DomainId = userdomain.Id,
                ParentModuleId = menu1.Id,
                ModuleName = "实时数据监测",
                Controller = "Monitor",
                Action = "Actual",
                PermissionId = submenuPermission2.Id,
                ModuleLevel = 2,
                ModuleIndex = 2001,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu2);

            var menuPermission2 = new Permission()
            {
                Id = Guid.Parse("4272ff65-4393-4d8e-bdbe-3de87a4e2624"),
                DomainId = userdomain.Id,
                PermissionName = "Query",
                PermissionDisplayName = "数据查询",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(menuPermission2);

            var menu2 = new Module()
            {
                Id = Guid.Parse("31c6b62e-c6b8-443a-a082-0f7f1fe3a8b4"),
                DomainId = userdomain.Id,
                ModuleName = "数据查询",
                IconString = "glyphicon glyphicon-search",
                Controller = "Query",
                Action = string.Empty,
                PermissionId = menuPermission2.Id,
                ModuleLevel = 1,
                ModuleIndex = 1001,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(menu2);

            var submenuPermission3 = new Permission()
            {
                Id = Guid.Parse("ac1ff757-38ab-4a85-9615-04623f1ae534"),
                DomainId = userdomain.Id,
                PermissionName = "CleanRate",
                PermissionDisplayName = "清洁度查询",
                ParentPermissionId = menuPermission2.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission3);

            var submenu3 = new Module()
            {
                Id = Guid.Parse("aad53b14-45f3-49de-af05-542f9f7471fb"),
                DomainId = userdomain.Id,
                ParentModuleId = menu2.Id,
                ModuleName = "清洁度查询",
                Controller = "Query",
                Action = "CleanRate",
                PermissionId = submenuPermission3.Id,
                ModuleLevel = 2,
                ModuleIndex = 2002,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu3);

            var submenuPermission4 = new Permission()
            {
                Id = Guid.Parse("d560a920-87b4-4bd0-b297-01271db1a6d7"),
                DomainId = userdomain.Id,
                PermissionName = "LinkageRate",
                PermissionDisplayName = "联动比查询",
                ParentPermissionId = menuPermission2.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission4);

            var submenu4 = new Module()
            {
                Id = Guid.Parse("8fb76e4d-6ef2-48cb-8a9b-690c420b4526"),
                DomainId = userdomain.Id,
                ParentModuleId = menu2.Id,
                ModuleName = "联动比查询",
                Controller = "Query",
                Action = "LinkageRate",
                PermissionId = submenuPermission4.Id,
                ModuleLevel = 2,
                ModuleIndex = 2003,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu4);

            var submenuPermission5 = new Permission()
            {
                Id = Guid.Parse("b14be3d9-df37-42a0-ba18-12c6ff073fa1"),
                DomainId = userdomain.Id,
                PermissionName = "RemovalRate",
                PermissionDisplayName = "去除率查询",
                ParentPermissionId = menuPermission2.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission5);

            var submenu5 = new Module()
            {
                Id = Guid.Parse("36998c60-817b-489d-a3c1-bef5b5117a8a"),
                DomainId = userdomain.Id,
                ParentModuleId = menu2.Id,
                ModuleName = "去除率查询",
                Controller = "Query",
                Action = "RemovalRate",
                PermissionId = submenuPermission5.Id,
                ModuleLevel = 2,
                ModuleIndex = 2004,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                IsEnabled = false
            };

            dbContext.Modules.Add(submenu5);

            var submenuPermission6 = new Permission()
            {
                Id = Guid.Parse("532a61dd-7646-4da4-98c3-a99a5613f4b5"),
                DomainId = userdomain.Id,
                PermissionName = "Alarm",
                PermissionDisplayName = "报警查询",
                ParentPermissionId = menuPermission2.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission6);

            var submenu6 = new Module()
            {
                Id = Guid.Parse("85a9b291-9a1d-4d83-afdf-553369b5594d"),
                DomainId = userdomain.Id,
                ParentModuleId = menu2.Id,
                ModuleName = "报警查询",
                Controller = "Query",
                Action = "Alarm",
                ModuleLevel = 2,
                ModuleIndex = 2005,
                IsMenu = true,
                PermissionId = submenuPermission6.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu6);

            var submenuPermission7 = new Permission()
            {
                Id = Guid.Parse("31d560a1-377f-495a-8000-31e2bd8e41eb"),
                DomainId = userdomain.Id,
                PermissionName = "HistoryData",
                PermissionDisplayName = "历史数据查询",
                ParentPermissionId = menuPermission2.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission7);

            var submenu7 = new Module()
            {
                Id = Guid.Parse("c8cfccf0-b872-4c4b-99b2-a6b871f1cf36"),
                DomainId = userdomain.Id,
                ParentModuleId = menu2.Id,
                ModuleName = "历史数据查询",
                Controller = "Query",
                Action = "HistoryData",
                PermissionId = submenuPermission7.Id,
                ModuleLevel = 2,
                ModuleIndex = 2006,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu7);

            var submenuPermission8 = new Permission()
            {
                Id = Guid.Parse("4ca5c010-b3e9-40bb-91bb-5da47bf65e70"),
                DomainId = userdomain.Id,
                PermissionName = "RunningTime",
                PermissionDisplayName = "运行时间",
                ParentPermissionId = menuPermission2.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission8);

            var submenu8 = new Module()
            {
                Id = Guid.Parse("d0bd6836-d0c3-4d63-b18b-95da6ffb1785"),
                DomainId = userdomain.Id,
                ParentModuleId = menu2.Id,
                ModuleName = "运行时间",
                Controller = "Query",
                Action = "RunningTime",
                ModuleLevel = 2,
                ModuleIndex = 2007,
                IsMenu = true,
                PermissionId = submenuPermission8.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu8);

            var menuPermission3 = new Permission()
            {
                Id = Guid.Parse("6c6923eb-1b2b-4196-a102-1bb7512d96a7"),
                DomainId = userdomain.Id,
                PermissionName = "Analysis",
                PermissionDisplayName = "统计分析",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(menuPermission3);

            var menu3 = new Module()
            {
                Id = Guid.Parse("4b1b7f18-3b4c-487c-b99a-f1119ab77c2d"),
                DomainId = userdomain.Id,
                ModuleName = "统计分析",
                IconString = "glyphicon glyphicon-picture",
                Controller = "Analysis",
                Action = string.Empty,
                ModuleLevel = 1,
                ModuleIndex = 1002,
                IsMenu = true,
                PermissionId = menuPermission3.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(menu3);

            var submenuPermission9 = new Permission()
            {
                Id = Guid.Parse("22ac589e-8355-42cd-bf71-3c119fbb3b9f"),
                DomainId = userdomain.Id,
                PermissionName = "ExceptionData",
                PermissionDisplayName = "异常数据统计",
                ParentPermissionId = menuPermission3.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission9);

            var submenu9 = new Module()
            {
                Id = Guid.Parse("baa4cfc2-ca73-44f3-a3ca-e7fd4cd64b3e"),
                DomainId = userdomain.Id,
                ParentModuleId = menu3.Id,
                ModuleName = "异常数据统计",
                Controller = "Analysis",
                Action = "ExceptionData",
                ModuleLevel = 2,
                ModuleIndex = 2008,
                IsMenu = true,
                PermissionId = submenuPermission9.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                IsEnabled = false
            };

            dbContext.Modules.Add(submenu9);

            var submenuPermission10 = new Permission()
            {
                Id = Guid.Parse("a4a36255-1b22-4068-850c-2bb16c574fc8"),
                DomainId = userdomain.Id,
                PermissionName = "RunningStatus",
                PermissionDisplayName = "运行状态统计",
                ParentPermissionId = menuPermission3.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission10);

            var submenu10 = new Module()
            {
                Id = Guid.Parse("70a4d091-26c4-4edc-80ba-63708998513f"),
                DomainId = userdomain.Id,
                ParentModuleId = menu3.Id,
                ModuleName = "运行状态统计",
                Controller = "Analysis",
                Action = "RunningStatus",
                ModuleLevel = 2,
                ModuleIndex = 2009,
                IsMenu = true,
                PermissionId = submenuPermission10.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                IsEnabled = false
            };

            dbContext.Modules.Add(submenu10);

            var submenuPermission11 = new Permission()
            {
                Id = Guid.Parse("8de6c713-8686-4809-a061-36ddafbe30fe"),
                DomainId = userdomain.Id,
                PermissionName = "GeneralReport",
                PermissionDisplayName = "综合报告",
                ParentPermissionId = menuPermission3.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission11);

            var submenu11 = new Module()
            {
                Id = Guid.Parse("aff54122-5f3d-4b82-b474-14c1f0be8798"),
                DomainId = userdomain.Id,
                ParentModuleId = menu3.Id,
                ModuleName = "综合报告",
                Controller = "Analysis",
                Action = "GeneralReport",
                ModuleLevel = 2,
                ModuleIndex = 2010,
                IsMenu = true,
                PermissionId = submenuPermission11.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu11);

            var submenuPermission12 = new Permission()
            {
                Id = Guid.Parse("0b144420-d539-4812-958a-79507dd50811"),
                DomainId = userdomain.Id,
                PermissionName = "GeneralComparison",
                PermissionDisplayName = "综合对比",
                ParentPermissionId = menuPermission3.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission12);

            var submenu12 = new Module()
            {
                Id = Guid.Parse("59654562-ee10-466d-898d-62e02c139799"),
                DomainId = userdomain.Id,
                ParentModuleId = menu3.Id,
                ModuleName = "综合对比",
                Controller = "Analysis",
                Action = "GeneralComparison",
                ModuleLevel = 2,
                ModuleIndex = 2011,
                IsMenu = true,
                PermissionId = submenuPermission12.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu12);

            var submenuPermission13 = new Permission()
            {
                Id = Guid.Parse("67b72c18-744d-4439-bcf4-f98b37b4ae03"),
                DomainId = userdomain.Id,
                PermissionName = "TrendAnalysis",
                PermissionDisplayName = "趋势分析",
                ParentPermissionId = menuPermission3.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission13);

            var submenu13 = new Module()
            {
                Id = Guid.Parse("e1e0bdf8-4961-47fb-87e9-eb1b4dd4ed9e"),
                DomainId = userdomain.Id,
                ParentModuleId = menu3.Id,
                ModuleName = "趋势分析",
                Controller = "Analysis",
                Action = "TrendAnalysis",
                ModuleLevel = 2,
                ModuleIndex = 2012,
                IsMenu = true,
                PermissionId = submenuPermission13.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu13);

            var submenuPermission14 = new Permission()
            {
                Id = Guid.Parse("1b716c90-bcd6-4fdb-8d5a-fd3e3419dba7"),
                DomainId = userdomain.Id,
                PermissionName = "CleanlinessStatistics",
                PermissionDisplayName = "清洁度分类统计",
                ParentPermissionId = menuPermission3.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission14);

            var submenu14 = new Module()
            {
                Id = Guid.Parse("396604ce-53ec-4f91-a03b-15ec2282cbeb"),
                DomainId = userdomain.Id,
                ParentModuleId = menu3.Id,
                ModuleName = "清洁度分类统计",
                Controller = "Analysis",
                Action = "CleanlinessStatistics",
                ModuleLevel = 2,
                ModuleIndex = 2013,
                IsMenu = true,
                PermissionId = submenuPermission14.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu14);

            var menuPermission4 = new Permission()
            {
                Id = Guid.Parse("6a023746-295a-4e1a-b4b9-70899f23e00f"),
                DomainId = userdomain.Id,
                PermissionName = "Management",
                PermissionDisplayName = "基础资料",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(menuPermission4);

            var menu4 = new Module()
            {
                Id = Guid.Parse("2e6a7a8d-aea2-42c2-b109-ab7dfd4c9b77"),
                DomainId = userdomain.Id,
                ModuleName = "设备维护",
                IconString = "glyphicon glyphicon-wrench",
                Controller = "Management",
                Action = string.Empty,
                ModuleLevel = 1,
                ModuleIndex = 1003,
                IsMenu = true,
                PermissionId = menuPermission4.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(menu4);

            var submenuPermission15 = new Permission()
            {
                Id = Guid.Parse("fffc7ffb-8e8e-4911-b11a-c196e1a75eb5"),
                DomainId = userdomain.Id,
                PermissionName = "DeviceMaintenance",
                PermissionDisplayName = "检修管理",
                ParentPermissionId = menuPermission4.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission15);

            var submenu15 = new Module()
            {
                Id = Guid.Parse("3be1c2ec-9651-47d3-8790-745ac0f50d3d"),
                DomainId = userdomain.Id,
                ParentModuleId = menu4.Id,
                ModuleName = "检修管理",
                Controller = "Management",
                Action = "DeviceMaintenance",
                ModuleLevel = 2,
                ModuleIndex = 2014,
                IsMenu = true,
                PermissionId = submenuPermission15.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu15);

            var menu5 = new Module()
            {
                Id = Guid.Parse("67d9a3a7-00f5-468d-9d3f-2cee2e610d3f"),
                DomainId = userdomain.Id,
                ModuleName = "基础资料",
                IconString = "glyphicon glyphicon-file",
                Controller = "Management",
                Action = string.Empty,
                ModuleLevel = 1,
                ModuleIndex = 1004,
                IsMenu = true,
                PermissionId = menuPermission4.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(menu5);

            var submenuPermission16 = new Permission()
            {
                Id = Guid.Parse("f9660af0-a03d-4303-b174-2e5a35cfbe5e"),
                DomainId = userdomain.Id,
                PermissionName = "Area",
                PermissionDisplayName = "区域管理",
                ParentPermissionId = menuPermission4.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission16);

            var submenu16 = new Module()
            {
                Id = Guid.Parse("01623cfa-9217-4965-a208-eaeeccb2ac9a"),
                DomainId = userdomain.Id,
                ParentModuleId = menu5.Id,
                ModuleName = "区域管理",
                Controller = "Management",
                Action = "Area",
                ModuleLevel = 2,
                ModuleIndex = 2015,
                IsMenu = true,
                PermissionId = submenuPermission16.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu16);

            var submenuPermission17 = new Permission()
            {
                Id = Guid.Parse("ff8745cc-c333-4444-925d-f896d2c1a5aa"),
                DomainId = userdomain.Id,
                PermissionName = "CateringEnterprise",
                PermissionDisplayName = "餐饮企业管理",
                ParentPermissionId = menuPermission4.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission17);

            var submenu17 = new Module()
            {
                Id = Guid.Parse("3ac94c2b-0b3f-4322-b677-4055f1a7cd2b"),
                DomainId = userdomain.Id,
                ParentModuleId = menu5.Id,
                PermissionId = submenuPermission17.Id,
                ModuleName = "餐饮企业管理",
                Controller = "Management",
                Action = "CateringEnterprise",
                ModuleLevel = 2,
                ModuleIndex = 2016,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu17);

            var submenuPermission18 = new Permission()
            {
                Id = Guid.Parse("939534b1-e02e-40e8-9789-fab1931c17ff"),
                DomainId = userdomain.Id,
                PermissionName = "Hotel",
                PermissionDisplayName = "酒店管理",
                ParentPermissionId = menuPermission4.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission18);

            var submenu18 = new Module()
            {
                Id = Guid.Parse("11dc58a6-212f-4ec4-bb23-b3f64be3e4fb"),
                DomainId = userdomain.Id,
                ParentModuleId = menu5.Id,
                PermissionId = submenuPermission18.Id,
                ModuleName = "酒店管理",
                Controller = "Management",
                Action = "Hotel",
                ModuleLevel = 2,
                ModuleIndex = 2017,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu18);

            var submenuPermission19 = new Permission()
            {
                Id = Guid.Parse("adcb5b6a-b9eb-42bc-8077-e998e785fa41"),
                DomainId = userdomain.Id,
                PermissionName = "Device",
                PermissionDisplayName = "设备管理",
                ParentPermissionId = menuPermission4.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission19);

            var submenu19 = new Module()
            {
                Id = Guid.Parse("cf898c95-e226-4f18-84d9-4bde6bf9f88d"),
                DomainId = userdomain.Id,
                ParentModuleId = menu5.Id,
                PermissionId = submenuPermission19.Id,
                ModuleName = "设备管理",
                Controller = "Management",
                Action = "Device",
                ModuleLevel = 2,
                ModuleIndex = 2018,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu19);

            var menuPermission5 = new Permission()
            {
                Id = Guid.Parse("66711a7a-5345-4f66-b5fd-d17fd309244d"),
                DomainId = userdomain.Id,
                PermissionName = "System",
                PermissionDisplayName = "系统管理",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(menuPermission5);

            var menu6 = new Module()
            {
                Id = Guid.Parse("1eb7fb6c-d18a-4205-ae97-c65a0a84762d"),
                DomainId = userdomain.Id,
                ModuleName = "系统管理",
                IconString = "glyphicon glyphicon-cog",
                Controller = "System",
                Action = string.Empty,
                ModuleLevel = 1,
                ModuleIndex = 1005,
                IsMenu = true,
                PermissionId = menuPermission5.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(menu6);

            var submenuPermission20 = new Permission()
            {
                Id = Guid.Parse("cd324a23-e2f4-4ccc-9649-0bebad01d724"),
                DomainId = userdomain.Id,
                PermissionName = "UsersManage",
                PermissionDisplayName = "用户管理",
                ParentPermissionId = menuPermission5.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission20);

            var submenu20 = new Module()
            {
                Id = Guid.Parse("acaf8062-2896-44db-b4df-00c71d267a14"),
                DomainId = userdomain.Id,
                ParentModuleId = menu6.Id,
                PermissionId = submenuPermission20.Id,
                ModuleName = "用户管理",
                Controller = "System",
                Action = "UsersManage",
                ModuleLevel = 2,
                ModuleIndex = 2019,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu20);

            var submenuPermission21 = new Permission()
            {
                Id = Guid.Parse("6555fdd3-456f-4949-b33e-1705e9063d8c"),
                DomainId = userdomain.Id,
                PermissionName = "Authority",
                PermissionDisplayName = "权限管理",
                ParentPermissionId = menuPermission5.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission21);

            var submenu21 = new Module()
            {
                Id = Guid.Parse("7773c894-7e39-47bf-859e-11ba96fc1a88"),
                DomainId = userdomain.Id,
                ParentModuleId = menu6.Id,
                PermissionId = submenuPermission21.Id,
                ModuleName = "权限管理",
                Controller = "System",
                Action = "Authority",
                ModuleLevel = 2,
                ModuleIndex = 2020,
                IsMenu = false,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu21);

            var submenuPermission22 = new Permission()
            {
                Id = Guid.Parse("36bdd32c-e4d0-41df-a211-12500dbff7e9"),
                DomainId = userdomain.Id,
                PermissionName = "DepartmentManage",
                PermissionDisplayName = "部门管理",
                ParentPermissionId = menuPermission5.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission22);

            var submenu22 = new Module()
            {
                Id = Guid.Parse("51e6e0d5-7499-415a-bc21-530cd2c60f38"),
                DomainId = userdomain.Id,
                ParentModuleId = menu6.Id,
                PermissionId = submenuPermission22.Id,
                ModuleName = "部门管理",
                Controller = "System",
                Action = "DepartmentManage",
                ModuleLevel = 2,
                ModuleIndex = 2021,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu22);

            var submenuPermission23 = new Permission()
            {
                Id = Guid.Parse("6a1e5d80-b611-465c-89b5-d2019cd2510f"),
                DomainId = userdomain.Id,
                PermissionName = "RoleManage",
                PermissionDisplayName = "角色管理",
                ParentPermissionId = menuPermission5.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission23);

            var submenu23 = new Module()
            {
                Id = Guid.Parse("abe7305c-0112-4c78-9f32-045197cf4e4c"),
                DomainId = userdomain.Id,
                ParentModuleId = menu6.Id,
                PermissionId = submenuPermission23.Id,
                ModuleName = "角色管理",
                Controller = "System",
                Action = "RoleManage",
                ModuleLevel = 2,
                ModuleIndex = 2022,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Modules.Add(submenu23);

            var menuPermission6 = new Permission()
            {
                Id = Guid.Parse("4ebb5066-56e1-4ad5-933b-a86c7454cc0d"),
                DomainId = userdomain.Id,
                PermissionName = "Summary",
                PermissionDisplayName = "综合判断",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(menuPermission6);

            var menu7 = new Module()
            {
                Id = Guid.Parse("68919b20-56d4-4bc4-bed5-d5114061a6a5"),
                DomainId = userdomain.Id,
                ModuleName = "综合判断",
                IconString = "glyphicon glyphicon-retweet",
                Controller = "Summary",
                Action = string.Empty,
                ModuleLevel = 1,
                ModuleIndex = 1006,
                IsMenu = true,
                PermissionId = menuPermission6.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                IsEnabled = false
            };

            dbContext.Modules.Add(menu7);

            var submenuPermission24 = new Permission()
            {
                Id = Guid.Parse("6d0e94dc-d3e9-404e-90a0-06f8bf95cc02"),
                DomainId = userdomain.Id,
                PermissionName = "GeneralSummary",
                PermissionDisplayName = "综合判断",
                ParentPermissionId = menuPermission6.Id,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.Permissions.Add(submenuPermission24);

            var submenu24 = new Module()
            {
                Id = Guid.Parse("5d0cef23-ecaf-46fc-bd5e-d36c07f4ef2d"),
                DomainId = userdomain.Id,
                ParentModuleId = menu7.Id,
                PermissionId = submenuPermission24.Id,
                ModuleName = "综合判断",
                Controller = "Summary",
                Action = "GeneralSummary",
                ModuleLevel = 2,
                ModuleIndex = 2023,
                IsMenu = true,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                IsEnabled = false
            };

            dbContext.Modules.Add(submenu24);

            var deviceModel = new LampblackDeviceModel
            {
                Id = Guid.Parse("5306DA86-7B7C-40CF-933C-642061C24761"),
                Name = "油烟净化器",
                DomainId = userdomain.Id,
                CreateDateTime = DateTime.Now,
                CreateUserId = user.Id,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now,
                IsEnabled = true,
                Fail = 5000,
                Worse = 20000,
                Qualified = 50000,
                Good = 50001
            };

            dbContext.LampblackDeviceModels.Add(deviceModel);

            var device = new RestaurantDevice()
            {
                Id = Guid.Parse("b5285be6-6ac2-4f61-99e4-5f2d8ee25602"),
                DomainId = userdomain.Id,
                DeviceTypeId = lampblackDevicetype.Id,
                DeviceModelId = deviceModel.Id,
                DeviceCode =  "油烟一号",
                DevicePassword = "12345678",
                DeviceNodeId = "00000001",
                FirmwareSetId = lampblackfirmset.Id,
                StartTime = DateTime.Now,
                Status = DeviceStatus.Enabled,
                ProductionDateTime = DateTime.Now,
                Telephone = "18701987043",
                CollectFrequency = 1,
                DeviceName = "油烟一号",
                DeviceTerminalCode = "油烟一号",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                LastUpdateDateTime = DateTime.Now
            };

            dbContext.RestaurantDevices.Add(device);
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
