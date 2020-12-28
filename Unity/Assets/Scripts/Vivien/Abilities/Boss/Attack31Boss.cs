using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack31Boss : Ability
{
    [SerializeField] private float attackDuration = 3f;

    public override void TriggerAbility()
    {
        Bôssu boss = ((Bôssu)entity);

        boss.bossArm.SetColliderTwice(attackDuration);
        entity.Animator.SetTrigger("Attack31Boss");
    }
}
