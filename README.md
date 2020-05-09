# BotFramework
TBD
## Publishing the package
1. Update package version number in `BotFramework.csproj` file.
1. Package the project: 
```
dotnet pack --configuration Release
```
1. Publish the package to GitHub package repository:
```
dotnet nuget push "bin/Release/BotFramework.<VERSION_NUMBER>.nupkg" --source "github"
```

## Installing the package
1. Authenticate to GitHub Packages. For more information, see "[Authenticating to GitHub Packages.](https://help.github.com/en/packages/using-github-packages-with-your-projects-ecosystem/configuring-dotnet-cli-for-use-with-github-packages#authenticating-to-github-packages)".
1. To use a package, add `ItemGroup` and configure the `PackageReference` field in the .csproj project file, replacing the version number with the version you want to use: 
```
    <Project Sdk="Microsoft.NET.Sdk">
    
    ...

    <ItemGroup>
      <PackageReference Include="BotFramework" Version="1.0.0" />
    </ItemGroup>

   </Project>
```
1. Install the packages with the `restore` command. 
```
dotnet restore
```
