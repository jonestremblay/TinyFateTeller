using BirthService_REST.interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UserCatalogServiceReference;

namespace BirthService_REST.services
{
 

    class BirthEntryService : IBirthEntryService
    {
        UserCatalogDAOClient client = new UserCatalogDAOClient();

        public  void TreatUser(User user)
        {
            bool added = AddNewUser(user);
            Console.WriteLine(user.UserName + " has been added to the database : " + added);
            ConvertBirthDateInActivity(user.BirthDate);
        }

        public  bool AddNewUser(User newUser)
        {
            bool inserted = client.InsertNewUser(newUser);
            Console.WriteLine("New user inserted in DB : " + inserted);
            return inserted;
            
        }

        public string ConvertBirthDateInActivity(DateTime dateNaissance)
        {
            String message = "";
            if (dateNaissance.Year > 2010)
            {
                message = "Je ne sais pas quoi vous dire...";
            }
            else if (dateNaissance.Year >= 2001 && dateNaissance.Year <= 2010)
            {
                message = "Utilisation de ce service non-autorisée !!!";
            }
            else if (dateNaissance.Year >= 1991 && dateNaissance.Year <= 2000)
            {
                message = "Fais ce qui te plaît, tu as encore le temps !";
            }
            else if (dateNaissance.Year >= 1981 && dateNaissance.Year <= 1990)
            {
                message = "Il est grand temps de terminer tes études !";
            }
            else if (dateNaissance.Year >= 1971 && dateNaissance.Year <= 1980)
            {
                message = "Il est temps de commencer à travailler sérieusement...";
            }
            else
            {
                message = "Il est temps d'aller se promener à travers le monde";
            }
            return message;
        }

      /*  @GET
  @Produces(MediaType.TEXT_PLAIN)
  public String sayPlainTextHello()
        {
            return "Hello Jersey";
        }

        // This method is called if XML is request
        @GET
        @Produces(MediaType.TEXT_XML)
  public String sayXMLHello()
        {
            return "<?xml version=\"1.0\"?>" + "<hello> Hello Jersey" + "</hello>";
        }

        // This method is called if HTML is request
        @GET
        @Produces(MediaType.TEXT_HTML)*/
  public String sayHtmlHello()
        {
            return "<html> " + "<title>" + "Hello Jersey" + "</title>"
                + "<body><h1>" + "Hello Jersey" + "</body></h1>" + "</html> ";
        }
    }
}
