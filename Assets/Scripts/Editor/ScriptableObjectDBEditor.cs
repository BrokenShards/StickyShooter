// ScriptableObjectDBEditor.cs //

using System;
using UnityEngine;
using UnityEditor;

public abstract class ScriptableObjectDBEditor<T, DB> : EditorWindow
	where T  : class, ICloneable, new() 
	where DB : ScriptableObjectDB<T>
{
	protected DB  m_db;
	protected T   m_selected;
	protected int m_index;

    public const string DatabaseDir = "Assets/Database/";

	private Vector2 m_scrollpos;

	public abstract string FileName
	{
		get;
	}

	private void OnEnable()
	{
        m_db = AssetDatabase.LoadAssetAtPath<DB>( DatabaseDir + FileName );

		if( m_db == null )
        {
			if( !AssetDatabase.IsValidFolder( "Assets/Database/" ) )
				AssetDatabase.CreateFolder( "Assets", "Database" );

			m_db = CreateInstance<DB>();
			AssetDatabase.CreateAsset( m_db, DatabaseDir + FileName );
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		SelectItem( -1 );
	}
	private void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		ListView();
		DisplayItemEditor();
		EditorGUILayout.EndHorizontal();
	}

	private void ListView()
	{
		EditorGUILayout.BeginVertical( GUILayout.Width( 250 ) );

		m_scrollpos = EditorGUILayout.BeginScrollView( m_scrollpos, GUILayout.ExpandHeight( true ) );
		EditorGUILayout.BeginVertical();

		for( int i = 0; i < m_db.Count; i++ )
		{
			EditorGUILayout.BeginHorizontal();
			DisplayListItem( i );

			if( GUILayout.Button( "x", GUILayout.Width( 30 ) ) )
			{
				m_db.Remove( i );

				if( m_index == i )
					SelectItem( -1 );

				i--;
			}

			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();

		if( GUILayout.Button( "Create New" ) )
		{
			m_db.Add( new T() );
			SelectItem( m_db.Count - 1 );
		}

		EditorGUILayout.EndVertical();
	}

	protected void SelectItem( int index )
	{
		if( index < 0 || index >= m_db.Count )
		{
			m_selected = null;
			m_index    = -1;
			return;
		}

		m_selected = m_db.Get( index ).Clone() as T;
		m_index    = index;
	}
	protected void SaveSelectedtem()
	{
		if( m_index < 0 || m_index >= m_db.Count || m_selected == null )
			return;
		
		m_db.Replace( m_index, m_selected );
	}
	protected void DiscardItemChanges()
	{
		SelectItem( m_index );
	}

	protected abstract void DisplayListItem( int index );
	protected abstract void DisplayItemEditor();
}
