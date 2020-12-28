using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * This class will mainly be a data structure to contain information and behaviour of each ability.
 * MonoBehaviour inheritance will simply allow for in-editor ability editing
 */
public abstract class Ability : MonoBehaviour
{
    [Serializable]
    public struct AbilityInfo
    {
        public float cooldown;
        public AudioClip sound;
    }

    public AbilityInfo aInfo;
    [HideInInspector] public Entity entity;
    [HideInInspector] public int additionalDamage = 0;

    protected void Awake()
    {
        entity = (Entity)FindObjectOfType(typeof(Entity));
    }


    public abstract void TriggerAbility();
}
