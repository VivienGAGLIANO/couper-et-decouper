using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private Ability[] abilities;
    [SerializeField] private IconCooldown[] icons;
    private bool[] isAvailable;
    private bool isPlayer;

    public Ability[] Abilities { get { return abilities; } }

    private void Awake()
    {
        isAvailable = Enumerable.Repeat<bool>(true, abilities.Length).ToArray();
        Debug.Log(isAvailable.Length);
        Entity entity = GetComponent<Entity>();
        foreach (Ability ability in abilities)
        {
            ability.entity = entity;
        }
        isPlayer = entity.GetComponent<Player>();
    }

    /**
     * Check if required ability is ready, and cast it if necessary
     * Returns true if ability was cast, false if wrong key / impossible to cast due to cooldwon
     * 
     * Abilities are going to be called from outside this class. All input settings and call to the abiilties should be made in a player or entity controller
     */
    public bool CastAbility(int n)
    {
        if (n < isAvailable.Length)
        {
            if (isAvailable[n])
            {
                abilities[n].TriggerAbility();
                StartCoroutine(CoolDown(n, abilities[n].aInfo.cooldown));
                return true;
            }
        }
        return false;
    }

    private IEnumerator CoolDown(int ability, float cooldown)
    {
        // TODO Handle sprite and visual cooldown here

        isAvailable[ability] = false;

        if (isPlayer)
        {
            icons[ability].Cooldown(cooldown);
        }
        yield return new WaitForSeconds(cooldown);

        isAvailable[ability] = true;
    }
}
