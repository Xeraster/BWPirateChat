# BWPirateChat
A server plugin for Blackwake dedicated server that converts everyone's chat into pirate language

Installation:

put "BWPirateChat.dll" into BlackwakeServer_Data/Managed/Mods

put wordreplacement.yarrml at the root of your Blackwake server directory which is the same directory "BlackwakeServer.exe" is in

Make sure you are using the LATEST VERSION of da_google's mod loader:

https://github.com/dagoogle/BWModLoader/branches


Permissions explained:

The commands in this mod have permissions you can set. All the permissions are stored in BWPirateChat.cfg which is created on startup and filled with default values if the mod detects that it's not present.

A permission level of 0 means admins, moderators and normal users can access a command.

A permission level of 1 means only admins and moderators can access a command.

permission level of 2 means only admins have access to a command.

Note that you do not need to restart the server for any changes to permissions to take effect. If the server does restart, any values or changes to permissions that you made will persist when it turns back on.


Usage:

This mod has commands which I will list here with a description of what they do


!piratespeak <arg1>

	description: allows you to toggle pirate speak on or off. !piratespeak 0 turns it off, !piratespeak 1 turns it on


!piratespeak_perms <arg1>

	description: set permissions for execution of the !piratespeak command to either user, moderator or admin with the values 0, 1 or 2 respectively. The default value for this permission is moderator.
    example: !piratespeak_perms 1

	the above command sets it so that you have to be at least a moderator have permission to use the !piratespeak command.


!reloaddict

	description: reloads the dictionary file. This is useful if you want to make changes to the dictionary file and apply them without restarting the server

	The default permission level for this command is 2 which is for admins only


!reloaddict_perms <arg1>

	description: set permissions for execution of the !reloaddict command. Valid inputs are 0, 1 and 2 or user, moderator and admin respectively

	example: !reloaddict_perms 1


!impersonate <arg1> <arg2>

	description: allows you to "impersonate" a user in the chat box. It's not very useful but I thought it would be funny.

	arg1 is the name of the user you want to impersonate. If the name you want to impersonate has spaces, you will need to enclose it in quote

	arg2 is the message you want to use

	the defualt permission level for this command is 1 which means moderators and admins can use it

	example: !impersonate "Windows Vista" Windows XP is the coolest guy in all of blackwake.


!impersonate_perms <arg1>

	description: set permissions of the impersonate command

	example: !impersonate_perms 2

	the above command makes it so that only admins can use the !impersonate command


!perms BWPirateChat <arg1>

	description: sets the permissions for setting permissions in the PirateChat mod. Only admins have access to this command

	The default value is 2 and it is highly recommended you leave it at 2.

	example: !perms BWPirateChat 1
	
	the above command sets it so that moderators have access to changing permission of PirateChat commands. 