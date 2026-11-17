using UnityEngine;
using UnityEngine.InputSystem;

namespace BeatlesBattle.Player
{
    /// <summary>Local prototype input adapter. Network gameplay will consume commands, not device input directly.</summary>
    public sealed class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActions;

        private InputActionMap playerMap;
        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction jumpAction;
        private InputAction sprintAction;
        private InputAction crouchAction;
        private InputAction switchPerspectiveAction;

        public Vector2 Move => moveAction?.ReadValue<Vector2>() ?? Vector2.zero;
        public Vector2 Look => lookAction?.ReadValue<Vector2>() ?? Vector2.zero;
        public bool SprintHeld => sprintAction != null && sprintAction.IsPressed();
        public bool CrouchHeld => crouchAction != null && crouchAction.IsPressed();
        public bool JumpPressed => jumpAction != null && jumpAction.WasPressedThisFrame();
        public bool PerspectivePressed => switchPerspectiveAction != null && switchPerspectiveAction.WasPressedThisFrame();

        private void Awake()
        {
            if (inputActions == null)
            {
                Debug.LogError("Assign InputSystem_Actions to PlayerInputHandler.", this);
                enabled = false;
                return;
            }

            // A separate runtime instance avoids changing enable states on the shared project asset.
            inputActions = Instantiate(inputActions);
            playerMap = inputActions.FindActionMap("Player", true);
            moveAction = playerMap.FindAction("Move", true);
            lookAction = playerMap.FindAction("Look", true);
            jumpAction = playerMap.FindAction("Jump", true);
            sprintAction = playerMap.FindAction("Sprint", true);
            crouchAction = playerMap.FindAction("Crouch", true);

            // Prototype-only binding; no edits to the existing Input Actions asset.
            switchPerspectiveAction = new InputAction("SwitchPerspective", InputActionType.Button, "<Keyboard>/v");
        }

        private void OnEnable()
        {
            playerMap?.Enable();
            switchPerspectiveAction?.Enable();
        }

        private void Start()
        {
            // OnEnable precedes Awake's action setup on the first enable.
            playerMap?.Enable();
            switchPerspectiveAction?.Enable();
        }

        private void OnDisable()
        {
            playerMap?.Disable();
            switchPerspectiveAction?.Disable();
        }

        private void OnDestroy()
        {
            switchPerspectiveAction?.Dispose();
            if (inputActions != null)
                Destroy(inputActions);
        }
    }
}
