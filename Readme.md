Random2Find
===========

Adds random ordering to EPiServer Find's .NET API

### Build

In order to build Random2Find the NuGet packages that it depends on must be restored.
See http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages

### Usage

```c#
client.Search<Document>()
	.OrderRandom()
	.GetResult();
```