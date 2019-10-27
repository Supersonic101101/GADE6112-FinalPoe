using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    Transform target2;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        target2 = PlayerManager.instance.player2.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        float distance2 = Vector3.Distance(target2.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            agent.SetDestination(target2.position);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Wizard Attacks!");
    }
}
