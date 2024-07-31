using UnityEngine;
public class IdleState : IState
{
    float timer;

    public void OnEnter(EnemyController bot)
    {
        timer = Random.Range(2, 4);
    }

    public void OnExecute(EnemyController bot)
    {
        if(timer > 0)
        {
            bot.OnIdle();
            timer -= Time.deltaTime;
        }
        else if(timer <= 0)
        {
            bot.ChangeState(new PatrolState());
        }

        if(bot.detectedTarget != null)
        {
            bot.ChangeState(new ChasingState());
        }
    }

    public void OnExit(EnemyController bot)
    {
        timer = 0;
    }
}