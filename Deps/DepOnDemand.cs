using System;

namespace autofac_test.Deps
{
  public class DepOnDemand : IDisposable
  {
    public DepOnDemand()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepOnDemand.Ctor");
    }

    public void DoWork()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepOnDemand.DoWork");
    }

    public void Dispose()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepOnDemand.Dispose");
    }
  }
}