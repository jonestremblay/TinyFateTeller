using BirthService_REST.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCatalogServiceReference;

namespace BirthService_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirthController : ControllerBase
    {
        [HttpGet("getActivity")]
        public String  GetActivityByBirthDate(/*DateTime BirthDate*/String BirthDate)
        {
            BirthEntryService service = new BirthEntryService();
            return service.ConvertBirthDateInActivity(DateTime.Parse(BirthDate));
        }

        [HttpPost("AddUserToDatabase")]
        public int AddUserToDatabase(String Username, String Hostname, String LocalIP, 
                                     String PublicIP, String EntryDate, String BirthDate )
        {
            User user = new ();
            user.UserName = Username;
            user.HostName = Hostname;
            user.LOCAL_IP_Address = LocalIP;
            user.PUBLIC_IP_Address = PublicIP;
            user.EntryDate = EntryDate;
            Console.WriteLine(BirthDate);
            user.BirthDate = DateTime.Parse(BirthDate);
            BirthEntryService service = new();
            bool wasAdded = service.AddNewUser(user);
            return Convert.ToInt32(wasAdded);
        }

    }
}
