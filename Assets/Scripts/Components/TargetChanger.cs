using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChanger : MonoBehaviour
{
    [SerializeField] private Transform _nextTarget;

    public void SetNextTarget(GameObject goToChangeTarget)
    {
        var enemy = goToChangeTarget.GetComponent<Enemy>();
        if (enemy == null) return;

        enemy.SetTarget(_nextTarget);
    }
}
