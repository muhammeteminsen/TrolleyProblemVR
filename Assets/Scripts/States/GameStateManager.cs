using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameState currentState { get; set; }
    public PlayHandler playHandler { get; private set; }
    public PauseHandler pauseHandler { get; set; }
    
    public bool hasInteraction { get; set; }
    private void Awake()
    {
        ChangeState(new PauseState());
        playHandler = GetComponent<PlayHandler>();
        pauseHandler = GetComponent<PauseHandler>();
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
}
