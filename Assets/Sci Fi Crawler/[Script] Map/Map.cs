using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	public MapTile[,] tiles;
}

[System.Serializable]
public class MapTile {
	public Tile tile;
}