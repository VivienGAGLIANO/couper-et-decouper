using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack11Boss : Ability
{
    [SerializeField] private float attackDuration = 2.5f;

    public override void TriggerAbility()
    {
        Bôssu boss = ((Bôssu)entity);

        boss.bossArm.SetCollider(attackDuration);
        entity.Animator.SetTrigger("ArmAttack");
    }
}
