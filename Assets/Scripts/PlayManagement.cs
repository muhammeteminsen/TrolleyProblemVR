using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayManagement : MonoBehaviour
{
    private GameStateManager _gameStateManager;

    private void Awake()
    {
        _gameStateManager = GetComponent<GameStateManager>();
    }

    private void Update()
    {
        Play();
        Pause();
    }

    public void Play()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || Keyboard.current[Key.Z].wasPressedThisFrame)
        {
            if (_gameStateManager.hasInteraction) return;
            _gameStateManager.GetPulledInteraction();
        }
       
      
    }

    public void Pause()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three) || Keyboard.current[Key.X].wasPressedThisFrame)
        {
            _gameStateManager.ChangeState(new PauseState());
        }
        
    }
}