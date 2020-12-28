using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{
    public AudioClip fireballSound;
    public AudioClip wallSound;

    public void ThrowDaBall(GameObject fireball)
    {
        StartCoroutine(ThrowBallz(fireball));
    }
    public IEnumerator ThrowBallz(GameObject fireball)
    {
        animator.SetTrigger("CastSpell");
        yield return new WaitForSeconds(0.4f);
        SoundManager.instance.PlaySFX(fireballSound);
        GameObject fireboulz = Instantiate(fireball, transform.position + Vector3.up + transform.forward, Quaternion.identity);
        fireboulz.transform.forward = transform.forward;

    }

    public void MakeDaWall(GameObject wall, Vector3 position)
    {
        StartCoroutine(MakeWallz(wall,position));
    }

    public IEnumerator MakeWallz(GameObject wall, Vector3 position)
    {
        animator.SetTrigger("CastSpell");
        yield return new WaitForSeconds(0.4f);
        SoundManager.instance.PlaySFX(wallSound);
        Vector3 previousPos = new Vector3(position.x, -2.5f, position.z);
        GameObject newWall = Instantiate(wall, previousPos, Quaternion.identity);
        newWall.transform.forward = transform.forward;
        previousPos = newWall.transform.position;
        for (float i = 0 ; i < 0.7f; i+=Time.deltaTime)
        {
            newWall.transform.position = Vector3.Lerp(previousPos,position,i/0.7f);
            yield return null;
        }
        
        
    }
}
