using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack21Boss : Ability
{
    [SerializeField] private float attackDuration = 2.5f;

    public override void TriggerAbility()
    {
        Bôssu boss = ((Bôssu)entity);

        boss.bossArm.SetColliderTwice(attackDuration);
        entity.Animator.SetTrigger("Attack21Boss");
    }
}
