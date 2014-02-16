using System;
using Newtonsoft.Json;
using TWDevAssessmentVSPlugin.Models;

namespace TWDevAssessmentVSPlugin.Services
{
    public class UserService
    {
        private readonly HttpUtility httpUtility;

        public static User User = new User();

        public UserService()
        {
            httpUtility = new HttpUtility();
        }
         
        public bool BeginSessionForTheUser(string sessionId)
        {
            User = new User { Id = sessionId};

            var httpResponseMessage = httpUtility.GetJson(RequestType.GetSelectedQuestion, sessionId);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var userIdResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;
                User = JsonConvert.DeserializeObject<User>(userIdResponse);
            }
            var result = User.Problem != null && User.TimeGiven >= 0;
            return result;
        }

        public bool FreezeAssessment()
        {
            return httpUtility.PostJson(string.Empty, User.Id, RequestType.FreezeCode).IsSuccessStatusCode;
        }

        public void SaveQuestion()
        {
            throw new Exception();
        }
    }
}
