// PlayerController.cs //

using UnityEngine;

using static UnityEngine.GraphicsBuffer;

public enum Direction
{
	Up,
	Down,
	Left,
	Right,

	COUNT
}

[RequireComponent( typeof( ActorController ) )]
public class PlayerController : MonoBehaviour
{
	private ActorController m_actcon;

	private void Awake()
	{
		m_actcon = GetComponent<ActorController>();
	}

	private void Update()
	{
		m_actcon.MoveInput = new Vector2( Input.GetAxis( "MoveX" ), Input.GetAxis( "MoveY" ) );
		m_actcon.LookInput = new Vector2( Input.GetAxis( "LookX" ), Input.GetAxis( "LookY" ) );
		m_actcon.FireInput = Input.GetAxis( "Fire" ) > 0.6f;

		if( Input.GetAxis( "Mouse X" ) != 0 || Input.GetAxis( "Mouse Y" ) != 0 )
		{
			var mpos   = Input.mousePosition;
			var objpos = Camera.main.WorldToScreenPoint( transform.position );
			m_actcon.LookInput = mpos - objpos;
		}

		if( Input.GetKeyDown( KeyCode.Space ) )
			m_actcon.SetAbility( m_actcon.AbilityIndex + 1 );
	}
}
