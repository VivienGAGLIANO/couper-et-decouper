using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooling : MonoBehaviour
{
    public static ProjectilePooling _instance;

	[SerializeField] private Fireball originalFireball;
    //[SerializeField] private SwordStroke originalSwordStroke;

	private static Queue<Fireball> deadFireballs = new Queue<Fireball>();
    //private static Queue<SwordStroke> deadSwordStrokes = new Queue<SwordStroke>();
	
	void Awake(){

        if (_instance == null){

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
    
            //Rest of your Awake code
    
        } 
		else {
            Destroy(this);
        }
    }

	public GameObject GetFireball(){

		Fireball fireball;

		if (deadFireballs.Count > 0) {
			fireball = deadFireballs.Dequeue();
			fireball.gameObject.SetActive(true);
		}
		else {
			fireball = Instantiate(originalFireball);
		}
		return fireball.gameObject;
	}

	public static void EndFireball(Fireball fireball) {
		fireball.gameObject.SetActive(false);
		deadFireballs.Enqueue(fireball);
	}

 //   public GameObject GetSwordStroke(){

	//	SwordStroke swordStroke;

	//	if (deadSwordStrokes.Count > 0) {
	//		swordStroke = deadSwordStrokes.Dequeue();
	//		swordStroke.gameObject.SetActive(true);
	//	}
	//	else {
	//		swordStroke = Instantiate(originalSwordStroke);
	//	}
	//	return swordStroke.gameObject;
	//}

	//public static void EndSwordStroke(SwordStroke swordStroke) {
	//	swordStroke.gameObject.SetActive(false);
	//	deadSwordStrokes.Enqueue(swordStroke);
	//}
}
