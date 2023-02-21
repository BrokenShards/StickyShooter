// TitleLogic.cs //

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent( typeof( Image ) )]
public class TitleLogic : MonoBehaviour
{
	void Awake()
	{
	}

	void Update()
	{
		if( Input.GetAxis( "Submit" ) > 0.5f )
		{
			SceneManager.LoadScene( 2, LoadSceneMode.Single );
			return;
		}
	}
}
