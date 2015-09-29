using magic8ball.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace magic8ball.Controllers
{

    

    [RoutePrefix("api/answer")]
    public class AnswerController : ApiController
    {

        private readonly Encoding _encoding = new UTF8Encoding();
        

        public string returnRandomAsw()
        {
            string answer;
            Random random = new Random();
            string[] responses = { "No", "Yes", "Maybe", "It's not certain" };

            answer = responses[random.Next(responses.Length)];

            return answer;
        }



        [HttpGet]
        public async  Task<HttpResponseMessage> Get()
        {

            string randomAns = returnRandomAsw();

            HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.OK);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);


            
            var slackChannel = Request.RequestUri.ParseQueryString().Get("channel_id");
            var slackText = Request.RequestUri.ParseQueryString().Get("text");
            var userName = Request.RequestUri.ParseQueryString().Get("user_name");
            var userId = Request.RequestUri.ParseQueryString().Get("user_id");

             response.Content = new StringContent(slackChannel, Encoding.Unicode);

            var user = "<@" + userId + "|" + userName + ">";

            using (var client = new HttpClient())
            {
                      
                //Attachments attachments = new Attachments { Fallback = user + ": " + slackText + "\r\n magic8ball: " + randomAns, Color = "#000" , AuthorName = user , Title = "Magic 8 Ball" ,  Text = slackText + "\r\n magic8ball: " + randomAns };
                 Payload values = new Payload { Channel = slackChannel, Text =  user + ": " + slackText + "\r\n magic8ball: " + randomAns};
                //Payload values = new Payload { Attachment = attachments, Channel = slackChannel };
               
                var responsegf = client.PostAsync("https://hooks.slack.com/services/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",
                                                    new StringContent(JsonConvert.SerializeObject(values).ToString(),
                                        Encoding.UTF8, "application/json")).Result;

                response.Content = new StringContent(values.Attachment.Text, Encoding.Unicode);
            }

            

            return response;


        }

        //Post a message using simple strings
        //public void PostMessage(string text, string username = null, string channel = null)
        //{
        //    Payload payload = new Payload()
        //    {
        //        Channel = channel,
        //        Username = username,
        //        Text = text
        //    };

        //    PostMessage(payload);
        //}

        //public void PostMessage(Payload payload)
        //{
        //   // var path = process.env.INCOMING_WEBHOOK_PATH;
        //    Uri _uri = new Uri("https://hpegdpmsp.slack.com/services/hooks/incoming-webhook?token=B5wHeJME9nwMZ3y1yj7Z6u3d");

        //    string payloadJson = JsonConvert.SerializeObject(payload);

        //    using (WebClient client = new WebClient())
        //    {
        //        NameValueCollection data = new NameValueCollection();
        //        data["payload"] = payloadJson;

        //        var response = client.UploadValues(_uri, "POST", data);

        //        //The response text is usually "ok"
        //        string responseText = _encoding.GetString(response);
        //    }
        //}
    }




}
