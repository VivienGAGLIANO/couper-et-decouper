using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Demon : Monster
{
    public float attackRange = 1.5f;

    public float jumpAnimationTime = 1f;

    public float jumpRange = 30f;

    [SerializeField] private float jumpSpeed = 15f;

    [SerializeField] private float gravityForce = 9.807f;

    public float attackAnimationTime = 1.3f;

    public float baseAttackCD = 5f;

    private float attackCD = 0f;

    
    private bool isAttacking = false;

    private Vector3 distanceFromTarget;

    private Vector3 shootAngleHigh = Vector3.zero;

    private Vector3 tmp;

    public AudioClip teleport;

    public AudioClip slash;

    new void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        objective = DetermineObjective();
        agent.SetDestination(objective);
        maxHP = 5;
        currentHp = 5;
        distanceFromTarget = objective - transform.position;
    }

    protected new void Update()
    {
        if (!isDead)
        {
            base.Update();
            //Debug.DrawRay(transform.position,agent.velocity,Color.red);
            // Debug.DrawRay(transform.position,transform.forward,Color.blue);
            // Debug.Log(objective);
            // Debug.Log("Ma pos " + transform.position);
            //Debug.Log(transform.forward);
            attackCD += Time.deltaTime;
            if (distanceFromTarget.magnitude <= attackRange && !isAttacking)
            {
                agent.ResetPath();
                agent.isStopped = true;
                isAttacking = true;
                StartCoroutine(Attack());
            }
        }
        
    }

    protected void FixedUpdate()
    { 
        if (!isDead)
            distanceFromTarget = objective - transform.position;
    }

    private Vector3 DetermineObjective()
    {
    
        Vector3 randVect = GameManager.instance.floorTiles[Random.Range(0,GameManager.instance.floorTiles.Count-1)].position;
        var d = Vector3.Distance(GameManager.instance.player.transform.position,randVect);
        var a = Vector3.Angle(transform.position - GameManager.instance.player.transform.position, randVect - GameManager.instance.player.transform.position);
        while ( d<=jumpRange || a >=135 )
        {
            randVect = GameManager.instance.floorTiles[Random.Range(0,GameManager.instance.floorTiles.Count-1)].position;
            d = Vector3.Distance(GameManager.instance.player.transform.position,randVect);
            a = Vector3.Angle(transform.position - GameManager.instance.player.transform.position, randVect - GameManager.instance.player.transform.position);
        }
        return randVect;
    }

    private IEnumerator Attack()
    {
        agent.enabled = false;

        Debug.Log("ROAR");
        // ROAR PART
        Vector3 start = transform.forward;
        float timeElapsed = 0;
        while (timeElapsed < 0.3f)
        {
            tmp = GameManager.instance.player.transform.position - transform.position;
            transform.forward = Vector3.Lerp(start, (new Vector3(tmp.x,0,tmp.z)).normalized, timeElapsed / 0.3f);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        yield return new WaitForSeconds(jumpAnimationTime-0.3f);

        
        Debug.Log("JUMP");
        // JUMP PART

        Vector3 shootAngleLow;
        tmp = GameManager.instance.player.transform.position - transform.position;
        Vector3 jumpDirection = (new Vector3(tmp.x,0,tmp.z)).normalized;
        Vector3 jumpObjective = transform.position + 10 * jumpDirection.normalized;
        int ballisticResult = Fts.solve_ballistic_arc(transform.position, jumpSpeed, jumpObjective, gravityForce, out shootAngleLow, out shootAngleHigh);
        if (ballisticResult!=0)
        {
            rb.AddForce(ballisticResult == 1 ? shootAngleLow: shootAngleLow, ForceMode.VelocityChange);
            rb.detectCollisions = false;
            //rb.velocity = ballisticResult == 1 ? shootAngleLow: shootAngleHigh;
        }
        yield return new WaitForSeconds(1f);
        Vector3 finalPos = GameManager.instance.player.transform.position - 2 * jumpDirection.normalized;
        SoundManager.instance.PlaySFX(teleport);
        transform.position = new Vector3( finalPos.x, transform.position.y, finalPos.z);
        rb.detectCollisions = true;
        yield return new WaitForSeconds(1f);
        
        Debug.Log("SCRATCH");
        // SCRATCH TIME
        Vector3 playerPos;
        for (int i = 0; i<5; i++)
        {   
            KALM();
            float charging = 0.8f;
            start = transform.forward;
            timeElapsed = 0;
            while (timeElapsed < charging)
            {
                playerPos = GameManager.instance.player.transform.position - transform.position;
                transform.forward = Vector3.Lerp(start, (new Vector3(playerPos.x,0,playerPos.z)).normalized , timeElapsed / charging);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(0.4f);
            SoundManager.instance.PlaySFX(slash);
            rb.velocity = 50 * transform.forward;
            yield return new WaitForSeconds(0.1f);
            rb.velocity = new Vector3(0,0,0);
            yield return new WaitForSeconds(0.2f);
        }
        isAttacking = false;
        attackCD = 0f;
        objective = DetermineObjective();
        agent.enabled = true;
        agent.isStopped = false;
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
        isDead = false;
        isAttacking = false;
        attackCD = 0f;
        objective = DetermineObjective();
        agent.isStopped = false;
        agent.SetDestination(objective);
    }

}
