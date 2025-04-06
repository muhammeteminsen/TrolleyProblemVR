public class PauseState : GameState
{
    public override void EnterState(GameStateManager state)
    {
        state.playHandler?.ExitPlay(state);
    }

    public override void UpdateState(GameStateManager state)
    {
       
    }

    public override void ExitState(GameStateManager state)
    {
       
    }
}
