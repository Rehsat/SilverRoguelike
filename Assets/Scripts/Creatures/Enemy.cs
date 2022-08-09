using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{

    [SerializeField] private float _speed = 2;
    [SerializeField] private Transform _target;
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        SetDirection(CalculateDirection());
        var newVelocity = _direction * _speed;
        _rigidbody.velocity = newVelocity;
    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
        transform.localScale = new Vector2(-_direction.x, transform.lossyScale.y);
    }
    private Vector2 CalculateDirection()
    {
        var directionX = transform.position.x < _target.position.x ? 1 : -1;
        var directionY = transform.position.y < _target.position.y ? 1 : -1;
        if (Mathf.Abs(transform.position.y - _target.position.y) < 0.5f) directionY = 0;
        var direction = new Vector2(directionX, directionY);
        return direction;
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }

}
