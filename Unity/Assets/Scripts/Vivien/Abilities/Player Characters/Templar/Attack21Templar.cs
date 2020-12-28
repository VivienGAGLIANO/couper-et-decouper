using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack21Templar : Ability
{
    [SerializeField] private int dashDamage = 2;
    [SerializeField] private int resistanceBuff = 1;
    [SerializeField] private float dashDistance = 150f;
    [SerializeField] private float chargingDuration = 3f;
    [SerializeField][Tooltip("Amount by which character will be slowed")] private float chargeSlowDown = .7f;

    public override void TriggerAbility()
    {
        Templar templar = (Templar)entity;
        templar.ResistanceUp(chargingDuration, resistanceBuff);
        templar.WaitAndDash(dashDamage, dashDistance, chargingDuration);
        templar.SpeedUp(chargingDuration, -chargeSlowDown);
        
    }
}
