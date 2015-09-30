using ProductSurvey.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;

namespace ProductSurvey.Controllers
{
    [Authorize]
    public class SurveyController : ApiController
    {
        private SurveyDBEntities db = new SurveyDBEntities();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }

        private async Task<SurveyQuestion> NextQuestionAsync(string userId, int SurveyId)
        {
            int lastQuestionId = 0;
            try
            {
                var currentanswers = db.UserAnswers.Where(a => a.UserId == userId && a.SurveyId == SurveyId).OrderByDescending(a => a.LastModifiedDate).Select(a => a.QuestionId).ToList();
                if (currentanswers != null && currentanswers.Count() > 0)
                {
                    lastQuestionId = currentanswers.FirstOrDefault().Value;
                }
            }
            catch (Exception)
            {

            }
            var questionsCount = db.SurveyQuestions.Where(s => s.SurveyId == SurveyId).Count();
            var nextQuestionId = (lastQuestionId % questionsCount) + 1;
            return await this.db.SurveyQuestions.FindAsync(CancellationToken.None, nextQuestionId);
        }

        [Authorize]
        // GET api/Survey
        [ResponseType(typeof(SurveyQuestion))]
        public async Task<IHttpActionResult> Get()
        {
            var userId = User.Identity.GetUserId();

            SurveyQuestion nextQuestion = await this.NextQuestionAsync(userId,1);
            if (nextQuestion == null)
            {
                return this.NotFound();
            }

            return this.Ok(nextQuestion);
        }

        private async Task StoreAsync(UserAnswer answer)
        {
            try
            {
                this.db.UserAnswers.Add(answer);
                await this.db.SaveChangesAsync();
            }
            catch (Exception) { }
        }

        // POST api/Survey
        [ResponseType(typeof(UserAnswer))]
        public async Task<IHttpActionResult> Post(UserAnswer answer)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
            answer.LastModifiedDate = DateTime.Now;
            answer.SurveyId = 1;
            answer.UserEmail = User.Identity.Name;
            answer.UserId = User.Identity.GetUserId();
            answer.UserSurveyId = 0;

            await this.StoreAsync(answer);

            SurveyQuestion nextQuestion = await this.NextQuestionAsync(answer.UserId, 1);
            if (nextQuestion == null)
            {
                return this.NotFound();
            }

            return this.Ok(nextQuestion);
        }
    }
}
