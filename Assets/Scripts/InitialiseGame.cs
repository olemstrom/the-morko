using UnityEngine;
using System.Collections;

public class InitialiseGame : MonoBehaviour {

	public int RoomCount = 10;
	public int MaxRoomSizeInCells = 15;
	public int MinRoomSizeInCells = 8;
	public int CellCount = 100;
	public int CellSize = 3;
	public Map map;

	public GameObject player;
	public GameObject morran;
	public GameObject brick;

	void Start () {
		CreateMap ();
		CreatePlayer ();
		CreateMorran ();

	}

	public void CreateMap() {
		map = new Map (RoomCount, MaxRoomSizeInCells, MinRoomSizeInCells, CellSize, CellCount);
		map.Generate ();
		Map3D.Create (map, brick);

		foreach (var room in map.GetRooms()) {
			Vertex2 p = room.GetCenterPoint();
			WPSet.wpList.Add(new Vector3(p.x, 0.5f, p.y));
		}
	}

	private void CreatePlayer() {
		Vertex2 playerPos = map.GetRooms()[0].GetCenterPoint();
		int x = map.CellSize * playerPos.x;
		int z = map.CellSize * playerPos.y;
		player.transform.localPosition = new Vector3 (x, 0, z);

		Debug.Log (x);
		Debug.Log (z);

	}

	private void CreateMorran() {
		Vertex2 morPos = map.GetRooms()[1].GetCenterPoint();

		int x = map.CellSize * morPos.x;
		int z = map.CellSize * morPos.y;
		morran.transform.localPosition = new Vector3 (x, 0, z);

		MoveTo m = morran.GetComponents<MoveTo> ()[0];
		m.Init ();
	}
	
}
