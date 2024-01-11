# Toggle Cameras

![gallery-1](com.dataisinteresting.togglecameras/previews/gallery-1.png?raw=true)

This free Stream Deck plugin for Windows is designed to enhance your privacy by providing a convenient way to enable or disable all camera devices connected to your system with just a press of a button. Inspired by my own experiences with work environments where protecting against unwanted surveillance was top of mind, there are a number of conceivable use cases for such a tool, including:

* Mitigation of users' privacy concerns surrounding their cameras.
* Compliance of professionals with the policies of secure work environments.
* Disabling of cameras as part of IT troubleshooting or software testing.
* Protection of streamers against accidental broadcasts.

This is the first Stream Deck plugin I have developed. I chose to use C# for this project because it appeared to provide a suitable level of access to methods of device enumeration and control that are exposed by Windows while also being quite accessible to someone who had not developed for Windows before. Eventually, I would like to extend this project to include the control of audiovisual devices in general. Therefore, I may consider migrating this project to C++ as it seems to offer broader access to the Windows API. I welcome any constructive contributions.

## Technical Details:

* Requires Windows 10 or higher and the [.NET 4.7.2 runtime](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472). 
* Utilizes WMI (Windows Management Instrumentation) for camera control.
* Users must run the Elgato Stream Deck application as administrator.
* The plugin will not toggle cameras when any are in use by a process. 

## Installation:
Download the *.streamdeckPlugin* file from the 'releases' section of the [GitHub repository](https://github.com/dataisinteresting/togglecameras). Run the file to install the plugin.


## Usage:

1. Download and install the .NET Framework 4.7.2 Runtime from Microsoft [here](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472).

2. **Run the Elgato Stream Deck software as administrator**.  This is crucial for the proper functioning of the Toggle Cameras plugin due to its interaction with system-level components. To run Stream Deck as administrator: 

    1. Close Stream Deck by right-clicking on the Stream Deck icon in the task tray and selecting **Quit Stream Deck**.

    2. Right-click on a Stream Deck shortcut and select **properties**.

    3. Select the compatibility tab.

    4. Check the option **Run this program as administrator**.

    5. At the bottom, click on **apply** and then **ok**.


3. After installation, find the Toggle Cameras action in the Stream Deck interface and drag it to your preferred button. 

4. The plugin is now ready for use. Press your assigned button to enable or disable your cameras; you can verify their status by checking the Device Manager.

    ![usage](usage.gif)

### Important Notes: 

* This plugin will not toggle cameras when any are in use by a process. Ensure any processes using a camera are closed before attempting to toggle cameras.
  
* This plugin requires Elgato's Stream Deck software to be run with administrator privileges; completely exit and restart the application if you find it to not be functioning as expected.
   
* This plugin queries the Windows Management Infrastructure (WMI) for devices having PNP Classes of 'Camera' or 'Image'. While this approach has been successful in toggling my own camera devices, this may not cover all such devices, and so in the future I will be investigating any more robust approaches that may exist. You can easily confirm whether the plugin is successful in toggling your own devices by reviewing their respective entries in Device Manager.

## Support:
For any issues, questions, or feedback, please raise an issue in the [GitHub repository for Toggle Cameras](https://github.com/dataisinteresting/togglecameras/issues).

## v1.0.0 Release Notes: 

* Single-Button Camera Control: Toggle all connected camera devices as enabled or disabled using a single button on your Stream Deck.

* Dynamic Icon Representation: The button icon changes dynamically to represent the current state of the cameras. Beneath the icon, the text dynamically updates to indicate the most recent action - “Disabled” or “Enabled”.

* Visual Feedback: In-built error handling with immediate visual feedback on the Stream Deck to alert users of issues during camera toggling.







