using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolRandom : MonoBehaviour
{
    public float speed;
    public float startwaitTime=2.0f;

    private float waitTime;
  
    public Transform moveSpot;
    [SerializeField]private Transform playerTransform;
   

    bool once;

    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float searchDistance=5.0f;
    private float retreatDistance=5.0f;

    //private List<Vector2> prevPos = new List<Vector2>();
    private Vector2 prevPos;
    private State state;

    private enum State
    {
        Roaming,
        ChaseTarget,
        Retreat,
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpot.position = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        waitTime = startwaitTime;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, moveSpot.position);
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, playerTransform.position);

    }
    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(waitTime);
        switch (state)
        {
            case State.Roaming:
                if (Vector2.Distance(transform.position, moveSpot.position) > 0.2f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
                    
                }
                else
                {
                    Wait();
                }
                FindTarget();
                break;
            case State.ChaseTarget:
                if (Vector2.Distance(transform.position, playerTransform.position) > searchDistance)
                {
                    state = State.Roaming;
                }
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                break;
            case State.Retreat:
                if (Vector2.Distance(transform.position, prevPos) > retreatDistance)
                {
                    moveSpot.position = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
                    state = State.Roaming;
                }
                transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, -speed * Time.deltaTime);
                break;

            default:
                break;
        }

        //FindTarget();


    }


    private void Wait()
    {
        if (waitTime<=0)
        {
            moveSpot.position = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
            waitTime = startwaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
    private void FindTarget()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) < searchDistance)
        {
            state = State.ChaseTarget;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Obstacle")&&state!=State.ChaseTarget)
        {


            state = State.Retreat;
            //prevPos.Add(collision.gameObject.transform.position);
            prevPos = collision.gameObject.transform.position;


        }
    }
}
