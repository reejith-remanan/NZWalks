using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Text.Json;
using System.Text;

namespace NZWalks.UI.Controllers
{
    public class DifficultyController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DifficultyController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<DifficultyDto> response = new List<DifficultyDto>();

            try
            {
                var client = httpClientFactory.CreateClient();

                var responseMessage = await client.GetAsync("https://localhost:7202/api/Difficulty/Get");


                responseMessage.EnsureSuccessStatusCode();

                response.AddRange(await responseMessage.Content.ReadFromJsonAsync<IEnumerable<DifficultyDto>>());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDifficultyViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7202/api/Difficulty/Create"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Index","Difficulty");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<DifficultyDto>($"https://localhost:7202/api/Difficulty/GetById/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DifficultyDto model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7202/api/Difficulty/Update/{model.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };


            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Difficulty");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DifficultyDto model)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7202/api/Difficulty/Delete/{model.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Difficulty");
            }
            catch (Exception ex)
            {
                //Console 
            }
            return View("Edit");



        }
    }
}
