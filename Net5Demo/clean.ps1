Write-Host Deactivating .NET Tracer and Agent
$env:CORECLR_ENABLE_PROFILING=0
$env:DOTNET_STARTUP_HOOKS=""

cd DataService
Remove-Item -LiteralPath "publish" -Force -Recurse
Remove-Item -LiteralPath "bin" -Force -Recurse
cd ..
cd UserService
Remove-Item -LiteralPath "publish" -Force -Recurse
Remove-Item -LiteralPath "bin" -Force -Recurse
cd ..
cd TripPlanner
Remove-Item -LiteralPath "publish" -Force -Recurse
Remove-Item -LiteralPath "bin" -Force -Recurse
cd ..

