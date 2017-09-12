using JingAnWebService.AddEnterBaseInfo;
using JingAnWebService.InsertDeviceBaseInfo;

namespace JingAnWebService
{
    public class JingAnLampblackService
    {
        readonly  InsertDeviceBaseInfoClient _deviceBaseInfoClient;

        private readonly InsertBaseInfoClient _baseInfoClient;

        public JingAnLampblackService()
        {
            _deviceBaseInfoClient = new InsertDeviceBaseInfoClient();
            _baseInfoClient = new InsertBaseInfoClient();
        }

        public string InsertBaseInfo(string jsons) => _baseInfoClient.addEnterBaseInfo(jsons);

        public string InsertDeviceBaseInfo(string jsons) => _deviceBaseInfoClient.insertDeviceBaseInfo(jsons);
    }
}
