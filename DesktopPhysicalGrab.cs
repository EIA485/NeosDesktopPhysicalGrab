using BepInEx;
using BepInEx.NET.Common;
using BepInExResoniteShim;
using FrooxEngine;
using HarmonyLib;
using System.Reflection;

namespace DesktopPhysicalGrab;

[ResonitePlugin(PluginMetadata.GUID, PluginMetadata.NAME, PluginMetadata.VERSION, PluginMetadata.AUTHORS, PluginMetadata.REPOSITORY_URL)]
[BepInDependency(BepInExResoniteShim.PluginMetadata.GUID)]
public class Plugin : BasePlugin
{
    public override void Load() => HarmonyInstance.PatchAll();

    static MethodInfo meth = typeof(InteractionHandler).GetMethod("Grab", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(bool), typeof(ICollider) }, null);

    [HarmonyPatch(typeof(InteractionHandler), "TryPointGrab")]
    class Patch
    {
        static bool Prefix(InteractionHandler __instance)
        {
            return !(bool)meth.Invoke(__instance, new object[] { false, null });
        }
    }
}
