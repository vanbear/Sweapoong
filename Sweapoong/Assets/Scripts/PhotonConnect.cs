using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonConnect : Photon.PunBehaviour 
{

	public string versionName = "0.99";

	public void ConnectToPhoton()
	{
		PhotonNetwork.ConnectUsingSettings (versionName);
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
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("Joined a room");
	}
}
