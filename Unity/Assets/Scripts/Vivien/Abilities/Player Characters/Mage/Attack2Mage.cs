using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2Mage : Ability
{
    [SerializeField] private GameObject iceWall;
    
    public override void TriggerAbility()
    {
        RaycastHit hit;
        Vector3 position = new Vector3(0, -1, 0);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        if (!Object.Equals(hit, null))
        {
            if (!hit.collider.CompareTag("Ennemy"))
            {
                position = hit.point;
            }
        }
        if (position != new Vector3(0, -1, 0))
        {
            entity.gameObject.GetComponent<Mage>().MakeDaWall(iceWall,position + Vector3.up * iceWall.transform.localScale.y / 2);
        }
    }
}
