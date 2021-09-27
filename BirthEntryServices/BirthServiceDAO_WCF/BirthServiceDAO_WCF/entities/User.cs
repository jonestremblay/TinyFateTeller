using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BirthEntryServiceDAO_WCF.entities
{
    public class User
    {
      
        public string UserName { get; set; }
        public string HostName { get; set; }
        public string LOCAL_IP_Address { get; set; }
        public string PUBLIC_IP_Address { get; set; }
        public string EntryDate { get; set; }
        public DateTime BirthDate { get; set; }

        public User()
        {

        }
        public User(string userName, string hostName, string lOCAL_IP_Address, string pUBLIC_IP_Address, string entryDate)
        {
            UserName = userName;
            HostName = hostName;
            LOCAL_IP_Address = lOCAL_IP_Address;
            PUBLIC_IP_Address = pUBLIC_IP_Address;
            EntryDate = entryDate;
        }

        public User(string userName, string hostName, string lOCAL_IP_Address, string pUBLIC_IP_Address, string entryDate, DateTime birthDate)
        {
            UserName = userName;
            HostName = hostName;
            LOCAL_IP_Address = lOCAL_IP_Address;
            PUBLIC_IP_Address = pUBLIC_IP_Address;
            EntryDate = entryDate;
            BirthDate = birthDate;
        }

        public override string ToString()
        {
            return this.UserName + " \n"
                + "\t Date of birth : " + this.BirthDate + "\n"
                + "\t Hostname | Local IP : " + this.HostName + " | " + this.LOCAL_IP_Address
                + "\t Public IP | Date the user has been added : " + this.PUBLIC_IP_Address + " | " + this.EntryDate;
        }
    }
}
