cd .\Matrix.Agent.Business
nuget pack
xcopy Matrix.Agent.Business.*.nupkg C:\Packages /y /MOV
cd ..

cd .\Matrix.Agent.Configuration
nuget pack
xcopy Matrix.Agent.Configuration.*.nupkg C:\Packages /y /MOV
cd ..

cd .\Matrix.Agent.Database
nuget pack
xcopy Matrix.Agent.Database.*.nupkg C:\Packages /y /MOV
cd ..

cd .\Matrix.Agent.Host
nuget pack
xcopy Matrix.Agent.Host.*.nupkg C:\Packages /y /MOV
cd ..

cd .\Matrix.Agent.Jobs
nuget pack
xcopy Matrix.Agent.Jobs.*.nupkg C:\Packages /y /MOV
cd ..

cd .\Matrix.Agent.Middlewares
nuget pack
xcopy Matrix.Agent.Middlewares.*.nupkg C:\Packages /y /MOV
cd ..

cd .\Matrix.Agent.Model
nuget pack
xcopy Matrix.Agent.Model.*.nupkg C:\Packages /y /MOV
cd ..

exit