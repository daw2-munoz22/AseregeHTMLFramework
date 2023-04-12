using Newtonsoft.Json;
using System;

namespace AseregeBlazor.Manager
{
    public static class PostJson
    {     
        public static T GetJsonResult<T>(string json)          
        {
            try 
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch(JsonReaderException ilegalJson) 
            {
                Console.WriteLine(ilegalJson.Message);                                               
            }
            return default(T);
        }
    }
}
