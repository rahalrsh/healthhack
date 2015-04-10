using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsTravellers.Models
{
    public class HomePageServices
    {
        public void QuestionHandelr(string question)
        {
            MYSQLServices ms = new MYSQLServices();
            try
            {
                string qid = ms.AddToQuestionTable(question);
                ms.CreateResponseTable(qid);
                ms.AddToHashtable(qid, question);
            }
            catch (Exception e) { throw; }
        }

        public List<string> GetTags(string question)
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

    }
}