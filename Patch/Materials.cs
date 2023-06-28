using System.Collections.Generic;
using HarmonyLib;
using ItemManager;
using UnityEngine;
using static MoreBiomes.Plugin;
using static Heightmap;
using static Heightmap.Biome;
using static ZoneSystem;
using static ZoneSystem.ZoneVegetation;

namespace MoreBiomes;

[HarmonyPatch]
public class Materials
{
    [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake)), HarmonyPostfix, HarmonyWrapSafe]
    public static void GetBiomeHeight()
    {
        foreach (var asset in bundleDesert.LoadAllAssets<GameObject>())
        {
            var renderers = asset.GetComponentsInChildren<Renderer>();
            if (renderers != null && renderers.Length > 0)
            {
                foreach (Renderer? renderer in renderers)
                {
                    if (!renderer) continue;
                    foreach (Material? material in renderer.sharedMaterials)
                    {
                        if (!material) continue;
                        string name = material.shader.name;
                        material.shader = Shader.Find(name);
                        Debug($"Shader for {asset.name}({renderer.name}) set to {material.shader.name}");
                    }
                }
            }


            var instanceRenderers = asset.GetComponentsInChildren<InstanceRenderer>();
            if (instanceRenderers != null && instanceRenderers.Length > 0)
            {
                foreach (InstanceRenderer? renderer in instanceRenderers)
                {
                    if (!renderer) continue;
                    if (!renderer.m_material)
                    {
                        DebugError($"No material found for InstanceRenderer {renderer.name}", true);
                        continue;
                    }

                    renderer.m_material.shader = Shader.Find(renderer.m_material.shader.name);
                }
            }
        }

        foreach (var asset in bundleJungle.LoadAllAssets<GameObject>())
        {
            var renderers = asset.GetComponentsInChildren<Renderer>();
            if (renderers != null && renderers.Length > 0)
            {
                foreach (Renderer? renderer in renderers)
                {
                    if (!renderer) continue;
                    foreach (Material? material in renderer.sharedMaterials)
                    {
                        if (!material) continue;
                        string name = material.shader.name;
                        material.shader = Shader.Find(name);
                        Debug($"Shader for {asset.name}({renderer.name}) set to {material.shader.name}");
                    }
                }
            }


            var instanceRenderers = asset.GetComponentsInChildren<InstanceRenderer>();
            if (instanceRenderers != null && instanceRenderers.Length > 0)
            {
                foreach (InstanceRenderer? renderer in instanceRenderers)
                {
                    if (!renderer) continue;
                    if (!renderer.m_material)
                    {
                        DebugError($"No material found for InstanceRenderer {renderer.name}", true);
                        continue;
                    }

                    renderer.m_material.shader = Shader.Find(renderer.m_material.shader.name);
                    Debug($"Shader for {asset.name}({renderer.name}) set to {renderer.m_material.shader.name}");
                }
            }
        }
    }
}