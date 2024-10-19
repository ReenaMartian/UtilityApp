using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UtilityApp.Models;

namespace UtilityApp
{
    public class Common
    {
        List<Setting> SettingList = null;
        public Common()
        {
            SettingList = Configuration.BuildAppSettings();
        }

        public List<UserModel> GetApiData()
        {
            User usr = new User();
            List<UserModel> UserList = new List<UserModel>();

            try
            {
                foreach (Setting item in SettingList)
                {
                    if (item.ApiId == (int)ApiDetails.RandomUserApi)
                    {
                        var data = usr.GetUser(item.ApiPath);

                        var obj = JsonConvert.DeserializeObject<RandomUserApi.Root>(data.Result.ToString());
                        foreach (var res in obj.results)
                        {
                            UserModel model = new UserModel();
                            model.firstName = res.name.first;
                            model.lastName = res.name.last;
                            model.email = res.email;
                            model.sourceId = (int)ApiDetails.RandomUserApi;

                            UserList.Add(model);
                        }
                    }
                    else if (item.ApiId == (int)ApiDetails.DummyJsonApi)
                    {
                        var data = usr.GetUser(item.ApiPath);

                        var obj = JsonConvert.DeserializeObject<DummyJsonApi.Root>(data.Result.ToString());
                        foreach (var res in obj.users)
                        {
                            UserModel model = new UserModel();
                            model.firstName = res.firstName;
                            model.lastName = res.lastName;
                            model.email = res.email;
                            model.sourceId = (int)ApiDetails.DummyJsonApi;

                            UserList.Add(model);
                        }
                    }
                    else if (item.ApiId == (int)ApiDetails.ReqApi)
                    {
                        var data = usr.GetUser(item.ApiPath);

                        var obj = JsonConvert.DeserializeObject<ReqApi.Root>(data.Result.ToString());

                        foreach (var res in obj.data)
                        {
                            UserModel model = new UserModel();
                            model.firstName = res.first_name;
                            model.lastName = res.last_name;
                            model.email = res.email;
                            model.sourceId = (int)ApiDetails.ReqApi;

                            UserList.Add(model);
                        }
                    }
                    //else if (item.ApiId == (int)ApiDetails.JaonPlaceHolderApi)
                    //{
                    //    var data = usr.GetUser(item.ApiPath);

                    //    var obj = JsonConvert.DeserializeObject<JsonPlaceHolder.Root>(data.Result.ToString());


                    //    //foreach (var res in obj)
                    //    //{
                    //    //UserModel model = new UserModel();
                    //    //    model.firstName = res.first_name;
                    //    //    model.lastName = res.last_name;
                    //    //    model.email = res.email;
                    //    //    model.sourceId = (int)ApiDetails.ReqApi;

                    //    // UserList.Add(model);
                    //    //}
                    //}


                }
            }
            catch (Exception ex)
            {
                throw ;
            }

            return UserList;
        }

        public void GenerateFile(List<UserModel> UserList, string path, string format)
        {
            
            JObject result = null;

            path = @$"{path}/UserFolder";
            try
            {
                bool exists = Directory.Exists(path);

                if (!exists)
                    Directory.CreateDirectory(path);

                string json = JsonConvert.SerializeObject(UserList);
                if (format.ToLower() == "json")
                {
                    using (StreamWriter file = File.CreateText($"{path}/Users_{DateTime.Now.Ticks}.txt"))
                    {

                        JsonSerializer serializer = new JsonSerializer();
                        //serialize object directly into file stream
                        serializer.Serialize(file, json);
                    }
                }
                else if (format.ToLower() == "csv")
                {
                    string filename = $"{path}/Users_{DateTime.Now.Ticks}.csv";
                    CSVHelper.ExportCsv(UserList, filename);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}

