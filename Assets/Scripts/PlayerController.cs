// PlayerController.cs //

using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,

    COUNT
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Range( 0.1f, 1000.0f)]
    float m_speed = 5.0f;

    Rigidbody m_rb;
    Vector3   m_facing;

    void Awake()
    {
        m_rb     = GetComponent<Rigidbody>();
        m_facing = GetComponent<Transform>().forward;
    }

    void Update()
    {
        var moveinput = new Vector3( Input.GetAxis( "MoveX" ), 0.0f, Input.GetAxis( "MoveY" ) );
		var lookinput = new Vector3( Input.GetAxis( "LookX" ), 0.0f, Input.GetAxis( "LookY" ) );
        
        GetComponent<Transform>().forward = m_facing;
        
        if( moveinput.magnitude > 1.0f )
			moveinput = moveinput.normalized;
		if( lookinput.magnitude > 1.0f )
			lookinput = lookinput.normalized;

        if( lookinput != Vector3.zero )
			m_facing = lookinput;

        m_rb.AddForce( m_speed * moveinput * 10.0f, ForceMode.Force );

        var vel = new Vector2( m_rb.velocity.x, m_rb.velocity.z );

        if( vel.magnitude > m_speed )
        {
            var limit = vel.normalized * m_speed;
            m_rb.velocity = new Vector3( limit.x, m_rb.velocity.y, limit.y );
        }
	}

	private void OnDrawGizmos()
	{
        Debug.DrawLine( GetComponent<Transform>().position, GetComponent<Transform>().position + ( m_facing * 2.0f ), Color.magenta );
	}
}
