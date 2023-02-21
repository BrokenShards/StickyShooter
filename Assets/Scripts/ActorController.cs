// ActorController.cs //

using UnityEngine;

[RequireComponent( typeof( Rigidbody ) )]
public class ActorController : MonoBehaviour
{
	public Transform DebugTarget = null;

    [SerializeField][Range( 0.1f, 1000f)][Tooltip( "How fast the player accellerates." )]
	private float m_speed = 5.0f;
	[SerializeField][Range( 0.1f, 1000f)][Tooltip( "The players top speed." )]
	private float m_topSpeed = 6.0f;
	[SerializeField][Range( 0f, 1000f)][Tooltip( "The friction applied to the players movement." )]
	private float m_drag = 5.0f;
	[SerializeField][Range( 0f, 1000f)][Tooltip( "Rate of Fire" )]
	private float m_fireRate = 0.1f;
	[SerializeField][Tooltip( "The index of the equipped ability in the database." )]
	private int m_abilityIndex = -1;

	private Vector2 m_moveinput = default,
					m_lookinput = default;

	private Rigidbody m_rb;
	private Vector3   m_facing;
	private float     m_lastfire;

	private GameObject m_abilityPrefab;

	public float Speed
	{
		get { return m_speed; }
		set { m_speed = Mathf.Clamp( value, 0.1f, 1000f ); }
	}
	public float TopSpeed
	{
		get { return m_topSpeed; }
		set { m_topSpeed = Mathf.Clamp( value, 0.1f, 1000f ); }
	}
	public float Drag
	{
		get { return m_drag; }
		set { m_drag = Mathf.Clamp( value, 0f, 1000f ); }
	}
	public float FireRate
	{
		get { return m_fireRate; }
		set { m_fireRate = Mathf.Clamp( value, 0f, 1000f ); }
	}

	public Vector2 MoveInput
	{
		get { return m_moveinput; }
		set { m_moveinput = value.magnitude > 1f ? value.normalized : value; }
	}
	public Vector2 LookInput
	{
		get { return m_lookinput; }
		set { m_lookinput = value.magnitude > 1f ? value.normalized : value; }
	}
	public bool FireInput
	{
		get; set;
	}

	public int AbilityIndex
	{
		get { return m_abilityIndex; }
		set { SetAbility( value ); }
	}

	public void SetAbility( int index )
	{
		var db  = Database.Load<AbilityData, AbilityDB>( AbilityDB.GetFileName() );

		if( index < 0 || db == null || index >= db.Count )
		{
			m_abilityPrefab = null;
			m_abilityIndex  = -1;
			return;
		}

		var item = db.Get( index );

		if( item == null )
		{
			m_abilityPrefab = null;
			m_abilityIndex = -1;
			return;
		}

		m_abilityPrefab = item.prefab;
		m_abilityIndex  = index;
	}

	private void Awake()
	{
		m_rb        = GetComponent<Rigidbody>();
		m_facing    = GetComponent<Transform>().forward;
		m_lastfire  = Time.time;
		SetAbility( 0 );
	}

	private void Update()
	{
		m_rb.drag = Drag;
		
		GetComponent<Transform>().forward = m_facing;

		if( LookInput != Vector2.zero )
		{
			m_facing.x = LookInput.x;
			m_facing.z = LookInput.y;
		}

		Vector3 move = new( MoveInput.x, 0f, MoveInput.y );

		m_rb.AddForce( 10.0f * m_speed * move, ForceMode.Force );

		var vel = new Vector2( m_rb.velocity.x, m_rb.velocity.z );

		if( vel.magnitude > m_topSpeed )
		{
			var limit = vel.normalized * m_topSpeed;
			m_rb.velocity = new Vector3( limit.x, m_rb.velocity.y, limit.y );
		}

		if( FireInput && m_abilityPrefab != null && ( m_fireRate == 0f || ( Time.time - m_lastfire >= m_fireRate ) ) )
		{
			var proj = Instantiate( m_abilityPrefab );

			if( proj == null )
				return;

			proj.SetActive( false );

			proj.transform.position = transform.position + transform.forward;
			proj.transform.forward  = transform.forward;

			if( proj.GetComponent<Mover>() != null )
				proj.GetComponent<Mover>().position = proj.transform.position;

			if( proj.GetComponent<Projectile>() != null )
			{
				proj.GetComponent<Projectile>().Owner   = gameObject;
				proj.GetComponent<Projectile>().destroy = true;
			}

			var childs = proj.GetComponentsInChildren<Projectile>();

			for( int i = 0; childs != null && i < childs.Length; i++ )
			{
				childs[ i ].Owner   = gameObject;
				childs[ i ].destroy = true;
			}

			proj.SetActive( true );
			m_lastfire = Time.time;
		}
	}
}
