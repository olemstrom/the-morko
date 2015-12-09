using System.Collections;
using System;
public class Edge : IComparable {
	public Vertex2 start;
	public Vertex2 end;
	public int Size;

	public Edge(Vertex2 start, Vertex2 end) {
		this.start = start;
		this.end = end;

		double width = Math.Abs (start.x - end.x);
		double height = Math.Abs (start.y - end.y);
		Size = (int)Math.Sqrt (Math.Pow (width, 2) + Math.Pow (height, 2));
	}

	public override int GetHashCode() {
		return start.GetHashCode () + end.GetHashCode ();
	}

	public override bool Equals(object obj) { 
		if (obj == null || GetType() != obj.GetType()) return false;

		Edge e = (Edge)obj;
		bool sameStart = start.Equals (e.end) || start.Equals(e.start);
		bool sameEnd = end.Equals (e.start) || end.Equals (e.end);

		return sameStart && sameEnd;
	}

	public override string ToString ()
	{
		return "("+start.x+" "+start.y+")" + "("+ end.x + " " + end.y+")";
	}

	int IComparable.CompareTo(object o)
	{
		Edge e = (Edge)o;
		return Size.CompareTo (e.Size);
	}
	
	public bool IsConnected(Edge e) {
		bool startConnected = start.Equals (e.start) || start.Equals (e.end);
		bool endConnected = end.Equals (e.start) || end.Equals (e.end);

		return  startConnected || endConnected;
	}

	public bool IsConnected(Vertex2 e) {
		
		return  start.Equals (e) || end.Equals(e);
	}


}
