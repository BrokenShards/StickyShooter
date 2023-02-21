// Mover.cs //

using UnityEngine;

public class Mover : MonoBehaviour
{
	public Vector3 position;

	[Range( 0f, 1000f)]
	public float wobble_strength = 0f;
	[Range( 0f, 1000f)]
	public float wobble_speed = 10f;

	private float m_last = 0;

	void Awake()
	{
		m_last = Time.time;
	}
	private void OnEnable()
	{
		position = transform.position;
	}
	void Update()
    {
        transform.position = position;

		if( wobble_strength != 0f && wobble_speed != 0f )
			transform.position += Mathf.Sin( ( Time.time - m_last ) * wobble_speed ) * Time.deltaTime *
				wobble_strength * Vector3.Cross( transform.forward, Vector3.up ).normalized;
	}
}
