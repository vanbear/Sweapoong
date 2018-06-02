using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PhotonConnect : Photon.PunBehaviour 
{

	public string versionName = "0.99";
	//public string playerName = Random.Range(1000,9999).ToString();

	void Start()
	{
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.playerName = "Player" + Random.Range (1000, 9999).ToString();

	}
	public void ConnectToPhoton()
	{
		PhotonNetwork.ConnectUsingSettings (versionName);
		//PhotonNetwork.playerName = playerName;
		Debug.Log ("Connecting to photon...");

	}


	public override void OnConnectedToMaster()
	{
		//PhotonNetwork.JoinLobby(TypedLobby.Default);
		PhotonNetwork.JoinRandomRoom();
		Debug.Log ("Connected to Master");

	}

	public override void OnJoinedLobby()
	{
		Debug.Log ("joined lobby");
	}

	public override void OnDisconnectedFromPhoton()
	{
		Debug.LogWarning("Disconnected from photon");        
	}

	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
	{
		Debug.Log("No Rooms, Creating a new one");
		// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
		PhotonNetwork.CreateRoom(null, new RoomOptions() { IsVisible = true, MaxPlayers = 2 }, null);

	}

	public override void OnJoinedRoom()
	{
		Debug.Log("Joined a room");
		Debug.Log ("PLAYERNAME: " + PhotonNetwork.playerName);
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer newPLayer)
	{
		Debug.Log ("Second PLayer has joined");
		if (PhotonNetwork.countOfPlayers == 2) 
		{
			PhotonNetwork.LoadLevel ("GameScene");
		}

	}


}
