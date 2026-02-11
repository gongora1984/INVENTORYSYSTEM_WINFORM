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

# Read JSON and strip comments (simple regex for // comments)
try {
    $rawJson = Get-Content $configPath -Raw
    $cleanJson = $rawJson -replace '(?m)^\s*//.*$', '' -replace '(?m)\s*//.*$', ''
    $json = $cleanJson | ConvertFrom-Json
} catch {
    Write-Host "Error parsing appsettings.json. Please ensure it's valid JSON. Detail: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

if ($null -eq $json.ConnectionStrings) {
    Write-Host "No ConnectionStrings section found in appsettings.json" -ForegroundColor Gray
    exit 0
}

$connString = $json.ConnectionStrings.DefaultConnection

# Encrypt if it looks like a plain connection string (contains server or data source)
if ($connString -and ($connString.ToLower().Contains("server=") -or $connString.ToLower().Contains("data source=") -or $connString.ToLower().Contains("initial catalog="))) {
    Write-Host "Plain text connection string found. Encrypting..." -ForegroundColor Cyan
    $encrypted = Encrypt-String $connString
    $json.ConnectionStrings.DefaultConnection = $encrypted
    
    # Save back to file
    try {
        $json | ConvertTo-Json -Depth 10 | Set-Content $configPath
        Write-Host "Successfully encrypted connection string." -ForegroundColor Green
    } catch {
        Write-Host "Error saving encrypted appsettings.json: $($_.Exception.Message)" -ForegroundColor Red
        exit 1
    }
} else {
    Write-Host "Connection string already encrypted or no plain-text pattern found." -ForegroundColor Gray
}
