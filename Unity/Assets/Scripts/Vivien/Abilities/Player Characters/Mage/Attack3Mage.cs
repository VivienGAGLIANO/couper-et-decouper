using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3Mage : Ability
{
    [SerializeField] private float baseCd;

    public AudioClip TP;

    public override void TriggerAbility()
    {
        aInfo.cooldown = baseCd;
        Debug.Log(aInfo.cooldown);

        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        if (hit.collider.CompareTag("Ennemy"))
        {
            Vector3 tmpPos = entity.transform.position;
            Vector3 tmpDir = entity.transform.forward;
            entity.transform.position = new Vector3(hit.collider.transform.position.x, entity.transform.position.y, hit.collider.transform.position.z);
            entity.transform.forward = hit.collider.transform.forward;
            hit.collider.transform.position = new Vector3(tmpPos.x, hit.collider.transform.position.y, tmpPos.z);
            hit.collider.transform.forward = tmpDir;
            SoundManager.instance.PlaySFX(TP);
        }
        else
        {
            aInfo.cooldown = 0;
        }
    }
}
