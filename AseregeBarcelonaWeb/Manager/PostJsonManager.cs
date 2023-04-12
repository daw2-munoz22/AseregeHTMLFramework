﻿using Newtonsoft.Json;
using System;

namespace AseregeBarcelonaWeb.Manager
{
    public static class PostJsonManager
    {
        public static T GetJsonResult<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonReaderException ilegalJson)
            {
                Console.WriteLine(ilegalJson.Message);
            }
            return default;
        }
    }
}