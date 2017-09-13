// ReSharper disable InconsistentNaming

namespace Lampblack_Platform.Models.PlatfromAccess
{
    public class JinganEnterBaseInfo
    {
        private const string DataSource = "上海乾铎环境科技发展有限公司";

        private const string RegionName = "静安区";

        public string ENTER_ID { get; set; }

        public string ENTER_NAME { get; set; }

        public string LINK_MAN { get; set; }

        public string LINK_PHONE { get; set; }

        public string ADDRESS { get; set; }

        public string REGIST_DATE { get; set; }

        public string REGION_NAME { get; set; } = RegionName;

        public string STREET { get; set; } = RegionName;

        public string BUSINESS_DATE { get; set; }

        public string LONGITUDE { get; set; }

        public string LATITUDE { get; set; }

        public string DATASOURCE { get; set; } = DataSource;
    }

    public class JinganApiResult
    {
        public string MESSAGE { get; set; }
    }

    public class JinganDeviceBaseInfo
    {
        public string ENTER_ID { get; set; }

        public string DEVICE_NAME { get; set; }

        public string DEVICE_CODE { get; set; }

        public string PRODUCTION_DATE { get; set; }

        public string DEVICE_STATE { get; set; }

        public string CHANNEL_NUM { get; set; }

        public string CLEAN_LINESS { get; set; }

        public string LAMPBLACK_VALUE { get; set; }

        public string PURIFIER_STATE { get; set; }

        public string FAN_STATE { get; set; }

        public string MONITORTIME { get; set; }
    }
}