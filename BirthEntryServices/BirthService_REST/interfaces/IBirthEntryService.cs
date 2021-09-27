using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UserCatalogServiceReference;

namespace BirthService_REST.interfaces
{
    [ServiceContract]
    interface IBirthEntryService
    {
        [OperationContract]
        void TreatUser(User user);

        [OperationContract]
        string ConvertBirthDateInActivity(DateTime dateNaissance);

        [OperationContract]
        bool AddNewUser(User user);
    }
}
