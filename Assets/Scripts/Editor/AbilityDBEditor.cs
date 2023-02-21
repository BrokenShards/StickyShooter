// AbilityDBEditor.cs //

using UnityEngine;
using UnityEditor;

public class AbilityDBEditor : ScriptableObjectDBEditor<AbilityData, AbilityDB>
{
	private Texture2D m_texture;
	private const int IconSize = 96;

    [MenuItem( "Shooter/Abilities" )]
    public static void Init()
    {
        AbilityDBEditor window = GetWindow<AbilityDBEditor>();
        window.minSize = new Vector2( 400, 300 );
        window.titleContent.text = "Abilities";
        window.Show();
	}

	public override string FileName
	{
		get { return AbilityDB.GetFileName(); }
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

		m_selected.name = EditorGUILayout.TextField( "Name", m_selected.name );
		m_selected.info = EditorGUILayout.TextField( "Info", m_selected.info );

		EditorGUILayout.BeginHorizontal();

		GUILayout.Label( "Icon", GUILayout.Width( 147 ) );

		m_texture = m_selected.icon != null ? m_selected.icon.texture : null;

		if( GUILayout.Button( m_texture, GUILayout.Width( IconSize ), GUILayout.Height( IconSize ) ) )
		{
			int controllerID = GUIUtility.GetControlID( FocusType.Passive );
			EditorGUIUtility.ShowObjectPicker<Sprite>( null, false, null, controllerID );
		}

		EditorGUILayout.EndHorizontal();

		m_selected.prefab = EditorGUILayout.ObjectField( "Prefab", m_selected.prefab, typeof( GameObject ), false ) as GameObject;

		if( Event.current.commandName == "ObjectSelectorUpdated" )
		{
			var select = EditorGUIUtility.GetObjectPickerObject();

			if( select != null )
			{
				if( select as Sprite != null )
				{
					m_selected.icon = (Sprite)select;
					Repaint();
				}
				else if( select as GameObject != null )
				{
					m_selected.prefab = (GameObject)select;
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
