using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] patrolPoints;
    public float waitTime;
    int currentPointIndex;
    bool once;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, patrolPoints[currentPointIndex].position)>0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            //Debug.Log(transform.position);
        }
        else
        {
            //Debug.Log("yahoo");

            if (once==false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(waitTime);
        Debug.Log(currentPointIndex);

        if (currentPointIndex+1<patrolPoints.Length)
        {

            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }

        once = false;
    }
    private void FindTarget()
    {
        
    }
}
