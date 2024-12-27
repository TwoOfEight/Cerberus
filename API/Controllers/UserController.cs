using API.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly Repository _repository;

    public UserController(ILogger<AuthenticationController> logger, Repository repository)
    {
        _logger = logger;
        _repository = repository;
    }

}
