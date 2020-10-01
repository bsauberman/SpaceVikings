using UnityEditor;
using System.IO;

public partial class ScriptBatch 
{
    private static string GetCommandLineArgument(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }

    public static void BuildMaquetteAddonBuildBot()
    {
        string sBuildPath = @"C:\build\";
        string sCommandLineBuildPath = GetCommandLineArgument("-buildPath");
        if (sCommandLineBuildPath != null)
        {
            sBuildPath = sCommandLineBuildPath;
        }

        string sVersionPath = sBuildPath + "MaquetteUnityEditorAddonVersion.txt";
        StreamWriter writer = new StreamWriter(sVersionPath, true);
        writer.WriteLine(Maquette.UnityAddon.MqAddonVersionManager.currentVersion);
        writer.Close();

        AssetDatabase.ExportPackage(
            "Assets", 
            sBuildPath + "MaquetteUnityEditorAddon.unitypackage", 
            ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Recurse
            );
    }
}
