using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum AnimationState
{
    idle = 0,
    run = 1,
    dance = 2,
    dead = 3
}

public class EnemyController : MonoBehaviour
{
    [Header("Bot Setting")]
    [SerializeField] private CanvasCharacter canvasCharacter;
    [SerializeField] private float detectTargetRange;
    [SerializeField] private LayerMask playerLayer;
    public int maxHP;

    [Header("Bot status")]
    public Transform detectedTarget;
    public Transform currentTarget;
    public MapController currentMap;
    public int currentHP;
    public NavMeshAgent agent;
    public bool isDead;

    [Header("Animation")]
    [SerializeField] private Animator anim;
    private AnimationState currentAnimationState;
    private IState currentState;
    private bool targetDetected;

    // Start is called before the first frame update
    private void Start()
    {
        isDead = false;
        currentHP = maxHP;
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new IdleState());
    }

    private void Update()
    {
        DetectPlayer();
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

        if(GameManager.Instance.currentGameState == GameState.Loose)
        {
            ChangeState(new DanceState());
        }
    }

    public void TakeDamage(int damage)
    {
        canvasCharacter.UpdateLostedHP(damage);
        currentHP -= damage;
        if (currentHP <= 0)
        {
            agent.isStopped = true;
            OnDie();
        }
    }

    //Debug detect target
    void OnDrawGizmosSelected()
    {
        // Draw detect radius in screen view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectTargetRange);
    }

    private void DetectPlayer()
    {
        targetDetected = false;
        //use Overlap to detect all collider in attack range
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, detectTargetRange, playerLayer);
        //Get player as target
        foreach (Collider item in targetsInViewRadius)
        {
            if (item.CompareTag(Constant.PLAYER_TAG_NAME))
            {
                targetDetected = true;
                detectedTarget = item.transform;
                break;
            }
        }

        if (!targetDetected)
        {
            if (detectedTarget != null)
            {
                detectedTarget = null;
            }
        }
    }

    public void OnIdle()
    {
        agent.ResetPath();
        ChangeAnim(AnimationState.idle);
    }

    public void OnPatrol()
    {
        //get a random postion on map
        float maxMovingRange = Random.Range(15f, 20f);
        Vector3 randomDirection = Random.insideUnitCircle * maxMovingRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, maxMovingRange, 1);
        Vector3 finalPostion = hit.position;

        //move character
        ChangeAnim(AnimationState.run);
        agent.destination = finalPostion;
    }

    public void OnChasing()
    {
        currentTarget = detectedTarget;
        ChangeAnim(AnimationState.run);
    }

    private void OnDie()
    {
        currentMap.currentEnemies--;
        if (currentMap.currentEnemies == 0)
        {
            GameManager.Instance.currentGameState = GameState.Victory;
            UIManager.Instance.OpenUI<CanvasWin>();
        }
        isDead = true;
        StartCoroutine(SetDie());
    }

    public void OnDance()
    {
        agent.isStopped = true;
        ChangeAnim(AnimationState.dance);
    }

    IEnumerator SetDie()
    {
        ChangeAnim(AnimationState.dead);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void ChangeAnim(AnimationState animationState)
    {
        if (currentAnimationState != animationState)
        {
            anim.ResetTrigger(animationState.ToString());
            currentAnimationState = animationState;
            anim.SetTrigger(currentAnimationState.ToString());
        }
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

}
