using UnityEngine;

namespace Creatures
{
    [RequireComponent(typeof(Stamina))]
    public class Silver : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpLength;
        [SerializeField] private float _throwPower;
        [SerializeField] private float _damageVelocity;

        [SerializeField] private float _staminaRegeneration;
        [SerializeField] private float _stamina—onsumption;

        [SerializeField] private LayerChecker _groundCheck;
        [SerializeField] private LayerChecker _grabableChecker;
        
        [SerializeField] private bool _invertScale;

        private Animator _animator;

        private float _deaultGravityScale;
        private Stamina _stamina;

        private bool _isGrounded;
        private bool _isJumping;
        private bool _isFlying;

        private GrabActions _grabActions;
        private Vector2 _direction;
        private Rigidbody2D _rigidbody;

        private readonly int isRunningKey = Animator.StringToHash("is-running");
        private readonly int isFlyingKey = Animator.StringToHash("is-flying");
        private readonly int isJumpingKey = Animator.StringToHash("is-jumping");
        private readonly int isGroundedKey = Animator.StringToHash("is-grounded");
        private readonly int hitKey = Animator.StringToHash("hit");
        private readonly int throwKey = Animator.StringToHash("throw");
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _grabActions = GetComponent<GrabActions>();
            _animator = GetComponent<Animator>();

            _stamina = GetComponent<Stamina>();
            _stamina.SetRegeneration(_staminaRegeneration);

            _deaultGravityScale = _rigidbody.gravityScale;
        }
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
        private void Update()
        {
            _isGrounded = _groundCheck.IsTouchingLayer;
            _animator.SetBool(isGroundedKey, _isGrounded);
            CheckStamina(_stamina.CurrentStamina);
        }
        private void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed;

            var isRunning = Mathf.Abs(_direction.x) > 0.1f;
            var isJumping = Mathf.Abs(_rigidbody.velocity.y) > 0.25f;
            _animator.SetBool(isRunningKey, isRunning);
            _animator.SetBool(isJumpingKey, isJumping);

            if (_isFlying) xVelocity = _direction.x * _speed * 3;
            _animator.SetBool(isFlyingKey, _isFlying);

            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            UpdateSpriteDirection();
        }
        public void SetFlyState(bool state)
        {
            _isFlying = state;
            if (state == true)
            {
                _rigidbody.gravityScale = 0;
                _stamina.SetRegeneration(-_stamina—onsumption);
            }
            else
            {
                _rigidbody.gravityScale = _deaultGravityScale;
                _stamina.SetRegeneration(_staminaRegeneration);
            }
        }
        private float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var jumping = _direction.y > 0;
            if (jumping)
            {
                _isJumping = true;
                bool isFalling = _rigidbody.velocity.y <= 0.001f;
                if (_isFlying) return _jumpLength;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (_rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.3f;
            }
            return yVelocity;
        }
        private float CalculateJumpVelocity(float yVelocity)
        {
            if (_isGrounded)
            {
                yVelocity += _jumpLength;
            }
            return yVelocity;
        }
        public void UpdateSpriteDirection()
        {
            var multiplier = _invertScale ? -1 : 1;
            if (_direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1, 1);
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multiplier, 1, 1);
            }
        }
        public void GrabObjects()
        {
            var objectsToGrab = _grabableChecker.FindTouchingGameObjects();
            foreach (var objectToGrab in objectsToGrab)
            {
                var grabable = objectToGrab.GetComponent<Grabable>();
                if (grabable != null) _grabActions.Grab(grabable);
            }
        }
        public void Throw()
        {
            _grabActions.Throw(_throwPower);
            _animator.SetTrigger(throwKey);
        }
        public virtual void TakeDamage()
        {
            _isJumping = false;
            _animator.SetTrigger(hitKey);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageVelocity);
        }
        private void CheckStamina(float newStaminaValue)
        {
            if(newStaminaValue <= 0) SetFlyState(false);
        }
    }
}