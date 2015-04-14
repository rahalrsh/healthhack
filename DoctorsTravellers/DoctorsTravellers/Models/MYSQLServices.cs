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
                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = sqlcommand;
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<string> LoadData(string sqlcommand)
        {
            List<string> result = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                       cmd.CommandText = sqlcommand;

                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {

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
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return result;
        }

        public void CreateResponseTable(int qid)
        {
            string command = "CREATE TABLE IF NOT EXISTS response" + qid.ToString() + " (id INT AUTO_INCREMENT, respondentID VARCHAR(25), responseText MEDIUMTEXT, PRIMARY KEY (id) )";
            SendCommand(command);
        }

        //qid is table name for responses table name = response'qid' ,eg response77
        public int AddToResponseTable(int qid, string response)
        {
            int rid = -1;
            string command = "INSERT INTO response" + qid.ToString() + "(responseText,respondentID) VALUES('" + response + "', '" + GetId().ToString() + "')";
            SendCommand(command);
            Question qhelp = new Question();
            return rid = qhelp.GetRID(response, qid);
        }

        public int AddToQuestionTable(string question)
        {
            int qid = -1;
            string command = "INSERT INTO question_table(Question) VALUES('" + question + "')";
            SendCommand(command);
            Question qhelp = new Question();
            return qid = qhelp.GetQID(question);
        }

        //need to check if hashtag exits or not though
        public void AddToHashtable(int qid, string question)
        {
            HomePageServices hps = new HomePageServices();
            List<string> hashtagList = new List<string>();
            Question qhelp = new Question();
            hashtagList = qhelp.GetTags(question);
            string hashstring = "";
            using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                        foreach (string i in hashtagList)
                        {
                            cmd.CommandText = "INSERT INTO hash_tag_table(Tag, QID) VALUES('" + i + "', " + qid.ToString() + " )";
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }



        /******RAHAL ADD THESE TO A USER MODEL CLASS****/
        public string GetType()
        {
            return "doctor";
        }

        public int GetId()
        {
            return 5;
        }
        /******RAHAL ADD THESE TO A USER MODEL CLASS****/
    }
}