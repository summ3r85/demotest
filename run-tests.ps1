param([string]$browser = "firefox", [string]$category = "all")
$proj_dir = "c:\project\out"
$nunit = "$proj_dir\packages\NUnit.ConsoleRunner.3.6.0\tools\nunit3-console.exe"
$test_dll = "$proj_dir\DemoTests.dll"
if ($category -eq "all")
{ $where = "" }
else 
{ $where = "--where:cat=$category" }
$env:DEMOTEST_BROWSER = $browser

& $nunit $where $test_dll 


