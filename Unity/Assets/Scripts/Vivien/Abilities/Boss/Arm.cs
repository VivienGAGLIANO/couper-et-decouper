using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public enum bossPhase { FIRST, SECOND, THIRD};

    public bossPhase phase = bossPhase.FIRST;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            int damage = 0;
            switch (phase)
            {
                case bossPhase.FIRST:
                    damage = 20;
                    break;
                case bossPhase.SECOND:
                    damage = 25;
                    break;
                case bossPhase.THIRD:
                    damage = 30;
                    break;
                default:
                    break;
            }
            collision.collider.GetComponent<Entity>().LoseHP(damage);
        }
    }

    public void SetCollider(float duration)
    {
        StartCoroutine(SetColliderCoroutine(duration));
    }

    private IEnumerator SetColliderCoroutine(float duration)
    {
        BoxCollider armCollider = GetComponent<BoxCollider>();
        Bôssu boss = GetComponentInParent<Bôssu>();
        armCollider.enabled = true;
        boss.Agent.isStopped = true;
        boss.transform.LookAt(GameManager.instance.player.transform);

        yield return new WaitForSeconds(duration);

        armCollider.enabled = false;
        boss.Agent.isStopped = false;
    }

    public void SetColliderTwice(float duration)
    {
        StartCoroutine(SetColliderTwiceCoroutine(duration));
    }

    private IEnumerator SetColliderTwiceCoroutine(float duration)
    {
        BoxCollider armCollider = GetComponent<BoxCollider>();
        Bôssu boss = GetComponentInParent<Bôssu>();
        armCollider.enabled = true;
        boss.Agent.isStopped = true;
        boss.transform.LookAt(GameManager.instance.player.transform);

        yield return new WaitForSeconds((float)duration * 3 / 5);

        boss.transform.LookAt(GameManager.instance.player.transform);

        yield return new WaitForSeconds((float)duration * 2 / 5);

        armCollider.enabled = false;
        boss.Agent.isStopped = false;
    }
}
