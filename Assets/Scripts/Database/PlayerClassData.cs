// PlayerClassData.cs //

using UnityEngine;

[System.Serializable]
public class PlayerClassData : System.ICloneable
{
	public PlayerClassData()
	{
		name   = string.Empty;
		info   = string.Empty;
		prefab = null;
	}

	public PlayerClassData( string name, string info = null, GameObject prefab = null )
	{
		this.name   = name ?? string.Empty;
		this.info   = info ?? string.Empty;
		this.prefab = prefab;
	}

	public string     name,
	                  info;
	public GameObject prefab;

	public object Clone()
	{
		return new PlayerClassData( name, info, prefab );
	}
}
