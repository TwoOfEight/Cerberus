using API.Models.DTOs;
using API.Models.Entities;
using API.Models.Mappers;
using API.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly Repository _repository;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, Repository repository)
    {
        _logger = logger;
        _repository = repository;
    }

}