Write-Host ------------------------------------------------
Write-Host Running Application WITH Instana Tracing + HOOKS
Write-Host ------------------------------------------------
$env:CORECLR_ENABLE_PROFILING=1
$env:CORECLR_PROFILER_PATH=(Get-Location).ToString()+"\instana_tracing\CoreRewriter_x64.dll"
$env:CORECLR_PROFILER="{FA8F1DFF-0B62-4F84-887F-ECAC69A65DD3}"
$env:DOTNET_STARTUP_HOOKS=(Get-Location).ToString()+"\Instana.Tracing.Core.dll"
$env:INSTANA_OMIT_CALLSTACK=0
$env:INSTANA_DEBUG_TRACER=0
$env:INSTANA_LOG_SPANS=0
& $Args[0]