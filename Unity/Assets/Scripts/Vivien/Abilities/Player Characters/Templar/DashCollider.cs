using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCollider : MonoBehaviour
{
    [HideInInspector] public int damage;

    private void Start()
    {
        GetComponent<SphereCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ennemy"))
        {
            other.GetComponent<Entity>().LoseHP(damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ennemy"))
        {
            collision.collider.GetComponent<Entity>().LoseHP(damage);
        }
    }
}
