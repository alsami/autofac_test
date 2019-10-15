using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autofac_test.Deps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace autofac_test.Controllers
{
  [ApiController]
  [Route("test1")]
  public class Test1Controller : ControllerBase
  {
    private readonly IServiceProvider services;
    private readonly DepScoped scoped;
    private readonly DepSingleton singleton;
    private readonly DepPerTenant tenant1;

    public Test1Controller(
        IServiceProvider services,
        DepSingleton singleton,
        DepPerTenant tenant1,
        DepScoped scoped)
    {
      this.tenant1 = tenant1;
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
      this.tenant1?.DoWork();
      return Ok();
    }
  }
}
