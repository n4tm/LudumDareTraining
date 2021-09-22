using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float jumpSpeed = 14.5f;
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float attackSpeed = 6f;
        [SerializeField] private float baseDamage = 20f;
        [SerializeField] private float gravity = 1f;
        [SerializeField] private float maxSpeed = 8f;
        //[SerializeField] private float _totalHP = 100f;
        //[SerializeField] private bool _isUnlocked = false;
        private Rigidbody2D _charRig;
        private Animator _charAnimator;
        private CharacterAnimation _charAnim;
        private CharacterAttack _charAttack;
        private CharacterInput _charInput;
        private CharacterMovement _charMove;
        private CharacterJump _charJump;


        private void Awake()
        {
            _charRig = GetComponent<Rigidbody2D>();
            _charAnimator = GetComponent<Animator>();
            _charMove = new CharacterMovement(_charRig, moveSpeed, maxSpeed);
            _charJump = new CharacterJump(jumpSpeed, _charRig);
            _charAttack = new CharacterAttack(baseDamage, attackSpeed);
            _charAnim = new CharacterAnimation(gameObject.name, _charAnimator, _charMove, _charJump, _charAttack)
            {
                CharFacing = CharacterAnimation.Facing.right
            };
            _charInput = new CharacterInput(_charMove, _charJump, _charAttack);
        }

        private void OnEnable() => _charInput.EnableInputMap();
        private void OnDisable() => _charInput.DisableInputMap();

        private void FixedUpdate()
        {
            _charJump.CheckJump();
            _charAttack.CheckAttack();
            _charMove.Move(_charAnim);
            _charMove.ManagePhysics(_charJump.OnGround);
            _charAnim.ManageAnimation(_charRig.velocity.x);
        }
    }
}
