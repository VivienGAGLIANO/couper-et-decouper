using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack22Templar : Ability
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float radius = 5;
    [SerializeField] private float shockwavePushback = 200f;

    public AudioClip wing;
    public override void TriggerAbility()
    {
        SoundManager.instance.PlaySFX(wing);
        Collider[] hits = Physics.OverlapSphere(entity.transform.position, radius);
        foreach (Collider col in hits)
        {
            if (col.CompareTag("Ennemy"))
            {
                Entity ent = col.GetComponent<Entity>();
                ent.LoseHP(damage);
                Vector3 force = (ent.transform.position - transform.position) * shockwavePushback;
                ent.rb.AddForce(force);
            }
        }
    }
}
