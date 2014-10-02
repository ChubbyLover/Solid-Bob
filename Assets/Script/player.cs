using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	
	
	bool grounded = false;
	public touchControl input;
	GameObject platform = default(GameObject);
	// Use this for initialization
	void Start () {
		
	} 
	
	// Update is called once per frame
	void FixedUpdate () {
		
			
			float sin = Mathf.Sin(-Mathf.PI/4);
			float cos = Mathf.Cos(-Mathf.PI/4);
		
			Vector3 movement = new Vector3 (input.joystick.x * cos - input.joystick.z*2 * sin, 0, input.joystick.x * sin + input.joystick.z*2 * cos);
	
		
		
			Vector3 relativeMovement = this.transform.InverseTransformPoint(transform.position+movement);
		
			relativeMovement /= 100;
			if(relativeMovement.sqrMagnitude > 1) relativeMovement.Normalize();
			relativeMovement = new Vector3(relativeMovement.z, 0, relativeMovement.x);
		
		
		
		
		
			
			Vector3 speed = transform.forward * relativeMovement.x * 5 * (1-Mathf.Abs(relativeMovement.z));
			
			rigidbody.velocity = new Vector3 (speed.x, rigidbody.velocity.y, speed.z);
		if(platform) rigidbody.velocity += new Vector3 (platform.rigidbody.velocity.x, 0, platform.rigidbody.velocity.z);
			
			//if (input.joystick.x>= 0) 
			rigidbody.angularVelocity = (Vector3.up * 5 * relativeMovement.z);
			//else rigidbody.angularVelocity = (Vector3.up * -2f * input.joystick.y);
		
		
		if (Input.GetKey("space"))//&&Physics.Raycast(this.transform.position, Vector3.down, 0.5f)) 
		{
			jump ();
		}
		
		if(transform.position.y < -2f) { die(); return; }
	}
	
	void OnTriggerEnter(Collider other) {
		if(!other.isTrigger) grounded=true;
		if(other.attachedRigidbody) platform = other.gameObject;
		//if (other.CompareTag("platform")) 
	}
	void onTriggerStay(Collider other) {
		// if(other.attachedRigidbody) platform = other.gameObject;
	}
	
	void OnTriggerExit(Collider other) {
		if(!Physics.Raycast(this.transform.position, Vector3.down, 0.5f)) grounded=false;
		// if (other.CompareTag("platform")) 
		if(platform == other.gameObject) platform = default(GameObject);
	}
	
	void die()
	{
		Vector3 newScale = transform.localScale * 0.99f;
		transform.localScale = newScale;
		
		if(transform.position.y<-70) Application.LoadLevel(Application.loadedLevel);
	}
	
	public void jump()
	{
		// print (rigidbody.GetPointVelocity(transform.position));
		if(grounded && rigidbody.velocity.y < 0.1)
		{
			rigidbody.AddForce(Vector3.up*5, ForceMode.VelocityChange);
			grounded=false;
		}
	}
	
	
}
