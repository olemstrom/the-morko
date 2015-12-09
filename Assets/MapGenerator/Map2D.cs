using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoTurtle.BitmapDrawing;
using MIConvexHull;
public class Map2D : MonoBehaviour {
	
	private Map map;



	void Start () {
		map = GetComponentInParent<InitialiseGame> ().map;	
		Draw (map);


	}

	void Update() {
		bool boulily = true;
	}

	void Draw(Map map) {
		IList<Room> rooms = map.GetRooms ();
		Texture2D tex = new Texture2D (map.CellCount, map.CellCount, TextureFormat.RGB24, false, true);
		Renderer rend = GetComponent<Renderer> ();
		rend.enabled = true;
		tex.filterMode = FilterMode.Point;
		tex.wrapMode = TextureWrapMode.Clamp;
		tex.DrawFilledRectangle (new Rect (0, 0, map.CellCount, map.CellCount), Color.black);


		/*foreach (Room room in rooms) {
			int height = room.GetHeight();
			int width = room.GetWidth();
			int x = room.GetX();
			int y = room.GetY();

			tex.DrawFilledRectangle (new Rect (x, y, width, height), Color.white);
		}*/

		/*
		foreach (Room room in rooms) {
			Vertex2 c = room.GetCenterPoint();
			float x = (float)scale (c.x);
			float y = (float)scale (c.y);
			tex.DrawFilledRectangle(new Rect(x, y, 3, 3), Color.cyan);
		} 

		foreach(Edge e in map.allEdges) {
			//if(!map.edges.Contains(e)) tex.DrawLine(scale((int)e.start.x), scale((int)e.start.y), scale((int)e.end.x), scale((int)e.end.y), Color.blue);
		}
*/
		foreach(Edge e in map.edges) {
			//tex.DrawLine(e.start.x, e.start.y, e.end.x, e.end.y, new Color(0.5f, 0.5f, 0.5f, 0.1f));
		}
	
		int[,] tiles = map.ptiles;
		int h = tiles.GetLength(0);
		int w = tiles.GetLength (0);
		
		for(int i = 0; i < h; i++) {
			for(int j = 0; j < w; j++) {
				int cell = tiles[h-i-1, w-j-1]; 
				if(cell == PathGenerator.OPEN_ENDPOINT) {
					tex.SetPixel(map.CellCount - j, i, Color.green);
				} else if(cell == PathGenerator.BUFFER) {
					tex.SetPixel(map.CellCount - j, i, Color.red);
				}
				
			}
		}


		tex.Apply ();
		rend.material.mainTexture = tex;
	}


}
