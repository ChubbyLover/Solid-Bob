using UnityEngine;
using System.Collections;

public class jumpGUI : MonoBehaviour {
	
	public player hero; 
	public GUIText ausgabe;
	
	
	
	void FixedUpdate () { 
		if(Input.touchCount > 0)
		{
			for(int i=0; i < Input.touchCount; i++)
			{
				if(this.guiTexture.HitTest (Input.GetTouch (i).position))
				{
					hero.jump();
					/*ausgabe.text = "Sprung";*/
				}
			} 
		}	
	}
	
	
}
