using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsTravellers.Models
{
    public class ResponsePageServices
    {
        public Question GetQuestion(int qid)
        {
            try
            {
                MYSQLServices ms = new MYSQLServices();
                List<string> temp = new List<string>();
                temp = ms.LoadData(" SELECT questions.*,register_info.type,register_info.username From questions INNER JOIN register_info ON register_info.UID = questions.UID  WHERE QID = " + qid);
                string[] fields = temp[0].Split('%');
                Question result = new Question() { qid = qid, uid = Int32.Parse(fields[1]), tagString = fields[2], date = fields[3], likes = Int32.Parse(fields[4]), question = fields[2], type = fields[5], name = fields[6] };
                return result;
            }
            catch (Exception e) { }
            return new Question();

        }
        public List<Response> GetResponses(int qid, bool mostpopular)
        {
            List<Response> result = new List<Response>();
            try
            {
 
                List<string> temp = new List<string>();
                MYSQLServices ms = new MYSQLServices();
                if(!mostpopular)
                    temp = ms.LoadData("SELECT responses.*,register_info.type,register_info.username From responses INNER JOIN register_info ON register_info.UID = responses.UID WHERE QID = " + qid+ " ORDER BY RDate DESC        ");
                else
                    temp = ms.LoadData("SELECT responses.*,register_info.type,register_info.username From responses INNER JOIN register_info ON register_info.UID = responses.UID WHERE QID = " + qid +" ORDER BY RLikes DESC");
                foreach (string i in temp)
                {
                    string[] fields = i.Split('%');
                    Response response = new Response() { rid = Int32.Parse(fields[0]), qid = qid, uid = Int32.Parse(fields[2]), responseText = fields[3], date = fields[4], likes = Int32.Parse(fields[5]), type = fields[6], username = fields[7] };
                    result.Add(response);
                }
            }
            catch (Exception e) { throw; }
            return result;

        }

        public void LikesUpdate(int rid, int likes) 
        {
            try
            {
                MYSQLServices ms = new MYSQLServices();
                string command = "UPDATE responses SET RLikes ='" + likes + "' WHERE RID = '"+rid+"' ";
                ms.SendCommand(command);

            }
            catch (Exception e) { throw; }
        }
    }
}