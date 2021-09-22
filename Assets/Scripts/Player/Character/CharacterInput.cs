using UnityEngine;

namespace Character
{
    public class CharacterInput
    {
        private readonly InputMap _inputMap;
        public CharacterInput(CharacterMovement charMove, CharacterJump charJump, CharacterAttack charAttack)
        {
            _inputMap = new InputMap();
            // Read a Vector2 from inputMap Movement to assign to the player's direction
            _inputMap.Player.Movement.started += ctx => charMove.Direction = ctx.ReadValue<Vector2>();
            // Assign the player's direction to a null Vector2;
            _inputMap.Player.Movement.canceled += ctx => charMove.Direction = Vector2.zero;

            _inputMap.Player.Jump.performed += ctx => charJump.JumpPressed = true;
            _inputMap.Player.Jump.canceled += ctx => charJump.JumpPressed = false;

            _inputMap.Player.Attack.performed += ctx => charAttack.Attack1Pressed = true;
            _inputMap.Player.Attack.canceled += ctx => charAttack.Attack1Pressed = false;
        }
    
        public void EnableInputMap() => _inputMap.Enable();
        public void DisableInputMap() => _inputMap.Disable();
        
    }
}
