using System.Data.Entity;

namespace SHWD.Platform.Repository.Entities
{
    public class DevelopInitializer : DropCreateDatabaseIfModelChanges<RepositoryDbContext>
    {
        protected override void Seed(RepositoryDbContext dbContext)
        {
            base.Seed(dbContext);

            //var domain = new Domain
            //{
            //    Id = Guid.Parse("DB07AB5E-A23F-4238-94CE-D52411199C18"),
            //    DomainName = "admin",
            //    DomianType = "system",
            //    CreateDateTime = DateTime.Now,
            //    IsEnabled = true
            //};

            //var user = new WdUser
            //{
            //    Id = Guid.Parse("77F05B52-AE62-4225-9F09-153C5634031F"),
            //    UserName = "admin",
            //    LoginName = "admin",
            //    Password = "21232f297a57a5a743894a0e4a801fc3",
            //    Email = "shweidongtech@126.com",
            //    Telephone = "18679361687",
            //    Status = UserStatus.Enabled,
            //    CreateDateTime = DateTime.Now,
            //    Domain = domain,
            //    Roles = new List<WdRole>(),
            //    IsEnabled = true
            //};

            //var role = new WdRole
            //{
            //    Id = Guid.Parse("650BFB4C-7277-484A-812E-6052F6DB71C7"),
            //    RoleName = "Admin",
            //    Users = new List<WdUser>(),
            //    CreateDateTime = DateTime.Now,
            //    Status = RoleStatus.Enabled,
            //    CreateUser = user,
            //    Domain = domain,
            //    IsEnabled = true
            //};

            //user.CreateUser = user;
            //domain.CreateUser = user;
            //user.Roles.Add(role);
            //role.Users.Add(user);

            //dbContext.Users.Add(user);
            //dbContext.Roles.Add(role);
            //dbContext.SysDomains.Add(domain);
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