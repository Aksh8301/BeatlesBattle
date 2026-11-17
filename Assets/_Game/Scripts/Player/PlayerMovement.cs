using UnityEngine;

namespace BeatlesBattle.Player
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInputHandler))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField, Min(0.1f)] private float walkSpeed = 4.5f;
        [SerializeField, Min(0.1f)] private float sprintSpeed = 7f;
        [SerializeField, Min(0.1f)] private float jumpHeight = 1.1f;
        [SerializeField] private float gravity = -20f;

        [Header("Crouch")]
        [SerializeField, Range(0.6f, 1.8f)] private float crouchHeight = 1.2f;
        [SerializeField, Min(0.1f)] private float crouchSpeed = 2.3f;
        [SerializeField, Min(0.1f)] private float stanceChangeSpeed = 5f;
        [SerializeField] private LayerMask standingObstacleLayers = ~0;

        [Header("Slide")]
        [SerializeField, Min(0.1f)] private float slideDuration = 0.65f;
        [SerializeField, Min(0.1f)] private float slideStartSpeed = 9f;
        [SerializeField, Min(0.1f)] private float slideEndSpeed = 3f;

        private CharacterController controller;
        private PlayerInputHandler input;
        private float verticalSpeed;
        private float standingHeight;
        private float standingCenterY;
        private float slideRemaining;
        private Vector3 slideDirection;
        private readonly RaycastHit[] clearanceHits = new RaycastHit[16];

        public float CurrentHeight => controller != null ? controller.height : 2f;
        public float StandingHeight => standingHeight > 0f ? standingHeight : 2f;
        public bool IsSliding => slideRemaining > 0f;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInputHandler>();
            standingHeight = controller.height;
            standingCenterY = controller.center.y;
        }

        private void Update()
        {
            if (!input.isActiveAndEnabled)
                return;

            bool grounded = controller.isGrounded;
            if (grounded && verticalSpeed < 0f)
                verticalSpeed = -2f;

            Vector2 axis = Vector2.ClampMagnitude(input.Move, 1f);
            Vector3 planar = transform.right * axis.x + transform.forward * axis.y;

            // Slide is a one-shot action, only while sprinting forward/on the ground.
            if (!IsSliding && grounded && input.SprintHeld && input.CrouchPressed &&
                axis.y > 0.1f && planar.sqrMagnitude > 0.01f)
            {
                slideRemaining = slideDuration;
                slideDirection = planar.normalized;
            }

            bool wantsCrouch = input.CrouchHeld || IsSliding;
            bool canStand = !wantsCrouch && CanStand();
            bool crouched = wantsCrouch || !canStand;
            UpdateCapsuleHeight(crouched ? Mathf.Min(crouchHeight, standingHeight) : standingHeight);

            if (grounded && !crouched && input.JumpPressed)
                verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);

            Vector3 planarVelocity;
            if (IsSliding)
            {
                float progress = 1f - Mathf.Clamp01(slideRemaining / slideDuration);
                float speed = Mathf.Lerp(slideStartSpeed, slideEndSpeed, progress);
                planarVelocity = slideDirection * speed;
                slideRemaining = Mathf.Max(0f, slideRemaining - Time.deltaTime);
                if (!grounded)
                    slideRemaining = 0f;
            }
            else
            {
                float speed = crouched ? crouchSpeed : input.SprintHeld ? sprintSpeed : walkSpeed;
                planarVelocity = planar * speed;
            }

            verticalSpeed += gravity * Time.deltaTime;
            controller.Move((planarVelocity + Vector3.up * verticalSpeed) * Time.deltaTime);
        }

        private void UpdateCapsuleHeight(float desiredHeight)
        {
            // Preserve the original foot position as capsule height changes.
            float minimumHeight = controller.radius * 2f + 0.01f;
            float nextHeight = Mathf.MoveTowards(controller.height,
                Mathf.Max(minimumHeight, desiredHeight), stanceChangeSpeed * Time.deltaTime);
            controller.height = nextHeight;
            Vector3 center = controller.center;
            center.y = standingCenterY - (standingHeight - nextHeight) * 0.5f;
            controller.center = center;
        }

        private bool CanStand()
        {
            if (controller.height >= standingHeight - 0.01f)
                return true;

            // Sweep the upper cap upward; ignore player's own colliders.
            // This avoids ground contact being mistaken for a low ceiling.
            float radius = Mathf.Max(0.05f, controller.radius - 0.025f);
            float feetY = transform.position.y + standingCenterY - standingHeight * 0.5f;
            float currentTopSphereY = feetY + controller.height - radius;
            float distance = standingHeight - controller.height;
            Vector3 origin = new Vector3(transform.position.x, currentTopSphereY, transform.position.z);
            int count = Physics.SphereCastNonAlloc(origin, radius, Vector3.up,
                clearanceHits, distance, standingObstacleLayers, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < count; i++)
            {
                Collider hitCollider = clearanceHits[i].collider;
                if (hitCollider != null && !hitCollider.transform.IsChildOf(transform))
                    return false;
            }
            return true;
        }
    }
}
