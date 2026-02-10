param([string]$publishDir)

# Clean up path (handles trailing backslash escaping quotes in MSBuild)
$publishDir = $publishDir.TrimEnd('"').TrimEnd('\')

$configPath = Join-Path $publishDir "appsettings.json"
if (-not (Test-Path $configPath)) {
    Write-Host "appsettings.json not found at $configPath" -ForegroundColor Yellow
    exit 0
}

Write-Host "Encrypting connection string in $configPath..."

# AES Key Logic (must match SecurityService.cs)
$keyStr = "InvSys_Secret_Key_2024_@!"
$keyBytes = [System.Text.Encoding]::UTF8.GetBytes($keyStr)
$finalKey = New-Object Byte[] 32
[System.Array]::Copy($keyBytes, $finalKey, [System.Math]::Min($keyBytes.Length, 32))

function Encrypt-String($plainText) {
    if ([string]::IsNullOrEmpty($plainText)) { return $plainText }
    
    $aes = [System.Security.Cryptography.Aes]::Create()
    $aes.Key = $finalKey
    $aes.IV = New-Object Byte[] 16 # All zeros IV matches C#
    
    $encryptor = $aes.CreateEncryptor()
    $memoryStream = New-Object System.IO.MemoryStream
    $cryptoStream = New-Object System.Security.Cryptography.CryptoStream($memoryStream, $encryptor, [System.Security.Cryptography.CryptoStreamMode]::Write)
    $writer = New-Object System.IO.StreamWriter($cryptoStream)
    
    $writer.Write($plainText)
    $writer.Close()
    $cryptoStream.Close()
    
    $bytes = $memoryStream.ToArray()
    $memoryStream.Close()
    return [Convert]::ToBase64String($bytes)
}

# Read JSON
$json = Get-Content $configPath -Raw | ConvertFrom-Json
$connString = $json.ConnectionStrings.DefaultConnection

# Encrypt if it looks like a plain connection string
if ($connString -and $connString.ToLower().Contains("server=")) {
    $encrypted = Encrypt-String $connString
    $json.ConnectionStrings.DefaultConnection = $encrypted
    $json | ConvertTo-Json -Depth 10 | Set-Content $configPath
    Write-Host "Successfully encrypted connection string." -ForegroundColor Green
} else {
    Write-Host "Connection string already encrypted or missing." -ForegroundColor Gray
}
