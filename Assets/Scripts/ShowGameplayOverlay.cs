// ShowGameplayOverlay.cs //

using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowGameplayOverlay : MonoBehaviour
{
	public bool show = false;

	private void Update()
	{
		if( show && !m_showing )
		{
			SceneManager.LoadSceneAsync( 3, LoadSceneMode.Additive );
			m_showing = show;
		}
		else if( !show && m_showing )
		{
			SceneManager.UnloadSceneAsync( 3 );
			m_showing = show;
		}
	}

	private bool m_showing = false;
}
