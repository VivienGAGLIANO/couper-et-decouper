using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SwordStrike : MonoBehaviour
{
    [SerializeField] private int SpellDamage = 5;
    /*[HideInInspector]*/ public string targetTag;

    private BoxCollider col;

    public AudioClip swoosh;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        col.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("[SwordStrike] Collision detected, target is " + targetTag);

        if (collision.collider.CompareTag(targetTag)) 
        {
            collision.collider.GetComponent<Entity>().LoseHP(SpellDamage);
        }
    }

    public void ActivateCollider(float duration)
    {
        StartCoroutine(ActivateColliderCoroutine(duration));
    }

    private IEnumerator ActivateColliderCoroutine(float duration)
    {
        col.enabled = true;
        yield return new WaitForSeconds(duration/2);
        SoundManager.instance.PlaySFX(swoosh,1f);
        yield return new WaitForSeconds(duration/2);
        col.enabled = false;
    }
}
