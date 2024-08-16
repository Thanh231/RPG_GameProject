public class EnemyStateMachine 
{
    public EnemyState currentState;
    
    public void Initial(EnemyState state)
    {
        currentState = state;
        currentState.Enter();
    }
    public void ChangeState(EnemyState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
