// TestCube.cs //

using UnityEngine;

public class TestCube : MonoBehaviour
{
	public Vector3 sin   = default,
	               speed = new( 1f, 1f, 1f );

	private Vector3 m_pos;
	private float m_time;

	void Awake()
	{
		m_pos  = transform.position;
		m_time = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		var pos = m_pos;

		pos.x += Mathf.Sin( ( Time.time - m_time ) * speed.x ) * sin.x;
		pos.y += Mathf.Sin( ( Time.time - m_time ) * speed.y ) * sin.y;
		pos.z += Mathf.Sin( ( Time.time - m_time ) * speed.z ) * sin.z;

		transform.position = pos;
	}
}
