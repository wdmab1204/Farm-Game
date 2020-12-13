/*
<copyright file="BGAssetBundleLoader.cs" company="BansheeGz">
    Copyright (c) 2018-2020 All Rights Reserved
</copyright>
*/

using System.IO;
using UnityEngine;

namespace BansheeGz.BGDatabase.Example
{
    public partial class BGAssetBundleLoader : MonoBehaviour
    {
        private const string BundleName = "audio";

        void Awake()
        {
            var loadedList = AssetBundle.GetAllLoadedAssetBundles();
            foreach (var bundle in loadedList)
            {
                if (BundleName.Equals(bundle.name)) return;
            }
            AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, BundleName));
        }
    }
}