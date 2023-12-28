# ToggleCameras

**ToggleCameras** is a plugin developed for the Windows version of Elgato's Stream Deck with the goal of enabling a user to disable or enable all of their camera hardware with a single button-press. Inspired by my own experiences with work environments where protecting against unwanted surveillance was top of mind, there are a number of conceivable use cases for such a tool, including:

* Mitigation of users' privacy concerns surrounding their cameras
* Complicance of professionals with the policies of secure work environments.
* Disabling cameras as part of IT troubleshooting or software testing
* Protection of streamers against accidental broadcasts

Important Caveats: 
1. It should be noted that in my testing this plugin has required Elgato's Stream Deck software to be run with administrator privileges; completely exit and restart the application if you find it to not be functioning as expected.
   
3. This plugin queries the Windows Management Infrastructure (WMI) for devices having PNP Classes of 'Camera' or 'Image'. While this approach been successful in toggling my own camera devices, this almost certainly doesn't cover all such devices, and so in the future I will be investiating any more robust approaches that may exist. I welcome any constructive contributions to this project.

I chose to use C# for this project because it appeared to provide a suitable level of access to methods of device enumeration and control that are exposed by Windows while also being quite accessible to someone who had not developed for Windows before.
Eventually I would like to extend this project toward the control of audiovisual devices in general, and so I may therefore consider migrating to C++ as it appears that the breadth of the Windows API exposed to C++ seems to be considerably richer according to my intial studies. 

# Usage




