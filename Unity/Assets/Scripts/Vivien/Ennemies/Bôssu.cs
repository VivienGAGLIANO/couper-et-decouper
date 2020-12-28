using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bôssu : Monster
{
    [SerializeField, Range(0, 1)] private float firstPhaseLimit = .5f;
    [SerializeField, Range(0, 1)] private float secondPhaseLimit = .25f;
    [SerializeField] private float armRange = 10f;
    [SerializeField] private float whirlwindRange = 5f;
    public Arm bossArm;

    public NavMeshAgent Agent { get { return agent; } }

    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
        if ((double)currentHp / maxHP > firstPhaseLimit)
            FirstAttackPattern();
        else if ((double)currentHp / maxHP > secondPhaseLimit)
            SecondAttackPattern();
        else
            ThirdAttackPattern();
        agent.SetDestination(GameManager.instance.player.transform.position);

        Debug.DrawRay(transform.position, transform.forward * 10);  
    }

    private void FirstAttackPattern()
    {
        Debug.Log("[Boss] : 1st attack pattern");
        bossArm.phase = Arm.bossPhase.FIRST;

        Player player = GameManager.instance.player;
        float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        //if ( distanceToPlayer <= whirlwindRange)
        //{
        //    abilityManager.CastAbility(1);
        //}
        /*else*/ if (distanceToPlayer <= armRange)
        {
            abilityManager.CastAbility(0);
        }
    }

    private void SecondAttackPattern()
    {
        Debug.Log("[Boss] : 2nd attack pattern");
        bossArm.phase = Arm.bossPhase.SECOND;

        Player player = GameManager.instance.player;
        float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        if (!abilityManager.CastAbility(3))
        {
            if (distanceToPlayer <= armRange)
            {
                abilityManager.CastAbility(1);
            }
        }
    }

    private void ThirdAttackPattern()
    {
        Debug.Log("[Boss] : 3rd attack pattern");
        bossArm.phase = Arm.bossPhase.THIRD;

        Player player = GameManager.instance.player;
        float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        //if ( distanceToPlayer <= whirlwindRange)
        //{
        //    abilityManager.CastAbility(1);
        //}
        /*else*/
        if (distanceToPlayer <= armRange)
        {
            abilityManager.CastAbility(2);
        }
    }

}
