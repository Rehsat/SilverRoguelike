using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    [SerializeField] private float _damping;
    private Transform _target;
    private bool _isGrabed;
    private Vector2 _positionChangeDelta;
    void Update()
    {
        if (_isGrabed)
        {
            var randomX = Random.Range(0f, 1f);
            var randomY = Random.Range(0f, 1f);
            _positionChangeDelta = new Vector2(randomX, randomY);
            transform.position = Vector2.Lerp(transform.position, (Vector2)_target.position , _damping * Time.deltaTime);
        }
    }
    public void SetTarget(Transform target)
    {
        _target = target;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (target == null) _isGrabed = false;
        else _isGrabed = true;
    }
}
