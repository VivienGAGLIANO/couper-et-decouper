using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : Monster
{

    public AudioClip Swing;

    public float attackRange = 2f;

    public float attackAnimationTime = 1.3f;

    public float baseAttackCD = 1f;

    private float attackCD = 0f;

    private bool canMove = true;

    private bool isAttacking = false;
    
    private Vector3 distanceFromTarget;

    new void Start()
    {
        base.Start();
        currentHp = 10;
        maxHP = 10;
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
                attackCD = 0f;
            }
        }
        
    }

    protected void FixedUpdate()
    {
        if (!isDead)
        {
            objective = GameManager.instance.player.transform.position;
            distanceFromTarget = objective - transform.position;
            if (canMove)
            {
                agent.SetDestination(objective);
            }
        }
    }

    

    private IEnumerator Attack()
    {
        KALM();
        float timeElapsed = 0;
        Vector3 start = transform.forward;
        while (timeElapsed < 0.3f)
        {
            transform.forward = Vector3.Lerp(start, GameManager.instance.player.transform.position - transform.position, timeElapsed / 0.5f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        abilityManager.CastAbility(0);
        animator.SetTrigger("BaseAttack");
        yield return new WaitForSeconds(attackAnimationTime);
        canMove = true;
        agent.isStopped = false;
        isAttacking = false;
    }


    public new void Reset()
    {
        base.Reset();
        canMove = true;
        isAttacking = false;
        attackCD = 0f;
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        agent.isStopped = false;
    }
}
