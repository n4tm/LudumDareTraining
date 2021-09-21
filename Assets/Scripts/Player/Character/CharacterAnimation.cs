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
    
        public CharacterAnimation(string name, Animator animator)
        {
            _characterName = name;
            _animator = animator;
        }
        
        // Animation priority: ATTACK > JUMP > RUN > IDLE
        public void ManageAnimation(CharacterInput charInput, float horizontalVelocity)
        {
            if (charInput.Attack1Pressed) PlayAnimation(AnimationType.attack1);
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(_characterName+'_'+AnimationType.attack1) 
                || _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                if (charInput.JumpPressed) PlayAnimation(AnimationType.jump);
                else if (charInput.OnGround)
                {
                    if (charInput.Direction != Vector2.zero)
                    {
                        if (charInput.Direction == Vector2.left) CharFacing = Facing.left;
                        else if (charInput.Direction == Vector2.right) CharFacing = Facing.right;
                        PlayAnimation(AnimationType.run);
                    }
                    else if (_currentAnimation != AnimationType.idle.ToString() 
                             || _currentAnimation == AnimationType.run.ToString() 
                             && Mathf.Abs(horizontalVelocity) <= 0.1f)
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
