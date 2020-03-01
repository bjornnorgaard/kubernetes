$TOKEN = ((kubectl -n kube-system describe secret default | Select-String "token:") -split " +")[1]

kubectl config set-credentials docker-for-desktop --token="${TOKEN}"

$ENV:KUBECONFIG = "$ENV:KUBECONFIG;$HOME\.kube\config"

kubectl config view

$null = Read-Host -Prompt 'Press any key to continue...'