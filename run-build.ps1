$output = "C:\Project\out\"
$msbuild = "c:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" 
#Write-Host $msbuild "$projdir\DemoTests.sln" /T:""DemoTests"" "/property:Configuration=Release"
& $msbuild "DemoTests.sln" "/property:OutDir=$output" "/property:Configuration=Release"
