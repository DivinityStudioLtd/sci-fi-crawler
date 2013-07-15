using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Linker : MonoBehaviour {
	static public List<Manager> managers = new List<Manager> ();
	static public List<Interface> interfaces = new List<Interface> ();
	static public List<Utility> utilities = new List<Utility> ();
	static public List<Factory> factories = new List<Factory> ();
	
	static public void Reset () {
		managers = new List<Manager> ();
		interfaces = new List<Interface> ();
		utilities = new List<Utility> ();
		factories = new List<Factory> ();
	}
	
	#region Generic Search
	static public Manager GetManager (string type) {
		foreach (Manager m in managers)
			if (m.gameObject.GetComponent (type) != null)
				return (Manager) m.gameObject.GetComponent (type);
		return null;
	}
	
	static public Interface GetInterface (string type) {
		foreach (Interface i in interfaces)
			if (i.gameObject.GetComponent (type) != null)
				return (Interface) i.gameObject.GetComponent (type);
		return null;
	}
	
	static public Utility GetUtility (string type) {
		foreach (Utility u in utilities)
			if (u.gameObject.GetComponent (type) != null)
				return (Utility) u.gameObject.GetComponent (type);
		return null;
	}
	
	static public Factory GetFactory (string type) {
		foreach (Factory f in factories)
			if (f.gameObject.GetComponent (type) != null)
				return (Factory) f.gameObject.GetComponent (type);
		return null;
	}
	
	static public void AddManager (Manager m) { managers.Add(m); }
	
	static public void AddInterface (Interface i) { interfaces.Add(i); }
	
	static public void AddUtility (Utility u) { utilities.Add(u); }
	
	static public void AddFactory (Factory f) { factories.Add(f); }
	#endregion
	//static public Interface interface { get { return (Interface) GetInterface ("Interface"); } }
	static public InterfaceLogin interfaceLogin { get { return (InterfaceLogin) GetInterface ("InterfaceLogin"); } }
	static public InterfaceMainMenu interfaceMainMenu { get { return (InterfaceMainMenu) GetInterface ("InterfaceMainMenu"); } }
	static public InterfaceTDS interfaceTDS { get { return (InterfaceTDS) GetInterface ("InterfaceTDS"); } }
	
	//static public Manager manager { get { return (Manager) GetManager ("Manager"); } }
	static public ManagerEnvironment managerEnvironment { get { return (ManagerEnvironment) GetManager ("ManagerEnvironment"); } }
	static public ManagerGame managerGame { get { return (ManagerGame) GetManager ("ManagerGame"); } }
	static public ManagerInterface managerInterface { get { return (ManagerInterface) GetManager ("ManagerInterface"); } }
	static public ManagerMap managerMap { get { return (ManagerMap) GetManager ("ManagerMap"); } }
	static public ManagerCharacter managerCharacter { get { return (ManagerCharacter) GetManager ("ManagerCharacter"); } }
	static public ManagerPlayer managerPlayer { get { return (ManagerPlayer) GetManager ("ManagerPlayer"); } }
	static public ManagerPrefab managerPrefab { get { return (ManagerPrefab) GetManager ("ManagerPrefab"); } }
	
	
	//static public Factory factory { get { return (Factory) GetFactory ("Factory"); } }
	static public FactoryMap factoryMap { get { return (FactoryMap) GetFactory ("FactoryMap"); } }
	static public FactoryCharacter factoryCharacter { get { return (FactoryCharacter) GetFactory ("FactoryCharacter"); } }
}
