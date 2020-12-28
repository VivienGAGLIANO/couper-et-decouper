using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Entity
{
    protected NavMeshAgent agent;
    protected Vector3 objective;

    public AudioClip deathSound;


    public new void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
    }

    protected new void Update()
    {
        if (!isDead)
        {
            if (currentHp <= 0 || transform.position.y<=-20)
            {
                Death();
                isDead = true;
            }

            UpdateAnimator();
        }

    }

    private void UpdateAnimator()
    {
        animator.SetFloat("FrontVelocity", Vector3.Dot(agent.velocity, transform.forward));
        animator.SetFloat("LateralVelocity", Vector3.Dot(agent.velocity, transform.right));
        //animator.SetFloat("Velocity", rb.velocity.magnitude);
        animator.SetFloat("Velocity", agent.velocity.magnitude);
    }

    public void Reset()
    {
        if (agent != null)
        {
            agent.velocity = Vector3.zero;
            agent.angularSpeed = 0;
        }
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        currentHp = maxHP;
        isDead = false;
    }

    protected new void Death()
    {
        canAbility = false;
        canMoveCharacter = false;
        
        StartCoroutine(DieRoutine());
    }

    protected IEnumerator DieRoutine()
    {
        SoundManager.instance.PlaySFX(deathSound);
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(3f);
        MonsterManager.EndMonster(this);
    }

    protected new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        DashCollider dash = collision.collider.GetComponent<DashCollider>();

        if (dash != null)
        {
            LoseHP(dash.damage);
        }
    }
}
