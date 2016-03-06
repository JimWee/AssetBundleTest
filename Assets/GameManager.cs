using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour {

    enum AssetType
    {
        Texture,
        Material,
        Prefab,
    }

    string mAssetBundlesPath;
    Dictionary<AssetType, string> mAssetBundleNames = new Dictionary<AssetType, string>();
    Dictionary<AssetType, AssetBundle> mAssetBundles = new Dictionary<AssetType, AssetBundle>();

    Dictionary<AssetType, string> mResourceNames = new Dictionary<AssetType, string>();
    Dictionary<AssetType, Object> mResources = new Dictionary<AssetType, Object>();

    Dictionary<AssetType, AssetBundle> mResourcesAB = new Dictionary<AssetType, AssetBundle>();

    GameObject mCube;

	void Start ()
    {
        mAssetBundlesPath = Path.Combine(AssetBundleUtility.AssetBundlesOutputFolder, AssetBundleUtility.GetPlatformName());
        mAssetBundleNames.Add(AssetType.Texture, "texture-bundle");
        mAssetBundleNames.Add(AssetType.Material, "material-bundle");
        mAssetBundleNames.Add(AssetType.Prefab, "prefab-bundle");

        mResourceNames.Add(AssetType.Texture, "Assets/UnityLogo");
        mResourceNames.Add(AssetType.Material, "Assets/MyMaterial");
        mResourceNames.Add(AssetType.Prefab, "Assets/Cube");
	}
	
	void Update ()
    {
	
	}

    public void LoadAssetBundle(int type)
    {
        AssetType assetType = (AssetType)type;
        AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(mAssetBundlesPath, mAssetBundleNames[assetType]));

        mAssetBundles[assetType] = assetBundle;

        Debug.LogFormat("LoadAssetBundle success: {0}", mAssetBundleNames[assetType]);
    }

    public void Instantiate_AssetBundle()
    {
        GameObject cube = mAssetBundles[AssetType.Prefab].LoadAsset("Cube") as GameObject;
        mCube = Instantiate(cube);
    }

    public void LoadResource(int type)
    {
        AssetType assetType = (AssetType)type;
        Object resource = Resources.Load(mResourceNames[assetType]);

        mResources[assetType] = resource;

        Debug.LogFormat("LoadResource success: {0}", mResourceNames[assetType]);
    }

    public void Instantiate_Resources()
    {
        GameObject cube = mResources[AssetType.Prefab] as GameObject;
        mCube = Instantiate(cube);
    }

    public void LoadResourceAB(int type)
    {
        AssetType assetType = (AssetType)type;
        string path = "AssetBundles/" + AssetBundleUtility.GetPlatformName() + "/" + mAssetBundleNames[assetType];
        TextAsset text = Resources.Load(path) as TextAsset;
        AssetBundle assetBundle = AssetBundle.LoadFromMemory(text.bytes);

        mResourcesAB[assetType] = assetBundle;

        Debug.LogFormat("LoadResourceAB success: {0}", mAssetBundleNames[assetType]);
    }

    public void Instantiate_ResourceAB()
    {
        GameObject cube = mResourcesAB[AssetType.Prefab].LoadAsset("Cube") as GameObject;
        mCube = Instantiate(cube);
    }

    public void ClearAll()
    {
        Destroy(mCube);

        foreach (var item in mAssetBundles)
        {
            item.Value.Unload(true);
        }
        mAssetBundles.Clear();

        foreach (var item in mResources)
        {
            DestroyImmediate(item.Value, true);
        }
        mResources.Clear();

        foreach (var item in mResourcesAB)
        {
            item.Value.Unload(true);
        }
        mResourcesAB.Clear();

        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}
