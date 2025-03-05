cd ..
cd Symphony
dotnet build -c Release
cp bin/Release/net8.0/* ../bootstrap/compiler/
cd ..
cd bootstrap