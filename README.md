# Preparing development environment
0) Required: Visual Studio 2017 or JetBrains Rider
1) Install and run MongoDb database "https://www.mongodb.com/download-center/community"
2) Clone this repository and open "CFM.sln" in VS or Rider
4) Build the solution and run WebApp windows service (in this case it will be run as a console application)
5) Open the V-Max simulator from any browser "http://localhost:5000/Index.html" and play around
6) Check if all events are persisted in MongoDb "CFM" database (use "https://robomongo.org/" as a MongoDb client)

# Running tests
1) All unit and integration test can be run from VS 2017 or Rider


# Building the production ready version
1) Open a windows console and point it to the "WebApp" project directory
2) Run the following command "dotnet publish -r win10-x64 -c Release"
    * this will build the application for Windows 10 as an X64 application
3) Build output is located in "WebApp\bin\Release\netcoreapp2.1\win10-x64\publish" directory
4) To run the application execute the following command "WebApp.exe" from command line