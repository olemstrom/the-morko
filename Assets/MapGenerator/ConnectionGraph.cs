using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionGraph {
	private Dictionary<Vertex2, HashSet<Vertex2>> connectionMap = new Dictionary<Vertex2, HashSet<Vertex2>>();
	private Dictionary<Vertex2, Vertex2> edges = new Dictionary<Vertex2, Vertex2> ();
	public List<Edge> removedEdges = new List<Edge>();

	public int Count;
	public ConnectionGraph(List<Edge> edges) {
		edges.Sort ();
		bool cycle = false;
		Vertex2 root = edges [0].start;
	

		foreach (Edge e in edges) {

			if(!connectionMap.ContainsKey(e.start)) connectionMap[e.start] = new HashSet<Vertex2>();
			if(!connectionMap.ContainsKey(e.end)) connectionMap[e.end] = new HashSet<Vertex2>();
			connectionMap[e.start].Add(e.end);
			connectionMap[e.end].Add(e.start);
			Count++;
			bool isCycle = hasCycle (e.start);
			if(isCycle) {
				connectionMap[e.start].Remove(e.end);
				connectionMap[e.end].Remove(e.start);
				removedEdges.Add (e);
				Count--;
			}
		}

		addRandomEdges ();
	}
	
	private bool hasCycle(Vertex2 root) {
		var visited = new List<Vertex2> ();
		dfs (root, null, visited);
		bool cycle = visited.Contains (root);
		return cycle;
	}

	private void dfs(Vertex2 v, Vertex2 prev, List<Vertex2> visited) {
		HashSet<Vertex2> c = connectionMap[v];
		foreach (Vertex2 w in connectionMap[v]) {
			if(!visited.Contains(w) && (prev == null || !prev.Equals (w))) {
				visited.Add (w);
				dfs (w, v, visited);
			}
		}
	}

	private void addRandomEdges(){
		int edgeCount = removedEdges.Count / 2;
		System.Random rand = new System.Random ();
		removedEdges.Sort ();
		for(int i = 0; i < edgeCount; i++) {
			Edge e = removedEdges[i];
			removedEdges.RemoveAt(i);

			connectionMap[e.start].Add(e.end);
			connectionMap[e.end].Add(e.start);
		}
	}

	public HashSet<Edge> GetEdges() {
		var edges = new HashSet<Edge> ();

		foreach (var v1 in connectionMap.Keys) {
			foreach(var v2 in connectionMap[v1]) {
				edges.Add (new Edge(v1, v2));
			}
		}

		return edges;
	}

	public override String ToString() {
		string s = "";
		foreach (Vertex2 v1 in connectionMap.Keys) {
			s += v1.toString() + ": ";
			foreach(Vertex2 v2 in connectionMap[v1]){
				s+= v2.toString () + " ";
			}
			s+= "\n";
		}

		return s;
	}
}
