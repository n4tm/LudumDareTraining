using UnityEngine;

namespace Character
{
    public class CharacterMovement
    {
        public Vector2 Direction { get; set; }
        private float _moveSpeed;
        private float _maxSpeed;
        private readonly Rigidbody2D _charRig;
        private const float LINEAR_DRAG = 10f;
        
        public CharacterMovement(Rigidbody2D charRig, float moveSpeed,  float maxSpeed)
        { 
            _charRig = charRig;
            _moveSpeed = moveSpeed;
            _maxSpeed = maxSpeed;
        }
        
        public void Move(CharacterAnimation charAnim)
        {
            _charRig.AddForce(Vector2.right * (Direction.x * _moveSpeed));
            if (Mathf.Abs(_charRig.velocity.x) > _maxSpeed) 
                _charRig.velocity = new Vector2(Mathf.Sign(_charRig.velocity.x) * _maxSpeed, _charRig.velocity.y);
            if (Direction.x > 0 && charAnim.CharFacing != CharacterAnimation.Facing.right
                || Direction.x < 0 && charAnim.CharFacing == CharacterAnimation.Facing.right)
                Flip(charAnim);

        }
        
        private void Flip(CharacterAnimation charAnim)
        {
            charAnim.CharFacing = charAnim.CharFacing == CharacterAnimation.Facing.right ? CharacterAnimation.Facing.left : CharacterAnimation.Facing.right;
            _charRig.transform.rotation = Quaternion.Euler(0, charAnim.CharFacing == CharacterAnimation.Facing.right ? 0 : 180, 0);
        }
        public void ManagePhysics(bool onGround)
        {
            bool changingDirections = Direction.x > 0 && _charRig.velocity.x < 0 || Direction.x < 0 && _charRig.velocity.x > 0;
            if (onGround)
            {
                _charRig.drag = Mathf.Abs(Direction.x) < 0.4f || changingDirections? LINEAR_DRAG : 0f;
                _charRig.gravityScale = 0f;
            }
            else
            {
                _charRig.gravityScale = 2f;
            } 
        }
    }
}