using UnityEngine;

public class SwitchState : GameState
{
    public override void EnterState(GameStateManager state)
    {
        
    }

    public override void UpdateState(GameStateManager state)
    {
        state.playHandler?.UpdateSwitch();
    }

    public override void ExitState(GameStateManager state)
    {
        
    }
}
