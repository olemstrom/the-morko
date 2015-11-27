using UnityEngine;
using System.Collections;

public class WPNode
{
	private Vector3 _position;
	private WPNode next;

	public WPNode()
	{
		this._position = new Vector3(0.0f, 0.0f, 0.0f);
		this.next = null;
	}

	public WPNode(Vector3 position)
	{
		this._position = position;
		this.next = null;
	}

	public WPNode getNext()
	{
		return next;
	}

	public void setNext(WPNode next)
	{
		this.next = next;
	}

	public Vector3 getWaypoint()
	{
		return _position;
	}

	public void setWaypoint(Vector3 position)
	{
		this._position = position;
	}
}

