using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {

	private int width;
	private int height;
	private int x;
	private int y;
	private HashSet<Room> connections = new HashSet<Room>();

	public Room(int width, int height, int x, int y) {
		this.width = width;
		this.height = height;
		this.x = x;
		this.y = y;    
	}

	public int GetX() {
		return x;
	}

	public int GetY() {
		return y;
	}

	public int GetWidth() {
		return width;
	}

	public int GetHeight() {
		return height;
	}

	public void AddConnection(Room room) {
		connections.Add (room);
	}
	/*
	public Room GetClosest(List<Room> rooms) {

		Room closest = null;
		foreach (Room r in rooms) {
			if(closest == null && this != r || DistanceTo (r) < DistanceTo(closest)) {
				closest = r;
			}
		}

		return closest;
	}*/

	public Vertex2 GetCenterPoint() {
		int centerX = this.width / 2 + x;
		int centerY = this.height / 2 + y;

		return new Vertex2 (centerX, centerY);
	}

	/*public double DistanceTo(Point p) {
		return GetCenterPoint ().DistanceTo (p);
	}

	public double DistanceTo(Room r) {
		return GetCenterPoint ().DistanceTo (r.GetCenterPoint());
	}*/
}
