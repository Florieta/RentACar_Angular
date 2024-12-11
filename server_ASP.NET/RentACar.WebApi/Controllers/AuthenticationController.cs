using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentACar.Api.Logger;
using RentACar.Application.Abstract;
using RentACar.Domain.Entitites;
using RentACar.Domain.Entitites.Identity;
using RentACar.WebApi.ViewModels.Users;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace RentACar.WebApi.Controllers
{
    /// <summary>
    /// All authentication methods
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDealerRepository _repo;
        private readonly IRenterRepository _repoRenter;
        private readonly IApplicationUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, 
            IDealerRepository repo, IUnitOfWork unitOfWork, IRenterRepository repoRenter, IApplicationUserRepository userRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repo = repo;
            _unitOfWork = unitOfWork;
            _repoRenter = repoRenter;
            _userRepo = userRepo;
        }

        /// <summary>
        /// Generate a token for the logged user or returns unauthorized
        /// </summary>
        /// <returns>The user with the token ot unathorize</returns>
        /// <response code="200">Gets the logged user and the token.</response>
        /// <response code="401">Returns an authorized.</response>

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] UserLoginViewModel userLoginModel)
        {
            var user = await _userManager.FindByNameAsync(userLoginModel.UserName);
            var id = await _userManager.GetUserIdAsync(user);

            if (user != null && await _userManager.CheckPasswordAsync(user, userLoginModel.Password))
            {
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userLoginModel.UserName),
                    new Claim("user_id",id)


                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:7016/",
                    claims: authClaims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(authSigInKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    token_id = id,
                    user = user
                });

            }
            return Unauthorized();
        }

        /// <summary>
        /// Create a new user as dealer
        /// </summary>
        /// <param name="userAuthModel"></param>
        /// <returns>The new created user</returns>
        /// <response code="200">Gets the new instance of the created user</response>

        [HttpPost]
        [Route("register/dealer")]
        public async Task<IActionResult> EditDealer([FromBody] UserAuthViewModel userAuthModel)
        {
            var userExists = await _userManager.FindByNameAsync(userAuthModel.UserName);

            if (userExists != null)
                return BadRequest("User is not registered");


            ApplicationUser newUser = new ApplicationUser
            {
                FirstName = userAuthModel.FirstName,
                LastName = userAuthModel.LastName,
                PhoneNumber = userAuthModel.PhoneNumber,
                Email = userAuthModel.Email,
                UserName = userAuthModel.UserName,
            };

            var result = await _userManager.CreateAsync(newUser, userAuthModel.Password);

            Dealer dealer = new Dealer
            {
                CompanyName = userAuthModel.CompanyName,
                CompanyNumber = userAuthModel.CompanyNumber,
                Address = userAuthModel.Address,
                ApplicationUser = newUser,
                ApplicationUserId = newUser.Id
            };
            await _repo.AddAsync(dealer);
            await _unitOfWork.SaveAsync();

            if (!result.Succeeded)
            {
                return BadRequest("Failed to create user");
            }
            return Ok(result);
        }

        /// <summary>
        /// Create a new user as renter
        /// </summary>
        /// <param name="userRenterModel"></param>
        /// <returns>The new created user</returns>
        /// <response code="200">Gets the new instance of the created user</response>

        [HttpPost]
        [Route("register/renter")]
        public async Task<IActionResult> RegisterAsRenter([FromBody] UserRenterViewModel userRenterModel)
        {
            var userExists = await _userManager.FindByNameAsync(userRenterModel.UserName);

            if (userExists != null)
                return BadRequest("User is already registered");


            ApplicationUser newUser = new ApplicationUser
            {
                FirstName = userRenterModel.FirstName,
                LastName = userRenterModel.LastName,
                PhoneNumber = userRenterModel.PhoneNumber,
                Email = userRenterModel.Email,
                UserName = userRenterModel.UserName,
            };

            var result = await _userManager.CreateAsync(newUser, userRenterModel.Password);

            Renter renter = new Renter
            {
                Age = userRenterModel.Age,
                DrivingLicenceNumber = userRenterModel.DrivingLicenceNumber,
                ExpiredDate = userRenterModel.ExpiredDate,
                Address = userRenterModel.Address,
                ApplicationUser = newUser,
                ApplicationUserId = newUser.Id
            };
            await _repoRenter.AddAsync(renter);
            await _unitOfWork.SaveAsync();

            if (!result.Succeeded)
            {
                return BadRequest("Failed to create user");
            }
            return Ok(result);
        }
  
        /// <summary>
        /// Sign in out the user
        /// </summary>
        /// <param name="token">Receives a token from the header.</param>
        /// <returns>Ok</returns>
        /// <response code="200">Returns status ok</response>
        /// 
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout([FromHeader] string token)
        {
            Response.Headers.Remove(token);
            Log.Instance.LogInformation($"User logged out the system.");
            return Ok();
        }
        /// <summary>
        /// Edit dealer user
        /// </summary>
        /// <param name="userDealerModel"></param>
        /// <param name="id"></param>
        /// <returns> The successfully updated object.</returns>
        
        [HttpPut]
        [Route("user/dealer/{id}")]
        public async Task<IActionResult> EditDealerProfile ([FromBody] EditUserDealerModel userDealerModel, string id)
        {
            var userExists = await _userRepo.GetByIdAsync(id);

            if (userExists == null)
                return BadRequest("User is not registered");


            if (userExists != null)
            {
                userExists.FirstName = userDealerModel.FirstName;
                userExists.LastName = userDealerModel.LastName;
                userExists.PhoneNumber = userDealerModel.PhoneNumber;
                userExists.Email = userDealerModel.Email;
                userExists.UserName = userDealerModel.UserName;
            };

            var result = _userRepo.Update(userExists);


            var dealer = await _repo.GetByIdAsync(userDealerModel.DealerId);

            if (dealer != null)
            {
                dealer.CompanyName = userDealerModel.CompanyName;
                dealer.CompanyNumber = userDealerModel.CompanyNumber;
                dealer.Address = userDealerModel.Address;
            };
            await _repo.Update(dealer);
            await _unitOfWork.SaveAsync();

            if (!result.IsCompletedSuccessfully)
            {
                return BadRequest("Failed to update user");
            }
            return Ok(result);
        }

        /// <summary>
        /// Edit dealer user
        /// </summary>
        /// <param name="userRenterModel"></param>
        /// <param name="id"></param>
        /// <returns> The successfully updated object.</returns>

        [HttpPut]
        [Route("user/renter/{id}")]
        public async Task<IActionResult> EditRenterProfile([FromBody] EditUserRenterModel userRenterModel, string id)
        {
            var userExists = await _userRepo.GetByIdAsync(id);

            if (userExists == null)
                return BadRequest("User is not registered");


            if (userExists != null)
            {
                userExists.FirstName = userRenterModel.FirstName;
                userExists.LastName = userRenterModel.LastName;
                userExists.PhoneNumber = userRenterModel.PhoneNumber;
                userExists.Email = userRenterModel.Email;
                userExists.UserName = userRenterModel.UserName;
            };

            var result = _userRepo.Update(userExists);


            var renter = await _repoRenter.GetByIdAsync(userRenterModel.RenterId);

            if (renter != null)
            {
                renter.Age = userRenterModel.Age;
                renter.DrivingLicenceNumber = userRenterModel.DrivingLicenceNumber;
                renter.ExpiredDate = userRenterModel.ExpiredDate;
                renter.Address = userRenterModel.Address;
            };
            await _repoRenter.Update(renter);
            await _unitOfWork.SaveAsync();

            if (!result.IsCompletedSuccessfully)
            {
                return BadRequest("Failed to update user");
            }
            return Ok(result);
        }

    }
}
