$projdir="C:\Dropbox\c#\labs\demoTest\DemoTests"
$msbuild = "c:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" 
#Write-Host $msbuild "$projdir\DemoTests.sln" /T:""DemoTests"" "/property:Configuration=Release"
& $msbuild "$projdir\DemoTests.sln" "/property:OutDir=$projdir\out\" "/property:Configuration=Release"
