using UnityEngine;
using System.Collections;

public class touchControl : MonoBehaviour {
	
	
	public int steuerkreuz = -1;
	public Vector3 joystick;
	public bool allowClickEmulation = true;
	
	Vector2 centerOfJoystick = new Vector2(161,101);
	
	void FixedUpdate () {
		if(Input.touchCount > 0)
		{
			for(int i=0; i < Input.touchCount; i++)
			{
				if(this.guiTexture.HitTest (Input.GetTouch (i).position))
				{
					if(Input.GetTouch (i).phase == TouchPhase.Began)
					{
						centerOfJoystick = Input.GetTouch(i).position;
						steuerkreuz = i;
					}
				}
				
				if(Input.GetTouch (i).phase == TouchPhase.Ended)
				{
					if(i==steuerkreuz) 
					{
						steuerkreuz = -1;
					}
				}
			} 
			
			if (steuerkreuz>=0) 
			{
				joystick = new Vector3(Input.GetTouch(steuerkreuz).position.x - centerOfJoystick.x,0, Input.GetTouch(steuerkreuz).position.y - centerOfJoystick.y);
				Debug.DrawLine(centerOfJoystick, joystick);
			} else {
				joystick = new Vector2(0,0);
			}
		}
		// #if UNITY_EDITOR
		if(allowClickEmulation)
		{
			if(Input.GetMouseButton(0))
			{
				joystick = new Vector3(Input.mousePosition.x - centerOfJoystick.x,0, Input.mousePosition.y - centerOfJoystick.y);
			// float x = map (joystick.x, 100, 300, -1, 1);
			} else {
				joystick = new Vector2(0,0);
			}
		}
		// #endif
	}
	
	float map(float value, float from1, float to1, float from2, float to2) {
		
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		
	}

}
