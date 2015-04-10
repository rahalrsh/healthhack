using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsTravellers.Models
{
    public class MYSQLServices
    {

        public void SendCommand(string sqlcommand) //For Rahal
        {
            using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sqlcommand;
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<string> LoadData(string conditionName, string conditionValue, string searchValue, string table)
        {
            List<string> result = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    if (conditionName == "non")
                    {
                        cmd.CommandText = "SELECT " + searchValue + " From " + table;
                    }
                    else
                    {
                        cmd.CommandText = "SELECT " + searchValue + " From " + table + " WHERE " + conditionName + " = '" + conditionValue+"'";
                    }

                    MySqlDataReader rdr = null;
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        int i = 0;
                        string temp = "";
                        while (i < rdr.VisibleFieldCount)
                        {
                            if (i != 0)
                                temp += "%";
                            temp += rdr.GetString(i);
                            i++;
                        }
                        result.Add(temp);

                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return result;
        }

        public void CreateResponseTable(string qid)
        {
            string command = "CREATE TABLE IF NOT EXISTS response" + qid + " (id INT AUTO_INCREMENT, respondentID VARCHAR(25),respondentType VARCHAR(25), responseText MEDIUMTEXT, PRIMARY KEY (id) )";
            SendCommand(command);
        }

        public string AddToQuestionTable(string question)
        {
            string qid = "";
            string command = "INSERT INTO question_table(Question) VALUES('" + question + "')";
            SendCommand(command);
            return qid = GetQID(question);
        }

        //need to check if hashtag exits or not though
        public void AddToHashtable(string qid, string question)
        {
            HomePageServices hps = new HomePageServices();
            List<string> hashtagList = new List<string>();
            hashtagList = hps.GetTags(question);
            using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    foreach (string i in hashtagList)
                    {
                        cmd.CommandText = "INSERT INTO hash_tag_table(Tag, QID) VALUES('" + i + "', " + qid + " )";
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public string GetQID(string question)
        {
            string result;
            try
            {
                result = LoadData("Question", question, "QID", "question_table")[0];
            }
            catch (Exception) { throw; }
            return result;

        }

    }
}