using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1Mage : Ability
{
    [SerializeField] private GameObject fireball;
    [SerializeField] private ParticleSystem[] castParticle;

    

    private void Start()
    {
        foreach (ParticleSystem particle in castParticle)
        {
            particle.gameObject.SetActive(false);
        }
    }

    public override void TriggerAbility()
    {
        foreach (ParticleSystem particle in castParticle)
        {
            particle.gameObject.SetActive(true);
        }
        entity.gameObject.GetComponent<Mage>().ThrowDaBall(fireball);
        
        
    }

    
}