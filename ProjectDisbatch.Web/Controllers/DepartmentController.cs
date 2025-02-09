using Microsoft.AspNetCore.Mvc;
using ProjectDisbatch.Web.Models;
using ProjectDisbatch.Web.Models.DTO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace ProjectDisbatch.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DepartmentController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<DepartmentDto> response = new List<DepartmentDto>();

            try
            {
                //Get All Departments from Web API
                var client = httpClientFactory.CreateClient(); //We use this client to consume API

                //Ideally the URI should be coming from appsettings where it has settings for dev and production URIs
                var httpResponseMessage = await client.GetAsync("https://localhost:7158/api/department");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<DepartmentDto>>());

                ViewBag.Response = response;
            }
            catch (Exception ex)
            {
                //Log the exception
                throw;
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDepartmentViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7158/api/department"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<DepartmentDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Department");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<DepartmentDto>($"https://localhost:7158/api/department/{id}");


            //I think we can just combine these into one. If response is null then we'd just be passing back null to view anyway
            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        //TODO Why is this HttpPost, shouldn't it be HttpPut (or patch) ? -> I think its because form method is POST in cshtml
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7158/api/department/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<DepartmentDto>();

            if (response is not null)
            {
                //Redirect to GET method so it gets the updated data from the API
                return RedirectToAction("Edit", "Department");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7158/api/department/{request.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Department");
            }
            catch (Exception ex)
            {
                //Console
            }

            return View("Edit");
        }
    }
}
