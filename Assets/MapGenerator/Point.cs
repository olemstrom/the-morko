// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
public class Point {
	int x;
	int y;

	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public int GetX() {
		return x;
	}

	public int GetY() {
		return y;
	}

	public void GetX(int x) {
		this.x = x;
	}

	public void GetY(int y) {
		this.y = y;
	}

	public double DistanceTo(Point p) {
		return Math.Sqrt(Math.Pow(p.GetX (), 2) + Math.Pow(p.GetY(), 2));
	}
}

