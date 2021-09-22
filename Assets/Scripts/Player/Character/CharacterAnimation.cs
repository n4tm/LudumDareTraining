using UnityEngine;

namespace Character
{
    public class CharacterAnimation
    {
        public enum Facing
        {
            left,
            right
        }

        private enum AnimationType
        {
            idle,
            run,
            jump,
            attack1,
            /*attack2,
             *attack3,
             *attack4,
             *dodge,
             *take_damage,
             *death
             */
        }
        public Facing CharFacing { get; set; }
        private string _currentAnimation;
        private readonly Animator _animator;
        private readonly string _characterName;
        private readonly CharacterMovement _charMove;
        private readonly CharacterJump _charJump;
        private readonly CharacterAttack _charAttack;
        

        public CharacterAnimation(string name, Animator animator, CharacterMovement charMove, CharacterJump charJump, CharacterAttack charAttack)
        {
            _characterName = name;
            _animator = animator;
            _charMove = charMove;
            _charJump = charJump;
            _charAttack = charAttack;
        }
        
        // Animation priority: ATTACK > JUMP > RUN > IDLE
        public void ManageAnimation(float horizontalVelocity)
        {
            if (_charAttack.Attack1Pressed) PlayAnimation(AnimationType.attack1);
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(_characterName+'_'+AnimationType.attack1) 
                || _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                if (_charJump.JumpPressed) PlayAnimation(AnimationType.jump);
                else if (_charJump.OnGround)
                {
                    if (_charMove.Direction != Vector2.zero)
                    {
                        if (_charMove.Direction == Vector2.left) CharFacing = Facing.left;
                        else if (_charMove.Direction == Vector2.right) CharFacing = Facing.right;
                        PlayAnimation(AnimationType.run);
                    }
                    else if (_currentAnimation != AnimationType.idle.ToString() 
                             || _currentAnimation == AnimationType.run.ToString() 
                             && Mathf.Abs(horizontalVelocity) <= 0.01f)
                    {
                        PlayAnimation(AnimationType.idle);
                    }
                }
            }
            
        }
        private void PlayAnimation(AnimationType animationName)
         {
            if (_currentAnimation == animationName.ToString()) return;
            _currentAnimation = animationName.ToString();
            _animator.Play(_characterName+'_'+_currentAnimation);
        }
    }
}
