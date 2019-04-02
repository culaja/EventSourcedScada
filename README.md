# Preparing development environment

1. Install and run MongoDb database: https://www.mongodb.com/download-center/community
2. Install .Net core SDK: https://dotnet.microsoft.com/download
3. Run all domain tests from console: dotnet test CommandSide\Tests
4. Run all query side tests from console: dotnet test QuerySide\Tests
5. Run standalone web server: dotnet run --project WebApp
6. Test the application by visiting UI page: http://localhost/Index.html

# Building the production ready version
1. Open a windows console and point it to the "WebApp" project directory
2. Run the following command "dotnet publish -r win10-x64 -c Release"
    - this will build the application for Windows 10 as an X64 application
3. Build output is located in "WebApp\bin\Release\netcoreapp2.1\win10-x64\publish" directory
4. To run the application execute the following command "WebApp.exe" from command line