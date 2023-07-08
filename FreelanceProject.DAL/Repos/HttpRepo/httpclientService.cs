using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceProject.DAL.Repos.HttpRepo
{
    public class HttpclientService:IHttpServiceImplementation
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public HttpclientService()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:5000/predictAPI/");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
        }
      
        public async Task PostImg(IFormFile image)
        {
            var content = new MultipartFormDataContent();
            var imageContent = new StreamContent(image.OpenReadStream());
            content.Add(imageContent, "image");

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/predictAPI");

            request.Content = content;
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);

            return;
        }
    }
}
