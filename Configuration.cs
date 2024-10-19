using Microsoft.Extensions.Configuration;

namespace UtilityApp
{
    internal class Configuration
    {
        public static List<Setting> BuildAppSettings()
        {
            var configuration = new ConfigurationBuilder().
                AddJsonFile("appSettings.json",false,false).Build();

            var list = new List<Setting>
            {
                new Setting
                {
                    ApiId = Convert.ToInt32(configuration["RandomUserApiID"]),
                    ApiPath = configuration["RandomUserApiPath"]
                },
                new Setting
                {
                    ApiId = Convert.ToInt32(configuration["DummyJsonApiID"]),
                    ApiPath = configuration["DummyJsonApiPath"]
                },
                new Setting
                {
                    ApiId = Convert.ToInt32(configuration["ReqApiID"]),
                    ApiPath = configuration["ReqApiPath"]
                },
                new Setting
                {
                    ApiId = Convert.ToInt32(configuration["JaonPlaceHolderApiID"]),
                    ApiPath = configuration["JaonPlaceHolderApiPath"]
                }
            };

            return list;
        }
    }
}
