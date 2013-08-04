using UnityEngine;
using System.Collections;

public class MissionArtifact : Mission {
	public Artifact artifact;
	
	public override bool Completed () {
		return artifact.collected;	
		
	}
	
	public override string MissionStatus () {
		if (Completed ()) {
			return "Artifact Collected";
		} else {
			return "Artifact" + "\n" + 
				string.Format("{0:0.00}", Vector3.Distance (managerPlayer.currentBody.transform.position, artifact.transform.position));
		}
	}
}
