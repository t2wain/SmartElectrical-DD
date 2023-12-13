Import-Module .\SELCmdLets.dll

# AppName provides the default value for other cmdlets
# The ServiceProvider object is stored in session variable SELServiceProvider
New-SELServiceProvider -ConfigFile ".\appSettings.json" -AppName SPEL | Out-Null

# Use default AppName
Get-SELItemNames

# Specify specific AppName
Get-SelItemNames -AppName SPEL
Get-SelItemNames -AppName SPAPLANT
Get-SelItemNames -AppName SPSITE

# Out-SELItemAttribution
Get-SELItemNames -AppName SPEL | Where-Object -Property Name -EQ Motor | Out-SELItemAttribution 
Get-SELItemNames -AppName SPEL | Where-Object -Property Name -EQ Motor | Out-SELItemAttribution -WithSQL

# Out-SELItemAttribution
Get-SELItemNames -AppName SPEL | Where-Object -Property Name -EQ Motor | Out-SELObjectRelation 
Get-SELItemNames -AppName SPEL | Where-Object -Property Name -EQ Motor | Out-SELObjectRelation -WithSQL

# Out-SELImportProject
Out-SELImportProject -FileName ".\Demo.xml" | Out-File -FilePath "c:\temp\Demo.xml.txt"