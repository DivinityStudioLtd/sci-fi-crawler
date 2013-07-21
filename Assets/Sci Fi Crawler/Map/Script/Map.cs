using UnityEngine;
using System.Collections;

public class Map : Entity {
	public int seed;
	public bool generated;
	public MapState mapState = MapState.PlaceHolder;
}

public enum MapState {
	PlaceHolder,
	Generated,
	Spawned,
	Completed
}