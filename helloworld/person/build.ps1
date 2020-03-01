$app = "person"
# $tag = Get-Date -Format "yyyy-MM-dd-HH-mm"
$tag = Get-Date -Format "HH-mm"
$image = $app + "/api:" + $tag

docker build -t $image ./src
docker images "$app/*"

Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
