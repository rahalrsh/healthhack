using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using DoctorsTravellers.Models;
using System.Data;

namespace DoctorsTravellers.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        //HOME page
        public ActionResult Index()
        {
            return RedirectToAction("Result");//This line is for testing 
        }

        //RESULT page
        public ActionResult Result()
        {
            string test = "I'm #pregnant and travelling with a #child and an #elder what #medication should I take with me? we are going to #Ibiza ";
           QuestionHandelr(test);
            return View();
        }

        void QuestionHandelr(string question)
        {
            try
            {

                string qid = AddToQuestionTable(question);

                CreateResponseTable(qid);
                
                AddToHashtable(qid, question);
            }
            catch (Exception e) { throw; }
        }

        void CreateResponseTable(string qid)
        {
            using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS " + qid + " (Id INT PRIMARY KEY AUTO_INCREMENT, respondentID VARCHAR(25),respondentType VARCHAR(25), responseText TEXT )";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }

            }

        }

        string AddToQuestionTable( string question)
        {
            string qid = GetQID(question);
            using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO question_table(QID, Question) VALUES(" + qid + ", '" + question + "')";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return qid;
        }

        //need to check if hashtag exits or not though
        void AddToHashtable(string qid, string question)
        {
            List<string> hashtagList = new List<string>();
            hashtagList = GetTags(question);
            using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    foreach (string i in hashtagList)
                    {
                        cmd.CommandText = "INSERT INTO hash_tag_table(Tag, QID) VALUES(" + i + ", '" + qid + "')";
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        string GetQID(string question) 
        {
            string result = ""; 
             using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                 try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT QID From question_table WHERE Qusetion = '" + question + "'";
                    MySqlDataReader rdr = null;
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        result = rdr["Question"].ToString();
                    }
                 }
                     catch (Exception)
                {
                    throw;
                }
                  
        }
            return result;}
        List<string> GetTags(string question)
        {
            List<string> result = new List<string>();
            string[] temp = question.Split(null);
            foreach (string i in temp)
            {
                if (i.Contains('#'))
                    result.Add(i);

            }

            return result;
        }



        List<string> LoadData(string condition, string field, string table)//condition is always the Primary Key 
        {
            List<string> result = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    if (condition == "non")
                    {
                        cmd.CommandText = "SELECT " + field + " From " + table;
                    }
                    else
                    {
                        cmd.CommandText = "SELECT " + field + " From " + table + " WHERE " + table + ".primary_key = " + condition;
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
    }
}
