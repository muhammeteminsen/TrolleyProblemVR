public class UnSwitchState : GameState
{
    public override void EnterState(GameStateManager state)
    {
        
    }

    public override void UpdateState(GameStateManager state)
    {
        state.playHandler?.UpdateUnSwitch(state);
    }

    public override void ExitState(GameStateManager state)
    {
        
    }
}
