using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3Templar : Ability
{
    [SerializeField] private float buffDuration = 3f;
    [SerializeField] private int damageBuff = 3;
    [SerializeField] private float speedBuff = .5f;

    public AudioClip changeSound;

    public override void TriggerAbility()
    {
        Templar templar = (Templar)entity;
        bool becomeAngel = templar.becomeAngel;
        templar.becomeAngel ^= true;
        SoundManager.instance.PlaySFX(changeSound,0.3f);

        templar.MaxHP = !becomeAngel ? templar.maxHPTemplar : templar.maxHPAngel;
        templar.CurrentHP = !becomeAngel ? (int)((float)templar.CurrentHP / templar.maxHPAngel * templar.maxHPTemplar) : (int)((float)templar.CurrentHP / templar.maxHPTemplar * templar.maxHPAngel);
        templar._hpListener = templar.CurrentHP;
        templar.AbilityManager = !becomeAngel ? templar.abilityManagerTemplar : templar.abilityManagerAngel;
        templar.wings.SetActive(becomeAngel);
        templar.angelIcons.enabled = becomeAngel;
        templar.templarIcons.enabled = !becomeAngel;

        if (becomeAngel)
            templar.SpeedUp(buffDuration, speedBuff);
        else
            templar.DamageUp(buffDuration, damageBuff);

        templar.Animator.SetBool("Angel", becomeAngel);
    }
}
