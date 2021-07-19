# This file is part of the cv4pve-pepper https://github.com/Corsinvest/cv4pve-pepper,
#
# This source file is available under two different licenses:
# - GNU General Public License version 3 (GPLv3)
# - Corsinvest Enterprise License (CEL)
# Full copyright and license information is available in
# LICENSE.md which is distributed with this source code.
#
# Copyright (C) 2016 Corsinvest Srl	GPLv3 and CEL

[System.Console]::Clear();

Write-Output "
    ______                _                      __
   / ____/___  __________(_)___ _   _____  _____/ /_
  / /   / __ \/ ___/ ___/ / __ \ | / / _ \/ ___/ __/
 / /___/ /_/ / /  (__  ) / / / / |/ /  __(__  ) /_
 \____/\____/_/  /____/_/_/ /_/|___/\___/____/\__/

                                    (Made in Italy)

 =========================================================
 == Build System
 ========================================================="

$pathNet = "Bin\Release\net5.0"

Remove-Item -Path ".\$pathNet"  -Recurse -Force

$rids = @("linux-x64", "linux-arm", "linux-arm64", "osx-x64", "win-x86", "win-x64", "win-arm", "win-arm64")
foreach ($rid in $rids) {
    Write-Output "========================================="
    Write-Output "== $rid"
    Write-Output "========================================="
    dotnet publish -r $rid -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true /p:IncludeNativeLibrariesForSelfExtract=true
    $path = "$pathNet\$rid\publish\"

    $fileName = Get-ChildItem $path -Exclude *.pdb -name
    $fileDest = "$pathNet\$fileName-$rid.zip"
    Remove-Item $fileDest -ErrorAction SilentlyContinue
    Compress-Archive $path\$fileName $fileDest
}