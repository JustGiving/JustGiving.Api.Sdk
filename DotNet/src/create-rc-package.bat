C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe /m:8 /p:Configuration=Release "JustGiving.Api.Sdk.sln"
.nuget\nuget pack justgiving-sdk-rc.1.6.13.nuspec
.nuget\nuget push justgiving-sdk-rc.1.6.13.nupkg %NugetApiKey% -Source https://api.nuget.org/v3/index.json