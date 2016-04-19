using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 通用处理程序
    /// </summary>
    public static class GeneralProcess
    {
        /// <summary>
        /// 根据用户登录名获取用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static WdUser GetUserByLoginName(string loginName) => DbRepository.Repo<UserRepository>().GetUserByLoginName(loginName);
    }
}
