using UnityEngine;
using System.Collections;

public class button : MonoBehaviour {
	
	public bool pressed = false;
	int collisions = 0;
	Vector3 myOrigin;
	// Use this for initialization
	void Start () {
		 myOrigin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter() {
		collisions++;
		 if(!pressed)
		 {
			Vector3 temp = transform.position;
			temp = myOrigin+transform.up*-0.1f;
			transform.position = temp;
		 	pressed=true;
		 }
	}
	
	void OnTriggerExit() {
		collisions--;
		if(collisions<=0)
		{
			Vector3 temp = transform.position;
			temp = myOrigin;
			transform.position = temp;
			pressed = false;
		}
		
		
	}
	
}
