/*
<copyright file="BGCollectable.cs" company="BansheeGz">
    Copyright (c) 2018-2020 All Rights Reserved
</copyright>
*/

using UnityEngine;

namespace BansheeGz.BGDatabase.Example

{
    public partial class BGCollectable : BGM_Collectable
    {
        public TextMesh Text
        {
            get { return GetComponentInChildren<TextMesh>(); }
        }

        //this callback is called then connected entity is changed
        public override void EntityChanged()
        {
            Text.text = "Gold:" + m_gold;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.GetComponent<BGPlayer>();
                
                //add gold
                player.m_gold += m_gold;
                
                //play sound
                var audioSource = player.GetComponent<AudioSource>();
                audioSource.clip = m_type.f_audio;
                audioSource.Play();

                //remove from database & scene
                Entity.Delete();
                Destroy(gameObject);
            }
        }
    }
}