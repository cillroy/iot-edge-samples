using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SampleModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        // example: http://localhost:3956/api/values
        [HttpPost]
        public ActionResult<string> Post(SmartCommunities_APP appData)
        {
            // using (var reader = new StreamReader(Request.Body))
            // {
            //     var body = reader.ReadToEnd();
            //     Console.WriteLine($"POST: Got From Request: {body}");
            // }
            return validateApplicationKey(appData);
        }
        public ContentResult validateApplicationKey(SmartCommunities_APP appData)
        {
            // Call into a IotEdge cached copy of Application ID's (GUID) - some DB on device?
            IDictionary<string, string> appList = new Dictionary<string, string>();
            appList.Add("ePolice", "some value for ePolice");
            appList.Add("eHealth", "some value for eHealth");

            ContentResult contentQuality = new ContentResult();

            if (appList.ContainsKey(appData.appID))
            {
                contentQuality.StatusCode = 200;
                contentQuality.Content = JsonConvert.SerializeObject(appData);
            }
            else
            {
                contentQuality.StatusCode = 400;
                contentQuality.Content = "invalid appID";
            }

            return contentQuality;
        }
    }

    public class SmartCommunities_APP
    {
        public string appID { get; set; }
        public string payload { get; set; }
    }
}
