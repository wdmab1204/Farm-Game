/*
<copyright file="BGSavingLoading.cs" company="BansheeGz">
    Copyright (c) 2018-2020 All Rights Reserved
</copyright>
*/

using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BansheeGz.BGDatabase.Example
{
    public partial class BGSavingLoading : MonoBehaviour
    {
        public bool HasSavedFile
        {
            get { return File.Exists(SaveFilePath); }
        }

        public string SaveFilePath
        {
            get { return Path.Combine(Application.persistentDataPath, "bg_save_example.dat"); }
        }

        private static BGEntity PlayerEntity
        {
            get
            {
                return R.Player.Meta.EntityFirst;
            }
        }

        public void Save()
        {
            //save
            var bytes = BGRepo.I.Addons.Get<BGAddonSaveLoad>().Save();
            File.WriteAllBytes(SaveFilePath, bytes);
        }

        public void Load()
        {
            //load
            if (!HasSavedFile) return;

            var content = File.ReadAllBytes(SaveFilePath);

            BGRepo.I.Addons.Get<BGAddonSaveLoad>().Load(content);

            //load saved scene
            SceneManager.LoadScene(R.Player.d_scene.Get(PlayerEntity).Name);
        }
    }
}