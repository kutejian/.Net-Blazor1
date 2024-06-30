using AutoMapper;
using EmailServer.Model;
using IdentityServer.Model;
using LearnBlazorDto.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Niunan.LearnBlazor.WebServer.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utility;

namespace Niunan.LearnBlazor.WebServer.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly EmailServer.Server.IEmailSender _emailSender;
        private readonly Utility.FilesService _fileService;

        public AccountController(UserManager<UserEntity> userManager, IMapper mapper, SignInManager<UserEntity> signInManager, EmailServer.Server.IEmailSender emailSender , FilesService fileService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _fileService = fileService;
        }
        
        
    }
}
