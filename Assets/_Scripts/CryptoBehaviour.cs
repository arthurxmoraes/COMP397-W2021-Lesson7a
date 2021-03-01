using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum CryptoState
{
    IDLE,
    RUN,
    JUMP
}

public class CryptoBehaviour : MonoBehaviour
{
    [Header("Line of sight")]
    public bool hasLOS;
    public GameObject player;

    private NavMeshAgent agent;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLOS)
        {
            agent.SetDestination(player.transform.position);
        }

        if (hasLOS && (Vector3.Distance(transform.position, player.transform.position) < 2.5))
        {
            //could be attack
            animator.SetInteger("AnimState", (int)CryptoState.IDLE);
            transform.LookAt(transform.position - player.transform.position);

            if (agent.isOnOffMeshLink)
            {
                animator.SetInteger("AnimState", (int)CryptoState.JUMP);
            }

        }
        else
        {
            animator.SetInteger("AnimState", (int)CryptoState.RUN);
        }    
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag("Player"))
        {
            hasLOS = true;
            player = other.transform.gameObject;
        }        
    }
}
