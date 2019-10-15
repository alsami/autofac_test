using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Multitenant;
using autofac_test.Deps;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace autofac_test
{
  public class Startup
  {
    public ILifetimeScope AutofacContainer { get; private set; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers()
      .AddControllersAsServices()
      ;
      services.AddAutofacMultitenantRequestServices();
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
      builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
      builder.RegisterType<DepSingleton>().AsSelf().SingleInstance();
      builder.RegisterType<DepScoped>().AsSelf().InstancePerLifetimeScope();
      builder.RegisterType<DepOnDemand>().AsSelf().InstancePerDependency();
      builder.Register(c => 
      {
        return new TenantIdentificationStrategy(c.Resolve<IHttpContextAccessor>());
      }).As<ITenantIdentificationStrategy>().SingleInstance();
    }

    internal static MultitenantContainer ConfigureMultitenantContainer(IContainer container)
    {
      Console.WriteLine("ConfigureMultitenantContainer(IContainer container)");
      var strategy = container.Resolve<ITenantIdentificationStrategy>();
      var mtc = new MultitenantContainer(strategy, container);

      mtc.ConfigureTenant("test", tenantBuilder =>
      {
        tenantBuilder.RegisterType<DepPerTenant>().AsSelf().InstancePerLifetimeScope();
      });

      mtc.ConfigureTenant("anx", tenantBuilder =>
      {
        tenantBuilder.RegisterType<DepPerTenant>().AsSelf().InstancePerLifetimeScope();
      });
      return mtc;
    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}