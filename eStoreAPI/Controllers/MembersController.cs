using BusinessObject.Models;
using eStoreAPI.DTO.Requests.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Members;
using System.Security.Claims;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly PRN231_AS1Context _context;
        private readonly HttpClient client = null;
        private readonly IConfiguration configuration;
        private string ApiPort = "";
        private readonly IMemberRepository repo;
        private string UsernameAdmin = null;
        private string PasswordAdmin = null;
        public MembersController(PRN231_AS1Context context, IMemberRepository repo, IConfiguration configuration)
        {
            _context = context;
            this.repo = repo;
            ApiPort = configuration.GetSection("ApiHost").Value;
            UsernameAdmin = configuration.GetSection("AdminAccount").GetSection("email").Value;
            PasswordAdmin = configuration.GetSection("AdminAccount").GetSection("password").Value;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            return repo.GetMembers();
        }

        [HttpGet]
        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    roleID = Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value),
                    username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    fullname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value

                };
            }
            return null;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<Member>> Login(LoginRequest request)
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = repo.Login(request.username, request.password);
            if (member == null)
            {
                return NotFound();
            }
            return member;

        }
        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMemberById(int id)
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = repo.GetMemberById(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, UpdateMemberRequest request)
        {


            try
            {
                Member member = repo.GetMemberById(id);
                member.CompanyName = request.CompanyName;
                member.Country = request.Country;
                member.Email = request.Email;
                member.City = request.City;
                member.Password = request.Password;
                repo.UpdateMember(member);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(CreateMemberRequest request)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'PRN231_AS1Context.Members'  is null.");
            }
            Member member = new Member
            {
                Password = request.Password,
                Email = request.Email,
                City = request.City,
                Country = request.Country,
                CompanyName = request.CompanyName
            };
            repo.SaveMember(member);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetMember", new { id = member.MemberId }, member);
            return NoContent();
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = repo.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }

            repo.DeleteMember(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
