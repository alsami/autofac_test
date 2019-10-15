using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autofac_test.Deps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace autofac_test.Controllers
{
  [ApiController]
  [Route("test2")]
  public class DependencyResolutionException : ControllerBase
  {
    private readonly IServiceProvider services;
    private readonly DepScoped scoped;
    private readonly DepSingleton singleton;
    private readonly DepPerTenant tenant1;

    public DependencyResolutionException(IServiceProvider services)
    {
      this.services = services;
      this.tenant1 = services.GetService<DepPerTenant>(); ;
      this.singleton = services.GetService<DepSingleton>();
      this.scoped = services.GetService<DepScoped>();
    }

    [HttpGet]
    public IActionResult Get()
    {
      var s1 = this.HttpContext.RequestServices;
      Console.WriteLine($"service provider {this.services.GetHashCode()} {this.services.GetType().Name} vs http context request services {s1.GetHashCode()} {s1.GetType().Name}");

      this.singleton.DoWork();
      this.scoped.DoWork();
      this.tenant1?.DoWork();
      return Ok();
    }
  }
}