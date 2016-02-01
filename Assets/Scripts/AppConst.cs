using UnityEngine;
using System.Collections;

public class AppConst {
    
    public static string GetDir()
    {
        return "file://" + System.Environment.CurrentDirectory.Replace("\\", "/");
    }

    public static string GetAssetBundleFolder()
    {
        return "TestAssetBundle";
    }

    public static string GetAssetBundleUrl()
    {
        return GetDir() + "/" + GetAssetBundleFolder();
    }
}
