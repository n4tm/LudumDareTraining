using UnityEngine;

namespace Character
{
    public class CharacterMovement
    {
        private readonly InputMap _inputMap;
        private Vector2 _direction;
        private readonly float _moveSpeed;

        public CharacterMovement(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
            _inputMap = new InputMap();

            // Read a Vector2 from inputMap Movement to assign to the player's direction
            _inputMap.Player.Movement.performed += ctx => _direction = ctx.ReadValue<Vector2>();
            // Assign the player's direction to a null Vector2;
            _inputMap.Player.Movement.canceled += ctx => _direction = Vector2.zero;
        }
    
        public void EnableInputMap() => _inputMap.Enable();
        public void DisableInputMap() => _inputMap.Disable();
    
        public void Move(Rigidbody2D charRig)
        {
            charRig.velocity = _direction * _moveSpeed;
        }
    }
}
