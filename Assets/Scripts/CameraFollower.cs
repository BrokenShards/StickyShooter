// CameraFollower.cs //

using UnityEngine;

public class CameraFollower : MonoBehaviour
{
	[SerializeField]
	Transform m_target;
	[SerializeField]
	Vector3 m_offset = new( 0.0f, 10.0f, 0.0f );
	[SerializeField]
	[Range( 0.01f, 1000.0f)]
	float m_speed = 5.0f;

	void Update()
	{
		if( m_target == null )
			return;

		GetComponent<Transform>().position = Vector3.Lerp( GetComponent<Transform>().position, m_target.position + m_offset, Time.deltaTime * m_speed );
	}
}
