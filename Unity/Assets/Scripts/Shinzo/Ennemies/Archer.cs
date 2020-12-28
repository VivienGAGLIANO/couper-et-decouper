using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Archer : Monster
{
    public float attackRange = 10000f;

    public float attackAnimationTime = 1.3f;

    public float baseAttackCD = 5f;

    private float attackCD = 0f;

    private bool canMove = true;
    
    private bool isAttacking = false;

    private Vector3 distanceFromTarget;


    new void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        objective = DetermineObjective();
        agent.SetDestination(objective);
        maxHP = 5;
        currentHp = 5;
    }

    protected new void Update()
    {
        if (!isDead)
        {
            base.Update();

            attackCD += Time.deltaTime;

            if (distanceFromTarget.magnitude <= attackRange && attackCD>= baseAttackCD && !isAttacking)
            {
                canMove = false;
                agent.isStopped = true;
                isAttacking = true;
                StartCoroutine(Attack());
            }
        }
        
    }

    protected void FixedUpdate()
    { 
        if (!isDead)
        {
            distanceFromTarget = GameManager.instance.player.transform.position - transform.position;
        }
        
    }

    private Vector3 DetermineObjective()
    {
    
        Vector3 randVect = GameManager.instance.floorTiles[Random.Range(0,GameManager.instance.floorTiles.Count-1)].position;
        var d = Vector3.Distance(GameManager.instance.player.transform.position,randVect);
        var a = Vector3.Angle(transform.position - GameManager.instance.player.transform.position, randVect - GameManager.instance.player.transform.position);
        while ( d<=16 || d>=48 || a >=90 )
        {
            randVect = GameManager.instance.floorTiles[Random.Range(0,GameManager.instance.floorTiles.Count-1)].position;
            d = Vector3.Distance(GameManager.instance.player.transform.position,randVect);
            a = Vector3.Angle(transform.position - GameManager.instance.player.transform.position, randVect - GameManager.instance.player.transform.position);
        }
        return randVect;
    }

    private IEnumerator Attack()
    {
        KALM();
        for (int i = 0; i< Random.Range(1,5); i++)
        {
            Vector3 start = transform.forward;
            float timeElapsed = 0;

            while (timeElapsed < 0.3f)
            {
                transform.forward = Vector3.Lerp(start, GameManager.instance.player.transform.position - transform.position, timeElapsed / 0.5f);
                timeElapsed += Time.deltaTime;

                yield return null;
            }
            abilityManager.CastAbility(0);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(attackAnimationTime);
        }
        canMove = true;
        agent.isStopped = false;
        isAttacking = false;
        attackCD = 0f;
        objective = DetermineObjective();
        agent.SetDestination(objective);
    }
    //YOLOO

    public new void Reset()
    {
        base.Reset();
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        objective = DetermineObjective();
        agent.SetDestination(objective);
        canMove = true;
        isAttacking = false;
        attackCD = 0f;
        agent.isStopped = false;
    }

}
