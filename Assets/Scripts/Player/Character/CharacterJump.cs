using UnityEngine;

namespace Character
{
    public class CharacterJump
    {
        public bool JumpPressed { get; set; }
        public bool OnGround { get; private set; }
        private readonly LayerMask _groundLayer;
        private readonly Rigidbody2D _charRig;
        private float _jumpSpeed;
        
        public CharacterJump(float jumpSpeed, Rigidbody2D charRig)
        {
            _charRig = charRig;
            _groundLayer = LayerMask.GetMask("Ground");
            _jumpSpeed = jumpSpeed;
        }
        public void CheckJump()
        {
            OnGround = Physics2D.Raycast(_charRig.transform.position, Vector2.down, 0.55f, _groundLayer);
            if (JumpPressed && OnGround)
            {
                Jump();
            }
        }
        private void Jump()
        {
            _charRig.velocity = new Vector2(_charRig.velocity.x, 0f);
            _charRig.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        }
    }
}