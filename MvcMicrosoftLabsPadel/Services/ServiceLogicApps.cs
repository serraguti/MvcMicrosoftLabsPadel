using MvcMicrosoftLabsPadel.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcMicrosoftLabsPadel.Services
{
    public class ServiceLogicApps
    {
        private IConfiguration configuration;

        public ServiceLogicApps(IConfiguration configuration)
        {
            this.configuration = configuration;
        }   

        public async Task<string> SendMailNewUserAsync(string email, string subject, string body)
        {
            using (HttpClient client = new HttpClient())
            {
                EmailModel model = new EmailModel
                {
                     Email = email, Body = body, Subject = subject
                };
                //CONVERTIMOS A JSON EL MODEL PARA PODER ENVIARLO A LOGIC APPS
                string jsonData = JsonConvert.SerializeObject(model);
                string endPoint =
                    this.configuration.GetValue<string>("LogicApps:SendMailEndPoint");
                StringContent stringContent = new StringContent(jsonData
                    , Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(endPoint, stringContent);
                return response.StatusCode.ToString();
            }
        }
    }
}
