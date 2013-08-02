using UnityEngine;
using System.Collections;

public class InterfaceMainMenu : Interface {
	public override bool SetDisplay (bool newDisplay) {
		return base.SetDisplay (newDisplay);
	}
	public void Start () {
		scrollPositions.Add (new Vector2 ());
	}
	
	public MainMenuEnum mainMenuEnum; 
	public Texture2D divinityLogo;
	public string gameName;
	new public void OnGUI () {
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
		
		GUILayout.BeginArea (new Rect (Screen.width- 110, Screen.height - 110, 100, 100));
		GUILayout.Label (divinityLogo);
		GUILayout.EndArea ();
	}
	
	void MainMenu () {
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.Label (gameName);
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		
		GUILayout.FlexibleSpace ();
		if (managerGame.state_e != ManagerStateEnum.Working) {
			if (GUILayout.Button ("Run")) {
				managerGame.state_e = ManagerStateEnum.Working;
			}
		} else {
			if (GUILayout.Button ("Return to Game")) {
				managerInterface.PreviousInterface ();
			}
			/*if (GUILayout.Button ("Restart")) {
			}*/
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
			scrollPositions [0] = new Vector2 ();
			mainMenuEnum = MainMenuEnum.Credit;
		}	
		GUILayout.FlexibleSpace ();
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
		//GUILayout.BeginHorizontal ();
			GUILayout.Label ("Game Volume");
			GUILayout.Label (PreferenceUtility.GameVolume.ToString ());
			PreferenceUtility.GameVolume = GUILayout.HorizontalSlider (
				PreferenceUtility.GameVolume, 
				0.0f, 
				1.0f
				);
		//GUILayout.EndHorizontal ();
		//GUILayout.BeginHorizontal ();
			GUILayout.Label ("Ambience Volume");
			GUILayout.Label (PreferenceUtility.AmbienceVolume.ToString ());
			PreferenceUtility.AmbienceVolume = GUILayout.HorizontalSlider (
				PreferenceUtility.AmbienceVolume, 
				0.0f, 
				1.0f
				);
		//GUILayout.EndHorizontal ();
		//GUILayout.BeginHorizontal ();
			GUILayout.Label ("Music Volume");
			GUILayout.Label (PreferenceUtility.MusicVolume.ToString ());
			PreferenceUtility.MusicVolume = GUILayout.HorizontalSlider (
				PreferenceUtility.MusicVolume, 
				0.0f, 
				1.0f
				);
		//GUILayout.EndHorizontal ();
		BackButton ();
	}
	public TextAsset credits;
	void Credits () {
		scrollPositions [0] = GUILayout.BeginScrollView (scrollPositions [0], GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
		GUILayout.TextArea (credits.text);
		
		GUILayout.EndScrollView ();
		BackButton ();
	}
	void BackButton () {
		GUILayout.FlexibleSpace ();
		if (GUILayout.Button ("Back"))
			mainMenuEnum = MainMenuEnum.MainMenu;
	}
}
