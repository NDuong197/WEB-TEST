using GoodsManagementMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GoodsManagementMVC.Controllers
{
    public class GoodsController : Controller
    {
        private readonly HttpClient _httpClient;

        public GoodsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5190/api/"); 
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("Goods");
                response.EnsureSuccessStatusCode();
                var goods = await response.Content.ReadFromJsonAsync<List<Goods>>();
                return View(goods ?? new List<Goods>());
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = $"Lỗi khi lấy danh sách hàng hóa: {ex.Message}";
                return View(new List<Goods>());
            }
        }

        public IActionResult Create()
        {
            return View(new Goods());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Goods goods)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";
                return View(goods);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("Goods", goods);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                TempData["Error"] = $"Lỗi khi thêm hàng hóa: {await response.Content.ReadAsStringAsync()}";
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = $"Lỗi kết nối API: {ex.Message}";
            }

            return View(goods);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Error"] = "ID không hợp lệ.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var response = await _httpClient.GetAsync($"Goods/{id}");
                response.EnsureSuccessStatusCode();
                var goods = await response.Content.ReadFromJsonAsync<Goods>();
                if (goods == null)
                {
                    TempData["Error"] = "Không tìm thấy hàng hóa.";
                    return RedirectToAction(nameof(Index));
                }
                return View(goods);
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = $"Lỗi khi lấy hàng hóa: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Goods goods)
        {
            if (id != goods.MaHangHoa || !ModelState.IsValid)
            {
                TempData["Error"] = "Dữ liệu không hợp lệ.";
                return View(goods);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Goods/{id}", goods);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                TempData["Error"] = $"Lỗi khi cập nhật hàng hóa: {await response.Content.ReadAsStringAsync()}";
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = $"Lỗi kết nối API: {ex.Message}";
            }

            return View(goods);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Error"] = "ID không hợp lệ.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var response = await _httpClient.GetAsync($"Goods/{id}");
                response.EnsureSuccessStatusCode();
                var goods = await response.Content.ReadFromJsonAsync<Goods>();
                if (goods == null)
                {
                    TempData["Error"] = "Không tìm thấy hàng hóa.";
                    return RedirectToAction(nameof(Index));
                }
                return View(goods);
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = $"Lỗi khi lấy hàng hóa: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Error"] = "ID không hợp lệ.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"Goods/{id}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                TempData["Error"] = $"Lỗi khi xóa hàng hóa: {await response.Content.ReadAsStringAsync()}";
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = $"Lỗi kết nối API: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}