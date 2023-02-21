// ActorDB.cs //

public class ActorDB : ScriptableObjectDB<ActorData>
{
	public override string FileName
	{
		get { return GetFileName(); }
	}

	public static string GetFileName()
	{
		return "actors.asset";
	}
}
