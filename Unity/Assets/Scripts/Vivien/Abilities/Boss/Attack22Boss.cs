using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack22Boss : Ability
{
    [SerializeField] private GameObject originalProjectile;
    [SerializeField] private int spellDamage = 45;
    [SerializeField] private float projectileSpeed = 10f;

    public override void TriggerAbility()
    {
        ProjectileBoss proj = Instantiate(originalProjectile, entity.transform.position, Quaternion.identity).GetComponent<ProjectileBoss>();
        proj.spellDamage = spellDamage;
        proj.travelSpeed = projectileSpeed;
        proj.transform.forward = (GameManager.instance.player.transform.position - entity.transform.position).normalized;
    }
}
