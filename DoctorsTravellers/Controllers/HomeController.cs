using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using DoctorsTravellers.Models;
using System.Data;
using System.Reflection;

namespace DoctorsTravellers.Controllers
{
    public class HomeController : Controller
    {

        // This is the cintroller for the home page (DoctorsTravellers.com)
        // looks similar to stackOverFlow.com home page
        // for now HomePage has 4 buttons
        // button 1 : post Question  - should post the question in the database, make a new page for the question and direct user to that page
        //                             should not allow users to post questions if NotFiniteNumberException logged in. if not logged in direct user to log in page        
        // button 2 : search a question - should search the answer and show the result in result page
        // button 3 : sign up  - should take user to the sign up page to enter sign up info like username, new password, email..etc
        // button 4 : Log In  - should take user to log in page and if login successfully take user back to the main page
        // These can be found in Views/Home/HomePage.cshtml
        public ActionResult HomePage()
        {
            // return HomePage.cshtml
            return View();
        }

        public ActionResult LogIn()
        {
            return Content("Log In Clicked");
        }

        public ActionResult SignUp()
        {
            return Content("Sign Up Clicked");
        }

        // TODO: should take in the users question from HomePage.cshtml
        // and put the question on the db.
        // them make a page dynamically for the qurstion and answer discussion
        // like in stackOverFlow.com
        // In stackOverFlow when you ask a question they create a new page for that question
        // example: question - "install gcc in home"
        //          new page -  http://stackoverflow.com/questions/26070784/install-gcc-in-home                
        public ActionResult Ask()
        {
            return Content("Ask Clicked");
        }

        // TODO: take in users search string and check the db for similar questions
        // then return the Result() view
        public ActionResult Search()
        {
            return Content("search Clicked");
        }

        //RESULT page
        public ActionResult Result()
        {
            string test = "Im #pregnant and travelling with a #child and an #elder what #medication should I take with me? we are going to #Ibiza ";//fix ' in text
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
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS response" + qid + " (id INT AUTO_INCREMENT, respondentID VARCHAR(25),respondentType VARCHAR(25), responseText MEDIUMTEXT, PRIMARY KEY (id) )";
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
            string qid = "";
            using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO question_table(Question) VALUES('" + question + "')";
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return qid = GetQID(question);
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
        string GetQID(string question) 
        {
            string result = ""; 
             using (MySqlConnection connection = new MySqlConnection(Config.MyConnectionString))
            {
                 try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT QID From question_table WHERE Question = '" + question + "'";
                    MySqlDataReader rdr = null;
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        result = rdr["QID"].ToString();
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
