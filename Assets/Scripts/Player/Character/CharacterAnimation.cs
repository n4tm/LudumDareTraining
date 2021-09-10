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
            /*jump,
             *attack1,
             *attack2,
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
        
        public void ManageAnimation(Vector2 characterVelocity)
        {
            if (characterVelocity == Vector2.zero) PlayAnimation(AnimationType.idle);
            else
            {
                if (characterVelocity.x < 0.1f) CharFacing = Facing.left;
                else if (characterVelocity.x > 0.1f) CharFacing = Facing.right;
                
                PlayAnimation(AnimationType.run);
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
