/*
<copyright file="BGPlayer.cs" company="BansheeGz">
    Copyright (c) 2018-2020 All Rights Reserved
</copyright>
*/

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BansheeGz.BGDatabase.Example
{
    //this class implement BGAddonSaveLoad.BeforeSaveReciever interface, 
    //so void BGAddonSaveLoad.BeforeSaveReciever.OnBeforeSave method will be called
    //before saving
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    public partial class BGPlayer : BGM_Player, BGAddonSaveLoad.BeforeSaveReciever
    {
        [NonSerialized] private bool active = true;

        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
                GetComponent<BGFirstPersonController>().enabled = value;
            }
        }

        public override void Awake()
        {
            base.Awake();
            //get pos and rotation from the table
            transform.position = m_position;
            transform.rotation = m_rotation;
        }

        //this method is called before saving
        void BGAddonSaveLoad.BeforeSaveReciever.OnBeforeSave()
        {
            //save current position , rotation and scene to the database
            m_position = transform.position;
            m_rotation = transform.rotation;
            m_scene = BGE_Scene.FindEntity(scene => string.Equals(scene.Name, SceneManager.GetActiveScene().name));
        }
    }
}