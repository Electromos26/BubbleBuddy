using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        public Vector2 Movement { get; private set; }
        public Vector2 MousePos { get; private set; }
        public bool IsAttacking { get; private set; }

        //public bool Dash { get; private set; }

        private PlayerInput _playerInput;
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
            
            _playerInput.Player.Move.performed += Move;
            _playerInput.Player.Move.canceled += Move;
            
            _playerInput.Player.Attack.performed += Attack;
            _playerInput.Player.Dash.performed += Dash;

            _playerInput.Player.Look.performed += Look;
        }

        private void OnDisable()
        {
            _playerInput.Player.Move.performed -= Move;
            _playerInput.Player.Move.canceled -= Move;

            _playerInput.Player.Attack.performed -= Attack;
            _playerInput.Player.Dash.performed -= Dash;
            
            _playerInput.Player.Look.performed -= Look;
            
            Movement = Vector2.zero;
            MousePos = Vector2.zero;
            IsAttacking = false;

            _playerInput.Disable();
        }

        #region Input Actions

        private void Move(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>();
        }

        private void Attack(InputAction.CallbackContext context)
        {
           _playerController.HandleAttack();
        }
        
        private void Dash(InputAction.CallbackContext context)
        {
            _playerController.HandleDash();
        }
        
        private void Look(InputAction.CallbackContext context)
        {
            MousePos = context.ReadValue<Vector2>();
        }

        #endregion
    }
}