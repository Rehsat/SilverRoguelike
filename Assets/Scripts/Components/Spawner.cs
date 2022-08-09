using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
        [SerializeField] private Transform[] _targets;
        [SerializeField] private GameObject _prefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            foreach (var target in _targets)
            {
               var instantinate = Instantiate(_prefab, target.position, Quaternion.identity);
               instantinate.transform.localScale = transform.lossyScale;
            }
        }
}
