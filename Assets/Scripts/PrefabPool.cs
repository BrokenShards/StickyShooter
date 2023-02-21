// PrefabPool.cs //

using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : MonoBehaviour
{
	public GameObject FirstInactive
	{
		get
		{
			if( m_objects == null )
				return null;

			for( int i = 0; i < m_objects.Count; i++ )
				if( !m_objects[ i ].activeSelf )
					return m_objects[ i ];

			return null;
		}
	}

	public bool ResetPool( uint? amt = null, GameObject pfab = null )
	{
		OnDestroy();

		if( pfab != null )
			prefab = pfab;

		if( prefab == null )
		{
			Debug.Log( "Null object given to object pool launcher.\n" );
			return false;
		}

		if( amt.HasValue )
			m_amount = amt.Value;

		m_objects = new List<GameObject>( (int)m_amount );

		try
		{
			for( int i = 0; i < (int)m_amount; i++ )
			{
				m_objects.Add( Instantiate( prefab ) );
				m_objects[ i ].SetActive( false );
			}
		}
		catch
		{
			Debug.Log( "Unable to instantiate objects.\n" );
			return false;
		}

		return true;
	}

	private void Awake()
	{
		if( !ResetPool() )
		{
			Debug.Log( "Failed creating projectiles.\n" );
			return;
		}
	}
	private void OnDestroy()
	{
		if( m_objects == null || m_objects.Count == 0 )
			return;

		for( int i = 0; i < m_objects.Count; i++ )
			if( m_objects[ i ] != null )
				Destroy( m_objects[ i ] );

		m_objects.Clear();
	}

	public GameObject prefab = null;

	[SerializeField]
	[Range(1u, 1000000u)]
	private uint m_amount = 10u;
	private List<GameObject> m_objects = null;
}
