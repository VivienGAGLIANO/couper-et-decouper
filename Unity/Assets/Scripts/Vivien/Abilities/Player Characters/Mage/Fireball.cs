using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private int spellDamage = 5;
    [SerializeField] private float travelSpeed = 20f;
    [SerializeField][Range(1f, 10f)] private float explosionRadius = 3.5f;
    [SerializeField] private float explosionForce = 150f;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private float autoDestroyTimer = 10f;

    public AudioClip explosionSound;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(AutoDestroyCoroutine(autoDestroyTimer));
    }

    private void FixedUpdate()
    {
        rb.velocity = (transform.forward * travelSpeed);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Terrain") || col.collider.CompareTag("Ennemy"))
        {
            Explode(col.contacts[0].point);
            Destroy(this.gameObject);
        }
    }

    private void Explode(Vector3 position)
    {
        //explosionParticle.gameObject.SetActive(true);  <--- Uncomment when particle effect
        SoundManager.instance.PlaySFX(explosionSound);
        Collider[] thingsHit = Physics.OverlapSphere(position, explosionRadius);
        foreach (Collider col in thingsHit)
        {
            if (col.CompareTag("Ennemy"))
            {
                col.attachedRigidbody.AddExplosionForce(explosionForce, position, explosionRadius);
                col.GetComponent<Entity>().LoseHP(spellDamage);
				col.gameObject.layer = 8;
				//StartCoroutine(col.GetComponent<Entity>().Project();
            }
        }

    }

    private IEnumerator AutoDestroyCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
