using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMenu : MonoBehaviour
{

    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    private bool waitingForKey;

    // Start is called before the first frame update
    void Start()
    {
		menuPanel = transform.Find("Controls");
        waitingForKey = false;

        for (int i = 0; i < menuPanel.childCount; i++)
        {
            if (menuPanel.GetChild(i).name == "1Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.instance.action_1.ToString();
            else if (menuPanel.GetChild(i).name == "2Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.instance.action_2.ToString();
            else if (menuPanel.GetChild(i).name == "3Key")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.instance.action_3.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {

        keyEvent = Event.current;

        
        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode; 
            waitingForKey = false;
        }
		else if (keyEvent.button == 0 && keyEvent.isMouse && waitingForKey)
        {
			newKey = KeyCode.Mouse0;
			waitingForKey = false;
		}
		else if (keyEvent.button == 1 && keyEvent.isMouse && waitingForKey)
		{
			newKey = KeyCode.Mouse1;
			waitingForKey = false;
		}

	}

	public void StartAssignment(string keyName)
	{
		if (!waitingForKey)
			StartCoroutine(AssignKey(keyName));
	}


	public void SendText(Text text)
	{
		buttonText = text;
	}

	
	IEnumerator WaitForKey()
	{
		while (!(keyEvent.isKey || keyEvent.button == 1 || keyEvent.button == 0))
			yield return null;
	}

	/*AssignKey 
	 */
	public IEnumerator AssignKey(string keyName)
	{
		waitingForKey = true;

		yield return WaitForKey(); 

		switch (keyName)
		{
			case "action_1":
				GameManager.instance.action_1 = newKey; 
				buttonText.text = GameManager.instance.action_1.ToString(); 
				PlayerPrefs.SetString("1Key", GameManager.instance.action_1.ToString()); 
				break;
			case "action_2":
				GameManager.instance.action_2 = newKey; 
				buttonText.text = GameManager.instance.action_2.ToString(); 
				PlayerPrefs.SetString("2Key", GameManager.instance.action_2.ToString()); 
				break;
			case "action_3":
				GameManager.instance.action_3 = newKey; 
				buttonText.text = GameManager.instance.action_3.ToString(); 
				PlayerPrefs.SetString("3Key", GameManager.instance.action_3.ToString()); 
				break;
		}

		GameManager.instance.UpdateAbilityKeys();

		yield return null;


	}
}
