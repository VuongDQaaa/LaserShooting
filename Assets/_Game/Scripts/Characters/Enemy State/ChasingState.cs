using UnityEngine;
public class ChasingState : IState
{
    private float timer;
    public void OnEnter(EnemyController bot)
    {
        timer = Random.Range(4, 8);
        bot.OnChasing();
    }

    public void OnExecute(EnemyController bot)
    {
        if (!bot.isDead)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                bot.agent.destination = bot.currentTarget.position;
            }
            else if (timer <= 0)
            {
                bot.currentTarget = null;
                bot.ChangeState(new IdleState());
            }
        }
    }

    public void OnExit(EnemyController bot)
    {
        timer = 0;
    }
}