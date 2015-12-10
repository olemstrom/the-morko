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
	public GameObject key;
	public GameObject door;

	void Start () {
		CreateMap ();
		CreatePlayer ();
		CreateMorran ();
		CreateKey ();
	}

	public void CreateKey() {
		Vertex2 keyPos = map.GetRooms () [2].GetCenterPoint ();
		key.transform.localPosition = new Vector3 (keyPos.x * CellSize, 10, keyPos.y * CellSize);
	}
	

	public void CreateMap() {
		map = new Map (RoomCount, MaxRoomSizeInCells, MinRoomSizeInCells, CellSize, CellCount);
		map.Generate ();
		Map3D.Create (map, brick);

		foreach (var room in map.GetRooms()) {
			Vertex2 p = room.GetCenterPoint();
			WPSet.wpList.Add(new Vector3(p.x*CellSize, 0.5f, p.y*CellSize));
		}
	}

	private void CreatePlayer() {
		Vertex2 playerPos = map.GetRooms()[0].GetCenterPoint();
		int x = CellSize * playerPos.x;
		int z = CellSize * playerPos.y;
		player.transform.localPosition = new Vector3 (x, 0, z);

		PlaceDoor ();
	}

	private void PlaceDoor() {
		GameObject closest = null;
		double distance = Mathf.Infinity;
		foreach (var door in GameObject.FindGameObjectsWithTag("door")) {
			double newdist = Vector3.Distance(player.transform.position, door.transform.position);
			if(newdist < distance) {
				closest = door;
				distance = newdist;
			}
		}
		
		foreach (var door in GameObject.FindGameObjectsWithTag("door")) {
			if(door.GetInstanceID() != closest.GetInstanceID()) Destroy (door);
		}
		
		Vector3 localScale = closest.transform.localScale;
		float xscale = localScale.x / CellSize;
		closest.transform.localScale = new Vector3(xscale*2, localScale.y, localScale.z);

		Vector3 localPos = closest.transform.localPosition;
		string name = closest.name;

		if (name == "DoorLeft")
			closest.transform.localPosition = new Vector3 (localPos.x -0.16f, localPos.y, localPos.z);
		else if (name == "DoorRight")
			closest.transform.localPosition = new Vector3 (localPos.x, localPos.y, localPos.z -0.16f);
		else if (name == "DoorFront")
			closest.transform.localPosition = new Vector3 (localPos.x +0.16f, localPos.y, localPos.z);
		else if (name == "DoorBack")
			closest.transform.localPosition = new Vector3 (localPos.x, localPos.y, localPos.z+0.16f);


	}

	private void CreateMorran() {
		Vertex2 morPos = map.GetRooms()[1].GetCenterPoint();

		int x = CellSize * morPos.x;
		int z = CellSize * morPos.y;
		morran.transform.localPosition = new Vector3 (x, 0, z);

		MoveTo m = morran.GetComponents<MoveTo> ()[0];
		morran.GetComponent<MoveTo>().setPlayerTransform(player.transform);
		morran.GetComponent<MoveTo>().setTargetPointTransform(GameObject.Find("PlayerVisiblePoint").transform);

		m.Init ();
	}
	
}
