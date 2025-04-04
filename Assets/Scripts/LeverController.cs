using UnityEngine;

public class LeverController : MonoBehaviour
{
    private void Update()
    {
        // Thumbsticks
        Vector2 primaryThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 secondaryThumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        if (primaryThumbstick != Vector2.zero) Debug.Log("Primary Thumbstick: " + primaryThumbstick);
        if (secondaryThumbstick != Vector2.zero) Debug.Log("Secondary Thumbstick: " + secondaryThumbstick);

        // Thumbstick Button Press
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)) Debug.Log("Primary Thumbstick Button Pressed");
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick)) Debug.Log("Secondary Thumbstick Button Pressed");

        // Buttons
        if (OVRInput.GetDown(OVRInput.Button.One)) Debug.Log("Button One Pressed");
        if (OVRInput.GetDown(OVRInput.Button.Two)) Debug.Log("Button Two Pressed");
        if (OVRInput.GetDown(OVRInput.Button.Three)) Debug.Log("Button Three Pressed");
        if (OVRInput.GetDown(OVRInput.Button.Four)) Debug.Log("Button Four Pressed");
        if (OVRInput.GetDown(OVRInput.Button.Start)) Debug.Log("Start Button Pressed");

        // Triggers
        float primaryIndexTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float secondaryIndexTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if (primaryIndexTrigger > 0) Debug.Log("Primary Index Trigger: " + primaryIndexTrigger);
        if (secondaryIndexTrigger > 0) Debug.Log("Secondary Index Trigger: " + secondaryIndexTrigger);

        // Hand Triggers
        float primaryHandTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        float secondaryHandTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        if (primaryHandTrigger > 0) Debug.Log("Primary Hand Trigger: " + primaryHandTrigger);
        if (secondaryHandTrigger > 0) Debug.Log("Secondary Hand Trigger: " + secondaryHandTrigger);

        // Thumb Rest Touch
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbRest)) Debug.Log("Primary Thumb Rest Touched");
        if (OVRInput.Get(OVRInput.Touch.SecondaryThumbRest)) Debug.Log("Secondary Thumb Rest Touched");

        // Reserved Button (if used)
        if (OVRInput.GetDown(OVRInput.Button.Back)) Debug.Log("Reserved Button Pressed");
    }
}