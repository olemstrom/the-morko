using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map3D : MonoBehaviour {

	private Map map;

	public static void Create (Map map, GameObject brick) {
		Map3D.GenerateWalls (map, brick);
	}

	public static void GenerateWalls(Map map, GameObject brick) {

		int CellSize = map.CellSize;
		int[,] tiles = map.ptiles;
		int h = tiles.GetLength (0);
		int w = tiles.GetLength (1);
		int mapSize = CellSize * map.CellCount;
		for (int y = 0; y < h; y++) {
			for(int x = 0; x < w; x++) {
				if(tiles[y, x] == PathGenerator.BUFFER) {
					GameObject cube = Instantiate (brick);
					cube.transform.localScale = new Vector3 (CellSize, 20, CellSize);
					cube.transform.localPosition = new Vector3 (x*CellSize, 20 / 2, y*CellSize);

				}

			}
		}

	}


}
