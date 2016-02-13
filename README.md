# Virtual-Wifi
An alternative for windows users using Connectify.
Virtual Wi-Fi is basically written in C#. It’s been constructed with a few C# Projects as building blocks from many sources. It basically uses Win32 API and other API's to execute a command (Windows command) which creates hotspot. Windows ICS (Internet Connection Sharing) API is used to provide a user interface in selecting type of connection to be shared.

	To Run the application browse to “\Virtual-Wifi\Virtual wifi\bin\Debug“ and double click the Virtual wifi.exe file.


Before Starting/Running the program. 
•	Set it to "Run as Administrator". By opening properties of the application Virtual wifi.exe Switch to Compatibility tab under privilege Level, check to “Run this program as Administrator”


To share internet through Virtual Wifi while connected to a WiFi source (This Feature is tested only on Win 10) 
•	Open Control panel->Network and sharing->Change Adapter settings->Wireless network connection 
•	In sharing tab check "Allow other network users to connect through the computer connection" 
•	Then select Virtual Wi-Fi in Home network connection 


To share internet through Virtual Wifi while connected to internet through Wired cables (Local Area Connection i.e. LAN)
•	Control panel->Network and sharing->Change Adapter settings->Local Area Connection In sharing tab check "Allow other network users to connect through the computer connection"
•	select Virtual Wi-Fi in Home network connection


To develop or view the source code 
•	Download the repo and open it as a project in Microsoft Visual Studio. Preferably in Microsoft visual studio 2012 to avoid any package conflict since this application is developed in Visual studio 2012.
