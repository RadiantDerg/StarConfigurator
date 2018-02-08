# StarConfigurator (Windows only)
This simple tool allows a user to quickly set up Steam Workshop mods in Starbound's sbinit.config for dedicated servers.

# Using:
Simply edit the directories in `StarConfigurator.exe.config` to match the install location of Starbound and Steam workshop folder.

If the application doesn't have file permissions, run it as Administrator.

Example: 
	`<add key="StarboundDirectory" value="C:\Program Files (x86)\Steam\steamapps\common\Starbound\win64"/>`
	`<add key="WorkshopFolder" value="C:\Program Files (x86)\Steam\steamapps\workshop\content\211820"/>`
	
	Notes:
	If there's a backslash at the end of your directory (\), delete it. Otherwise, the config file will not work, and Starbound will throw a fit.
	211820 is Starbound's app ID. Always use this workshop folder.