public class DanceState : IState
{
    public void OnEnter(EnemyController bot)
    {
        bot.OnDance();
    }

    public void OnExecute(EnemyController bot)
    {
        
    }

    public void OnExit(EnemyController bot)
    {
        
    }
}