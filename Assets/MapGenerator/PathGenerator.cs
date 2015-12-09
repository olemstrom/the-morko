using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ConcurrentPriorityQueue;
public class PathGenerator {
	public int[,] tiles;
	// 0 == not endpoint, open
	// 1 == endpoint, open
	// 2 == closed
	// 3 == buffer

	private Map map;
	public const int OPEN_NOT_ENDPOINT = 0;
	public const int OPEN_ENDPOINT = 1;
	public const int CLOSED = 2;
	public const int BUFFER = 3;
	private Vertex2 end;
	public PathGenerator(Map map) {
		this.map = map;
		int y = map.tiles.GetLength (0);
		int x = map.tiles.GetLength (1);
		tiles = new int[y, x];

		DisableRooms ();
	}

	public void AddPath(Edge e) {
		Vertex2 start = e.start;
		Vertex2 end = e.end;
		this.end = end;


		MarkRoom (map.GetRoom (start), PathGenerator.OPEN_NOT_ENDPOINT);
		DisableAll ();
		EnableRoom (map.GetRoom (end));
		CreatePath (start);
		EnableRoom (map.GetRoom (start));
		EnableAll ();

	}

	private void CreatePath(Vertex2 start) {
		PriorityQueue frontier = new PriorityQueue ();
		frontier.Add (0, start);
		Dictionary<Vertex2, Vertex2> from = new Dictionary<Vertex2, Vertex2> ();
		Dictionary<Vertex2, double> cost = new Dictionary<Vertex2, double> ();
		cost [start] = 0;
		from [start] = null;
		Vertex2 end = null;

		while (!frontier.IsEmpty()) {
			var current = frontier.GetMin ();
			if(IsGoal(current)) {
				end = current;
				break;
			}

			foreach(PathNode neighbor in GetNeighbors(current)) {
				Vertex2 vNeighbor = neighbor.location;
				double newCost = cost[current] + neighbor.cost;
				if(!cost.ContainsKey(vNeighbor) || newCost < cost[vNeighbor]) {
					cost[vNeighbor] = newCost;
					frontier.Add(newCost, vNeighbor);
					from[vNeighbor] = current;
				}
			}
		}

		if (end != null) {
			Vertex2 current = end;
			while(!current.Equals(start)) {
				EnablePoint(current);
				current = from[current];
			}
		}
	}

	public int[,] GetTiles() {
		return tiles;
	}

	private List<PathNode> GetNeighbors(Vertex2 v) {
		var pNeighbors = PotentialNeighbors(v);
		var neighbors = new List<PathNode> ();
		foreach(var pNeighbor in pNeighbors) {
			if (map.IsInside (pNeighbor.location) && IsAccessible (pNeighbor.location)) neighbors.Add (pNeighbor);
		}

		return neighbors;
	}

	private List<PathNode> GetAllNeighbors(Vertex2 v) {
		var pNeighbors = AllPotentialNeighbors(v);
		var neighbors = new List<PathNode> ();
		foreach(var pNeighbor in pNeighbors) {
			if (map.IsInside (pNeighbor.location) && IsAccessible (pNeighbor.location)) neighbors.Add (pNeighbor);
		}
		
		return neighbors;
	}

	private List<PathNode> PotentialNeighbors(Vertex2 v) {
		List<PathNode> neighbors = new List<PathNode>();
		neighbors.Add (new PathNode(1, new Vertex2(v.x - 1, v.y)));
		neighbors.Add (new PathNode(1, new Vertex2(v.x + 1, v.y)));
		neighbors.Add (new PathNode(1, new Vertex2(v.x, v.y - 1)));
		neighbors.Add (new PathNode(1, new Vertex2(v.x, v.y + 1)));

		return neighbors;
	}

