//-------------------------------------------------------------------------------------------------
// Copyright (C) Microsoft Corporation. All rights reserved.
//-------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Maquette.Unity
{
    public static class MqAddonUtility
    {
        public static bool NeedsMaquetteMaterial(string materialName)
        {
            return materialName.Contains("MaquetteMaterial");
        }

        public static Material CreateMaquetteMaterial(string materialName)
        {
            MqMaterialAssetList materialList =
                LoadAssetFromUnityProject<MqMaterialAssetList>(MqMaterialAssetList.AssetFileName);

            MqMaterialAssetItem matchedMaterialItem;
            bool isImage = materialName.Contains("Image_");
            if (isImage)
            {
                materialName = materialName.Replace("Image_", "");
            }

            matchedMaterialItem = materialList.materials.Find(x => x.material.name == materialName);
            if (matchedMaterialItem != null)
            {
                Material mat = new Material(matchedMaterialItem.material_VertexColor);
                if (isImage)
                {
                    // [HACK] Until we implement custom alpha value set, images alpha is set to full for visibility
                    Color c = mat.color;
                    c.a = 1f;
                    mat.color = c;
                }

                return mat;
            }
            else
            {
                Debug.LogError("Cannot find Maquette material for name = " + materialName);
                return null;
            }
        }

        public static T LoadAssetFromUnityProject<T>(string fileNameNoExtension) where T : Object
        {
#if UNITY_EDITOR
            string[] guids = UnityEditor.AssetDatabase.FindAssets(fileNameNoExtension, null);

            if (guids.Length == 0)
            {
                Debug.LogError("[Maquette] Cannot find MaquetteBrushAssetList.asset in your Unity project.");
                return default(T);
            }

            string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(assetPath);
#else
            Debug.LogWarning("Attempting to load editor only resource from run-time, returning NULL");
            return default(T);
#endif
        }
    }
}