using System;
using Autofac.Multitenant;
using Microsoft.AspNetCore.Http;

namespace autofac_test
{
  internal class TenantIdentificationStrategy : ITenantIdentificationStrategy
  {
    private IHttpContextAccessor httpContextAccessor;

    public TenantIdentificationStrategy(IHttpContextAccessor httpContextAccessor)
    {
      this.httpContextAccessor = httpContextAccessor;
    }

    public bool TryIdentifyTenant(out object tenantId)
    {
      tenantId = null;
      if(this.httpContextAccessor.HttpContext?.Request?.Query?.TryGetValue("t", out var t) == true)
      {
        tenantId = t;
        return true;
      }
      return tenantId != null;
    }
  }
}