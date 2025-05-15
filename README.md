![Build and Deploy](https://github.com/timheuer/SimpleFrameworkApp/workflows/Build%20and%20Deploy/badge.svg?branch=master)

# SimpleFrameworkApp
Sample repo for showing how to use GitHub Actions for a .NET Framework app.

Blog post: https://timheuer.com/blog/building-net-framework-apps-using-github-actions/

## Set up on Windows 10 machine
- install git
- install msbuild tools
- install nuget and configure default public nuget source - nuget sources Add -Name 'nuget.org' -Source https://api.nuget.org/v3/index.json
- run nuget restore
- run msbuild -  msbuild .\SimpleFrameworkApp.sln /p:Configuration=Debug
- install IIS
- Configure site and app pool