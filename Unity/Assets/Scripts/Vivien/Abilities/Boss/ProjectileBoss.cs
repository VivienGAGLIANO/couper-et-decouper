using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoss : MonoBehaviour
{
    [HideInInspector] public int spellDamage = 45;
    [HideInInspector] public float travelSpeed = 10f;
    [SerializeField] private ParticleSystem explosionParticle;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = (transform.forward * travelSpeed);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!col.collider.CompareTag("Ennemy"))
        {
            if (col.collider.CompareTag("Player"))
            {
                col.collider.GetComponent<Player>().LoseHP(spellDamage);
            }
            Demon demon = MonsterManager.instance.GetMonster(MonsterManager.MonsterType.Demon).GetComponent<Demon>();
            demon.transform.position = col.transform.position;
            Destroy(this.gameObject);
        }
    }
}