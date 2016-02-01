using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class CachingLoad : MonoBehaviour {
    public string AssetBundleName;
    public string AssetName;

    private AssetBundleManifest m_AssetBundleManifest;
    private Dictionary<string, AssetBundle> m_LoadedAssetBundles = new Dictionary<string, AssetBundle>();

    IEnumerator Start()
    {
        yield return StartCoroutine(DownloadManifest());

        yield return StartCoroutine(DownloadAndCache());

        GameObject prefab = m_LoadedAssetBundles[AssetBundleName].LoadAsset<GameObject>(AssetName);
        Instantiate(prefab);
    }

    IEnumerator DownloadManifest()
    {
        using(WWW www = new WWW(AppConst.GetAssetBundleUrl() + "/" + AppConst.GetAssetBundleFolder()))
        {
            yield return www;

            if (www.error != null)
            {
                throw new Exception("WWW download error:" + www.error);
            }

            AssetBundle bundle = www.assetBundle;
            m_AssetBundleManifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
    }

    IEnumerator DownloadAndCache()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        yield return StartCoroutine(GetAsset(AssetBundleName));

        string[] dependencies = m_AssetBundleManifest.GetAllDependencies(AssetBundleName);

        foreach (var item in dependencies)
        {
            yield return StartCoroutine(GetAsset(item));
        }
    }

    IEnumerator GetAsset(string assetBundleName)
    {
        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(AppConst.GetAssetBundleUrl() + "/" + assetBundleName, m_AssetBundleManifest.GetAssetBundleHash(assetBundleName)))
        {
            yield return www;
            if (www.error != null)
                throw new Exception("WWW download had an error:" + www.error);
            AssetBundle bundle = www.assetBundle;
            m_LoadedAssetBundles.Add(assetBundleName, bundle);

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
