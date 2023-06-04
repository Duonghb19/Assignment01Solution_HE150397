using BusinessObject.Models;
using eStoreClient.DTO.Request.Login;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly PRN231_AS1Context _context;
        private readonly HttpClient client = null;
        private readonly IConfiguration configuration;
        private string ApiPort = "";
        private string UsernameAdmin = null;
        private string PasswordAdmin = null;
        public LoginController(PRN231_AS1Context context, IConfiguration configuration)
        {
            _context = context;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            this.configuration = configuration;
            ApiPort = configuration.GetSection("ApiHost").Value;
            UsernameAdmin = configuration.GetSection("AdminAccount").GetSection("email").Value;
            PasswordAdmin = configuration.GetSection("AdminAccount").GetSection("password").Value;
        }

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //private UserModel GetCurrentUser()
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;
        //    if (identity != null)
        //    {
        //        var userClaims = identity.Claims;
        //        return new UserModel
        //        {
        //            roleID = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value),
        //            username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
        //            fullname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
        //            email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value

        //        };
        //    }
        //    return null;
        //}

        //[AllowAnonymous]
        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            UserModel currentUser;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            if (request.username.Equals(UsernameAdmin) && request.password.Equals(PasswordAdmin))
            {
                currentUser = new UserModel
                {
                    email = request.username,
                    password = request.password,
                    roleID = 1

                };
                var loginUser = JsonSerializer.Serialize(currentUser);
                HttpContext.Session.SetString("loginUser", loginUser);
                ViewData["user"] = currentUser.email;
                return RedirectToAction("Index", "Home");

            }
            else
            {
                try
                {
                    RestClient client = new RestClient(ApiPort);
                    var requesrUrl = new RestRequest($"api/Members/Login", RestSharp.Method.Post);
                    requesrUrl.AddHeader("content-type", "application/json");
                    var body = new LoginRequest
                    {
                        username = request.username,
                        password = request.password
                    };

                    requesrUrl.AddParameter("application/json-patch+json", body, ParameterType.RequestBody);
                    var response = await client.ExecuteAsync(requesrUrl);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var member = JsonSerializer.Deserialize<Member>(response.Content, options);
                        currentUser = new UserModel
                        {
                            email = member.Email,
                            password = member.Password,
                            roleID = 2

                        };
                        var loginUser = JsonSerializer.Serialize(currentUser);
                        HttpContext.Session.SetString("loginUser", loginUser);
                        ViewData["user"] = currentUser.email;
                        return RedirectToAction("Index", "Home");
                    }

                }
                catch (Exception)
                {

                    throw;
                }

            }

            return Ok("Tai khoan hoac mat khau ban nhap sai !!!");
        }
        // GET: LoginController/Details/5

    }
}
