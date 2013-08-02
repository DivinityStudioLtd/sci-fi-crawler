using UnityEngine;
using System.Collections;

public class InterfaceMainMenu : Interface {
	public override bool SetDisplay (bool newDisplay) {
		return base.SetDisplay (newDisplay);
	}
	
	
	public MainMenuEnum mainMenuEnum; 
	
	public void OnGUI () {
		if (!display) 
			return;
		GUILayout.BeginArea (InterfaceUtility.CenteredRect (InterfaceUtility.ScreenWidthDivided (1.5f), InterfaceUtility.ScreenHeightDivided (1.5f)));
		GUILayout.BeginVertical("box");
		switch (mainMenuEnum) {
		case MainMenuEnum.MainMenu :
			MainMenu ();
			break;
		case MainMenuEnum.Gameplay :
			Gameplay ();
			break;
		case MainMenuEnum.Graphic :
			Graphic ();
			break;
		case MainMenuEnum.Audio :
			Audio ();
			break;
		case MainMenuEnum.Credit :
			Credits ();
			break;
		case MainMenuEnum.MatchMaking :
			break;
		}
			
		
		
		
		
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}
	
	void MainMenu () {
		if (managerGame.state_e != ManagerStateEnum.Working) {
			if (GUILayout.Button ("Run")) {
				managerGame.state_e = ManagerStateEnum.Working;
			}
		} else {
			if (GUILayout.Button ("Return to Game")) {
				managerInterface.PreviousInterface ();
			}	
			if (GUILayout.Button ("Restart")) {
			}	
			if (GUILayout.Button ("Quit to Main Menu")) {
				Linker.Reset ();
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		
		if (GUILayout.Button ("Gameplay")) {
			mainMenuEnum = MainMenuEnum.Gameplay;
		}	
		if (GUILayout.Button ("Graphic")) {
			mainMenuEnum = MainMenuEnum.Graphic;
		}	
		if (GUILayout.Button ("Audio")) {
			mainMenuEnum = MainMenuEnum.Audio;
		}	
		if (GUILayout.Button ("Credits")) {
			mainMenuEnum = MainMenuEnum.Credit;
		}	
		if (GUILayout.Button ("Quit")) {
			Application.Quit ();
		}	
	}
	
	void Gameplay () {
		
		BackButton ();
	}
	void Graphic () {
		
		BackButton ();
	}
	void Audio () {
		
		BackButton ();
	}
	void Credits () {
		
		BackButton ();
	}
	void BackButton () {
		GUILayout.FlexibleSpace ();
		if (GUILayout.Button ("Back"))
			mainMenuEnum = MainMenuEnum.MainMenu;
	}
}
