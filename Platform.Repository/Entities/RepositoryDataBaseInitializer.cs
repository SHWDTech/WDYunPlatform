using System;
using System.Collections.Generic;
using System.Data.Entity;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

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