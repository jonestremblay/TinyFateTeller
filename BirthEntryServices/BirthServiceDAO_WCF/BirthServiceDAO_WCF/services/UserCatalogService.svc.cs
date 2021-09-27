using BirthEntryServiceDAO_WCF.ADO;
using BirthEntryServiceDAO_WCF.entities;
using BirthEntryServiceDAO_WCF.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BirthEntryServiceDAO_WCF.services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class UserCatalogService : IUserCatalogDAO
    {
        readonly UserCatalogDAO DAO = UserCatalogDAO.GetInstance();

        public bool InsertNewUser(User newUser)
        {
            return DAO.InsertNewUser(newUser);
        }


        public List<User> GetAllUsers()
        {
            List<User> usersList;
            usersList = DAO.GetAllUsers();
            return usersList;
        }

        /// <summary>
        /// Récupère tous les users selon la date qu'ils ont été insérés dans la BD.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>

        public List<User> GetUsersByDateEntry(string entryDate)
        {
            List<User> usersList;
            usersList = DAO.GetUsersByDateEntry(entryDate);
            return usersList;
        }

        /// <summary>
        /// Récupère tous les users selon un range de date où ils ont été insérés dans la BD.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>

        public List<User> GetUsersByDateEntryRange(string minimalDate, string maximumDate)
        {
            List<User> usersList;

            usersList = DAO.GetUsersByDateEntryRange(minimalDate, maximumDate);
            return usersList;
        }

        public List<User> GetUsersOlderThanAge(int age)
        {
            List<User> usersList;
            usersList = DAO.GetUsersOlderThanAge(age);
            return usersList;
        }


        public List<User> GetUsersByAgeRange(int minimalAge, int maximalAge)
        {
            List<User> usersList;
            usersList = DAO.GetUsersByAgeRange(minimalAge, maximalAge);
            return usersList;
        }

        public List<User> GetByUsername(string username)
        {
            List<User> usersList = DAO.GetByUsername(username);
            return usersList;
        }

        public List<User> GetByUsernamePattern(string username)
        {
            List<User> usersList = DAO.GetByUsernamePattern(username);
            return usersList;
        }

        public List<User> GetUsersByDateOfBirth(string birthDate)
        {

            List<User> usersList = DAO.GetUsersByDateOfBirth(birthDate);
            return usersList;
        }

        public List<User> GetUsersBeforeDateOfBirth(string birthDate)
        {

            List<User> usersList = DAO.GetUsersBeforeDateOfBirth(birthDate);
            return usersList;
        }

        public List<User> GetUsersAfterDateOfBirth(string birthDate)
        {

            List<User> usersList = DAO.GetUsersAfterDateOfBirth(birthDate);
            return usersList;
        }

        public List<User> GetUsersByEntryDateTimeline(string timeline)
        {
            List<User> usersList = DAO.GetUsersByEntryDateTimeline(timeline);
            return usersList;
        }

        public List<User> GetUsersByPublicIP(string publicIP)
        {
            List<User> usersList = DAO.GetUsersByPublicIP(publicIP);
            return usersList;
        }

    }
}
