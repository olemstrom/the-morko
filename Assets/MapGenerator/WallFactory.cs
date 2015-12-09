using UnityEngine;
using System.Collections;

public class WallFactory : MonoBehaviour {
	public static Vector2 DIR_SOUTH;
	public static Vector2 DIR_NORTH;
	public static Vector2 DIR_EAST;
	public static Vector2 DIR_WEST;

	public static GameObject CreateWall(int start, int end, int wallHeight, float thickness) {
		/*Mesh mesh = new Mesh ();
		Vector3[] vertices = new Vector3[4];
		vertices [0] = new Vector3 (start, 0, 0);
		vertices[1] = new Vector3(end, 0, 0);
		vertices[2] = new Vector3(end, wallHeight, 0);
		vertices[3] = new Vector3(start, wallHeight, 0);
		
		mesh.vertices = vertices;
		
		int[] tris = new int[6];
		//  Lower left triangle.
		tris[0] = 0;
		tris[1] = 1;
		tris[2] = 2;
		
		//  Upper right triangle.   
		tris[3] = 0;
		tris[4] = 2;
		tris[5] = 3;
		
		mesh.triangles = tris;
		
		Vector3[] normals = new Vector3[4];
		normals[0] = -Vector3.forward;
		normals[1] = -Vector3.forward;
		normals[2] = -Vector3.forward;
		normals[3] = -Vector3.forward;
		
		Vector2[] uv = new Vector2[4];
		
		uv[0] = new Vector2(0, 0);
		uv[1] = new Vector2(1, 0);
		uv[2] = new Vector2(0, 1);
		uv[3] = new Vector2(1, 1);
		
		mesh.uv = uv;*/

		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);

		cube.transform.localScale = new Vector3 (end-start, wallHeight, thickness);
		cube.transform.localPosition = new Vector3 (0, wallHeight / 2, 0);

		return cube;
	}
}
