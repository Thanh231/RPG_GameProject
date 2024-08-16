public class StateMachine 
{
   public StateActor state;
   public void Iniatial(StateActor currentState)
   {
        state = currentState;
        state.Enter();
   }
    public void ChangeState(StateActor newState)
    {
        state.Exit();
        state = newState;
        state.Enter();
    }
}