	private List<PathNode> AllPotentialNeighbors(Vertex2 v) {
		List<PathNode> neighbors = new List<PathNode>();
		neighbors.Add (new PathNode(1, new Vertex2(v.x - 1, v.y)));
		neighbors.Add (new PathNode(1, new Vertex2(v.x + 1, v.y)));
		neighbors.Add (new PathNode(1, new Vertex2(v.x, v.y - 1)));
		neighbors.Add (new PathNode(1, new Vertex2(v.x, v.y + 1)));

		neighbors.Add (new PathNode(1.4, new Vertex2(v.x + 1, v.y + 1)));
		neighbors.Add (new PathNode(1.4, new Vertex2(v.x + 1, v.y - 1)));
		neighbors.Add (new PathNode(1.4, new Vertex2(v.x - 1, v.y + 1)));
		neighbors.Add (new PathNode(1.4, new Vertex2(v.x - 1, v.y - 1)));

		return neighbors;
	}

	private bool IsAccessible(Vertex2 v) {
		int state = tiles[v.y, v.x];
		return state == PathGenerator.OPEN_ENDPOINT || state == PathGenerator.OPEN_NOT_ENDPOINT;
	}

	private bool IsGoal(Vertex2 v) {
		//return v.Equals (new Vertex2(0, 0));
		return tiles [v.y, v.x] == PathGenerator.OPEN_ENDPOINT;
	}

	private void DisableRooms() {
		foreach (Room r in map.GetRooms()) {
			DisableRoom (r);
		}
	}

	public void GrowOpen() {
		for(int i = 0; i < tiles.GetLength(0); i++) {
			for(int j = 0; j < tiles.GetLength(0); j++) {
				int state = tiles[i, j];
				if(state == PathGenerator.OPEN_ENDPOINT) {
					var neighbors = GetAllNeighbors(new Vertex2(j, i));
					foreach(PathNode neighbor in neighbors) {
						Vertex2 v = neighbor.location;
						if(tiles[v.y, v.x] != PathGenerator.OPEN_ENDPOINT) tiles[v.y, v.x] = PathGenerator.BUFFER;
					}
				}
			}
		}
	}

	private void DisableAll() {
		for(int i = 0; i < tiles.GetLength(0); i++) {
			for(int j = 0; j < tiles.GetLength(0); j++) {
				int state = tiles[i, j];
				if(state == PathGenerator.OPEN_ENDPOINT) tiles[i, j] = PathGenerator.CLOSED;
			}
		}
	}

	private void EnableAll() {
		for(int i = 0; i < tiles.GetLength(0); i++) {
			for(int j = 0; j < tiles.GetLength(0); j++) {
				int state = tiles[i, j];
				if(state == PathGenerator.CLOSED) tiles[i, j] = PathGenerator.OPEN_ENDPOINT;
			}
		}
	}

	private void MarkRoom(Room r, int newstate) {
		for (int i = r.GetX(); i < r.GetX() + r.GetWidth(); i++) {
			for(int j = r.GetY(); j < r.GetY() + r.GetHeight(); j++) {
				tiles[j, i] = newstate;
			}
		}
	}

	private void DisableRoom(Room room) {
		MarkRoom (room, PathGenerator.CLOSED);
	}

	private void EnableRoom(Room room) {
		MarkRoom (room, PathGenerator.OPEN_ENDPOINT);
	}

	private void EnablePoint(Vertex2 v) {
		tiles [v.y, v.x] = PathGenerator.OPEN_ENDPOINT;
	}



	private class PriorityQueue {
		ConcurrentPriorityQueue<Vertex2, double> queue = new ConcurrentPriorityQueue<Vertex2, double>();
		public void Add(double cost, Vertex2 v) {
			queue.Enqueue(v, -cost);
		}

		public Vertex2 GetMin() {
			return queue.Dequeue ();
		}

		public bool IsEmpty() {
			return queue.Count == 0;
		}
	}

	private class PathNode {
		public double cost;
		public Vertex2 location;
		public PathNode(double cost, Vertex2 location) {
			this.cost = cost;
			this.location = location;
		}
	}
}
