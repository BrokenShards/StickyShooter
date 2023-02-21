// PlayerClassDBEditor.cs //

using UnityEngine;
using UnityEditor;

public class PlayerClassDBEditor : ScriptableObjectDBEditor<PlayerClassData, PlayerClassDB>
{
    [MenuItem( "Shooter/Player Classes" )]
    public static void Init()
    {
		PlayerClassDBEditor window = GetWindow<PlayerClassDBEditor>();
        window.minSize = new Vector2( 400, 300 );
        window.titleContent.text = "Player Classes";
        window.Show();
	}

	public override string FileName
	{
		get { return PlayerClassDB.GetFileName(); }
	}

	protected override void DisplayListItem( int index )
	{
		if( index < 0 || index >= m_db.Count )
			return;

		if( GUILayout.Button( index.ToString() + ": " + m_db.Get( index ).name ) )
			SelectItem( index );
	}
	protected override void DisplayItemEditor()
	{
		EditorGUILayout.BeginVertical();

		if( m_selected == null )
		{
			EditorGUILayout.EndVertical();
			return;
		}

		m_selected.name   = EditorGUILayout.TextField( "Name", m_selected.name );
		m_selected.info   = EditorGUILayout.TextField( "Info", m_selected.info );
		m_selected.prefab = EditorGUILayout.ObjectField( "Prefab", m_selected.prefab, typeof( GameObject ), false ) as GameObject;

		if( Event.current.commandName == "ObjectSelectorUpdated" )
		{
			var select = EditorGUIUtility.GetObjectPickerObject();

			if( select != null )
			{
				if( select as GameObject != null )
				{
					m_selected.prefab = select as GameObject;
					Repaint();
				}
			}
		}

		EditorGUILayout.BeginHorizontal();

		if( GUILayout.Button( "Save" ) )
			SaveSelectedtem();
		if( GUILayout.Button( "Discard" ) )
			DiscardItemChanges();

		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
	}
}
