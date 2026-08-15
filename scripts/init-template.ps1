[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$Name,

    [Parameter(Mandatory = $false)]
    [string]$DisplayName
)

$ErrorActionPreference = 'Stop'

function Write-Step {
    param([string]$Message)

    $script:Step++
    Write-Host ("[{0}/10] {1}" -f $script:Step, $Message)
}

function Fail {
    param([string]$Message)

    throw $Message
}

function Invoke-Checked {
    param(
        [string]$FilePath,
        [string[]]$Arguments,
        [string]$Description,
        [string]$WorkingDirectory = $repoRoot
    )

    Write-Host ("  > {0}" -f $Description)
    & $FilePath @Arguments
    if ($LASTEXITCODE -ne 0) {
        Fail ("{0} failed with exit code {1}." -f $Description, $LASTEXITCODE)
    }
}

function Get-RelativePath {
    param([string]$Path)

    return $Path.Substring($repoRoot.Length).TrimStart('\', '/') -replace '\\', '/'
}

function Get-AbsolutePath {
    param([string]$RelativePath)

    return Join-Path $repoRoot ($RelativePath -replace '/', '\')
}

function Get-Slug {
    param([string]$Value)

    $tokens = [System.Collections.Generic.List[string]]::new()
    $buffer = [System.Text.StringBuilder]::new()

    for ($index = 0; $index -lt $Value.Length; $index++) {
        $character = $Value[$index]

        if ($character -eq '_') {
            if ($buffer.Length -gt 0) {
                $tokens.Add($buffer.ToString())
                $buffer.Clear() | Out-Null
            }
            continue
        }

        if ([char]::IsUpper($character) -and $buffer.Length -gt 0) {
            $previous = $Value[$index - 1]
            $next = if ($index + 1 -lt $Value.Length) { $Value[$index + 1] } else { [char]0 }
            $startsWordAfterLower = [char]::IsLower($previous) -or [char]::IsDigit($previous)
            $startsWordAfterAcronym = [char]::IsUpper($previous) -and [char]::IsLower($next)

            if ($startsWordAfterLower -or $startsWordAfterAcronym) {
                $tokens.Add($buffer.ToString())
                $buffer.Clear() | Out-Null
            }
        }

        $buffer.Append($character) | Out-Null
    }

    if ($buffer.Length -gt 0) {
        $tokens.Add($buffer.ToString())
    }

    return (($tokens | ForEach-Object { $_.ToLowerInvariant() }) -join '-')
}

function Test-ReservedName {
    param([string]$Value)

    $baseName = $Value.Split('.')[0].ToUpperInvariant()
    return $baseName -in @('CON', 'PRN', 'AUX', 'NUL', 'COM1', 'COM2', 'COM3', 'COM4', 'COM5', 'COM6', 'COM7', 'COM8', 'COM9', 'LPT1', 'LPT2', 'LPT3', 'LPT4', 'LPT5', 'LPT6', 'LPT7', 'LPT8', 'LPT9')
}

function Convert-ToTargetPath {
    param([string]$RelativePath)

    $result = $RelativePath
    $result = $result.Replace($sourceIdentity, $namespaceRoot)
    $result = $result.Replace($sourceIdentity.ToLowerInvariant(), $slug)
    $result = $result.Replace($sourceHyphen, $slug)
    return $result
}

function Test-IsIgnoredEnvironmentPath {
    param([string]$RelativePath)

    $normalized = $RelativePath -replace '\\', '/'
    return $normalized -match '(^|/)(\.env|\.env\.[^/]+)$' -and $normalized -notmatch '(^|/)(\.env\.example)$'
}

function Get-TrackedPaths {
    $paths = @(git -C $repoRoot ls-files)
    if ($LASTEXITCODE -ne 0) {
        Fail 'Unable to enumerate tracked files with Git.'
    }
    return $paths | Where-Object { $_ -and -not (Test-IsIgnoredEnvironmentPath $_) }
}

function Test-TextFile {
    param([string]$RelativePath)

    $extension = [System.IO.Path]::GetExtension($RelativePath).ToLowerInvariant()
    $fileName = [System.IO.Path]::GetFileName($RelativePath)
    return $fileName -in @('Dockerfile', '.env.example') -or $extension -in @('.sln', '.csproj', '.props', '.targets', '.cs', '.json', '.yml', '.yaml', '.md', '.tsx', '.ts', '.js', '.scss', '.html', '.ps1')
}

try {
    $script:Step = 0
    $scriptDirectory = (Resolve-Path $PSScriptRoot).Path
    $repoRoot = (Resolve-Path (Join-Path $scriptDirectory '..')).Path

    Write-Step 'Validating repository'
    if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
        Fail 'Git is required but was not found on PATH.'
    }

    $gitRoot = (& git -C $repoRoot rev-parse --show-toplevel).Trim()
    if ($LASTEXITCODE -ne 0 -or -not $gitRoot) {
        Fail 'The initializer must run inside a Git repository.'
    }
    if ((Resolve-Path $gitRoot).Path -ne $repoRoot) {
        Fail 'The initializer must be located at the root of the repository.'
    }

    $markerPath = Join-Path $repoRoot '.template/state.json'
    if (-not (Test-Path -LiteralPath $markerPath -PathType Leaf)) {
        Fail 'The initialization marker .template/state.json is missing.'
    }
    $marker = Get-Content -LiteralPath $markerPath -Raw | ConvertFrom-Json
    if ($null -eq $marker.initialized) {
        Fail 'The initialization marker is malformed.'
    }
    if ([bool]$marker.initialized) {
        $existingName = if ($marker.projectName) { $marker.projectName } else { 'an existing application' }
        Fail ("Repository has already been initialized as {0}." -f $existingName)
    }

    $gitStatus = @(git -C $repoRoot status --porcelain)
    if ($LASTEXITCODE -ne 0) {
        Fail 'Unable to inspect Git working tree status.'
    }
    if ($gitStatus.Count -gt 0) {
        Fail 'The Git working tree must be clean before initialization.'
    }

    Write-Step 'Deriving application identity'
    $Name = $Name.Trim()
    if ($Name.Length -lt 3 -or $Name.Length -gt 80) {
        Fail 'Name must contain between 3 and 80 characters.'
    }
    if ($Name -notmatch '^[A-Za-z][A-Za-z0-9_]*$') {
        Fail 'Name must start with a letter and contain only letters, digits, or underscores.'
    }
    if ($Name.Contains('/') -or $Name.Contains('\') -or $Name.Contains('..')) {
        Fail 'Name must not contain path separators or path traversal.'
    }
    if ($Name.ToCharArray() | Where-Object { [char]::IsControl($_) }) {
        Fail 'Name must not contain control characters.'
    }
    if (Test-ReservedName $Name) {
        Fail 'Name must not be a reserved Windows device name.'
    }

    $DisplayName = if ([string]::IsNullOrWhiteSpace($DisplayName)) { $Name } else { $DisplayName.Trim() }
    if ($DisplayName.Length -lt 1 -or $DisplayName.Length -gt 120) {
        Fail 'DisplayName must contain between 1 and 120 characters.'
    }
    if ($DisplayName.Contains('/') -or $DisplayName.Contains('\') -or ($DisplayName.ToCharArray() | Where-Object { [char]::IsControl($_) })) {
        Fail 'DisplayName must not contain path separators or control characters.'
    }

    $namespaceRoot = $Name
    $solutionName = $Name
    $databaseName = $Name
    $slug = Get-Slug $Name
    $dockerName = $slug
    $packageName = $slug
    if (-not $slug) {
        Fail 'Name could not be converted into a technical slug.'
    }

    Write-Host "  Project name: $namespaceRoot"
    Write-Host "  Display name: $DisplayName"
    Write-Host "  Slug: $slug"

    Write-Step 'Validating rename map'
    $sourceIdentity = 'Mono' + 'lith' + 'Template'
    $sourceLower = $sourceIdentity.ToLowerInvariant()
    $sourceHyphen = 'monolith' + '-template'
    $trackedPaths = @(Get-TrackedPaths)
    $renameSources = [System.Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)

    foreach ($trackedPath in $trackedPaths) {
        $segments = ($trackedPath -replace '\\', '/').Split('/')
        for ($index = 1; $index -lt $segments.Length; $index++) {
            $directory = ($segments[0..($index - 1)] -join '/')
            if ($segments[$index - 1].Contains($sourceIdentity)) {
                $renameSources.Add($directory) | Out-Null
            }
        }
        if ($trackedPath.Contains($sourceIdentity)) {
            $renameSources.Add($trackedPath) | Out-Null
        }
    }

    $renameMap = @($renameSources | ForEach-Object {
        [PSCustomObject]@{
            Source = $_
            Target = Convert-ToTargetPath $_
        }
    })
    $targetGroups = $renameMap | Group-Object Target | Where-Object { $_.Count -gt 1 }
    if ($targetGroups) {
        Fail ("Rename map contains duplicate target paths: {0}" -f (($targetGroups | ForEach-Object Name) -join ', '))
    }
    $sourceSet = [System.Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
    foreach ($source in $renameSources) { $sourceSet.Add($source) | Out-Null }
    foreach ($mapping in $renameMap) {
        $targetPath = Get-AbsolutePath $mapping.Target
        if ((Test-Path -LiteralPath $targetPath) -and -not $sourceSet.Contains($mapping.Target)) {
            Fail ("Rename target already exists: {0}" -f $mapping.Target)
        }
    }

    Write-Step 'Renaming project directories and files'
    $directoryMappings = @($renameMap | Where-Object { Test-Path -LiteralPath (Get-AbsolutePath $_.Source) -PathType Container } | Sort-Object { ($_.Source -split '/').Count } -Descending)
    $directorySources = [System.Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
    foreach ($directoryMapping in $directoryMappings) { $directorySources.Add($directoryMapping.Source) | Out-Null }
    $renamedDirectories = @{}
    foreach ($mapping in $directoryMappings) {
        $currentSource = $mapping.Source
        foreach ($renamed in $renamedDirectories.GetEnumerator()) {
            if ($currentSource.Equals($renamed.Key, [StringComparison]::OrdinalIgnoreCase)) {
                $currentSource = $renamed.Value
            } elseif ($currentSource.StartsWith($renamed.Key + '/', [StringComparison]::OrdinalIgnoreCase)) {
                $currentSource = $renamed.Value + $currentSource.Substring($renamed.Key.Length)
            }
        }
        $currentPath = Get-AbsolutePath $currentSource
        $parent = Split-Path $currentPath -Parent
        $targetLeaf = Split-Path $mapping.Target -Leaf
        $targetPath = Join-Path $parent $targetLeaf
        Move-Item -LiteralPath $currentPath -Destination $targetPath
        $renamedDirectories[$mapping.Source] = Get-RelativePath $targetPath
    }

    $fileMappings = @($renameMap | Where-Object { -not $directorySources.Contains($_.Source) } | Sort-Object { ($_.Source -split '/').Count } -Descending)
    foreach ($mapping in $fileMappings) {
        $currentSource = $mapping.Source
        foreach ($renamed in $renamedDirectories.GetEnumerator()) {
            if ($currentSource.StartsWith($renamed.Key + '/', [StringComparison]::OrdinalIgnoreCase)) {
                $currentSource = $renamed.Value + $currentSource.Substring($renamed.Key.Length)
            }
        }
        $currentPath = Get-AbsolutePath $currentSource
        if (-not (Test-Path -LiteralPath $currentPath -PathType Leaf)) {
            Fail ("Expected source file was not found during rename: {0}" -f $mapping.Source)
        }
        $parent = Split-Path $currentPath -Parent
        $targetLeaf = Split-Path $mapping.Target -Leaf
        Move-Item -LiteralPath $currentPath -Destination (Join-Path $parent $targetLeaf)
    }

    Write-Step 'Updating tracked identity content'
    $textFiles = @($trackedPaths | Where-Object { (Test-TextFile $_) -and -not (Test-IsIgnoredEnvironmentPath $_) })
    foreach ($originalPath in $textFiles) {
        $currentRelativePath = Convert-ToTargetPath $originalPath
        $currentPath = Get-AbsolutePath $currentRelativePath
        if (-not (Test-Path -LiteralPath $currentPath -PathType Leaf)) { continue }

        $content = [System.IO.File]::ReadAllText($currentPath)
        $content = $content.Replace($sourceIdentity, $namespaceRoot)
        $content = $content.Replace($sourceLower, $slug)
        $content = $content.Replace($sourceHyphen, $slug)

        if ($currentRelativePath -eq 'apps/web/src/shared/branding/appIdentity.ts') {
            $escapedDisplayName = $DisplayName.Replace('\', '\\').Replace('"', '\"')
            $content = [regex]::Replace($content, 'export const APP_NAME = "[^"]*";', ('export const APP_NAME = "{0}";' -f $escapedDisplayName))
        }
        if ($currentRelativePath -eq 'apps/web/index.html') {
            $escapedTitle = [System.Net.WebUtility]::HtmlEncode($DisplayName)
            $content = [regex]::Replace($content, '<title>.*?</title>', ('<title>{0}</title>' -f $escapedTitle))
        }
        if ($currentRelativePath -eq 'apps/web/package.json' -or $currentRelativePath -eq 'apps/web/package-lock.json') {
            $nodeScript = @'
const fs = require('fs');
const filePath = process.argv[2];
const packageName = process.argv[3];
const packageJson = JSON.parse(fs.readFileSync(filePath, 'utf8'));
packageJson.name = packageName;
if (packageJson.packages && packageJson.packages['']) {
  packageJson.packages[''].name = packageName;
}
fs.writeFileSync(filePath, JSON.stringify(packageJson, null, 2) + '\n');
'@
            $nodeScript | & node - $currentPath $packageName
            if ($LASTEXITCODE -ne 0) {
                Fail ("Unable to update package identity metadata in {0}." -f $currentRelativePath)
            }
            continue
        }
        if ($currentRelativePath -match 'appsettings(\.Development)?\.json$') {
            $json = $content | ConvertFrom-Json
            $json.SmtpSettings.SenderName = if ($currentRelativePath -match '\.Development\.json$') { "$DisplayName Dev" } else { $DisplayName }
            $json.Jwt.Issuer = "$namespaceRoot.Api"
            $json.Jwt.Audience = "$namespaceRoot.Api"
            $content = $json | ConvertTo-Json -Depth 32
        }
        if ($currentRelativePath -match 'docker-compose\.(dev|staging)\.yml$') {
            $content = $content.Replace("SmtpSettings__SenderName:-$namespaceRoot", "SmtpSettings__SenderName:-$DisplayName")
        }
        if ($currentRelativePath -match 'EmailTemplates/.*\.html$') {
            $content = $content.Replace("<strong>$namespaceRoot</strong>", "<strong>$DisplayName</strong>")
        }
        if ($currentRelativePath -eq 'apps/web/src/components/molecules/AppStateCard/AppStateCard.tsx') {
            $content = $content.Replace('eyebrow = "' + $namespaceRoot + '"', 'eyebrow = APP_NAME')
            if ($content -notmatch 'shared/branding/appIdentity') {
                $content = 'import { APP_NAME } from "../../../shared/branding/appIdentity";' + "`r`n" + $content
            }
        }
        if ($currentRelativePath -eq 'apps/web/src/app/routes/router.test.tsx') {
            $content = $content.Replace('screen.getByText("' + $namespaceRoot + '")', 'screen.getByText(APP_NAME)')
            if ($content -notmatch 'shared/branding/appIdentity') {
                $content = 'import { APP_NAME } from "../../shared/branding/appIdentity";' + "`r`n" + $content
            }
        }

        [System.IO.File]::WriteAllText($currentPath, $content, [System.Text.UTF8Encoding]::new($false))
    }

    Write-Step 'Checking stale identity'
    $staleFiles = Get-ChildItem -LiteralPath $repoRoot -File -Recurse | Where-Object {
        $_.FullName -notmatch '\\.git\\|\\.vs\\|\\bin\\|\\obj\\|\\node_modules\\|\\dist\\|\\coverage\\|\\build\\' -and
        (Test-TextFile (Get-RelativePath $_.FullName))
    }
    $staleMatches = @($staleFiles | Select-String -Pattern $sourceIdentity, $sourceLower, $sourceHyphen -SimpleMatch)
    if ($staleMatches.Count -gt 0) {
        Fail ("Stale template identity remains in: {0}" -f (($staleMatches | ForEach-Object Path | Select-Object -Unique) -join ', '))
    }

    Write-Step 'Validating backend'
    $solutionPath = Get-AbsolutePath "$solutionName.sln"
    Invoke-Checked 'dotnet' @('restore', $solutionPath) 'dotnet restore'
    Invoke-Checked 'dotnet' @('build', $solutionPath) 'dotnet build'
    Invoke-Checked 'dotnet' @('test', $solutionPath) 'dotnet test'

    Write-Step 'Validating frontend'
    Invoke-Checked 'npm' @('--prefix', (Get-AbsolutePath 'apps/web'), 'ci') 'npm ci'
    Invoke-Checked 'npm' @('--prefix', (Get-AbsolutePath 'apps/web'), 'run', 'lint') 'frontend lint'
    Invoke-Checked 'npm' @('--prefix', (Get-AbsolutePath 'apps/web'), 'run', 'test') 'frontend tests'
    Invoke-Checked 'npm' @('--prefix', (Get-AbsolutePath 'apps/web'), 'run', 'build') 'frontend build'

    if (Get-Command docker -ErrorAction SilentlyContinue) {
        Invoke-Checked 'docker' @('compose', '-f', (Get-AbsolutePath 'infra/docker-compose.dev.yml'), 'config') 'docker compose config'
    } else {
        Write-Warning 'Docker was not found; Docker Compose validation was skipped.'
    }

    Write-Step 'Finalizing initialization'
    $finalMarker = [ordered]@{
        initialized = $true
        projectName = $namespaceRoot
        displayName = $DisplayName
    }
    $markerContent = $finalMarker | ConvertTo-Json
    [System.IO.File]::WriteAllText($markerPath, $markerContent + [Environment]::NewLine, [System.Text.UTF8Encoding]::new($false))

    Write-Host ''
    Write-Host 'Initialization completed successfully.'
    Write-Host 'Next steps:'
    Write-Host '  git status'
    Write-Host '  review the diff'
    Write-Host '  git add .'
    Write-Host ("  git commit -m `"Initialize {0} from template`"" -f $namespaceRoot)
    exit 0
}
catch {
    Write-Error ("Initialization failed: {0}" -f $_.Exception.Message)
    exit 1
}
