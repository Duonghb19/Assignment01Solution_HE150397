using BusinessObject.Models;
using eStoreClient.DTO.Request.Login;
using eStoreClient.DTO.Request.Products;
using eStoreClient.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    [CustomAuthorizationFilter]
    public class ProductsController : Controller
    {
        private readonly PRN231_AS1Context _context;
        private readonly HttpClient client = null;
        private readonly IConfiguration configuration;
        private string ApiPort = "";
        public ProductsController(PRN231_AS1Context context, IConfiguration configuration)
        {
            _context = context;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            this.configuration = configuration;
            ApiPort = configuration.GetSection("ApiHost").Value;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ApiPort + "api/Products");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);
            var session = HttpContext.Session.GetString("loginUser");
            if (session != null)
            {
                UserModel currentUser = JsonSerializer.Deserialize<UserModel>(session);
                ViewData["user"] = currentUser.email;
            }
            return View(listProducts);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync(ApiPort + "api/Products/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var product = JsonSerializer.Deserialize<Product>(strData, options);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId,UnitsinStock,UnitPrice,Weight")] CreateProductRequest request)
        {

            try
            {
                RestClient client = new RestClient(ApiPort);
                var requesrUrl = new RestRequest($"api/Products", RestSharp.Method.Post);
                requesrUrl.AddHeader("content-type", "application/json");
                var body = new Product
                {
                    ProductName = request.ProductName
                ,
                    CategoryId = request.CategoryId
                ,
                    UnitsInStock = request.UnitsInStock
                ,
                    UnitPrice = request.UnitPrice,
                    Weight = request.Weight
                };
                requesrUrl.AddParameter("application/json-patch+json", body, ParameterType.RequestBody);
                var response = await client.ExecuteAsync(requesrUrl);
            }
            catch (Exception)
            {

                throw;
            }


            return RedirectToAction(nameof(Index));
            //}
            //return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }


            HttpResponseMessage response = await client.GetAsync(ApiPort + "api/Products/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var product = JsonSerializer.Deserialize<Product>(strData, options);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitsInStock")] UpdateProductRequest request)
        {
            try
            {
                RestClient client = new RestClient(ApiPort);
                var requesrUrl = new RestRequest($"api/Products/{id}", RestSharp.Method.Put);
                requesrUrl.AddHeader("content-type", "application/json");
                var body = new Product
                {
                    ProductName = request.ProductName
                ,
                    CategoryId = request.CategoryId
                ,
                    UnitsInStock = request.UnitsInStock
                ,
                    UnitPrice = request.UnitPrice,
                    Weight = request.Weight
                };
                requesrUrl.AddParameter("application/json-patch+json", body, ParameterType.RequestBody);
                var response = await client.ExecuteAsync(requesrUrl);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string requestURL = ApiPort + "api/Products/id?id=" + id.ToString();
            HttpResponseMessage response = await client.GetAsync(requestURL);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Product product = JsonSerializer.Deserialize<Product>(strData, options);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                string requestURL = ApiPort + "api/Products/id?id=" + id.ToString();
                HttpResponseMessage response = await client.DeleteAsync(requestURL);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        [HttpPost, ActionName("Search")]
        public async Task<IActionResult> Search(string? SearchByPName)
        {
            HttpResponseMessage response = await client.GetAsync(ApiPort + "api/Products");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var products = JsonSerializer.Deserialize<List<Product>>(strData, options);
            if (SearchByPName != null)
            {
                products = products.Where(x => x.ProductName!.Contains(SearchByPName)).ToList();
            }
            return View("Index", products);
        }
    }
}
