using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2Soldier : Ability
{
    [SerializeField] private float baseCd;
    [SerializeField] private int damage = 2;
    [SerializeField] private float jumpSpeed = 15f;
    [SerializeField] private float gravityForce = 9.807f;
    [SerializeField] private float groundSmashRadius = 5f;
    [SerializeField] private float groundSmashPushback = 150f;

    public AudioClip jumpSound;
    public override void TriggerAbility()
    {
        aInfo.cooldown = baseCd;
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, LayerMask.GetMask("Hole"));
        // /!\ Ne pas collide le ray avec les colliders invisibles du sol
        if (hit.point.y>=0)
        {
            Vector3 shootAngleLow;
            Vector3 shootAngleHigh;
            int ballisticResult = Fts.solve_ballistic_arc(entity.transform.position, jumpSpeed, hit.point, gravityForce, out shootAngleLow, out shootAngleHigh);

            if (ballisticResult != 0)
            {
                entity.Animator.SetTrigger("Jump");
                SoundManager.instance.PlaySFX(jumpSound);
                entity.rb.velocity = ballisticResult == 1 ? shootAngleLow : shootAngleHigh;
                entity.canMoveCharacter = false;
                entity.canAbility = false;
                SoldierPlayer player = entity.gameObject.GetComponent<SoldierPlayer>();
                player.gameObject.layer = 8;
                player.lookAtMouse = false;
                player.smashGround = true;
                player.groundSmashRadius = groundSmashRadius;
                player.groundSmashDamage = damage;
                player.groundSmashPushback = groundSmashPushback;

            }
            else
            {
                aInfo.cooldown = 0;
            }
        }
        else
        {
            aInfo.cooldown = 0;
        }
    }
}
