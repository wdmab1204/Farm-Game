/*
<copyright file="BGSpawner.cs" company="BansheeGz">
    Copyright (c) 2018-2020 All Rights Reserved
</copyright>
*/

using UnityEngine;

namespace BansheeGz.BGDatabase.Example
{
    public partial class BGSpawner :BGM_Scene
    {
        public override void  Start()
        {
            base.Start();
            Spawn();
            //use database events to keep track the moment, when all collectables are gathered
            BGRepo.I.Events.AddAnyEntityDeletedListener(BGE_Collectable.MetaDefault.Id, CheckForNewSpawns);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            BGRepo.I.Events.RemoveAnyEntityDeletedListener(BGE_Collectable.MetaDefault.Id, CheckForNewSpawns);
        }

        //spawn unity's gameobjects - corresponding to Repo objects
        private void Spawn()
        {
            //fetch collectables from database
            var collectables = m_Collectable;

            //no collectables- no luck
            if (BGUtil.IsEmpty(collectables)) return;

            //spawn collectables objects
            foreach (var collectable in collectables)
            {
                //create collectable GameObject
                var newCollectable = Instantiate(collectable.f_type.f_prefab, collectable.f_position, Quaternion.identity);

                //we know - collectable prefab has BGCollectable script attached - so init it with data
                newCollectable.GetComponent<BGCollectable>().Entity = collectable;
            }
        }

        //This method spawns new collectables randomly to all scenes if all objects are collected
        public void CheckForNewSpawns(object sender, BGEventArgsAnyEntity args)
        {
            //there are still some collectables
            if (BGE_Collectable.CountEntities > 0) return;

            //ok, we gathered all objects- lets spawn new ones
            BGE_Scene.ForEachEntity(scene =>
            {
               
                //number of collectables for one scene
                var count = Random.Range(3, 6);
                for (var i = 0; i < count; i++)
                {
                    //nested meta has utility method, which auto assign new collectable to owner entity (scene)
                    var newCollectable = BGE_Collectable.NewEntity(scene);

                    //set gold
                    newCollectable.f_gold = Random.Range(1, 10);

                    //bounds determines scene's frontiers 
                    var bounds = scene.f_bounds;
                    //set position
                    newCollectable.f_position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), bounds.center.y, Random.Range(bounds.min.z, bounds.max.z));
                    
                    //set type
                    newCollectable.f_type = BGE_CollectableType.GetEntity(Random.Range(0, BGE_CollectableType.CountEntities));
                }
            });

            //spawn GameObjects for current scene
            Spawn();
        }
    }
}