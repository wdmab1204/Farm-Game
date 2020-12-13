/*
<copyright file="BGGoToScene.cs" company="BansheeGz">
    Copyright (c) 2018-2020 All Rights Reserved
</copyright>
*/

using UnityEngine;
using UnityEngine.SceneManagement;

namespace BansheeGz.BGDatabase.Example
{
    public partial class BGGoToScene : BGM_Scene
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //get reference to player
                var player = other.GetComponent<BGPlayer>();
            
                //set player position & rotation to scene's spawn position & rotation 
                player.m_position = m_spawnPosition;
                player.m_rotation = Quaternion.Euler(m_spawnRotation);
            
                //load the scene
                SceneManager.LoadScene(Entity.Name);
            }
        }
    }
}