// SplashLogic.cs //

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent( typeof( Image ) )]
public class SplashLogic : MonoBehaviour
{
	[SerializeField]
	List<Sprite> m_images;

	byte  m_stage;
	Image m_img;
	float m_time,
	      m_delay = 2f;

	void Awake()
	{
		m_img   = GetComponent<Image>();
		m_time  = Time.time;
		m_stage = 0;
	}

	void Update()
	{
		if( m_stage >= m_images.Count )
		{
			SceneManager.LoadScene( 1, LoadSceneMode.Single );
			return;
		}

		m_img.sprite = m_images[ m_stage ];

		if( ( Time.time - m_time ) >= m_delay )
		{
			m_stage++;
			m_time = Time.time;
		}
	}
}
