# Ensure execution policy allows script execution
# Ensure execution policy allows script execution
Set-ExecutionPolicy Bypass -Scope Process -Force

# Install Chocolatey
[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072
Invoke-Expression ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
# Add Chocolatey to system PATH
$chocoPath = "C:\ProgramData\chocolatey\bin"
[Environment]::SetEnvironmentVariable("Path", $env:Path + ";$chocoPath", [System.EnvironmentVariableTarget]::Machine)

# Refresh environment variables
$env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")

# Install Git using Chocolatey
choco install git -y

# Add Git to system PATH
$gitPath = "C:\Program Files\Git\cmd"
[Environment]::SetEnvironmentVariable("Path", $env:Path + ";$gitPath", [System.EnvironmentVariableTarget]::Machine)

# Refresh environment variables after adding Git
$env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")

mkdir c:\repo
cd c:\repo

git clone https://github.com/d700qt/SimpleFrameworkApp.git
cd SimpleFrameworkApp

# Install Visual Studio Build Tools 2022
Write-Host "Installing Visual Studio Build Tools 2022..."

# Create a temporary directory for the installer
$tempDir = Join-Path $env:TEMP "VSBuildTools"
New-Item -ItemType Directory -Path $tempDir -Force | Out-Null

# Download Visual Studio Build Tools 2022 web installer
$vsInstallerUrl = "https://aka.ms/vs/17/release/vs_buildtools.exe"
$vsInstallerPath = Join-Path $tempDir "vs_buildtools.exe"
Write-Host "Downloading Visual Studio Build Tools 2022 installer..."
Invoke-WebRequest -Uri $vsInstallerUrl -OutFile $vsInstallerPath

# Run the installer in fully unattended mode
Write-Host "Starting Build Tools installation in quiet mode..."
$logFile = Join-Path $tempDir "vs_install.log"
$arguments = "--quiet --norestart --wait --nocache --installPath `"C:\BuildTools`" --add Microsoft.VisualStudio.Workload.MSBuildTools --add Microsoft.VisualStudio.Workload.NetFramework --add Microsoft.VisualStudio.Workload.WebBuildTools --includeRecommended --force --log `"$logFile`""

$process = Start-Process -FilePath $vsInstallerPath -ArgumentList $arguments -NoNewWindow -Wait -PassThru
Write-Host "Installer exited with code: $($process.ExitCode)"

# Add MSBuild to system PATH if installation succeeded
if ($process.ExitCode -eq 0 -or $process.ExitCode -eq 3010) {
    $msbuildPath = "C:\BuildTools\MSBuild\Current\Bin"
    [Environment]::SetEnvironmentVariable("Path", $env:Path + ";$msbuildPath", [System.EnvironmentVariableTarget]::Machine)
    Write-Host "Visual
