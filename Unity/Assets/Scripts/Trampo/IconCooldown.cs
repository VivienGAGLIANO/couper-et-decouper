using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconCooldown : MonoBehaviour
{

    [SerializeField] private float cooldownTime;
    [SerializeField] private Slider skill;
    [SerializeField] int key;
    private float t;
    //private bool isLoading = false;
    //private KeyCode keySelected;

    //public void ActionSelected()
    //{
    //    if (key == 1)
    //    {
    //        keySelected = GameManager.instance.action_1;
    //    }
    //    else if (key == 2)
    //    {
    //        keySelected = GameManager.instance.action_2;
    //    }
    //    else if (key == 3)
    //    {
    //        keySelected = GameManager.instance.action_3;
    //    }
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    skill.value = 0f;
    //    ActionSelected();
    //}

    // Update is called once per frame
    void Update()
    {
        //if (!PauseMenu.gameIsPaused)
        //{

        //    if (Input.GetKeyDown(keySelected))
        //    {
        //        if (skill.value <= 0f)
        //        {
        //            t = 0f;
        //            skill.value = 1f;
        //        }

        //    }
        //    isLoading = (skill.value > 0);
        //    if (isLoading)
        //    {
        //        skill.value = Mathf.Lerp(1, 0, t);
        //        t += (1f / cooldownTime) * Time.deltaTime;
        //    }
        //}
        //else
        //{
        //    ActionSelected();
        //}
    }

    public void Cooldown(float time)
    {
        StartCoroutine(CooldownCoroutine(time));
    }

    private IEnumerator CooldownCoroutine(float time)
    {
        skill.value = 1f;
        t = 0f;

        while (skill.value > 0) 
        {
            skill.value = Mathf.Lerp(1, 0, t);
            t += (1f / time) * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
