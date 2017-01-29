param([string]$browser = "firefox", [string]$category = "all")
$proj_dir = "c:\Dropbox\c#\labs\demoTest\DemoTests"
$nunit = "$proj_dir\packages\NUnit.ConsoleRunner.3.6.0\tools\nunit3-console.exe"
$test_dll = "$proj_dir\out\DemoTests.dll"
if ($category -eq "all")
{ $where = "" }
else 
{ $where = "--where:cat=$category" }
$env:DEMOTEST_BROWSER = $browser

& $nunit $where $test_dll 


