using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	private HostData[] hostList;
	private bool hasJoined = false;

	private void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnGUI()
	{
		if (!Network.isServer && hasJoined == false)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}
	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
		hasJoined = true;
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined!");
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initialized!");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
