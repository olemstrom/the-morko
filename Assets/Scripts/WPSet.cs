using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class WPSet
{
	public static Stack<Vector3> wpStack = new Stack<Vector3>();
	public static List<Vector3> wpList = new List<Vector3>();

	public static void shuffleWPList()
	{
		for(int t = 0; t < wpList.Count; t++){
			Vector3 tmp = wpList[t];
			int r = Random.Range(t, wpList.Count);
			wpList[t] = wpList[r];
			wpList[r] = tmp;
		}
	}
	
	public static void refillWPStack()
	{
		foreach(Vector3 v in wpList){
			wpStack.Push (v);
		}
		wpList.Clear ();
	}

}

