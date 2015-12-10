using UnityEngine;
using System.Collections;

public static class GlobalVariables
{
	public static int keyCount = 0;
	public static bool isChasing = false;
	public static bool keyChase = false;

	public static bool keyFound()
	{
		if (keyCount  > 0)
			return true;
		else
			return false;
	}
}

