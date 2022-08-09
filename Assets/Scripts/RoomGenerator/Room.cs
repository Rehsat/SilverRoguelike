using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform _begin;
    [SerializeField] private Transform _exit;
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject[] _objectsToDestroyToOpenDoor;
    public Transform Begin => _begin;
    public Transform Exit => _exit;
    private void Start()
    {
        StartCoroutine(CheckObjects());
    }
    private IEnumerator CheckObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            var objectsNotNull = 0;
            foreach (var go in _objectsToDestroyToOpenDoor)
            {
                if (go != null) objectsNotNull++;
            }
            if (objectsNotNull <= 0) Destroy(_door);
        }
    }
}
