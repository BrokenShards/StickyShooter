// DirectionalMover.cs //

using UnityEngine;

[RequireComponent( typeof( Mover ) )]
public class DirectionalMover : MonoBehaviour
{
	[Range( 0.1f, 1000.0f)]
	public float speed = 10f;

	private Mover m_move;

	private void Awake()
	{
		m_move = GetComponent<Mover>();
	}
	private void Update()
	{
		m_move.position += speed * Time.deltaTime * transform.forward;
	}
}
