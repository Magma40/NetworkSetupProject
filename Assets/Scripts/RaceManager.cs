using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;


public class RaceManager : NetworkBehaviour
{
    [SerializeField] List<GameObject> m_checkPointList;
    [SerializeField] List<Transform> m_checkPointTransforms;
    public GameObject m_checkPointOBJ;
    public GameObject player1WinsOBJ;
    public GameObject player2WinsOBJ;
    public GameObject m_currentGameWinnerOBJ;
    int player1Count;
    int player2Count;

    bool ActiveRace;
    Transform m_CheckpointSpawnLocation;

    private void Start()
    {
        m_CheckpointSpawnLocation = GameObject.Find("CheckPointsPanel").transform;
    }

    private void OnTriggerEnter(Collider other)
    {   if (ActiveRace) return;
        else
        {
            if (m_currentGameWinnerOBJ != null) DeSpawnPlayerWinServerRpc();
            RemoveAllCheckPointsServerRpc();
            SpawnCheckPointServerRpc();        
        }
    }

    public void PlayerWhoWon()
    {
        foreach(GameObject checkPoint in m_checkPointList)
        {
            checkPoint.SetActive(true);
            CheckpointTrigger checkpointScript = checkPoint.GetComponent<CheckpointTrigger>();
            if(checkpointScript.m_hostFirst) player1Count++;
            else if(checkpointScript.m_clientFirst) player2Count++;
            checkPoint.SetActive(false);
        }
        CurrentWinCalculateServerRpc();
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void CurrentWinCalculateServerRpc()
    {
        ActiveRace = false;
        if (player1Count > player2Count) SpawnPlayer1WinServerRpc();
        else SpawnPlayer2WinServerRpc();

        this.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayer1WinServerRpc()
    {
        m_currentGameWinnerOBJ = GameObject.Instantiate(player1WinsOBJ);
        m_currentGameWinnerOBJ.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayer2WinServerRpc()
    {
        m_currentGameWinnerOBJ = GameObject.Instantiate(player2WinsOBJ);
        m_currentGameWinnerOBJ.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeSpawnPlayerWinServerRpc()
    {
        m_currentGameWinnerOBJ.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnCheckPointServerRpc()
    {
        print("WeAREHERe");
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        ActiveRace = true;
        int currentCheckpointSpawning = 0;
        foreach (Transform obj in m_checkPointTransforms)
        {
            GameObject nextCheckpoint = GameObject.Instantiate(m_checkPointOBJ);
            nextCheckpoint.transform.localPosition = m_checkPointTransforms[currentCheckpointSpawning].localPosition;
            nextCheckpoint.transform.localRotation = m_checkPointTransforms[currentCheckpointSpawning].localRotation;
            nextCheckpoint.GetComponent<NetworkObject>().Spawn();
            m_checkPointList.Add(nextCheckpoint);
            currentCheckpointSpawning++;
        }
        int currentSettingOBJ = 1;
        foreach(GameObject gameObject in m_checkPointList)
        {
            if(currentSettingOBJ == m_checkPointList.Count)
            {
                gameObject.GetComponent<CheckpointTrigger>().m_nextCheckPoint = gameObject;            
                gameObject.GetComponent<CheckpointTrigger>().m_isfinishLine = true;
            }
            else
            {
                gameObject.GetComponent<CheckpointTrigger>().m_nextCheckPoint = m_checkPointList[currentSettingOBJ].gameObject;
            }
            gameObject.SetActive(false);
            currentSettingOBJ++;
        }
        m_checkPointList[0].SetActive(true);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RemoveAllCheckPointsServerRpc()
    {
        foreach (GameObject obj in m_checkPointList)
        {
            obj.GetComponent<NetworkObject>().Despawn();
            m_checkPointList.Remove(obj);
        }
    }
}
