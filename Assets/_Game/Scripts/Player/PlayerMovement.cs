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

        private CharacterController controller;
        private PlayerInputHandler input;
        private float verticalSpeed;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInputHandler>();
        }

        private void Update()
        {
            if (!input.isActiveAndEnabled)
                return;

            bool grounded = controller.isGrounded;
            if (grounded && verticalSpeed < 0f)
                verticalSpeed = -2f;

            if (grounded && input.JumpPressed)
                verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);

            Vector2 axis = Vector2.ClampMagnitude(input.Move, 1f);
            Vector3 planar = (transform.right * axis.x + transform.forward * axis.y);
            float speed = input.SprintHeld ? sprintSpeed : walkSpeed;

            verticalSpeed += gravity * Time.deltaTime;
            controller.Move((planar * speed + Vector3.up * verticalSpeed) * Time.deltaTime);
        }
    }
}
