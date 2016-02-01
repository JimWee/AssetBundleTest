using UnityEngine;
using System.Collections;
using UnityEditor;

public class BuildAssetBundle : MonoBehaviour {

    [MenuItem("AsssetBundles/BuildAssetBunldes")]
    static void BuildAssetBundles()
    {
        BuildPipeline.BuildAssetBundles(AppConst.GetAssetBundleFolder());
    }

    [MenuItem("AsssetBundles/Get AssetBundle names")]
    static void GetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
            Debug.Log("AssetBundle: " + name);
    }
}
