using UnityEngine;
using Unity.Netcode;

public class CheckpointTrigger : NetworkBehaviour
{

    public bool m_hostFirst;
    public bool m_clientFirst;
    public bool m_isfinishLine;

    RaceManager m_raceManager;
    public GameObject m_nextCheckPoint; //This is terrible

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_hostFirst || m_clientFirst) return;
        NetworkObject networkObject = other.GetComponent<NetworkObject>();
        Player player = other.GetComponent<Player>();
        if (player.IsClient || player.IsHost && networkObject != null && networkObject.IsOwner)
        {
            print("WeDOINGTHIS");
            ChangeCheckpointServerRpc(networkObject.OwnerClientId);
        }
        else
        {
            print("CantDoThis");
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void ChangeCheckpointServerRpc(ulong playerId)
    {
        // Simple team system: blue for even, red for odd
        if (playerId % 2 == 0){ m_hostFirst = true;  } else { m_clientFirst = true; }
        if (m_isfinishLine)
        {
            m_raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
            m_raceManager.PlayerWhoWon();
        }
        else
        {
            m_nextCheckPoint.SetActive(true);
        }
        this.gameObject.SetActive(false);
    }
}
