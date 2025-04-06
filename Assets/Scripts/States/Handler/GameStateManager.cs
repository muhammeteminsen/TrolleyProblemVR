using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameState currentState { get; set; }
    public PlayHandler playHandler { get; private set; }
    public bool hasInteraction { get; set; }
    public bool hasPulled { get; set; }
    
    private void Awake()
    {
        ChangeState(new PauseState());
        playHandler = GetComponent<PlayHandler>();
    }

    private void Update()
    {
        currentState?.UpdateState(this);
    }

    public void ChangeState(GameState newState)
    {
        if (currentState?.GetType() == newState.GetType()) return;
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void GetPulledInteraction()
    {
        switch (hasPulled)
        {
            case true:
                ChangeState(new SwitchState());
                break;
            default:
                ChangeState(new UnSwitchState());
                break;
        }
    }
    
}
