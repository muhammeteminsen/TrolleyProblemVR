using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Interaction _interaction;
    

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.XRLeft.Secondary.performed+= OnSecondaryButtonLeft;
        _playerInput.XRLeft.Primary.performed+= OnPrimaryButtonLeft;
        _playerInput.XRRight.Secondary.performed+= OnSecondaryButtonRight;
        _playerInput.XRRight.Primary.performed+= OnPrimaryButtonRight;
        _playerInput.XRLeft.Grip.performed+= OnGripButtonLeft;
        _playerInput.XRRight.Grip.performed+= OnGripButtonRight;
        _interaction = GetComponent<Interaction>();
    }

    private void OnGripButtonRight(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            Debug.Log("Grip Button Right Pressed");
            _interaction.LoadMainMenu();
            _playerInput.XRRight.Grip.performed-= OnGripButtonRight;
        }
    }

    private void OnGripButtonLeft(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            Debug.Log("Grip Button Left Pressed");
            _interaction.LoadActiveScene();
            _playerInput.XRLeft.Grip.performed-= OnGripButtonLeft;
        }
    }

    private void OnSecondaryButtonRight(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            Debug.Log("Secondary Button Right Pressed");
            _playerInput.XRRight.Secondary.performed-= OnSecondaryButtonRight;
        }
    }

    private void OnPrimaryButtonRight(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            Debug.Log("Primary Button Right Pressed");
            _playerInput.XRRight.Primary.performed-= OnPrimaryButtonRight;
        }
    }

    private void OnPrimaryButtonLeft(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            _interaction.HandleInteraction();
            Debug.Log("Primary Button Left Pressed");
        }
    }


    private void OnSecondaryButtonLeft(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            Debug.Log("Secondary Button Left Pressed");
            _playerInput.XRLeft.Secondary.performed-= OnSecondaryButtonLeft;
        }
    }
    
    public bool InteractPrimaryButtonLeft()
    {
        Debug.Log("InteractPrimaryButtonLeft");
        return _playerInput.XRLeft.Primary.ReadValue<float>() > 0.5f;
    }

    public bool InteractSecondaryButtonLeft()
    {
        return _playerInput.XRLeft.Secondary.ReadValue<float>() > 0.5f;
    }
    public bool InteractGripButtonLeft()
    {
        Debug.Log("InteractGripButtonLeft");
        return _playerInput.XRLeft.Grip.ReadValue<float>() > 0.5f;
    }
    
    public bool InteractPrimaryButtonRight()
    {
        Debug.Log("InteractPrimaryButtonRight");
        return _playerInput.XRRight.Primary.ReadValue<float>() > 0.5f;
    }

    public bool InteractSecondaryButtonRight()
    {
        return _playerInput.XRRight.Secondary.ReadValue<float>() > 0.5f;
    }
    public bool InteractGripButtonRight()
    {
        Debug.Log("InteractGripButtonRight");
        return _playerInput.XRRight.Grip.ReadValue<float>() > 0.5f;
    }
}
