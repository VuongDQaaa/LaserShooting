public class PatrolState : IState
{
    public void OnEnter(EnemyController bot)
    {
        bot.OnPatrol();
    }

    public void OnExecute(EnemyController bot)
    {
        if (!bot.isDead)
        {
            if (bot.agent.remainingDistance < 0.1f)
            {
                bot.ChangeState(new IdleState());
            }

            if (bot.detectedTarget != null)
            {
                bot.ChangeState(new ChasingState());
            }
        }
    }

    public void OnExit(EnemyController bot)
    {

    }
}