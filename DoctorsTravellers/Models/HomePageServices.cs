using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DoctorsTravellers.Models
{
    public class HomePageServices
    {
        public int RegisterInfoPostHandler(System.Web.Mvc.FormCollection collection)
        {
            string username = collection.Get("username");
            string password = collection.Get("password1");
            string email = collection.Get("email");
            string type = collection.Get("radio-group-1");
            string speciality = collection.Get("speciality");

            int qid = -1;
            MYSQLServices ms = new MYSQLServices();
            try
            {
                qid = ms.AddToRegisterInfoTable(username, password, email, type, speciality);
                return qid;
            }
            catch (Exception e) { throw; }
            return qid;
        }

        public int CheckIfRegisteredUserHandler(System.Web.Mvc.FormCollection collection)
        {
            int returnId = -1;
            string username = collection.Get("username");
            string password = collection.Get("password");

            MYSQLServices ms = new MYSQLServices();
            try
           {
             returnId = ms.CheckIfRegisteredUserHandler(username, password);
           }
           catch (Exception e) { throw; }

            return returnId;
        }

        public List<Question> QuestionSearchHandelr(string question)
        {
            MYSQLServices ms = new MYSQLServices();
            List<Question> result = new List<Question>();
           
            try
            {
                Question qhelp = new Question();
                List<int> qidlist = new List<int>();
                List<string> questionlist = new List<string>();
                List<string> tags = qhelp.GetTags(question);
                List<string> questionwords = question.Split(' ').ToList<string>();
                //if (tags.Count == 0) { return result; }
                string temp = "'";
                string qidliststring = "";

                foreach (string i in questionwords)
                { temp += i.Trim('#') + "','"; }
                temp = temp.Remove(temp.Length - 2, 2);

                var test = ms.LoadData("SELECT hash_tag_table.QID, questions.QuestionscolText  From hash_tag_table  INNER JOIN questions ON questions.QID = hash_tag_table.QID WHERE hash_tag_table.Tag IN(" + temp + ")");


                int count = 0;
                if (test.Count == 0) { return result; }
                foreach (string j in test)
                {
                    result.Add(new Question { question = j.Split('%')[1], qid = Int32.Parse(j.Split('%')[0]), url = "/ResponsePage/ResponsePageLoad/" + Int32.Parse(j.Split('%')[0]) + "/" + string.Join("-", qhelp.GetTags(j.Split('%')[1]).ToArray()) });
                    count++;
                }

            }
            catch (Exception e) { throw; }
            return result;
        }

        public int QuestionPostHandelr(string question)
        {
            int qid = -1;
            MYSQLServices ms = new MYSQLServices();
            try
            {
                qid = ms.AddToQuestionTable(question);
                ms.CreateResponseTable(qid);
                ms.AddToHashtable(qid, question);
                return qid;
            }
            catch (Exception e) { throw; }
            return qid;
        }





    }
}