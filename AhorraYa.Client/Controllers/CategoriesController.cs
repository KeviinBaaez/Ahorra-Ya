using AhorraYa.Application.Dtos.Category;
using AhorraYa.WebClient.ViewModels.Category;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AhorraYa.WebClient.Controllers
{
    public class CategoriesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7284/");
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly string _jwtToken;

        public CategoriesController(IMapper mapper)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
            _mapper = mapper;
            _jwtToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImE0NmM1ZmY3LTJkNDktNDNiMS0wZDM1LTA4ZGUyMjJkMzI2MyIsInN1YiI6ImE0NmM1ZmY3LTJkNDktNDNiMS0wZDM1LTA4ZGUyMjJkMzI2MyIsIm5hbWUiOiJhZG1pbiIsImVtYWlsIjoiYWRtaW5AYWRtaW4uY29tIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNzY0MTkyMjM1LCJleHAiOjE3NjQyMDY2MzUsImlhdCI6MTc2NDE5MjIzNX0.DjB3HzrqtBDnEoTAoZS14zwVZsbs8FixodgYd-rMyN4re_KwY-PF_8iNncJhE1EGoLWdkCrJew9nTaohB3F3EA";
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchText, string? orderCategories)
        {
            List<CategoryListVm> list = new List<CategoryListVm>();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Categories/All?searchText={searchText}&orderBy={orderCategories}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<CategoryListVm>>(data);
            }

            ViewBag.CurrentSearchText = searchText;
            ViewBag.OrderCategories = orderCategories;

            return View(list);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            if (id is null || id == 0)
            {
                var model = new CategoryEditVm()
                {
                    Id = 0
                };
                return View(model);
            }
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
                int idToFetch = id.Value;
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Categories/GetById?id={idToFetch}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    CategoryRequestDto? categoryDto = JsonConvert.DeserializeObject<CategoryRequestDto>(data);

                    if (categoryDto is null)
                    {
                        return NotFound($"Category With Id {id} Not Found!!");
                    }

                    CategoryEditVm categoryVm = _mapper.Map<CategoryEditVm>(categoryDto);
                    return View(categoryVm);
                }

                return NotFound($"Category With Id {id} Not Found. API status: {response.StatusCode}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CategoryEditVm categoryVm)
        {
            if (ModelState.IsValid)
            {
                CategoryRequestDto categoryRequest = _mapper.Map<CategoryRequestDto>(categoryVm);
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
                    string jsonContent = JsonConvert.SerializeObject(categoryRequest);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response;
                    string succesMessage;
                    if (categoryRequest.Id == 0)
                    {
                        response = await _httpClient.PostAsync("api/Categories/Create", content);
                        succesMessage = "successfully created category";
                    }
                    else
                    {
                        string url = $"api/Categories/Update?id={categoryRequest.Id}";
                        response = await _httpClient.PutAsync(url, content);
                        succesMessage = "successfully update category";
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["success"] = succesMessage;
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        string errorData = await response.Content.ReadAsStringAsync();

                        ModelState.AddModelError("", $"Error {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error {ex.Message}");
                }
            }
            return View(categoryVm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
                int idToFetch = id.Value;
                HttpResponseMessage response;

                response = await _httpClient.GetAsync($"api/Categories/GetById?id={idToFetch}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    CategoryRequestDto? categoryDto = JsonConvert.DeserializeObject<CategoryRequestDto>(data);

                    if (categoryDto is null)
                    {
                        return NotFound($"Category With Id {id} Not Found!!");
                    }

                    CategoryEditVm categoryVm = _mapper.Map<CategoryEditVm>(categoryDto);
                    return View(categoryVm);
                }
                else
                {
                    return NotFound($"Category With Id {id} Not Found. API status: {response.StatusCode}");
                }
            }
            catch (Exception)
            {
                TempData["error"] = "Error while trying to get a category";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
                int idToDelete = id.Value;


                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Categories/Remove?id={idToDelete}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Category deleted correctly";
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    TempData["error"] = errorData;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }

        }
    }
}
