kubectl apply -f fluentd-rbac.yaml
kubectl apply -f fluentd-deamonset.yaml

# Write-Host -NoNewLine 'Press any key to continue...';
# $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
