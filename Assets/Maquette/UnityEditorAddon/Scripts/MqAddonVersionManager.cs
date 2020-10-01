#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using UnityEngine.Networking;

namespace Maquette.UnityAddon
{
    [InitializeOnLoad]
    public class MqAddonVersionManager
    {
        public static string currentVersion = "8";
        private static string latestVersion = "";
        private static string versionUrl = @"https://www.maquette.ms/asset/MaquetteUnityEditorAddonVersion.txt";
        private static string unityPackageUrl = @"https://www.maquette.ms/asset/MaquetteUnityEditorAddon.unitypackage";
        private static bool checkUpdateFromMenu = false;

        static MqAddonVersionManager()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                checkUpdateFromMenu = false;
                CheckVersionUpdate();
            }
        }

        [MenuItem("Maquette/Check for Addon Updates", priority = 2)]
        private static void CheckUpdateFromMenu()
        {
            checkUpdateFromMenu = true;
            CheckVersionUpdate();
        }

        private static void CheckVersionUpdate()
        {
            var www = UnityWebRequest.Get(versionUrl);
            www.SendWebRequest();

            ContinuationManager.Add(() => www.isDone, () =>
            {
                if (!string.IsNullOrEmpty(www.error))
                {
                    Debug.Log("WWW failed: " + www.error);
                    return;
                }

                latestVersion = www.downloadHandler.text;
                OnVersionStringReceived(latestVersion);

            });
        }

        public static void OnVersionStringReceived(string lastestVersion)
        {
            int latestVersionNumber = 0;
            int currentVersionNumber = 0;
            if (int.TryParse(lastestVersion, out latestVersionNumber)
                && int.TryParse(currentVersion, out currentVersionNumber))
            {
                if (latestVersionNumber <= currentVersionNumber)
                {
                    if (checkUpdateFromMenu)
                    {
                        EditorUtility.DisplayDialog("Maquette Unity Editor Addon",
                            string.Format("Current version (version {0}) is up to date.", latestVersionNumber), "OK");
                    }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("Maquette Unity Editor Addon",
                        string.Format("A new version (version {0}) is availabe for download.", latestVersionNumber),
                        "Download update", "Cancel"))
                    {
                        var www = UnityWebRequest.Get(unityPackageUrl);
                        www.SendWebRequest();

                        ContinuationManager.Add(() => www.isDone, () =>
                        {
                            if (!string.IsNullOrEmpty(www.error))
                            {
                                Debug.Log("WWW failed: " + www.error);
                                return;
                            }

                            // Debug.Log("WWW downloaded: " + www.downloadHandler.data);

                            string downloadFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            string packagePath = Path.Combine(downloadFolderPath, "MaquetteUnityEditorAddon.unitypackage");

                            File.WriteAllBytes(packagePath, www.downloadHandler.data);

                            AssetDatabase.ImportPackage(packagePath, true);
                        });

                        latestVersion = "";
                    }
                }
            }
        }
    }

    internal static class ContinuationManager
    {
        private class Job
        {
            public Job(Func<bool> completed, Action continueWith)
            {
                Completed = completed;
                ContinueWith = continueWith;
            }
            public Func<bool> Completed { get; private set; }
            public Action ContinueWith { get; private set; }
        }

        private static readonly List<Job> jobs = new List<Job>();

        public static void Add(Func<bool> completed, Action continueWith)
        {
            if (!jobs.Any()) EditorApplication.update += Update;
            jobs.Add(new Job(completed, continueWith));
        }

        private static void Update()
        {
            for (int i = 0; i >= 0; --i)
            {
                var jobIt = jobs[i];
                if (jobIt.Completed())
                {
                    jobIt.ContinueWith();
                    jobs.RemoveAt(i);
                }
            }
            if (!jobs.Any()) EditorApplication.update -= Update;
        }
    }
}

#endif