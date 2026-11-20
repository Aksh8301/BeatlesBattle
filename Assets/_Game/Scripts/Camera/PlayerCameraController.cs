using UnityEngine;
using BeatlesBattle.Player;

namespace BeatlesBattle.CameraSystem
{
    /// <summary>Local-camera prototype. No networking or weapon-aim logic is coupled to this class.</summary>
    [RequireComponent(typeof(PlayerInputHandler))]
    public sealed class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Renderer playerVisual;
        [SerializeField] private Transform characterVisualRoot;

        private Renderer[] characterRenderers;


        [Header("Look")]
        [SerializeField, Min(0.01f)] private float mouseSensitivity = 0.12f;
        [SerializeField, Range(30f, 89f)] private float maxPitch = 80f;
        [Header("Camera positions")]
        [SerializeField] private float eyeHeight = 1.65f;
        [SerializeField] private Vector3 thirdPersonOffset = new Vector3(0.45f, 1.55f, -3f);
        [SerializeField, Min(0.01f)] private float cameraCollisionRadius = 0.2f;
        [SerializeField] private LayerMask cameraCollisionLayers = ~0;
        [SerializeField, Min(0.1f)] private float stanceCameraSpeed = 6f;

        private PlayerInputHandler input;
        private PlayerMovement movement;
        private float pitch;
        private float currentEyeHeight;
        private bool thirdPerson = true;
        private readonly RaycastHit[] cameraHits = new RaycastHit[16];

        private void Awake()
        {
            input = GetComponent<PlayerInputHandler>();
            movement = GetComponent<PlayerMovement>();
            currentEyeHeight = eyeHeight;

            if (playerVisual != null)
                playerVisual.enabled = false;

            if (characterVisualRoot != null)
                characterRenderers =
                    characterVisualRoot.GetComponentsInChildren<Renderer>(true);

            if (playerCamera == null)
                Debug.LogError("Assign Main Camera to PlayerCameraController.", this);
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (characterRenderers != null)
            {
                foreach (Renderer renderer in characterRenderers)
                {
                    if (renderer != null)
                        renderer.enabled = true;
                }
            }
        }

        private void Update()
        {
            if (playerCamera == null || !input.isActiveAndEnabled)
                return;

            if (KeyboardEscapePressed())
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (UnityEngine.InputSystem.Mouse.current?.leftButton.wasPressedThisFrame == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Vector2 look = input.Look * mouseSensitivity;
                transform.Rotate(Vector3.up, look.x, Space.Self);
                pitch = Mathf.Clamp(pitch - look.y, -maxPitch, maxPitch);
            }

            if (input.PerspectivePressed)
                thirdPerson = !thirdPerson;

            if (playerVisual != null && playerVisual.enabled)
                playerVisual.enabled = false;

            if (characterRenderers != null)
            {
                foreach (Renderer renderer in characterRenderers)
                {
                    if (renderer != null && renderer.enabled != thirdPerson)
                        renderer.enabled = thirdPerson;
                }
            }
        }

        private void LateUpdate()
        {
            if (playerCamera == null)
                return;

            float targetEyeHeight = movement != null
                ? Mathf.Min(eyeHeight, movement.CurrentHeight - 0.15f)
                : eyeHeight;
            currentEyeHeight = Mathf.MoveTowards(currentEyeHeight, targetEyeHeight,
                stanceCameraSpeed * Time.deltaTime);

            Quaternion lookRotation = transform.rotation * Quaternion.Euler(pitch, 0f, 0f);
            Vector3 target = transform.position + Vector3.up * currentEyeHeight;
            Vector3 desired = thirdPerson
                ? target + lookRotation * new Vector3(thirdPersonOffset.x, thirdPersonOffset.y - eyeHeight, thirdPersonOffset.z)
                : target;

            if (thirdPerson)
            {
                Vector3 delta = desired - target;
                float distance = delta.magnitude;
                if (distance > 0.001f)
                {
                    Vector3 direction = delta / distance;
                    int hitCount = Physics.SphereCastNonAlloc(target, cameraCollisionRadius, direction,
                        cameraHits, distance, cameraCollisionLayers, QueryTriggerInteraction.Ignore);
                    float nearest = distance;
                    for (int i = 0; i < hitCount; i++)
                    {
                        // Never collide the camera with the player's own body.
                        if (cameraHits[i].collider.transform.IsChildOf(transform))
                            continue;
                        nearest = Mathf.Min(nearest, cameraHits[i].distance);
                    }
                    if (nearest < distance)
                        desired = target + direction * Mathf.Max(0f, nearest - cameraCollisionRadius);
                }
            }

            playerCamera.transform.SetPositionAndRotation(desired, lookRotation);
        }

        private static bool KeyboardEscapePressed()
        {
            return UnityEngine.InputSystem.Keyboard.current?.escapeKey.wasPressedThisFrame == true;
        }
    }
}
