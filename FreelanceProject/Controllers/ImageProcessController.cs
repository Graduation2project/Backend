using FreelanceProject.DAL.Repos.HttpRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace FreelanceProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageProcessController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> SendImage(IFormFile image)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                var streamContent = new StreamContent(image.OpenReadStream());
                streamContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(image.ContentType);
                content.Add(streamContent, "image", image.FileName);

                HttpResponseMessage response = await httpClient.PostAsync("http://127.0.0.1:5000/predictAPI", content);
                if (response.IsSuccessStatusCode)
                {
                    return Ok(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    return BadRequest(new { message = "Failed" });
                }
            }
        }
    }
}
