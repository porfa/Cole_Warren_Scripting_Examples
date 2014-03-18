using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
	private static Inventory instance;

	public static Inventory Instance {
		get {
			return instance;
		}
	}

	public int myFish;
	public int myLogs;
	public int myFruit;
	public int myArrows;
	public int myMeat;

	void Awake ()
	{
		instance = this;
		myFish = 0;
		myLogs = 0;
	}

	public void AddItem (string id, int i)
	{
		////////////////////////////////////////
		switch (id) {
			
		case("fish"):
			myFish += i;
			return;
			break;
		
		case("log"):
			myLogs += i;
			return;
			break;
		case("fruit"):
			myFruit += i;
			return;
			break;
		case("arrow"):
			myArrows += i;
			return;
		case("meat"):
			myMeat += i;
			return;
		}
		Debug.Log ("Tried place non-existent item in inventory");
	}

	public int getItem (int id)
	{
		////////////////////////////////////////
		switch (id) {

		case(0):
			return myFish;
			break;
		case(1):
			return myLogs;
			break;
		case(2):
			return myFruit;
			break;
		case(3):
			return myArrows;
			break;
		case(4):
			return myMeat;
			break;
		}
		Debug.Log ("Tried to get value of non-existent item in inventory");
		return 0;
	}
}
