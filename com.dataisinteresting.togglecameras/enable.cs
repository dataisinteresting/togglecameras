using BarRaider.SdTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Management;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace com.dataisinteresting.togglecameras
{
    [PluginActionId("com.dataisinteresting.togglecameras.enablecameras")]
    public class EnableCameras : PluginBase
    {
        private class PluginSettings
        {
            public static PluginSettings CreateDefaultSettings()
            {
                PluginSettings instance = new PluginSettings();
                instance.OutputFileName = String.Empty;
                instance.InputString = String.Empty;
                return instance;
            }

            [FilenameProperty]
            [JsonProperty(PropertyName = "outputFileName")]
            public string OutputFileName { get; set; }

            [JsonProperty(PropertyName = "inputString")]
            public string InputString { get; set; }
        }

        #region Private Members

        private PluginSettings settings;

        #endregion

        
    public EnableCameras(SDConnection connection, InitialPayload payload) : base(connection, payload)
    {
        if (payload.Settings == null || payload.Settings.Count == 0)
        {
            this.settings = PluginSettings.CreateDefaultSettings();
            SaveSettings();
        }
        else
        {
            this.settings = payload.Settings.ToObject<PluginSettings>();
        }
    }

    public override void Dispose()
    {
        Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
    }

    public override async void KeyPressed(KeyPayload payload)
    {
        Logger.Instance.LogMessage(TracingLevel.INFO, "Key Pressed");
        try
        {
            EnableAllCameras();
        }

        catch (ManagementException e)
        {
                HandleException(e);
        }

        catch (Exception e)
        {
                HandleException(e);
        }
    }

    public override void KeyReleased(KeyPayload payload) { }

    public override void OnTick() { }

    public override void ReceivedSettings(ReceivedSettingsPayload payload)
    {
        Tools.AutoPopulateSettings(settings, payload.Settings);
    }

    public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }

    #region Private Methods
    private void EnableAllCameras()
    {
        // Enable camera devices
        ManagementObjectSearcher searcher =
            new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE PNPClass='Camera' OR PNPClass='Image'");

        foreach (ManagementObject queryObj in searcher.Get())
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, string.Format( "Enabling: {0}", queryObj["Name"]));

            // Apply the 'Enable' method
            ManagementBaseObject outParams = queryObj.InvokeMethod("Enable", null, null);

            // Check the return value to verify whether it was successful
            uint returnValue = (uint)outParams["ReturnValue"];
            if (returnValue == 0)
            {
                Logger.Instance.LogMessage(TracingLevel.INFO, string.Format("Successfully enabled {0}", queryObj["Name"]));
            }
            else
            {
                Logger.Instance.LogMessage(TracingLevel.INFO, string.Format("Failed to enable {0}. Error code: {1}", queryObj["Name"]));
            }
        }
        
    }

    private void HandleException(ManagementException e)
    {
        Logger.Instance.LogMessage(TracingLevel.ERROR, $"ManagementException: {e.Message}, ErrorCode: {e.ErrorCode}");
        Logger.Instance.LogMessage(TracingLevel.ERROR, $"Stack Trace: {e.StackTrace}");

        if (e.InnerException != null)
        {
            Logger.Instance.LogMessage(TracingLevel.ERROR, $"Inner Exception: {e.InnerException.Message}");
        }

        // Show alert on Stream Deck to indicate error
        await Connection.ShowAlert();
    }

    private void HandleException(Exception e)
    {
        // Handle other exceptions here
        Logger.Instance.LogMessage(TracingLevel.ERROR, $"Generic Exception: {e.Message}");
        Logger.Instance.LogMessage(TracingLevel.ERROR, $"Stack Trace: {e.StackTrace}");

        if (e.InnerException != null)
        {
            Logger.Instance.LogMessage(TracingLevel.ERROR, $"Inner Exception: {e.InnerException.Message}");
        }

        // Show alert on Stream Deck to indicate error
        await Connection.ShowAlert();
    }
        private Task SaveSettings()
    {
        return Connection.SetSettingsAsync(JObject.FromObject(settings));
    }

    #endregion
}

}






