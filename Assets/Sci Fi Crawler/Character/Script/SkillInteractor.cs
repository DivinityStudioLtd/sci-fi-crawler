using UnityEngine;
using System.Collections;

public class SkillInteractor : Entity {
	public Controller controller;
	
	void OnTriggerStay(Collider other) {
		ObstacleSkill os = other.GetComponent<ObstacleSkill> ();
        if (os != null) {
			print ("srgre");
            if (os.obstacleSkill.software > 0) {
				int dif = controller.character.skills.software - (os.obstacleSkill.software - 1);
				if (dif > 0)
					os.currentObstacleHealth -= dif * Time.deltaTime;
			}
		}
    }
}
