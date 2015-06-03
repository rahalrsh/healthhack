﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorsTravellers.Models
{
    public class Question
    {
        public int qid;
        public string question;
        public List<string> tags;
        public string url;
        public int uid;
        public string tagString;
        public string date;
        public int likes;
        public string type;
        public string name;
        public List<string> GetTags(string question)
        {
            List<string> result = new List<string>();
            string[] temp = question.Split(null);
            foreach (string i in temp)
            {
                if (i.Contains('#'))
                    result.Add(i.Trim('#'));
            }
            return result;
        }



        public int GetQID(string question)
        {
            MYSQLServices ms = new MYSQLServices();
            int result;
            try
            {
                result = Int32.Parse(ms.LoadData("SELECT QID From questions WHERE QuestionscolText = '" + question + "'")[0]);
            }
            catch (Exception) { throw; }
            return result;

        }

        //qid is the table name for responses
        public int GetRID(string response, int qid)
        {
            MYSQLServices ms = new MYSQLServices();
            int result;
            try
            {
                result = Int32.Parse(ms.LoadData("SELECT RID From responses WHERE ResponseText = '" + response + "'")[0]);
            }
            catch (Exception) { throw; }
            return result;

        }
    }
}