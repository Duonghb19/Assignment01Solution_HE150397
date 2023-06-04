using BusinessObject.Models;
using eStoreClient.DTO.Request.Login;
using eStoreClient.DTO.Request.Members;
using eStoreClient.Filters;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class MembersController : Controller
    {
        private readonly PRN231_AS1Context _context;
        private readonly HttpClient client = null;
        private readonly IConfiguration configuration;
        private string ApiPort = "";
        public MembersController(PRN231_AS1Context context, IConfiguration configuration)
        {
            _context = context;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            this.configuration = configuration;
            ApiPort = configuration.GetSection("ApiHost").Value;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ApiPort + "api/Members");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Member> listMembers = JsonSerializer.Deserialize<List<Member>>(strData, options);
            var session = HttpContext.Session.GetString("loginUser");
            if (session != null)
            {
                UserModel currentUser = JsonSerializer.Deserialize<UserModel>(session);
                ViewData["user"] = currentUser.email;
            }
            return View(listMembers);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync(ApiPort + "api/Members/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var member = JsonSerializer.Deserialize<Member>(strData, options);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }
        [CustomAuthorizationFilter]
        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,Email,CompanyName,City,Country,Password")] CreateMemberRequest request)
        {
            try
            {
                RestClient client = new RestClient(ApiPort);
                var requesrUrl = new RestRequest($"api/Products", RestSharp.Method.Post);
                requesrUrl.AddHeader("content-type", "application/json");
                var body = new Member
                {
                    Email = request.Email,
                    CompanyName = request.CompanyName,
                    City = request.City,
                    Country = request.Country,
                    Password = request.Password
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

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync(ApiPort + "api/Members/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var member = JsonSerializer.Deserialize<Member>(strData, options);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,Email,CompanyName,City,Country,Password")] CreateMemberRequest request)
        {
            try
            {
                RestClient client = new RestClient(ApiPort);
                var requesrUrl = new RestRequest($"api/Member/{id}", RestSharp.Method.Put);
                requesrUrl.AddHeader("content-type", "application/json");
                var body = new Member
                {
                    Email = request.Email,
                    CompanyName = request.CompanyName,
                    City = request.City,
                    Country = request.Country,
                    Password = request.Password
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
        [CustomAuthorizationFilter]
        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            string requestURL = ApiPort + "api/Members/" + id.ToString();
            HttpResponseMessage response = await client.GetAsync(requestURL);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Member member = JsonSerializer.Deserialize<Member>(strData, options);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                string requestURL = ApiPort + "api/Members/" + id.ToString();
                HttpResponseMessage response = await client.DeleteAsync(requestURL);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
