## PowerShell CmdLets for Analyzing SEL Data Dictionaries

This library implements the following PowerShell cmdlets to generate the data from SEL Data Dictionaries:

- New-SELServiceProvider
- Get-SELItemNames
- Out-SELItemAttribution
- Out-SELObjectRelation

These cmdlets are using EntityFramework Core to access the Oracle SEL databases and, therefore, these cmdlets will only run in PowerShell version 7. The cmdlets New-SELServiceProvider will create a ServiceProvider object based on a JSON configuration file (ex. appSettings.json) then save it to a PowerShell session variable SELServiceProvider. The other cmdlets will access the ServiceProvider from the session variable.

These cmdlets may have the following properties:

- ConfigFile: a full file path to the JSON configuration file
- AppName : a value of "SPEL", "SPAPLANT", or "SPSITE" refering the Data Dictionary (ValueFromPipelineByPropertyName)
- WithSQL: a switch that either generate only the item's property names or the full SQL statements
- ItemID: the integer ID value of an Item (ValueFromPipeline, ValueFromPipelineByPropertyName)

Note, the properties AppName and ItemID are pipeline properties. The output of Get-SELItemNames can be piped to Out-SELItemAttribution and Out-SELObjectRelation cmdlets.

## PowerShell CmdLets for Analyzing SEL Import Project File

The cmdlet Out-SELImportProject in this library will parse the SPEL Import project file (.xml) to generate an easier to read text file.

## Debugging custom cmdlets

During development, you can launch the project in the debug mode. You will need to set the debug launch environment as follow:

- Executable: C:\Program Files\PowerShell\7\pwsh.exe
- Command line arguments: -NoProfile -NoExit -Command "Import-Module .\SELCmdLets.dll"

Also, since the DLL project type does not collect all the dll libraries from the reference NuGet packages when compiled, you will need to copy those libraries manually into the bin folder. The strategy is to create a dummy console app and referencing the cmdlets project. Once the console app project is compiled, all the dll libraries from the NuGet packages will be collected in its bin folder. Copy these libraries into the cmdlet project bin folder before lauching it in debug mode.
