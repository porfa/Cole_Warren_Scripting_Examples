using UnityEngine;
using System.Collections;

public class HUD_Images : MonoBehaviour
{

	int i;
	int health, hunger, thirst;
	public Texture2D[] itemImages;
	public Texture2D weaponImg = null;
	private Texture2D crosshair, backPack, fishImg, logImg, fruitImg, healthBar, hungerBar, thirstBar, arrowImg, meatImg;
	bool inInventory;
	GUIContent inventoryItemContent = new GUIContent ();

	void Start ()
	{
		hunger = 100;
		health = 100;
		thirst = 100;
		crosshair = Resources.Load ("crosshair") as Texture2D;   
		backPack = Resources.Load ("backpack") as Texture2D;
		fishImg = Resources.Load ("fishImg") as Texture2D;
		logImg = Resources.Load ("log") as Texture2D;
		fruitImg = Resources.Load ("fruitImg") as Texture2D;
		healthBar = Resources.Load ("healthBar") as Texture2D;
		hungerBar = Resources.Load ("hungerBar") as Texture2D;
		thirstBar = Resources.Load ("thirstBar") as Texture2D;
		arrowImg = Resources.Load ("arrow") as Texture2D;
		meatImg = Resources.Load ("meat_img") as Texture2D;


		
		itemImages = new Texture2D[]{fishImg, logImg, fruitImg, arrowImg, meatImg};
	}

	void Update ()
	{
		/////////////////////////////////////////
		i = HUD_Text.UpdateHUDImage (i);
		switch (i) {
		case 0:
			weaponImg = Resources.Load ("Fist") as Texture2D;      
			break;
		case 1:
			weaponImg = Resources.Load ("AxeIcon") as Texture2D;      
			break;
		case 2:
			weaponImg = Resources.Load ("PiercingStareOfDesolation") as Texture2D;      
			break;
		case 3:
			weaponImg = Resources.Load ("Spear") as Texture2D;
			break;
		case 4:
			weaponImg = Resources.Load ("bowImg") as Texture2D;
			break;
		}
	}

	void OnGUI ()
	{
		/////////////////////////////////////////
		GUI.DrawTexture (new Rect ((Screen.width / 2) - 15, (Screen.height / 2) - 15, 30, 30), crosshair, ScaleMode.StretchToFill, true, 10.0F);
		GUI.DrawTexture (new Rect (10, 10, 100, 100), weaponImg, ScaleMode.StretchToFill, true, 10.0F);
		GUI.DrawTexture (new Rect (10, 100, health, 10), healthBar, ScaleMode.StretchToFill, true, 10.0F);
		GUI.DrawTexture (new Rect (10, 120, hunger, 10), hungerBar, ScaleMode.StretchToFill, true, 10.0F);
		GUI.DrawTexture (new Rect (10, 140, thirst, 10), thirstBar, ScaleMode.StretchToFill, true, 10.0F);

		if (GUI.Button (new Rect (Screen.width - 100, 10, 100, 100), backPack)) {
			inInventory = !inInventory;
		}

		if (inInventory) {
			int i2 = 0;
			int buttonId;
			Rect buttonArea;
			foreach (Texture2D item in itemImages) {
				buttonArea = new Rect (Screen.width / 2 - (150 - (i2 * 50)), Screen.height / 2 - 150, 50, 50);
				inventoryItemContent.text = Inventory.Instance.getItem (i2).ToString ();
				inventoryItemContent.image = itemImages [i2];
				if (GUI.Button (new Rect (Screen.width / 2 - (150 - (i2 * 50)), Screen.height / 2 - 150, 50, 50), inventoryItemContent)) {
					switch (i2) {
					case(2):
						if (Inventory.Instance.getItem (2) > 0) {
							Inventory.Instance.AddItem ("fruit", -1);
							hunger += 15;
						}
						break;
					}
				}
				if (buttonArea.Contains (Event.current.mousePosition)) {
					switch (i2) {
					case 2:
						GUI.Box (new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), "Eat");
						break;
					}
				}
				i2++;
				
			}
			GUI.Box (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400), "");
		}


	}


}
