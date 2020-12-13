/*
<copyright file="BGUi.cs" company="BansheeGz">
    Copyright (c) 2018-2020 All Rights Reserved
</copyright>
*/

using UnityEngine;
using UnityEngine.UI;

namespace BansheeGz.BGDatabase.Example
{
    [RequireComponent(typeof(BGSavingLoading))]
    public partial class BGUi : BGM_Scene
    {
#pragma warning disable 649
        [SerializeField] private Text ObjectsText;
        [SerializeField] private Image Menu;
        [SerializeField] private Text Details;
#pragma warning restore 649
        private BGE_Player Player
        {
            get { return BGE_Player.GetEntity(0); }
        }

        public override void Start()
        {
            base.Start();
            Menu.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) EscapePressed();

            //count collectables for 1) this scene 2) overall
            var thisSceneObjects = m_Collectable;
            ObjectsText.text = "Objects: " + (thisSceneObjects == null ? 0 : thisSceneObjects.Count) + " / " + BGE_Collectable.CountEntities;
        }

        //this method opens the menu and is called when Esc button pressed
        public void EscapePressed()
        {
            var player = FindObjectOfType<BGPlayer>();
            player.Active = !player.Active;
            Menu.gameObject.SetActive(!player.Active);

            if (player.Active) return;

            //show save file info
            var saving = GetComponent<BGSavingLoading>();
            Details.text = saving.HasSavedFile ? "Save file exist at " + saving.SaveFilePath : "No save file";
        }
    }
}