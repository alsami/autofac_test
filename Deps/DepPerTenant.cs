using System;

namespace autofac_test.Deps
{
  public class DepPerTenant : IDisposable
  {
    public DepPerTenant()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepPerTenant.Ctor");
    }

    public void DoWork()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepPerTenant.DoWork");
    }

    public void Dispose()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepPerTenant.Dispose");
    }
  }
}