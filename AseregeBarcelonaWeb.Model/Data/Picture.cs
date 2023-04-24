using System;

namespace AseregeBarcelonaWeb.Model.Data
{
    public class Picture
    {
        public Authorize Authorize { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public string Format { get; set; }
        public DateTime Date { get; set; }
    }
}
