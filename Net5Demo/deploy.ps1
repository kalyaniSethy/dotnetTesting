Write-Host Deactivating .NET Tracer and Agent
$env:CORECLR_ENABLE_PROFILING=0
$env:DOTNET_STARTUP_HOOKS=""

cd DataService
Remove-Item -LiteralPath "publish" -Force -Recurse
dotnet publish -c Release -o publish
cp ..\run_full_instana.ps1 .\publish\
cd ..
cd UserService
Remove-Item -LiteralPath "publish" -Force -Recurse
dotnet publish -c Release -o publish
cp ..\run_full_instana.ps1 .\publish\

cd ..
cd TripPlanner
Remove-Item -LiteralPath "publish" -Force -Recurse
dotnet publish -c Release -o publish
cp ..\run_full_instana.ps1 .\publish\
cd ..

