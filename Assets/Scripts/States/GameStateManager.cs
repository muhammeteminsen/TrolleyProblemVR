using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private GameState currentState { get; set; }
    public PlayHandler playHandler { get; private set; }
    public PauseHandler PauseHandler { get; private set; }
    private void Awake()
    {
        ChangeState(new PauseState());
        playHandler = GetComponent<PlayHandler>();
        PauseHandler = GetComponent<PauseHandler>();
    }

    private void Update()
    {
        currentState?.UpdateState(this);
    }

    public void ChangeState(GameState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}
