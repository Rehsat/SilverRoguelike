using System;
using System.Linq;
using UnityEngine;

    public class SpawnersList : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawners;

        public void Spawn(string id)
        {
            var spawner = _spawners.FirstOrDefault(element => element.Id == id);
            spawner?.Component.Spawn();
        }
        [Serializable]
        public class SpawnData
        {
            public string Id;
            public Spawner Component;
        }
    }
