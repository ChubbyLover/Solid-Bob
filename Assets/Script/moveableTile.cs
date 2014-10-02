using UnityEngine;
using System.Collections;

public class moveableTile : MonoBehaviour {
	
	/*public button triggerButton;
	Vector3 startpos;
	public Vector3 endpos;
	public float speed;
	
	void Start () {
		startpos = transform.position;
	}*/
	
	// Update is called once per frame
	
		public button triggerButton;
		public Vector3 target;
		public Vector3 origin;
		public float speedToTarget;
		public float speedToOrigin;
		public int wait = 2; //frames
		public int type;
		int waitCounter;
		// auto = 0;
		// button single pressed = 1;
		// button stay pressed = 2;
		
		bool back = true;
		
		void Start () {
			if(wait<2) wait = 2;
			origin = transform.position;
			target = origin + target;
		}
		void FixedUpdate() {
			
			
			float step;
			if(back) step = speedToOrigin * Time.deltaTime;
			else step = speedToTarget * Time.deltaTime;
			
			if(type == 0) // simple Platform
			{
			
			if(back) rigidbody.MovePosition(Vector3.MoveTowards(rigidbody.position, origin, step));
			else rigidbody.MovePosition(Vector3.MoveTowards(rigidbody.position, target, step));
			/*if(back) rigidbody.MovePosition(Vector3.MoveTowards(transform.position, origin, step));
			else rigidbody.MovePosition(Vector3.MoveTowards(transform.position, target, step));*/
				
				if(rigidbody.position == target || rigidbody.position == origin) waitCounter--;
				if(waitCounter<=0) {waitCounter = wait; back = !back;}
				
			}
			
			else if(type == 1) // Button Door stays opened
			{
				if(triggerButton.pressed)
				{
					rigidbody.MovePosition(Vector3.MoveTowards(transform.position, target, step));
					back=false;
				}
			}
			
			else if(type == 2) // Button Door closes again
			{
				if(triggerButton.pressed)
				{
					rigidbody.MovePosition(Vector3.MoveTowards(transform.position, target, step));
					back=false;
				}
				else 
				{
					back=true;
					rigidbody.MovePosition(Vector3.MoveTowards(transform.position, origin, step));
				}
			}
			
			
		}
		
	
}
