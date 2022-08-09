using UnityEngine.Events;
using UnityEngine;
using System;

public class ColliderEventer : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private EnterEvent _onEnterCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.IsInLayer(_layer))
            _onEnterCollider?.Invoke(collision.gameObject);
    }
    
}
[Serializable]
public class EnterEvent : UnityEvent<GameObject>
{ }