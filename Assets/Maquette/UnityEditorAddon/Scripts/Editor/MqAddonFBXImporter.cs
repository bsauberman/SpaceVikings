//-------------------------------------------------------------------------------------------------
// Copyright (C) Microsoft Corporation. All rights reserved.
//-------------------------------------------------------------------------------------------------

using UnityEngine;
using UniGLTF;
using System.IO;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using Maquette.Unity;

namespace Maquette.UnityAddon
{
    public class MqAddonFBXImporter : AssetPostprocessor
    {
        [MenuItem("Maquette/Re-generate Maquette prefab from FBX file", priority = 1)]
        public static void ImportMenu()
        {
            if (Selection.activeObject == null)
            {
                EditorUtility.DisplayDialog("Regenerate Maquette Prefab", "Error: Please select a FBX file in the project window", "OK");
                return;
            }

            string path = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
            if (Path.GetExtension(path).ToLower() == ".fbx")
            {
                if (!ImportFBX(path))
                {
                    EditorUtility.DisplayDialog("Regenerate Maquette Prefab", "Error: FBX file not exported from Maquette", "OK");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Regenerate Maquette Prefab", "Error: Not a FBX file", "OK");
            }
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string path in importedAssets)
            {
                var ext = Path.GetExtension(path).ToLower();
                if (ext == ".fbx")
                {
                    ImportFBX(path);
                }
            }

            foreach (string path in movedAssets)
            {
                var ext = Path.GetExtension(path).ToLower();
                if (ext == ".fbx")
                {
                    ImportFBX(path);
                }
            }
        }

        public struct MeshWithRenderer
        {
            public Mesh Mesh;
            public Renderer Rendererer;
        }

        static bool ImportFBX(string srcPath)
        {
            GameObject existingPrefab = AssetDatabase.LoadAssetAtPath(srcPath, typeof(GameObject)) as GameObject;

            var newPrefabPath = srcPath.Replace(".fbx", ".prefab");

            string prefabName = Path.GetFileNameWithoutExtension(newPrefabPath);

            // Debug.Log("Version " + Application.unityVersion);

            var instanceRoot = PrefabUtility.InstantiatePrefab(existingPrefab) as GameObject;

            var unityNodes = instanceRoot.transform.Traverse()
                .Skip(1) // exclude root object for the symmetry with the importer
                .ToList();

            var unityRenderers = unityNodes
                .Where(x => x.GetComponent<Renderer>() != null)
                .Select(x => x.GetComponent<Renderer>())
                .ToList();

            string srcFolder = Path.GetDirectoryName(srcPath);
            string materialFolder = Path.Combine(srcFolder, "Materials");
            if (!Directory.Exists(materialFolder))
            {
                Directory.CreateDirectory(materialFolder);
            }

            string textureFolder = Path.Combine(srcFolder, "Textures");

            bool isMaquetteFBX = false;
            Dictionary<string, Material> replaceMaterials = new Dictionary<string, Material>();

            for (int i = 0; i < unityRenderers.Count; ++i)
            {
                var x = unityRenderers[i];
                if (x.sharedMaterial != null && 
                    x.sharedMaterial.name != null && 
                    x.sharedMaterial.name.Contains("MaquetteMaterial"))
                {
                    isMaquetteFBX = true;

                    Material originalMaterial = x.sharedMaterial;

                    string originalMaterialName = originalMaterial.name;
                    originalMaterialName = originalMaterialName.Replace(" (Instance)", "");

                    string replaceMaterialName = originalMaterial.name;
                    replaceMaterialName = replaceMaterialName.Replace(" (Instance)", "");
                    replaceMaterialName = replaceMaterialName.Replace("MaquetteMaterial", prefabName);

                    Texture2D imageTexture = null;
                    if (replaceMaterialName.Contains("Image_") && originalMaterial.mainTexture != null)
                    {
                        // Image
                        replaceMaterialName = replaceMaterialName.Replace("Image_", "");
                        string imageName = originalMaterial.mainTexture.name;
                        string imgPath = Path.Combine(textureFolder, imageName + ".png");
                        imageTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(imgPath);
                        originalMaterialName = originalMaterialName.Replace("_" + imageName, "");
                    }

                    Material replaceMaterial = null;
                    if (replaceMaterials.ContainsKey(replaceMaterialName))
                    {
                        replaceMaterial = replaceMaterials[replaceMaterialName];
                    }
                    else
                    {
                        replaceMaterial = MqAddonUtility.CreateMaquetteMaterial(originalMaterialName);

                        if (imageTexture != null)
                        {
                            replaceMaterial.mainTexture = imageTexture;
                        }

                        string newMaterialPath =
                            Path.Combine(materialFolder, replaceMaterialName + ".mat");

                        AssetDatabase.CreateAsset(replaceMaterial, newMaterialPath);
                        AssetDatabase.SaveAssets();

                        replaceMaterial = AssetDatabase.LoadAssetAtPath<Material>(newMaterialPath);

                        replaceMaterials.Add(replaceMaterialName, replaceMaterial);
                    }

                    x.sharedMaterial = replaceMaterial;

                    if (originalMaterialName.Contains("Image_"))
                    {
                        Material[] imageMaterialsNoHighlight = new Material[1];
                        imageMaterialsNoHighlight[0] = x.sharedMaterials[0];
                        x.sharedMaterials = imageMaterialsNoHighlight;
                    }
                }
            }

            if (isMaquetteFBX)
            {
#if UNITY_2018_3_OR_NEWER
                var newPrefab = PrefabUtility.SaveAsPrefabAsset(instanceRoot, newPrefabPath);
#else
                var newPrefab = PrefabUtility.CreatePrefab(newPrefabPath, instanceRoot);
#endif
            }

            GameObject.DestroyImmediate(instanceRoot);
            return isMaquetteFBX;
        }
    }
}