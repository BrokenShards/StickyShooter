// Actor.cs //

using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
	[SerializeField][Range( 1u, Constants.MaxActorHP )]
	private uint m_maxhp = 1u;
	[SerializeField]
	private StatSet m_stats = new();
	[SerializeField]
	private GameObject spawnOnDie = null;

	public bool destroy = true;

	public uint HP
	{
		get; private set;
	}
	public uint MaxHP
	{
		get { return m_maxhp; }
		set { m_maxhp = (uint)Mathf.Clamp( value, 1u, isPlayer ? Constants.MaxPlayerHP : Constants.MaxActorHP ); }
	}

	public Stat GetStat( StatType st )
	{
		return m_stats.Get( st );
	}

	public void TakeDamage( uint dmg )
	{
		if( HP == 0 || dmg == 0 )
			return;

		HP = HP < dmg ? 0 : HP - dmg;
	}
	public void RestoreHealth( uint hel )
	{
		HP = ( hel > MaxHP || ( MaxHP - hel ) > HP ) ? MaxHP : HP + hel;
	}

	void Awake()
	{
		if( HP == 0 )
			HP = MaxHP;
	}

	void Update()
	{
		if( HP == 0 )
		{
			if( spawnOnDie != null )
			{
				var obj = Instantiate( spawnOnDie );
				obj.SetActive( false );
				obj.transform.position = transform.position;
				obj.transform.forward  = transform.forward;
				obj.SetActive( true );
			}

			if( destroy )
				Destroy( gameObject );
			else
				gameObject.SetActive( false );

			return;
		}
	}

	[NonSerialized]
	public bool isPlayer;
}
