using BirthEntryServiceDAO_WCF.entities;
using BirthEntryServiceDAO_WCF.interfaces;
using BirthEntryServiceDAO_WCF.Connections;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Data;

namespace BirthEntryServiceDAO_WCF.ADO
{
    public class UserCatalogDAO : IUserCatalogDAO
    {

        private UserCatalogDAO() { }

        private static UserCatalogDAO _instance;

        private static readonly object _lock = new object();

        public static UserCatalogDAO GetInstance()
        {
            if (_instance == null)
            {
                // Since there's no Singleton instance yet, multiple threads can
                // simultaneously pass the previous conditional and reach this
                // point almost at the same time. The first of them will acquire
                // lock and will proceed further, while the rest will wait here.
                lock (_lock)
                {
                    
                    if (_instance == null)
                    {
                        _instance = new UserCatalogDAO();
                    }
                }
            }
            return _instance;
        }

        public bool InsertNewUser(User user)
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            String query = "INSERT INTO dates_of_birth (entry_date, hostname, local_ip, public_ip, user_name, birth_date) "
                           + "VALUES (@entryDate, @hostname, @local_ip, @public_ip, @user_name, @birth_date)";
            bool cree = false;
            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add(new MySqlParameter("entryDate", user.EntryDate));
                cmd.Parameters.Add(new MySqlParameter("hostname", user.HostName));
                cmd.Parameters.Add(new MySqlParameter("local_ip", user.LOCAL_IP_Address));
                cmd.Parameters.Add(new MySqlParameter("public_ip", user.PUBLIC_IP_Address));
                cmd.Parameters.Add(new MySqlParameter("user_name", user.UserName));
                cmd.Parameters.Add(new MySqlParameter("birth_date", user.BirthDate));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    cree = true;
                }
                cnx.Close();
                return cree;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (cnx != null) { cnx.Close(); }
            }
            return cree;
        }

        public List<User> GetAllUsers()
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            string query = "SELECT * FROM dates_of_birth dob ;";
            User tmpUser;
            List<User> userList = new List<User>();
            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    /*Créer user, et le add a la liste */
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    userList.Add(tmpUser);
                }
                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return userList;
        }

        /// <summary>
        /// Récupère tous les users selon la date qu'ils ont été insérés dans la BD.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        
        public List<User> GetUsersByDateEntry(string entryDate)
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            string query = "SELECT * FROM dates_of_birth dob WHERE "
                         + "dob.entry_date= @date";
            User tmpUser;
            List<User> userList = new List<User>();
            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("date", entryDate));

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    /*Créer user, et le add a la liste */
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    userList.Add(tmpUser);
                }
                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return userList;
        }

        /// <summary>
        /// Récupère tous les users selon un range de date où ils ont été insérés dans la BD.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<User> GetUsersByDateEntryRange(string minimalDate, string maximumDate)
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            string query = "SELECT * FROM dates_of_birth dob WHERE dob.entry_date "
                         + "BETWEEN @minDate AND @maxDate ;";
            User tmpUser;
            List<User> userList = new List<User>();

            DateTime minDate = DateTime.Parse(minimalDate);
            minDate = minDate.Date.Add(new TimeSpan(0, 0, 0));
            DateTime maxDate = DateTime.Parse(maximumDate);
            maxDate = maxDate.Date.Add(new TimeSpan(0, 0, 0));

            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("minDate", minDate));
                cmd.Parameters.Add(new MySqlParameter("maxDate", maxDate));


                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    /* Créer user, et le add a la liste */
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    userList.Add(tmpUser);
                }
                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return userList;
        }

        public List<User> GetUsersOlderThanAge(int age)
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            string query = "SELECT entry_date, hostname, ip_address, user_name, "
                         + "birth_date, TIMESTAMPDIFF(YEAR, dob.birth_date, CURDATE()) AS age "
                         + "FROM dates_of_birth dob WHERE dob.age >= @age ;";
            User tmpUser;
            List<User> userList = new List<User>();
            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("age", age));

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    /* Créer user, et le add a la liste */
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    userList.Add(tmpUser);
                }
                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return userList;
        }

        public List<User> GetUsersByAgeRange(int minimalAge, int maximalAge)
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            string query = "SELECT entry_date, hostname, ip_address, user_name, "
                         + "birth_date, TIMESTAMPDIFF(YEAR, dob.birth_date, CURDATE()) AS age "
                         + "FROM dates_of_birth dob WHERE dob.age BETWEEN @minAge AND @maxAge ;";
            User tmpUser;
            List<User> userList = new List<User>();
            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("minAge", minimalAge));
                cmd.Parameters.Add(new MySqlParameter("maxAge", maximalAge));

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    /* Créer user, et le add a la liste */
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    userList.Add(tmpUser);
                }
                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return userList;
        }

        public List<User> GetByUsernamePattern(string username)
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            string query = "SELECT * FROM dates_of_birth dob WHERE "
                         + "dob.user_name LIKE @username ";
            User tmpUser;
            List<User> userList = new List<User>();
            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("username", "%" + username + "%"));

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    userList.Add(tmpUser);

                }
                // return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return userList;
        }

        public List<User> GetUsersByDateOfBirth(string dateOfBirth)
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            string query = "SELECT * FROM dates_of_birth dob WHERE "
                         + "dob.birth_date = @dateOfBirth";
            User tmpUser;
            List<User> userList = new List<User>();

            DateTime dob = DateTime.Parse(dateOfBirth);
            dob = dob.Date.Add(new TimeSpan(0, 0, 0));

            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("dateOfBirth", dob));

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    /* Créer user, et le add a la liste */
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    userList.Add(tmpUser);
                }
                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return userList;
        }

        public List<User> GetUsersAfterDateOfBirth(string dateOfBirth)
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            string query = "SELECT * FROM dates_of_birth dob WHERE "
                         + "dob.birth_date > @dateOfBirth";
            User tmpUser;
            List<User> userList = new List<User>();

            DateTime dob = DateTime.Parse(dateOfBirth);
            dob.Date.Add(new TimeSpan(0, 0, 0));
            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("dateOfBirth", dob));

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    /* Créer user, et le add a la liste */
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    userList.Add(tmpUser);
                }
                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return userList;
        }
     
        public List<User> GetUsersBeforeDateOfBirth(string dateOfBirth)
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            string query = "SELECT * FROM dates_of_birth dob WHERE "
                         + "dob.birth_date < @dateOfBirth";
            User tmpUser;
            List<User> userList = new List<User>();

            DateTime dob = DateTime.Parse(dateOfBirth);
            dob.Date.Add(new TimeSpan(0, 0, 0));

            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("dateOfBirth", dob));

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    /* Créer user, et le add a la liste */
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    userList.Add(tmpUser);
                }
                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return userList;
        }

        public List<User> GetByUsername(string username)
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            string query = "SELECT * FROM dates_of_birth dob WHERE "
                         + "dob.user_name = @username ";
            User tmpUser;
            List<User> usersList = new List<User>();
            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("username", username));

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    usersList.Add(tmpUser);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return usersList;
        }

        public List<User> GetUsersByEntryDateTimeline(string timeline)
        {
            string query = "";
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            DateTime today = DateTime.Today.Add(new TimeSpan(0, 0, 0));

            switch (timeline)
            {
                case "today":
                    query = "SELECT * FROM dates_of_birth dob " +
                                "WHERE SUBSTRING_INDEX(dob.entry_date, ' ', 1) = @today ";
                    break;
                case "this_week":
                    query = "SELECT * FROM dates_of_birth dob " +
                                "WHERE WEEK(SUBSTRING_INDEX(dob.entry_date, ' ', 1)) = WEEK(now())";
                    break;
                case "this_month":
                    query = "SELECT * FROM dates_of_birth dob " +
                                "WHERE MONTH(SUBSTRING_INDEX(dob.entry_date, ' ', 1)) = MONTH(now())";
                    break;
                case "last_3_months":
                    query = "SELECT * FROM dates_of_birth dob WHERE dob.entry_date >= now() - interval 3 month ";
                    break;
                case "last_6_months":
                    query = "SELECT * FROM dates_of_birth dob WHERE dob.entry_date >= now() - interval 6 month ";
                    break;
                case "this_year":
                    query = "SELECT * FROM dates_of_birth dob " +
                               "WHERE YEAR(SUBSTRING_INDEX(dob.entry_date, ' ', 1)) = YEAR(now())";
                    break;
            }

            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            
            User tmpUser;
            List<User> userList = new List<User>();
            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("today", today));

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    /* Créer user, et le add a la liste */
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    userList.Add(tmpUser);
                }
                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return userList;
        }

        public List<User> GetUsersByPublicIP(string publicIP)
        {
            DbConnection cnx = null;
            DbCommand cmd = null;
            DbDataReader dataReader = null;
            string query = "SELECT * FROM dates_of_birth dob WHERE "
                         + "dob.public_ip = @publicIP ";
            User tmpUser;
            List<User> usersList = new List<User>();
            try
            {
                cnx = Database.GetConnection();
                cmd = cnx.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new MySqlParameter("publicIP", publicIP));

                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    tmpUser = new User();
                    tmpUser.HostName = dataReader["hostname"].ToString();
                    tmpUser.LOCAL_IP_Address = dataReader["local_ip"].ToString();
                    tmpUser.PUBLIC_IP_Address = dataReader["public_ip"].ToString();
                    tmpUser.UserName = dataReader["user_name"].ToString();
                    tmpUser.BirthDate = Convert.ToDateTime(dataReader["birth_date"].ToString());
                    tmpUser.EntryDate = dataReader["entry_date"].ToString();
                    usersList.Add(tmpUser);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dataReader != null) { dataReader.Close(); }
                if (cnx != null) { cnx.Close(); }
            }
            return usersList;
        }
    }
}