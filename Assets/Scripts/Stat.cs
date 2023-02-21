// Stat.cs //

using System;

public enum StatType
{
	Health,
	Power,
	Speed,

	COUNT
}

[Serializable]
public class Stat
{
	public Stat()
	{
		BaseValue = Constants.MinActorStat;
	}
	public Stat( uint val, uint buff = 0, uint debuff = 0 )
	{
		BaseValue   = val;
		BuffValue   = buff;
		DebuffValue = debuff;
	}

	public uint MaxBuffValue
	{
		get { return Constants.MaxActorStat - BaseValue; }
	}
	public uint MaxDebuffValue
	{
		get { return BaseValue - Constants.MinActorStat; }
	}

	public uint BaseValue
	{
		get { return m_base; }
		set { m_base = Math.Clamp( value, Constants.MinActorStat, Constants.MaxActorStat ); }
	}
	public uint BuffValue
	{
		get { return m_buff; }
		set { m_buff = Math.Min( value, MaxBuffValue ); }
	}
	public uint DebuffValue
	{
		get { return m_debuff; }
		set { m_debuff = Math.Min( value, MaxDebuffValue ); }
	}
	public uint TotalValue
	{
		get { return BaseValue + BuffValue - DebuffValue; }
	}

	private uint m_base,
	             m_buff,
	             m_debuff;
}

[Serializable]
public class StatSet
{
	public StatSet()
	{
		int count = (int)StatType.COUNT;

		m_stats = new Stat[ count ];

		for( int i = 0; i < count; i++ )
			m_stats[ i ] = new Stat();
	}

	public Stat Get( StatType st )
	{
		if( st < 0 || st >= StatType.COUNT )
			return null;

		return m_stats[ (int)st ];
	}

	readonly Stat[] m_stats;
}