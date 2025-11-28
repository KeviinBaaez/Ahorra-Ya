using AhorraYa.WebClient.ViewModels.Brand;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AhorraYa.WebClient.Controllers
{
    public class BrandsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7284/");
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly string _jwtToken;

        public BrandsController(IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
            _jwtToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImE0NmM1ZmY3LTJkNDktNDNiMS0wZDM1LTA4ZGUyMjJkMzI2MyIsInN1YiI6ImE0NmM1ZmY3LTJkNDktNDNiMS0wZDM1LTA4ZGUyMjJkMzI2MyIsIm5hbWUiOiJhZG1pbiIsImVtYWlsIjoiYWRtaW5AYWRtaW4uY29tIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNzY0MzQ4NDYxLCJleHAiOjE3NjQzNjI4NjEsImlhdCI6MTc2NDM0ODQ2MX0.NpS6OqAzL43YdmJNTsX5sbxN604D7Q5maKZBKkN2AhoEN7xyk7uqBOB9EOs4Kj_bLtbldjC14PHJz3xk6jIfsA";
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? searchText, string? orderBrands)
        {
            List<BrandListVm> list = new List<BrandListVm>();
            //Paso el token de autorización.
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
            //Envió una petición al endpoint y guardo la rta completa del servidor
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Brands/All");

            if(response.IsSuccessStatusCode)//(200 y 299)
            {
                string data = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<BrandListVm>>(data);
            }

            ViewBag.CurrentSearchText = searchText;
            ViewBag.OrderBrands = orderBrands;

            return View(list);
        }
    }
}
