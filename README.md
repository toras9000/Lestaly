# Lestaly

[![NugetShield]][NugetPackage]

[NugetPackage]: https://www.nuget.org/packages/Lestaly
[NugetShield]: https://img.shields.io/nuget/v/Lestaly

Contains small methods, mainly for shortening code in C# scripts.

```csharp
#r "nuget: Lestaly, 0.9.0"
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using Lestaly;

async ValueTask MainAsync()
{
    var canceller = new CancellationTokenSource();
    using var handler = ConsoleWig.CancelKeyHandlePeriod(canceller);
    await Enumerable.Range(1, 100)
        .Select(n => new IPAddress(stackalloc byte[] { 192, 168, 10, (byte)n, }))
        .ToParallelAsync(parallels: 8, ordered: true, async ip =>
        {
            var reply = await new Ping().SendPingAsync(ip, 2000);
            var time = reply.Status == IPStatus.Success ? $"{reply.RoundtripTime} ms" : "";
            return new { Address = ip, Reply = reply.Address, Status = reply.Status, RoundtripTime = time, };
        }, canceller.Token)
        .SaveToExcelAsync(ThisSource.GetRelativeFile("./list.xlsx").FullName);
}

return await Paved.RunAsync(() => MainAsync(), o => o.PauseOnCancel = true);
```
