using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public Transform leftDoor;
	public Transform rightDoor;
	
	float closed = 1.25f;
	float open = 3.75f;
	
	public float transitionTime;
	float currentTransitionTime;
	
	public DoorState doorState;
	
	public void Open () {
		doorState = DoorState.Open;
	}
	
	public void Close () {
		doorState = DoorState.Close;
	}
	
	void FixedUpdate () {
		switch (doorState) {
			case DoorState.Open :
				currentTransitionTime += Time.deltaTime;
				break;
			case DoorState.Close :
				currentTransitionTime -= Time.deltaTime;
				break;
		}
		
		currentTransitionTime = Mathf.Clamp (currentTransitionTime, 0.0f, transitionTime);
		
		if (currentTransitionTime == 0.0f || currentTransitionTime == transitionTime)
			doorState = DoorState.Stop;
		
		float doorLocalX = closed + ((currentTransitionTime / transitionTime) * (open - closed));
		leftDoor.localPosition = new Vector3 (doorLocalX, 1.25f, 0.0f);
		rightDoor.localPosition = new Vector3 (-doorLocalX, 1.25f, 0.0f);
		
		Close ();
	}
	
	void OnTriggerStay(Collider other) {
		if (FindUtility.FindEntity (other).GetComponent<Controller> () != null)
            Open ();
    }
}

public enum DoorState {
	Open,
	Stop,
	Close
}
