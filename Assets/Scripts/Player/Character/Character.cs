using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float jumpSpeed = 6f;
        [SerializeField] private float moveSpeed = 6f;
        //[SerializeField] private float _baseDamage = 20f;
        //[SerializeField] private float _totalHP = 100f;
        //[SerializeField] private bool _isUnlocked = false;
        private Rigidbody2D _charRig;
        private Animator _charAnimator;
        private CharacterAnimation _charAnim;
        // private CharacterAttack _charAttack;
        private CharacterInput _charInput;


        private void Awake()
        {
            _charRig = GetComponent<Rigidbody2D>();
            _charAnimator = GetComponent<Animator>();
            //_charAttack = new CharacterAttack(_baseDamage);
            _charAnim = new CharacterAnimation(gameObject.name, _charAnimator)
            {
                CharFacing = CharacterAnimation.Facing.right
            };
            _charInput = new CharacterInput(moveSpeed, jumpSpeed, transform, _charAnim);
        }

        private void OnEnable() => _charInput.EnableInputMap();
        private void OnDisable() => _charInput.DisableInputMap();

        private void Update()
        {
            _charInput.CheckJump(transform);
            _charInput.CheckTelep();
        }

        private void FixedUpdate()
        {
            _charInput.Move();
            _charAnim.ManageAnimation(_charRig.velocity);
        }
    }
}
