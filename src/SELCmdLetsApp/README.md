## PowerShell CmdLets for SEL

The purpose of this Console application is to collect all the .NET DLL libraries to its bin directory (after compiled) that are referenced by the SELCmdLets.dll. The project SELCmdLets is compiled to a dll and so it does not collect all the needed dependent libraries in its bin directory.

Note, the powershell script, UseSELCmdLets.ps, demo the typical usage of these custom PowerShell cmdlets.