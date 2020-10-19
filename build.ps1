$env:BASYX_REPO = "$(Get-Location)\basyx-packages"
mkdir basyx-packages -erroraction 'silentlycontinue'
Get-ChildItem $env:USERPROFILE\.nuget -Directory -Filter basyx* -Recurse | Remove-Item -force -Recurse
dotnet build .\src\submodules\basyx\sdks\dotnet\basyx-core\BaSyx.Core.sln
dotnet build .\src\submodules\basyx\sdks\dotnet\basyx-components\BaSyx.AAS.Client.Http\BaSyx.AAS.Client.Http.csproj
dotnet build .\src\submodules\basyx\sdks\dotnet\basyx-components\BaSyx.Components.sln

dotnet build Mos.sln