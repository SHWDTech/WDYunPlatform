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

            user.CreateUserId = user.Id;
            user.LastUpdateUserId = user.Id;
            user.LastUpdateDateTime = DateTime.Now;
            user.LastLoginDateTime = DateTime.Now;
            domain.CreateUserId = user.Id;
            domain.LastUpdateUserId = user.Id;
            domain.LastUpdateDateTime = DateTime.Now;
            role.CreateUserId = user.Id;
            role.LastUpdateUserId = user.Id;
            role.LastUpdateDateTime = DateTime.Now;
            user.Roles.Add(role);
            role.Users.Add(user);

            dbContext.Users.Add(user);
            dbContext.Roles.Add(role);
            dbContext.SysDomains.Add(domain);

            var field = new SysDictionary
            {
                Id = Guid.Parse("7402cdb5-1e1e-4633-a7e9-7d6d15634fc0"),
                ItemName = "Field",
                ItemKey = "7E0384B37CFJ",
                ItemValue = "TESTFIELD",
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
                ItemName = "FSubield",
                ItemKey = "7E0384B37CFL",
                ItemValue = "TESTSUB FIELD",
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
                DeviceTypeCode = "AACC",
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

            var firmb = new Firmware
            {
                Id = Guid.Parse("ee1c7113-ff3e-4768-9788-ee66207d40fc"),
                FirmwareName = "firmb",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id
            };

            dbContext.Firmwares.Add(firma);
            dbContext.Firmwares.Add(firmb);

            var firmSet = new FirmwareSet
            {
                Id = Guid.Parse("6c36fddf-d3d9-416b-84df-cd849006eef1"),
                FirmwareSetName = "jiujiu",
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true
            };

            firmSet.Firmwares.Add(firma);
            dbContext.FirmwareSets.Add(firmSet);

            dbContext.DeviceTypes.Add(deviceType);

            

            var proa = new Protocol
            {
                Id = Guid.Parse("f59022bc-6f8c-4ced-954f-3a6d7dd29335"),
                FieldId = field.Id,
                SubFieldId = subfield.Id,
                CustomerInfo = "1",
                Version = "versiona",
                ReleaseDateTime = DateTime.Now,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                CheckType = ProtocolCheckType.Crc16,
                Head = 0xAACC,
                Tail = 0xB1B1
            };

            var proa1 = new Protocol
            {
                Id = Guid.Parse("4aed48ad-f3f9-466d-ac4e-926a88069e3c"),
                FieldId = field.Id,
                SubFieldId = subfield.Id,
                CustomerInfo = "2",
                Version = "versiona1",
                ReleaseDateTime = DateTime.Now,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                CheckType = ProtocolCheckType.Crc16,
                Head = 0xAACC,
                Tail = 0xB1B1
            };

            firma.Protocols.Add(proa);
            firma.Protocols.Add(proa1);

            var prob = new Protocol
            {
                Id = Guid.Parse("cbe44af8-3f9a-412a-8a52-8698461df31c"),
                FieldId = field.Id,
                SubFieldId = subfield.Id,
                CustomerInfo = "3",
                Version = "versionb",
                ReleaseDateTime = DateTime.Now,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                CheckType = ProtocolCheckType.Crc16,
                Head = 0xAACC,
                Tail = 0xB1B1
            };

            var prob1 = new Protocol
            {
                Id = Guid.Parse("01d6601b-3bfb-43ea-a952-0ae9e416dff5"),
                FieldId = field.Id,
                SubFieldId = subfield.Id,
                CustomerInfo = "4",
                Version = "versionb1",
                ReleaseDateTime = DateTime.Now,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                CheckType = ProtocolCheckType.Crc16,
                Head = 0xAACC,
                Tail = 0xB1B1
            };

            firmb.Protocols.Add(prob);
            firmb.Protocols.Add(prob1);

            var structurea = new ProtocolStructure
            {
                Id = Guid.Parse("dcdb914f-62ec-42dc-bece-04124cfd61fa"),
                ProtocolId = proa.Id,
                ComponentName = "Head",
                ComponentIndex = 1,
                ComponentLength = 1,
                ComponentDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Character
            };

            var structurea1 = new ProtocolStructure
            {
                Id = Guid.Parse("b1d67223-3b20-44ca-bec6-f7f5449b6efb"),
                ProtocolId = proa1.Id,
                ComponentName = "Head",
                ComponentIndex = 1,
                ComponentLength = 1,
                ComponentDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Character
            };

            var structureb = new ProtocolStructure
            {
                Id = Guid.Parse("18bbd2ce-a799-4edc-9ebb-0e513b00c56a"),
                ProtocolId = proa1.Id,
                ComponentName = "Head",
                ComponentIndex = 1,
                ComponentLength = 1,
                ComponentDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Character
            };

            var structureb1 = new ProtocolStructure
            {
                Id = Guid.Parse("313d5bfc-6de1-43b2-b6b6-fa33d6899b70"),
                ProtocolId = proa1.Id,
                ComponentName = "Head",
                ComponentIndex = 1,
                ComponentLength = 1,
                ComponentDataLength = 1,
                CreateUserId = user.Id,
                CreateDateTime = DateTime.Now,
                LastUpdateDateTime = DateTime.Now,
                LastUpdateUserId = user.Id,
                IsEnabled = true,
                DataType = ProtocolDataType.Character
            };

            dbContext.ProtocolStructures.Add(structurea);
            dbContext.ProtocolStructures.Add(structurea1);
            dbContext.ProtocolStructures.Add(structureb);
            dbContext.ProtocolStructures.Add(structureb1);

            var device = new Device
            {
                Id = Guid.Parse("ba0ca1dc-0331-4d5a-96e5-49ac20665a13"),
                DeviceTypeId = deviceType.Id,
                DeviceCode = "wohao",
                DevicePassword = string.Empty,
                DeviceModuleGuid = Guid.Parse("024849d6-2538-48a0-8f0d-5a289e27f955"),
                DeviceNodeId = new byte[] { 0, 0, 0, 0, 0, 2, 5, 4 },
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