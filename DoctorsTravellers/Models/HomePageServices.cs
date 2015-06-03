using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DoctorsTravellers.Models
{
    public class HomePageServices
    {
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
        public List<string> getUserInfo(int UID)
        {
            List<String> returnStrings = new List<string>();
            MYSQLServices ms = new MYSQLServices();
            try
            {
                returnStrings = ms.getUserInfo(UID);
            }
            catch (Exception e) { throw; }

            return returnStrings;
        }
        public string getUserSpeciality(int UID)
        {

            String speciality;
            MYSQLServices ms = new MYSQLServices();
            try
            {
                speciality = ms.getUserSpeciality(UID);
            }
            catch (Exception e) { throw; }

            return speciality;
        }

        public List<Notice> IsThereNotice(string username){
            MYSQLServices ms = new MYSQLServices();
           List<Notice> notices = new List<Notice>();
           try
           {
               notices = ms.GetNotifications(username);
           
           }
           catch(Exception e){}
           return notices;
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

        public int QuestionPostHandelr(string question, string username)
        {
            int qid = -1;
                 List<String> doctorsUID = new List<string>();
            MYSQLServices ms = new MYSQLServices();
            if(username != null)
            try
            {
                qid = ms.AddToQuestionTable(question,username);
                ms.CreateResponseTable(qid);
                ms.AddToHashtable(qid, question);
                                // when we post the question we should check if there is a doctor that qualifies to answer the question
                // and if we can find a doctor to answer that question we send a notification to that doctor
                doctorsUID = ms.getMatchingDoctors(question);
                if (doctorsUID == null)
                {
                    return qid;
                }
                else
                {
                    var questions = QuestionSearchHandelr(question);
                    string URL = QuestionSearchHandelr(question)[questions.Count - 1].url;
                    ms.AddTONotifications(doctorsUID, URL);
                }

                
            }
            catch (Exception e){
                return qid;
            }
            return qid;
        }

        public int RegisterInfoPostHandler(System.Web.Mvc.FormCollection collection)
        {
            string username = collection.Get("username");
            string password = collection.Get("password1");
            string email = collection.Get("email");
            string type = collection.Get("radio-group-1");
            string speciality = collection.Get("speciality");
            string location = collection.Get("location");
            int qid = -1;
            MYSQLServices ms = new MYSQLServices();
            try
            {
                qid = ms.AddToRegisterInfoTable(username, password, email, type, speciality, location);
                return qid;
            }
            catch (Exception e) { throw; }
            return qid;
        }




    }
}