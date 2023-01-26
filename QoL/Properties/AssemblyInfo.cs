using System;
using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(FreeCam.BuildInfo.Description)]
[assembly: AssemblyDescription(FreeCam.BuildInfo.Description)]
[assembly: AssemblyCompany(FreeCam.BuildInfo.Company)]
[assembly: AssemblyProduct(FreeCam.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + FreeCam.BuildInfo.Author)]
[assembly: AssemblyTrademark(FreeCam.BuildInfo.Company)]
[assembly: AssemblyVersion(FreeCam.BuildInfo.Version)]
[assembly: AssemblyFileVersion(FreeCam.BuildInfo.Version)]
[assembly: MelonInfo(typeof(FreeCam.Loader), FreeCam.BuildInfo.Name, FreeCam.BuildInfo.Version, FreeCam.BuildInfo.Author, FreeCam.BuildInfo.DownloadLink)]
[assembly: MelonColor()]
[assembly: MelonGame(null, null)]