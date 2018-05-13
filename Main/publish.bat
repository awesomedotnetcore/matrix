cd .\Matrix
nuget pack
xcopy Matrix.*.nupkg C:\Packages /y /MOV
cd ..

cd .\Matrix.Messages
nuget pack
xcopy Matrix.Messages.*.nupkg C:\Packages /y /MOV
cd ..

cd .\Matrix.SDK
nuget pack
xcopy Matrix.SDK.*.nupkg C:\Packages /y /MOV
cd ..

exit