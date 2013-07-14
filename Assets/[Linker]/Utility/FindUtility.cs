using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Utility/FindUtility")]

public class FindUtility : Utility {
	/*
	static public Entertainer FindEntertainer (Transform t) {
		while (t != null) {
			Entertainer e;
			e = t.GetComponent <Entertainer> ();
			if (e != null)
				return e;
			
			e = t.GetComponent <Actor> () as Entity;
			if (e != null)
				return e;
			
			e = t.GetComponent <Character> () as Entity;
			if (e != null)
				return e;
			
			t = t.parent;
		}	
		return null;
	}
	
	static public Entertainer FindEntertainer (GameObject g) {
		return FindEntertainer (g.transform);
	}
	
	static public Entertainer FindEntertainer (Collider c) {
		return FindEntertainer (c.transform);
	}
	*/
	static public Entity FindEntity (Transform t) {
		while (t != null) {
			Entity e;
			e = t.GetComponent <Entity> ();
			if (e != null)
				return e;
			t = t.parent;
		}	
		return null;
	}
	
	static public Entity FindEntity (GameObject g) {
		return FindEntity (g.transform);
	}
	
	static public Entity FindEntity (Collider c) {
		return FindEntity (c.transform);
	}
	public static GameObject[] TC (string tag, string component) {
		List<GameObject> found = new List<GameObject> ();
		
		GameObject[] tagsFound = GameObject.FindGameObjectsWithTag(tag);
		
		foreach (GameObject componentFound in tagsFound)
			if (componentFound.GetComponent (component) != null)
				found.Add (componentFound);
		
		return found.ToArray ();
	}
	/*
	public static Entity FindNetworkViewID (int viewID) {
		foreach (Entity	e in GameObject.FindObjectsOfType (typeof(Entity)) as Entity[])
			if (e.viewID == viewID)
				return e;
		return null;
	}
	*/
}
