// AbilityData.cs //

using UnityEngine;

[System.Serializable]
public class AbilityData : System.ICloneable
{
	public AbilityData()
	{
		name   = string.Empty;
		info   = string.Empty;
		icon   = null;
		prefab = null;
	}

	public AbilityData( string name, string info = null, Sprite icon = null, GameObject prefab = null )
	{
		this.name   = name ?? string.Empty;
		this.info   = info ?? string.Empty;
		this.icon   = icon;
		this.prefab = prefab;
	}

	public string name,
	              info;
	public Sprite icon;

	public GameObject prefab;

	public object Clone()
	{
		return new AbilityData( name, info, icon, prefab );
	}
}
