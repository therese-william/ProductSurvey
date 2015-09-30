using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductSurvey.EntityModel
{
    public partial class SurveyQuestion
    {
        public List<SurveyOption> QuestionOptions {
            get 
            {
                List<SurveyOption> options = new List<SurveyOption>();
                using (SurveyDBEntities db = new SurveyDBEntities())
                {
                    options = db.SurveyOptions.Where(o => o.SurveyId == this.SurveyId).ToList();
                }
                return options;
            } 
        }
    }
}