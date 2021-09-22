using System;
using UnityEngine;

namespace Character
{
    public class Character : MonoBehaviour
    {
        [Header("Jump")]
        [SerializeField] private float jumpSpeed = 14.5f;
        [SerializeField] private float jumpDelay = 0.25f;
        [SerializeField] private Vector3 leftColliderOffset;
        [SerializeField] private Vector3 rightColliderOffset;
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float maxSpeed = 8f;
        [Header("Attack")]
        [SerializeField] private float attackSpeed = 6f;
        [SerializeField] private float baseDamage = 20f;
        [Header("Physics")]
        [SerializeField] private float gravity = 2f;
        [SerializeField] private float fallMultiplier = 5f;
        /*[Header("Status")]
        [SerializeField] private float _totalHP = 100f;
        [SerializeField] private bool _isUnlocked = false;
        */
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
            _charJump = new CharacterJump(jumpSpeed, jumpDelay, _charRig);
            _charAttack = new CharacterAttack(baseDamage, attackSpeed);
            _charAnim = new CharacterAnimation(gameObject.name, _charAnimator, _charMove, _charJump, _charAttack)
            {
                CharFacing = CharacterAnimation.Facing.right
            };
            _charInput = new CharacterInput(_charMove, _charJump, _charAttack);
        }

        private void OnEnable() => _charInput.EnableInputMap();
        private void OnDisable() => _charInput.DisableInputMap();

        private void Update()
        {
            _charJump.CheckJump(new Tuple<Vector3, Vector3>(rightColliderOffset, leftColliderOffset));
        }

        private void FixedUpdate()
        {
            _charJump.Jump();
            _charAttack.CheckAttack();
            _charMove.Move(_charAnim);
            _charMove.ManagePhysics(_charJump.OnGround, gravity, fallMultiplier, _charJump.JumpPressed);
            _charAnim.ManageAnimation(_charRig.velocity.x);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + rightColliderOffset, transform.position + rightColliderOffset + Vector3.down * 0.55f);
            Gizmos.DrawLine(transform.position - leftColliderOffset, transform.position - leftColliderOffset + Vector3.down * 0.55f);
        }
    }
}
