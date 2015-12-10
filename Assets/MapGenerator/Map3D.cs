using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map3D : MonoBehaviour {

	private Map map;

	public static void Create (Map map, GameObject brick, int WallHeight) {
		int CellSize = map.CellSize;
		int[,] tiles = map.ptiles;
		int h = tiles.GetLength (0);
		int w = tiles.GetLength (1);
		int mapSize = CellSize * map.CellCount;
		for (int y = 0; y < h; y++) {
			for(int x = 0; x < w; x++) {
				if(tiles[y, x] == PathGenerator.BUFFER) {
					GameObject cube = Instantiate (brick);
					cube.transform.localScale = new Vector3 (CellSize, WallHeight, CellSize);
					cube.transform.localPosition = new Vector3 (x*CellSize, WallHeight / 2, y*CellSize);
					
				}
				
			}
		}

	}


}
