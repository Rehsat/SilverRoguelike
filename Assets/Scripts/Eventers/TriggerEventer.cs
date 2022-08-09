using UnityEngine;

public class TriggerEventer : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private EnterEvent _onEnterTrigger;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.IsInLayer(_layer))
            _onEnterTrigger?.Invoke(collision.gameObject);
    }
}