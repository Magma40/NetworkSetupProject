using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using Unity.Networking.Transport;

public class NetworkManagerLobby : NetworkManager
{
    ////Might need to write the actual name of the scene
    //[Scene][SerializeField] private string menuScene = string.Empty;

    //[Header("Room")]
    //[SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab = null;

    //public static event Action OnClientConnected;
    //public static event Action OnClientDisonnected;

    //public override void StartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    //public override void OnStartClient()
    //{
    //    var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

    //    foreach(var prefab in spawnablePrefabs)
    //    {
    //       ClientScene.RegisterPrefab(prefab);
    //    }
    //}

    //public override void OnClientConnect(NetworkClient conn)
    //{
    //    base.OnClientConnected(conn);
    //    OnClientConnect?.Invoke();
    //}

    //public override void OnClientDisconnect(NetworkConnection conn)
    //{
    //    base.OnClientDisconnect(conn);
    //    OnClientDisconnect?.Invoke();
    //}

    //public override void OnServerConnect(NetworkConnection conn)
    //{
    //    if(numPlayers >= maxConnections)
    //    {
    //        conn.Disconnect();
    //        return;
    //    }

    //    if(SceneManager.GetActiveScene().name != menuScene)
    //    {
    //        conn.Disconnect();
    //        return;
    //    }
    //}

    //public override void OnServerAddPlayer(NetworkConnection conn)
    //{
    //    if(SceneManager.GetActiveScene().name = menuScene) 
    //    {
    //        NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);
    //        NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
    //    }
    //}
}
