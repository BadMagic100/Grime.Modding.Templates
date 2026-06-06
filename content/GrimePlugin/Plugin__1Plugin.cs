using BepInEx;

namespace GrimePlugin._1;

[BepInAutoPlugin(id: "io.github.githubusername.plugin__1")]
public partial class Plugin__1Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        // Put your initialization logic here
        Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");
    }
}
