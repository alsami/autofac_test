## requirements

netcore 3.0

## how to run

```
git clone git@github.com:wmaryszczak/autofac_test.git
cd autofac_test
dotnet run
```

## test1

It throws `DependencyResolutionException` while resolving controller contructor's deps.

`action:`
GET http://localhost:5000/test1

`current behaviour:`
unable to resolve dependencies regsistered via MultitenantContainer

`expected behaviour:`
all dependencies are resolved from root container because of `services.AddControllersAsServices()` inside Starup.cs

## test2

Autofac does not respect declared lifecycle on registration phase

`action:`
GET http://localhost:5000/test2

`current behaviour:`
dependencies are resolved explicitly from MultitenantContainer, but its lifetime is not respected. `InstancePerLifetimeScope` dependencies acts as singleton.

`expected behaviour:`
`InstancePerLifetimeScope` is respected, so Dispose must be called after http request finish for deps. resistreder in MultitenantContainer (class: `DepPerTenant`) and in root container (class: `DepScoped`)

## test3

Everything work fine, only because I do not resolve any tenant register dependency

`action:`
GET http://localhost:5000/test3


## hint

Each dependency print its lifecycle on the console output.