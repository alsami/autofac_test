using System;

namespace autofac_test.Deps
{
  public class DepScoped : IDisposable
  {
    public DepScoped()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepScoped.Ctor");
    }

    public void DoWork()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepScoped.DoWork");
    }

    public void Dispose()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepScoped.Dispose");
    }
  }
}