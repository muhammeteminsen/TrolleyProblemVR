
public abstract class GameState
{
    public abstract void EnterState(GameStateManager state);
    public abstract void UpdateState(GameStateManager state);
    public abstract void ExitState(GameStateManager state);
}
