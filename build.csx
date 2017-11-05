#r "nuget:SimpleExec, 1.0.0"

#load "packages/simple-targets-csx.5.3.0/simple-targets.csx"

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static SimpleTargets;
using static SimpleExec.Command;

// version
var versionSuffix = Environment.GetEnvironmentVariable("VERSION_SUFFIX") ?? "";
var buildNumber = Environment.GetEnvironmentVariable("BUILD_NUMBER") ?? "000000";
var buildNumberSuffix = versionSuffix == "" ? "" : "-build" + buildNumber;
var version = File.ReadAllText("src/LiteGuard/LiteGuard.csproj")
    .Split(new[] { "<Version>" }, 2, StringSplitOptions.RemoveEmptyEntries)[1]
    .Split(new[] { "</Version>" }, 2, StringSplitOptions.None).First() + versionSuffix + buildNumberSuffix;

// locations
var logs = "./artifacts/logs";
var output = "./artifacts/output";
var nuget = ".nuget/v4.3.0/NuGet.exe";
var xunitNet452 = "packages/xunit.runner.console.2.3.1/tools/net452/xunit.console.exe";
var xunitNetCoreApp2 = "packages/xunit.runner.console.2.3.1/tools/netcoreapp2.0/xunit.console.dll";

// targets
var targets = new TargetDictionary();

targets.Add("default", DependsOn("pack", "test"));

targets.Add("logs", () => Directory.CreateDirectory(logs));

targets.Add(
    "build",
    DependsOn("logs"),
    () => Run(
        "dotnet", "build", "LiteGuard.sln", "/property:Configuration=Release", "/nologo", "/maxcpucount",
        "/fl", $"/flp:LogFile={logs}/build.log;Verbosity=Detailed;PerformanceSummary", $"/bl:{logs}/build.binlog"));

targets.Add("output", () => Directory.CreateDirectory(output));

targets.Add(
    "pack",
    DependsOn("build", "output"),
    () =>
    {
        foreach (var nuspec in new[] { "./src/LiteGuard/LiteGuard.nuspec", "./src/LiteGuard/LiteGuard.Source.nuspec", })
        {
            Run(nuget, "pack", nuspec, "-Version", version, "-OutputDirectory", output, "-NoPackageAnalysis");
        }
    });

targets.Add(
    "test",
    DependsOn("build"),
    () =>
    {
        var net452 = Path.GetFullPath("tests/LiteGuardTests/bin/Release/net452/LiteGuardTests.dll");
        Run(xunitNet452, net452, "-html", $"{net452}.TestResults.html", "-xml", $"{net452}.TestResults.xml");

        var netcoreApp2 = Path.GetFullPath("tests/LiteGuardTests/bin/Release/netcoreapp2.0/LiteGuardTests.dll");
        Run("dotnet", xunitNetCoreApp2, netcoreApp2, "-html", $"{netcoreApp2}.TestResults.html", "-xml", $"{netcoreApp2}.TestResults.xml");
    });

Run(Args, targets);
