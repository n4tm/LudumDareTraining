using System;
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
        private float _jumpDelay;
        private float _jumpTimer;
        
        public CharacterJump(float jumpSpeed, float jumpDelay, Rigidbody2D charRig)
        {
            _charRig = charRig;
            _groundLayer = LayerMask.GetMask("Ground");
            _jumpSpeed = jumpSpeed;
            _jumpDelay = jumpDelay;
        }
        public void CheckJump(Tuple<Vector3, Vector3> jumpColliderOffset)
        {
            var (offset1, offset2) = jumpColliderOffset;
            OnGround = Physics2D.Raycast(_charRig.transform.position + offset1, Vector2.down, 0.55f, _groundLayer) 
                       || Physics2D.Raycast(_charRig.transform.position - offset2, Vector2.down, 0.55f, _groundLayer);
            if (JumpPressed)
            {
                _jumpTimer = Time.time + _jumpDelay;
            }
        }
        public void Jump()
        {
            if (!(_jumpTimer > Time.time) || !OnGround) return;
            _charRig.velocity = new Vector2(_charRig.velocity.x, 0f);
            _charRig.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            _jumpTimer = 0;
        }
    }
}