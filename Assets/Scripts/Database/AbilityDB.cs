// AbilityDB.cs //

public class AbilityDB : ScriptableObjectDB<AbilityData>
{
	public override string FileName
	{
		get { return GetFileName(); }
	}

	public static string GetFileName()
	{
		return "abilities.asset";
	}
}
