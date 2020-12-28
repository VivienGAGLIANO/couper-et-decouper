using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Templar : Player
{
    /*[HideInInspector]*/ public bool becomeAngel = true;
    
    [Header("Templar form")] [Space]
    public int maxHPTemplar = 50;
    public int currentHpTemplar = 50;
    public Canvas templarIcons;
    [HideInInspector] public AbilityManager abilityManagerTemplar;

    [Header("Angel form")] [Space]
    public int maxHPAngel = 30;
    public int currentHpAngel = 30;
    public AbilityManager abilityManagerAngel;
    public Canvas angelIcons;
    public GameObject wings; 

    public AbilityManager AbilityManager { set { abilityManager = value; } }
    public int MaxHP { set { maxHP = value; } }
    public int CurrentHP { get { return currentHp; } set { currentHp = value; } }


    private int resistance = 0;
    [HideInInspector]public int _hpListener = 50;

    public AudioClip rush;


    private new void Start()
    {
        base.Start();

        maxHPTemplar = maxHP;
        currentHpTemplar = currentHp;
        abilityManagerTemplar = abilityManager;

        abilityManagerAngel.enabled = false;
        wings.SetActive(false);
    }

    private new void Update()
    {
        // Wow, this is disgusting but ça marche
        base.Update();
        if (_hpListener != currentHp)
        {
            currentHp += resistance;
            _hpListener = currentHp;
        }
    }

    public void DamageUp(float buffDuration, int damageBuff)
    {
        StartCoroutine(DamageUpCoroutine(buffDuration, damageBuff));
    }

    private IEnumerator DamageUpCoroutine(float buffDuration, int damageBuff)
    {
        Ability[] abilities = abilityManager.Abilities;
        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i].additionalDamage += damageBuff;
        }
        yield return new WaitForSeconds(buffDuration);
        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i].additionalDamage -= damageBuff;
        }
    }

    public void ResistanceUp(float buffDuration, int resistanceBuff)
    {
        StartCoroutine(ResistanceUpCoroutine(buffDuration, resistanceBuff));
    }

    private IEnumerator ResistanceUpCoroutine(float buffDuration, int resistanceBuff)
    {
        resistance += resistanceBuff;

        yield return new WaitForSeconds(buffDuration);

        resistance -= resistanceBuff;
    }

    //public new void LoseHP(int damage)
    //{
    //    Debug.Log("fonction new");
    //    Debug.Log(this.name + " AIE CA FAIT MAL");
    //    currentHp -= Mathf.Max(damage - resistance, 0);
    //    currentHp = Mathf.Max(currentHp, 0);
    //}

    public void WaitAndDash(int damage, float dashDistance, float timeBeforeDash)
    {
        StartCoroutine(WaitAndDashCoroutine(damage, dashDistance, timeBeforeDash));
    }

    private IEnumerator WaitAndDashCoroutine(int damage, float dashDistance, float timeBeforeDash)
    {
        yield return new WaitForSeconds(timeBeforeDash);

        DashCollider dashCollider = GetComponentInChildren<DashCollider>();
        dashCollider.GetComponent<SphereCollider>().enabled = true;

        dashCollider.damage = damage;
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        Vector3 direction;
        if (!Equals(hit, null))
        {
            direction = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
        else
            direction = transform.forward;
        rb.AddForce(direction * dashDistance);
        SoundManager.instance.PlaySFX(rush);
        canMoveCharacter = false;

        yield return new WaitForSeconds(1.5f);

        dashCollider.GetComponent<SphereCollider>().enabled = false;
        canMoveCharacter = true;
    }
}
