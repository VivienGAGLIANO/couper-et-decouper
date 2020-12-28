using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPlayer : Player
{
    public bool smashGround = false;

    public bool spinin = false;

    [HideInInspector] public float groundSmashRadius;
    [HideInInspector] public int groundSmashDamage;
    [HideInInspector] public float groundSmashPushback;

    public AudioClip smashSound;

    public AudioClip beybladeSound;

    private float beybladeFrequency = 0.7f;
    private float beybladeCounter = 0.7f;

    protected new void Update()
    {
        base.Update();
        if (spinin)
        {
            beybladeCounter += Time.deltaTime;
            if (beybladeCounter>= beybladeFrequency)
            {
                beybladeCounter = 0;
                SoundManager.instance.PlaySFX(beybladeSound,0.3f);
            }
        }
    }

    public void Beyblade(float abilityDuration, float tickDuration, int damagePerTick, float spinRadius)
    {
        StartCoroutine(BeybladeCoroutine(abilityDuration, tickDuration, damagePerTick, spinRadius));
    }

    private IEnumerator BeybladeCoroutine(float abilityDuration, float tickDuration, int damagePerTick, float spinRadius)
    {
        animator.SetBool("Beyblade", true);
        spinin = true;
        lookAtMouse = false;
        for (int i = 0; i < (int)(abilityDuration / tickDuration); i++)
        {
            DamageTick(damagePerTick, spinRadius);
            yield return new WaitForSeconds(tickDuration);
        }
        animator.SetBool("Beyblade", false);
        spinin = false;
        lookAtMouse = true;
    }

    private void DamageTick(int damagePerTick, float spinRadius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, spinRadius);
        foreach (Collider col in hits)
        {
            if (col.CompareTag("Ennemy"))
            {
                col.GetComponent<Entity>().LoseHP(damagePerTick);
            }
        }
    }

    private new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (smashGround)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, groundSmashRadius);
            foreach (Collider col in hits)
            {
                if (col.CompareTag("Ennemy"))
                {
                    Entity ent = col.GetComponent<Entity>();
                    ent.LoseHP(groundSmashDamage);
                    Vector3 force = (ent.transform.position - transform.position) * groundSmashPushback;
                    ent.rb.AddForce(force);
                }
            }

            Debug.Log("Soldier Player : Floor goes boom");
            SoundManager.instance.PlaySFX(smashSound);
            smashGround = false;
        }
        if (collision.collider.CompareTag("Terrain"))
        {
            gameObject.layer = 10;
        }
    }
}
