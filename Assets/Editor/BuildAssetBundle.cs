using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class BuildAssetBundle : MonoBehaviour {

    [MenuItem("AsssetBundles/BuildAssetBunldes_LZMA")]
    static void BuildAssetBundles_LZMA()
    {
        BuildAssetBundleOptions buildOptions = BuildAssetBundleOptions.DisableWriteTypeTree;
        BuildAssetBundles(buildOptions);
    }

    [MenuItem("AsssetBundles/BuildAssetBunldes_LZ4")]
    static void BuildAssetBundles_LZ4()
    {
        BuildAssetBundleOptions buildOptions = BuildAssetBundleOptions.DisableWriteTypeTree | BuildAssetBundleOptions.ChunkBasedCompression;
        BuildAssetBundles(buildOptions);
    }

    static void BuildAssetBundles(BuildAssetBundleOptions buildOptions)
    {
        string outputPath = Path.Combine(AssetBundleUtility.AssetBundlesOutputFolder, AssetBundleUtility.GetPlatformName());
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        BuildPipeline.BuildAssetBundles(outputPath, buildOptions, EditorUserBuildSettings.activeBuildTarget);
    }
}
