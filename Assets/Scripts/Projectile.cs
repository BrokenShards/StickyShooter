// Projectile.cs //

using System;
using UnityEngine;

[RequireComponent( typeof( Rigidbody ) )]
public class Projectile : MonoBehaviour
{
	[Range( 0, 999999)][Tooltip( "The amount of damage caused if the projectile hits an actor." )]
	public uint damage = 0u;
	[Range( 0f, 120f)][Tooltip( "How long (in seconds) the projectile will live for before destroying itself (0 means infinite)." )]
	public float lifetime = 7f;
	[Tooltip( "Object that will be spawned when the projectile hits something or the lifetime ends." )]
	public GameObject spawnOnHit;

	public bool destroyOnHit = true;

	[NonSerialized]
	public bool destroy = true;

	public GameObject Owner
	{
		get; set;
	}
	public float LastFired
	{
		get; private set;
	}
	public float TimeSinceLastFired
	{
		get { return Time.realtimeSinceStartup - LastFired; }
	}

	public void Reset()
	{
		LastFired = Time.realtimeSinceStartup;
	}

	private void Awake()
	{
		LastFired = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		if( TimeSinceLastFired >= lifetime )
		{
			if( spawnOnHit != null )
			{
				var obj = Instantiate( spawnOnHit );
				obj.SetActive( false );
				obj.transform.position = transform.position;
				obj.transform.forward  = transform.forward;
				obj.SetActive( true );
			}

			if( destroy )
				Destroy( gameObject );
			else
				gameObject.SetActive( false );
		}
	}

	private void OnTriggerEnter( Collider other )
	{
		if( !destroyOnHit || other == null || other.gameObject == Owner )
			return;

		if( spawnOnHit != null )
		{
			var obj = Instantiate( spawnOnHit );
			obj.SetActive( false );
						
			if( obj.TryGetComponent<Projectile>( out var proj ) )
			{
				proj.Owner   = Owner;
				proj.destroy = true;

				var childs = proj.GetComponentsInChildren<Projectile>();

				for( int i = 0; i < childs.Length; i++ )
				{
					if( childs[ i ] != null )
					{
						proj.Owner = Owner;
						proj.destroy = true;
					}
				}
			}

			obj.transform.position = transform.position;
			obj.transform.forward  = transform.forward;
			obj.SetActive( true );
		}

		if( destroy )
			Destroy( gameObject );
		else
			gameObject.SetActive( false );
	}
}
