// HomingMover.cs //

using UnityEngine;

[RequireComponent( typeof( Mover ) )]
public class HomingMover : MonoBehaviour
{
	[Range( 0.1f, 1000.0f)]
	public float speed = 10f;
	[Range( 0.1f, 1000f)]
	public float turnSpeed = 10f;

	private Mover m_move;

	public Transform Target
	{
		get; set;
	}

	void Awake()
	{
		m_move = GetComponent<Mover>();
		
		if( Target == null )
			Target = transform;
	}
	void Update()
	{
		Quaternion rotation    = Quaternion.LookRotation( ( Target.position - transform.position ).normalized );
		transform.rotation     = Quaternion.Lerp( transform.rotation, rotation, turnSpeed * Time.deltaTime );
		m_move.position += speed * Time.deltaTime * transform.forward;
	}
}
