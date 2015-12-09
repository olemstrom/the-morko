using UnityEngine;
using System.Collections;
using MIConvexHull;

public class Vertex2 : IVertex
{

	public double[] Position { get; set; }
	
	public int x { get { return (int)Position[0]; } }
	public int y { get { return (int)Position[1]; } }
	
	public Vertex2(int x, int y)
	{
		Position = new double[] { (double)x, (double)y };
	}

	public Vector2 ToVector2() {
		return new Vector2((int)Position[0], (int)Position[1]);
	}
	
	public Vector3 ToVector3() {
		return new Vector3((int)Position[0], (int)Position[1], 0);
	}

	public override int GetHashCode ()
	{
		return x.GetHashCode () + y.GetHashCode ();
	}

	public override bool Equals(object obj) {
		if (obj == null || GetType() != obj.GetType()) return false;

		Vertex2 v = (Vertex2)obj;
		return x == v.x && y == v.y;
	}

	public string toString() {
		return "(" + x +", "+y+")";
	}
}
