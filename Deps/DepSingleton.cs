using System;

namespace autofac_test.Deps
{
  public class DepSingleton : IDisposable
  {
    public DepSingleton()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepSingleton.Ctor");
    }

    public void DoWork()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepSingleton.DoWork");
    }

    public void Dispose()
    {
      Console.WriteLine($"{this.GetHashCode()} autofac_test.Deps.DepSingleton.Dispose");
    }
  }
}