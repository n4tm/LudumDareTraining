using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 6f;
        //[SerializeField] private float _baseDamage = 20f;
        //[SerializeField] private float _totalHP = 100f;
        //[SerializeField] private bool _isUnlocked = false;
        private Rigidbody2D _charRig;
        private Animator _charAnimator;
        private CharacterAnimation _charAnim;
        // private CharacterAttack _charAttack;
        private CharacterMovement _charMovement;


        private void Awake()
        {
            _charRig = GetComponent<Rigidbody2D>();
            _charAnimator = GetComponent<Animator>();
            _charMovement = new CharacterMovement(_moveSpeed);
            //_charAttack = new CharacterAttack(_baseDamage);
            _charAnim = new CharacterAnimation(gameObject.name, _charAnimator)
            {
                CharFacing = CharacterAnimation.Facing.right
            };
        }

        private void OnEnable() => _charMovement.EnableInputMap();
        private void OnDisable() => _charMovement.DisableInputMap();

        private void FixedUpdate()
        {
            _charMovement.Move(_charRig);
            _charAnim.ManageAnimation(_charRig.velocity);
            if (_charRig.velocity != Vector2.zero) transform.rotation = _charAnim.CharFacing == CharacterAnimation.Facing.left ? 
                Quaternion.AngleAxis(180, Vector3.down) : Quaternion.identity;
        }
    }
}
