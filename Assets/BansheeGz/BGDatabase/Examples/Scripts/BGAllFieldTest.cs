/*
<copyright file="BGAllFieldTest.cs" company="BansheeGz">
    Copyright (c) 2018-2020 All Rights Reserved
</copyright>
*/

using UnityEngine;

namespace BansheeGz.BGDatabase.Example
{
    public partial class BGAllFieldTest : MonoBehaviour
    {
        private BGEntityGo entity;

        void Start()
        {
            entity = GetComponent<BGEntityGo>();
            
            Rebuild();
            var events = BGRepo.I.Events;
            events.On = true;
            events.AddEntityUpdatedListener(entity.EntityId, EntityChanged);
        }

        void OnDestroy()
        {
            BGRepo.I.Events.RemoveEntityUpdatedListener(entity.EntityId, EntityChanged);
        }
        
        private void EntityChanged(object sender, BGEventArgsEntityUpdated e)
        {
            Rebuild();
        }

        private void Rebuild()
        {
            if (entity.Entity == null) return;
            var value = "                   ALL FIELDS VALUES\n\n";
            entity.Meta.ForEachField(field => value += field.Name + "=" + field.ToString(entity.Entity.Index) + '\n');
            GetComponent<TextMesh>().text = value;
        }
    }
}