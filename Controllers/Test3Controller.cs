using System;
using autofac_test.Deps;
using Microsoft.AspNetCore.Mvc;

namespace autofac_test.Controllers
{
  [ApiController]
  [Route("test3")]
  public class Test3Controller : ControllerBase
  {
    private readonly IServiceProvider services;
    private readonly DepScoped scoped;
    private readonly DepSingleton singleton;

    public Test3Controller(
        IServiceProvider services,
        DepSingleton singleton,
        DepScoped scoped)
    {
      this.singleton = singleton;
      this.scoped = scoped;
      this.services = services;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var s1 = this.HttpContext.RequestServices;
      Console.WriteLine($"service provider {this.services.GetHashCode()} {this.services.GetType().Name} vs http context request services {s1.GetHashCode()} {s1.GetType().Name}");

      this.singleton.DoWork();
      this.scoped.DoWork();
      return Ok();
    }
  }
}
