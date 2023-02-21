// ScriptableObjectDB.cs //

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class ScriptableObjectDB<T> : ScriptableObject where T : class
{
	[SerializeField]
	List<T> database = new();

	public abstract string FileName
	{
		get;
	}

	public int Count
	{ 
		get { return database.Count; } 
	}

	public T Get( int index )
	{
		if( index < 0 || index >= Count )
			return null;

		return database[ index ];
	}

	public void Add( T item )
	{
		if( item == null )
			return;

		database.Add( item );
		SetDBDirty();
	}
	public void Insert( int index, T item )
	{
		if( item == null )
			return;

		database.Insert( index, item );
		SetDBDirty();
	}
	public void Replace( int index, T item )
	{
		if( item == null )
			return;

		database[ index ] = item;
		SetDBDirty();
	}

	public void Remove( T data )
	{
		if( database.Remove( data ) )
			SetDBDirty();
	}
	public void Remove( int index )
	{
		if( index < 0 || index >= database.Count )
			return;

		database.RemoveAt( index );
		SetDBDirty();
	}

	public void Clear()
	{
		if( Count > 0 )
		{
			database.Clear();
			SetDBDirty();
		}
	}

	private void SetDBDirty()
	{
#if UNITY_EDITOR
		EditorUtility.SetDirty( this );
#endif
	}
}

public static class Database
{
	public static DB Load<T, DB>( string filename ) where T : class where DB : ScriptableObjectDB<T>
	{
		var task = Addressables.LoadAssetAsync<DB>( "Assets/Database/" + filename );
		
		while( !task.IsDone )
			task.WaitForCompletion();

		return task.Result;
	}
}
