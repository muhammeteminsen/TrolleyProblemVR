using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Interaction _interaction;
    private TutorialManagement _tutorialManagement;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.XRLeft.Primary.performed += OnPrimaryButtonLeft;
        _playerInput.XRRight.Primary.performed += OnPrimaryButtonRight;
        _playerInput.XRLeft.Grip.performed += OnGripButtonLeft;
        _playerInput.XRRight.Grip.performed += OnGripButtonRight;
        _interaction = GetComponent<Interaction>();
        _tutorialManagement = GetComponent<TutorialManagement>();
    }

    private void OnGripButtonRight(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            _interaction?.LoadMainMenu();
            _playerInput.XRRight.Grip.performed -= OnGripButtonRight;
        }
    }

    private void OnGripButtonLeft(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            _interaction?.LoadActiveScene();
            _playerInput.XRLeft.Grip.performed -= OnGripButtonLeft;
        }
    }

    private void OnPrimaryButtonRight(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            _tutorialManagement?.ExitTutorial();
            _playerInput.XRRight.Primary.performed -= OnPrimaryButtonRight;
        }
    }

    private void OnPrimaryButtonLeft(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if (_tutorialManagement !=null )
            {
                if (TutorialBase.isTutorialExit)
                {
                    _interaction?.HandleInteraction(); 
                }
            }
            else
            {
                _interaction?.HandleInteraction(); 
            }
            _tutorialManagement?.StartTutorial();
        }
    }

    public bool InteractSecondaryButtonLeft()
    {
        return _playerInput.XRLeft.Secondary.ReadValue<float>() > 0.5f;
    }
    
    
    public bool InteractSecondaryButtonRight()
    {
        return _playerInput.XRRight.Secondary.ReadValue<float>() > 0.5f;
    }
}