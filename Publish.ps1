$siteName = "IAR"
$sitePath = "\\appbi\e$\inetpub\$siteName"
# If site name is not the same as the site folder, use the following line instead
# $sitePath = "\\appbi\e$\inetpub\IAR"

Write-Output "Stopping $siteName"

# https://learn.microsoft.com/en-us/powershell/module/webadministration/stop-webitem
invoke-command -ComputerName appbi -ScriptBlock {
    import-module WebAdministration
    Stop-WebItem -PSPath "IIS:\Sites\$using:siteName"
}

# Ask for confirmation before deleting files
$confirm = Read-Host "`nAre you sure you want to delete $sitePath\*? (y/n)"
if ($confirm -ne "y") {
    Write-Output "`nRestarting $siteName"
}
else {
    Write-Output "`nRemoving $sitePath\*"

    Remove-Item $sitePath\* -Recurse -Force

    Write-Output "`nPublishing to $sitePath`n"

    dotnet publish -c Release -o $sitePath

    Write-Output "`nStarting $siteName"
}

# https://learn.microsoft.com/en-us/powershell/module/webadministration/start-webitem
invoke-command -ComputerName appbi -ScriptBlock {
    import-module WebAdministration
    Start-WebItem -PSPath "IIS:\Sites\$using:siteName"
}