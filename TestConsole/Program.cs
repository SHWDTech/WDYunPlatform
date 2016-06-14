using System;
using Platform.Process.Process;
using SHWD.Platform.Repository.Repository;

namespace TestConsole
{
    class Program
    {
        static void Main()
        {
            var serverUser = GeneralProcess.GetUserByLoginName("CommnicationServer");

            if (serverUser == null)
            {
                Console.WriteLine(@"通信管理员账号信息错误，请检查配置！");
                return;
            }

            RepositoryBase.ContextGlobal = new RepositoryContext()
            {
                CurrentUser = serverUser,
                CurrentDomain = serverUser.Domain
            };

            var user = UserRepository.CreateDefaultModel();

            var userDb = UserRepository.CreateDefaultModelFromDataBase();

            Console.WriteLine(user.Email);
            Console.WriteLine(userDb.LoginName);
        }
    }
}
