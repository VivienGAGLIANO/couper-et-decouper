using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : Ability
{
    [SerializeField] private GameObject arrow;

    [SerializeField] private ParticleSystem[] castParticle;

    public AudioClip shooot;

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
        
        entity.Animator.SetTrigger("Attack");
        SoundManager.instance.PlaySFX(shooot);
        GameObject aro = Instantiate(arrow, entity.gameObject.transform.position + entity.gameObject.transform.forward, Quaternion.identity);
        aro.transform.forward = entity.transform.forward;
        aro.GetComponent<Arrow>().targetTag = entity.GetComponent<Player>() ? "Ennemy" : "Player";
    }
}
