using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int spellDamage = 5;
    [SerializeField] private float travelSpeed = 20f;
    public string targetTag;

    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = (transform.forward * travelSpeed);
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.CompareTag("Terrain"))
        {
            Destroy(this.gameObject);
        }
        if (col.CompareTag(targetTag))
        {
            col.GetComponent<Entity>().LoseHP(spellDamage);
            Destroy(this.gameObject);
        }
    }
}
