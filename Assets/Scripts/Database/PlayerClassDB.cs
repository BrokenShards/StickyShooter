// PlayerClassDB.cs //

public class PlayerClassDB : ScriptableObjectDB<PlayerClassData>
{
	public override string FileName
	{
		get { return GetFileName(); }
	}

	public static string GetFileName()
	{
		return "players.asset";
	}
}
