# Demo Project
To run this project, you must first add the `BotFramework` package by following the guidelines below:
## Installing the package
1. Authenticate to GitHub Packages. For more information, see "[Authenticating to GitHub Packages.](https://help.github.com/en/packages/using-github-packages-with-your-projects-ecosystem/configuring-dotnet-cli-for-use-with-github-packages#authenticating-to-github-packages)".
2. To use a package, add `ItemGroup` and configure the `PackageReference` field in the .csproj project file, replacing the version number with the version you want to use: 
```
    <Project Sdk="Microsoft.NET.Sdk">
    
    ...

    <ItemGroup>
      <PackageReference Include="BotFramework" Version="1.1.0" />
    </ItemGroup>

   </Project>
```
3. Install the packages with the `restore` command. 
```
dotnet restore
```
## Run the project
After you have installed the package you can run the project. You can test using the [Hubtel USSD Mocker](https://github.com/hubtel/ussd-mocker) using the route `http://your-host/webhook/hubtelussd`;
