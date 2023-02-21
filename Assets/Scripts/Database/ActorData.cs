// ActorData.cs //

using UnityEngine;

[System.Serializable]
public class ActorData : System.ICloneable
{
	public ActorData()
	{
		name   = string.Empty;
		info   = string.Empty;
		prefab = null;
	}

	public ActorData( string name, string info = null, GameObject prefab = null )
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
		return new ActorData( name, info, prefab );
	}
}
