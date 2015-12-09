using System.Collections;
using System.Collections.Generic;
using System;
using MIConvexHull;
public class Map {

	private List<Room> Rooms = new List<Room>();
	private int RoomCount;
	private int MaxRoomSize;
	private int MinRoomSize;
	public int CellSize;
	public int CellCount;
	public bool[,] tiles;
	public int[,] ptiles;

	public VoronoiMesh<Vertex2, Cell2, VoronoiEdge<Vertex2, Cell2>> voronoiMesh;
	public HashSet<Edge> edges = new HashSet<Edge>();
	public HashSet<Edge> allEdges = new HashSet<Edge> ();
	public ConnectionGraph c;

	public Map(int roomCount, int maxRoomSize, int minRoomSize, int CellSize, int CellCount) {
		this.RoomCount = roomCount;
		this.MaxRoomSize = maxRoomSize;
		this.MinRoomSize = minRoomSize;
		this.CellCount = CellCount;
		this.CellSize = CellSize;
		tiles = new bool[CellCount, CellCount];
	}  

	public void Generate() {
		int tries = 0;
		while (Rooms.Count < RoomCount) {
			tries++;
			if(tries > RoomCount + 1000*1000) break;
			Random rand = new Random();
			int width = rand.Next(MinRoomSize, MaxRoomSize);
			int height = rand.Next(MinRoomSize, MaxRoomSize);
			int x = rand.Next(CellCount);
			int y = rand.Next(CellCount);

			Room room = new Room(width, height, x, y);
			if(IsInside (room) && !Overlaps(room, 2)) AddRoom (room);
		}
		//AddRoom (new Room (2, 2, 0, 0));

		RoomCount = Rooms.Count;

		Vertex2[] vertices = new Vertex2[Rooms.Count];

		int index = 0;
		foreach (Room r in Rooms) {
			vertices[index++] = r.GetCenterPoint();
		}

		voronoiMesh = VoronoiMesh.Create<Vertex2, Cell2> (vertices);
		foreach (var edge in voronoiMesh.Vertices) {
			int x1 = (int)edge.Vertices[0].x;
			int y1 = (int)edge.Vertices[0].y;
			int x2 = (int)edge.Vertices[1].x;
			int y2 = (int)edge.Vertices[1].y;
			edges.Add (new Edge(new Vertex2(x1, y1), new Vertex2(x2, y2)));
			
			x1 = x2;
			y1 = y2;
			x2 = (int)edge.Vertices[2].x;
			y2 = (int)edge.Vertices[2].y;
			edges.Add (new Edge(new Vertex2(x1, y1), new Vertex2(x2, y2)));
			
			x1 = x2;
			y1 = y2;
			x2 = (int)edge.Vertices[0].x;
			y2 = (int)edge.Vertices[0].y;
			edges.Add (new Edge(new Vertex2(x1, y1), new Vertex2(x2, y2)));
		}

		Edge[] edgeAry = new Edge[edges.Count];
		edges.CopyTo (edgeAry);
		
		c = new ConnectionGraph (new List<Edge>(edges));
		allEdges = edges;
		edges = c.GetEdges ();

		PathGenerator p = new PathGenerator (this);
		Edge[] pedge = new Edge[edges.Count];
		edges.CopyTo (pedge);
		Array.Sort (pedge);
		foreach (var edge in edges) {
			p.AddPath(edge);
		}

		p.GrowOpen ();
		savePaths (p);

		ptiles = p.tiles; 
	}

	private void savePaths(PathGenerator p) {
		int[,] paths = p.GetTiles ();
		int y = paths.GetLength (0);
		int x = paths.GetLength (1);
		for (int i = 0; i < y; i++) {
			for(int j = 0; j < x; j++) {
				int tile = paths[i, j];
				tiles[i, j] = tile == PathGenerator.OPEN_ENDPOINT;
			}
		}
	}

	public List<Room> GetRooms() {
		return Rooms;
	}

	public void AddRoom(Room room) {
		Rooms.Add (room);
		int x = room.GetX ();
		int y = room.GetY ();
		int w = room.GetWidth ();
		int h = room.GetHeight ();

		for (int i = y; i < y+h; i++) {
			for(int j = x; j < x+w; j++) tiles[i, j] = true; 
		}

	}

	public bool Overlaps(Room room, int buffer) {

		bool overlaps = false;

		foreach (Room other in Rooms) {
			bool overlapsX = room.GetX() < other.GetX() + other.GetWidth() + buffer 
							 && room.GetX() + room.GetWidth() + buffer > other.GetX();
			bool overlapsY = room.GetY() < other.GetY() + other.GetHeight() + buffer 
							 && room.GetY() + room.GetHeight() + buffer > other.GetY();

			if(!overlaps) overlaps = overlapsX && overlapsY;
		}

		return overlaps;
	}

	public bool IsInside(Room room) {
		int MapSize = CellCount;
		return  room.GetX () + room.GetWidth () < MapSize && room.GetY () + room.GetHeight () < MapSize;
	}

	public bool IsInside(Room r, Vertex2 point) {
		int rx = r.GetX ();
		int ry = r.GetY ();
		int rw = r.GetWidth ();
		int rh = r.GetHeight ();

		bool isx = point.x >= rx && point.x <= rx + rw;
		bool isy = point.y >= ry && point.y < ry + rh;

		return isx && isy;
	}

	public bool IsInside(Vertex2 v) {
		int MapSize = CellCount;
		return  v.x < MapSize && v.x >= 0 && v.y < MapSize && v.y >= 0;
	}

	public Room GetRoom(Vertex2 point) {
		foreach(Room r in Rooms) {
			bool isInside = IsInside (r, point); 
			if(isInside) return r;
		}

		return null;
	}


	public int GetMapSize() {

		return CellCount;
	}
}
