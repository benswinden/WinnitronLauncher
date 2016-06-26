﻿using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class DataManager : Singleton<DataManager>  {

	public List<Playlist> playlists;
	public List<Song> songs;
	public List<Sprite> attractModeImages;

    PlaylistNavigationManager playlistNavManager;

	// Use this for initialization
	void Awake () {
		playlists = new List<Playlist> ();
		songs = new List<Song> ();
		attractModeImages = new List<Sprite> ();

        playlistNavManager = GameObject.Find("PlaylistNavigationManager").GetComponent<PlaylistNavigationManager>();

		//SyncData ();
		LoadData();
	}

	public void SyncData()
	{
		//GM.runner.RunSync();
		LoadData();
	}

	public void LoadData()
	{
		//GM.Load ();

		//Load everything!
		GetPlaylists ();
		GetAttractModeImages ();
		GetMusic ();
		
        // Build the visuals from this data
        playlistNavManager.BuildPlaylists();

		//Do this when done loading

		GM.state.Change(StateManager.WorldState.Intro);
	}

	// Builds a list of Game objects based on the game directory inside its main directory. Then instantiates the GameNavigationManager, which then instantiates the ScreenShotDisplayManager
	public void GetPlaylists() 
	{
		var playlistDir = new DirectoryInfo(GM.options.playlistsPath);
		
		foreach (var dir in playlistDir.GetDirectories()) 
		{
			//Don't pick any directories that start with a dot
			if(dir.Name.Substring(0, 1) != ".")
			{
				//Add the playlist to the list
				//The Playlist builds the games out from the Playlist constructor
				playlists.Add(new Playlist(dir.FullName));
			}
		}   
	}
	
	public void GetAttractModeImages()
	{
		//Get stuff here
	}

	public void GetMusic()
	{
		//Get stuff here
	}

	public bool UpdatePlaylists()
	{
		//For when there are new games added to the list without shutting the launcher down
		return true;
	}

	public JSONNode LoadJson(string fileLocation)
	{
		string json;

		try
		{
			using (StreamReader r = new StreamReader(fileLocation))
				return JSONNode.Parse(r.ReadToEnd());
		} 

		catch
		{
			Debug.LogError ("DATA MANAGER Failed to load JSON at " + fileLocation);
			return null;
		}
	}
}
