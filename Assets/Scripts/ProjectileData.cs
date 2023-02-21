// ProjectileData.cs //

using System;
using UnityEngine;

[Serializable]
public class ProjectileData
{
	public GameObject prefab = null;

	public string name;

	[Range( 0, 999999)]
	public uint  damage = 1u;
	[Range( 0f, 300f)]
	public float lifetime = 8f;
	[Range( 0.1f, 1000.0f)]
	public float turn_speed = 10f;
}
