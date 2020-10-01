﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace UniGLTF
{
    class PrefabContext : IImporterContext
    {
        public string Path
        {
            get;
            private set;
        }

        public GameObject MainGameObject
        {
            get;
            private set;
        }

        string m_prefabPath;

        IEnumerable<UnityEngine.Object> GetSubAssets()
        {
            return AssetDatabase.LoadAllAssetsAtPath(m_prefabPath);
        }

        public PrefabContext(String path, bool overwrite = true)
        {
            Path = path;

            var dir = System.IO.Path.GetDirectoryName(Path);
            var name = System.IO.Path.GetFileNameWithoutExtension(Path);

            m_prefabPath = string.Format("{0}/{1}.prefab", dir, name);
            if (!overwrite)
            {
                m_prefabPath = AssetDatabase.GenerateUniqueAssetPath(m_prefabPath);
            }

            if (File.Exists(m_prefabPath))
            {
                //Debug.LogFormat("Exist: {0}", m_prefabPath);

                // clear subassets
                foreach (var x in GetSubAssets())
                {
                    if (x is Transform
                        || x is GameObject)
                    {
                        continue;
                    }
                    GameObject.DestroyImmediate(x, true);
                }
            }
        }

        public void SetMainGameObject(string key, GameObject go)
        {
            MainGameObject = go;
        }

        public void AddObjectToAsset(string key, UnityEngine.Object o)
        {
            AssetDatabase.AddObjectToAsset(o, m_prefabPath);
        }

        public void Dispose()
        {
            if (MainGameObject == null)
            {
                return;
            }

            ///
            /// create prefab, after subasset AssetDatabase.AddObjectToAsset
            ///

            PrefabUtility.SaveAsPrefabAssetAndConnect(MainGameObject, m_prefabPath, InteractionMode.UserAction);

            GameObject.DestroyImmediate(MainGameObject);
        }
    }
}
