using BepInEx;

namespace GrimePlugin._1;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class GrimePlugin__1 : BaseUnityPlugin
{
    private void Awake()
    {
        // Put your initialization logic here
        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} has loaded!");
    }
}