using UnityEngine;

namespace Character
{
    public class CharacterInput
    {
        public Vector2 Direction { get; set; }
        private bool _jumpPressed;
        private bool onGround;
        private readonly LayerMask _groundLayer;
        private readonly InputMap _inputMap;
        private readonly CharacterAnimation _charAnim;
        private readonly Transform _charTransform;
        private readonly Rigidbody2D _charRig;
        private readonly float _moveSpeed;
        private readonly float _jumpSpeed;
        private readonly float _maxSpeed;
        private const float LINEAR_DRAG = 5f;

        public CharacterInput(float moveSpeed, float jumpSpeed, Transform transform, CharacterAnimation charAnim, float maxSpeed=8f)
        {
            _jumpSpeed = jumpSpeed;
            _moveSpeed = moveSpeed;
            _charTransform = transform;
            _charRig = transform.GetComponent<Rigidbody2D>();
            _charAnim = charAnim;
            _maxSpeed = maxSpeed;
            _inputMap = new InputMap();
            _groundLayer = LayerMask.GetMask("Ground");
            // Read a Vector2 from inputMap Movement to assign to the player's direction
            _inputMap.Player.Movement.performed += ctx => Direction = ctx.ReadValue<Vector2>();
            // Assign the player's direction to a null Vector2;
            _inputMap.Player.Movement.canceled += ctx => Direction = Vector2.zero;

            _inputMap.Player.Jump.started += ctx => _jumpPressed = true;
            _inputMap.Player.Jump.canceled += ctx => _jumpPressed = false;
        }
    
        public void EnableInputMap() => _inputMap.Enable();
        public void DisableInputMap() => _inputMap.Disable();
    
        public void Move()
        {
            _charRig.AddForce(Vector2.right * (Direction.x * _moveSpeed));
            if (Mathf.Abs(_charRig.velocity.x) > _maxSpeed) 
                _charRig.velocity = new Vector2(Mathf.Sign(_charRig.velocity.x) * _maxSpeed, _charRig.velocity.y);
            if (Direction.x > 0 && _charAnim.CharFacing != CharacterAnimation.Facing.right
                || Direction.x < 0 && _charAnim.CharFacing == CharacterAnimation.Facing.right)
                Flip();
            ModifyPhysics();
        }
        
        private void ModifyPhysics()
        {
            bool changingDirections = Direction.x > 0 && _charRig.velocity.x < 0 || Direction.x < 0 && _charRig.velocity.x > 0;
            _charRig.drag = Mathf.Abs(Direction.x) < 0.4f || changingDirections? LINEAR_DRAG : 0f;
        }
        private void Flip()
        {
            _charAnim.CharFacing = _charAnim.CharFacing == CharacterAnimation.Facing.right ? CharacterAnimation.Facing.left : CharacterAnimation.Facing.right;
            _charTransform.rotation = Quaternion.Euler(0, _charAnim.CharFacing == CharacterAnimation.Facing.right ? 0 : 180, 0);
        }

        public void CheckJump(Transform tr)
        {
            onGround = Physics2D.Raycast(tr.position, Vector2.down, 0.55f, _groundLayer);
            if (_jumpPressed && onGround)
            {
                Jump();
            }
        }

        private void Jump()
        {
            _charRig.velocity = new Vector2(_charRig.velocity.x, 0);
            _charRig.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        }
    }
}
