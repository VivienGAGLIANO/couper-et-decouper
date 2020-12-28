using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack11Templar : Ability
{
    [SerializeField] private float attackDuration = 1.5f;
    [SerializeField] private ParticleSystem[] castParticle;


    private void Start()
    {
        if (castParticle != null)
        {
            foreach (ParticleSystem particle in castParticle)
            {
                particle.gameObject.SetActive(false);
            }
        }
    }

    public override void TriggerAbility()
    {

        entity.Animator.SetTrigger("BaseAttack");

        SwordStrike sword = entity.GetComponentInChildren<SwordStrike>();
        sword.targetTag = "Ennemy";
        sword.ActivateCollider(attackDuration);
    }
}
