using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3Soldier : Ability
{
    [SerializeField] private float abilityDuration = 3f;
    [SerializeField] private int damagePerTick = 1;
    [SerializeField] private float tickDuration = 0.5f;
    [SerializeField] private float spinRadius = 1f;
    [SerializeField] private float mvtSpeedBoost = .5f;
 
    public override void TriggerAbility()
    {
        // /!\ Will only work with soldier player entity
        entity.SpeedUp(abilityDuration, mvtSpeedBoost);
        entity.gameObject.GetComponent<SoldierPlayer>().Beyblade(abilityDuration, tickDuration, damagePerTick, spinRadius);
    }


}
