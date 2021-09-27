using BirthEntryServiceDAO_WCF.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BirthEntryServiceDAO_WCF.interfaces
{
    [ServiceContract]
    interface IUserCatalogDAO
    {
        [OperationContract]
        bool InsertNewUser(User user);

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        List<User> GetUsersByDateOfBirth(string dateOfBirth);

        [OperationContract]
        List<User> GetUsersAfterDateOfBirth(string dateOfBirth);

        [OperationContract]
        List<User> GetUsersBeforeDateOfBirth(string dateOfBirth);

        [OperationContract]
        List<User> GetUsersByDateEntryRange(string minimalDate, string maximumDate);

        [OperationContract]
        List<User> GetUsersByEntryDateTimeline(string timeline);

        [OperationContract]
        List<User> GetUsersByPublicIP(string publicIP);

        [OperationContract]
        List<User> GetByUsername(string username);

        [OperationContract]
        List<User> GetByUsernamePattern(string username);

        [OperationContract]
        List<User> GetUsersByDateEntry(string entryDate);

        [OperationContract]
        List<User> GetUsersOlderThanAge(int age);

        [OperationContract]
        List<User> GetUsersByAgeRange(int minimalAge, int maximalAge);

    }
}
