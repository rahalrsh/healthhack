using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DoctorsTravellers.Models
{
    public class HomePageServices
    {

        public List<Question> QuestionSearchHandelr(string question)
        {
            MYSQLServices ms = new MYSQLServices();
            List<Question> result = new List<Question>();
            try
            {
                List<int> qidlist = new List<int>();
                List<string> questionlist = new List<string>();
                List<string> tags = GetTags(question);
                string temp = "'";
                string qidliststring = "";

                foreach (string i in tags)
                { temp += i + "','"; }
                temp = temp.Remove(temp.Length - 2, 2);

                qidlist = ms.LoadData("SELECT QID From hash_tag_table WHERE Tag IN(" + temp + ")").OrderBy(i => i.Count()).Select(x => int.Parse(x)).Distinct().ToList();

                foreach (int k in qidlist)
                {

                    qidliststring += k.ToString();
                    if (qidlist[qidlist.Count() - 1] != k)
                        qidliststring += " OR QID = ";

                }

                questionlist = ms.LoadData("SELECT Question From question_table WHERE QID = " + qidliststring);

                int count = 0;
                foreach (int j in qidlist)
                {
                    result.Add(new Question { qid = j, question = questionlist[count] });
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

        public int ResponsePostHandelr(int qid, string response)
        {
            MYSQLServices ms = new MYSQLServices();
            int rid = -1;
            try
            {
                rid = ms.AddToResponseTable(qid, response);


            }
            catch (Exception e) { throw; }
            return rid;
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