
using UnityEngine;

namespace BeatlesBattle.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerMovement))]
    public sealed class PlayerAnimationController : MonoBehaviour
    {
        private static readonly int SpeedHash =
            Animator.StringToHash("Speed");

        private static readonly int IsCrouchingHash =
            Animator.StringToHash("IsCrouching");

        private static readonly int IsSlidingHash =
        Animator.StringToHash("IsSliding");

        private static readonly int IsJumpingHash =
        Animator.StringToHash("IsJumping");

        [Header("References")]
        [SerializeField] private Animator soldierAnimator;

        [Header("Animation")]
        [SerializeField, Min(0f)]
        private float speedDamping = 0.1f;

        private CharacterController characterController;
        private PlayerMovement playerMovement;

        private bool jumpActive;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            playerMovement = GetComponent<PlayerMovement>();

            if (soldierAnimator == null)
            {
                Debug.LogWarning(
                    "Assign the soldier Animator to PlayerAnimationController.",
                    this);

                enabled = false;
                return;
            }

            // PlayerMovement controls movement, not animation root motion.
            soldierAnimator.applyRootMotion = false;
        }

        private void LateUpdate()
        {
            Vector3 velocity = characterController.velocity;

            // Ignore vertical movement from jumping and gravity.
            float horizontalSpeed = new Vector2(
                velocity.x,
                velocity.z
            ).magnitude;

            soldierAnimator.SetFloat(
                SpeedHash,
                horizontalSpeed,
                speedDamping,
                Time.deltaTime
            );

            // Use the actual capsule height so low ceilings are handled.
            bool isCrouching =
                playerMovement.CurrentHeight <
                playerMovement.StandingHeight - 0.05f;

            soldierAnimator.SetBool(
                IsCrouchingHash,
                isCrouching
            );

            soldierAnimator.SetBool(
                IsSlidingHash,
                playerMovement.IsSliding
            );

            // A genuine player jump starts with upward movement.
            // Walking off a ledge does not trigger this animation.
            if (characterController.isGrounded)
            {
                jumpActive = false;
            }
            else if (!playerMovement.IsSliding &&
                     velocity.y > 0.1f)
            {
                jumpActive = true;
            }

            soldierAnimator.SetBool(
                IsJumpingHash,
                jumpActive
            );
        }
    }
}
