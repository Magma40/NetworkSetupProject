using UnityEngine;
using Unity.Netcode;
using UnityEditor.PackageManager;


public class CheckpointTrigger : NetworkBehaviour
{

    public NetworkVariable<Color> m_NetworkColor = new NetworkVariable<Color>(Color.white);
    private Material m_InstanceMaterial;

    public bool m_hostFirst;
    public bool m_clientFirst;
    public bool m_isfinishLine;

    RaceManager m_raceManager;
    public GameObject m_nextCheckPoint; //This is terrible

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        m_NetworkColor.OnValueChanged += OnColorChanged;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            m_InstanceMaterial = new Material(meshRenderer.material);
            meshRenderer.material = m_InstanceMaterial;
            UpdateMaterialColor(m_NetworkColor.Value);
        }
    }

    public override void OnNetworkDespawn()
    {
        m_NetworkColor.OnValueChanged -= OnColorChanged;
    }
    private void OnColorChanged(Color oldColor, Color newColor)
    {
        UpdateMaterialColor(newColor);
    }
    private void UpdateMaterialColor(Color newColor)
    {
        if (m_InstanceMaterial != null)
        {
            m_InstanceMaterial.SetColor("_BaseColor", newColor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_hostFirst || m_clientFirst) return;
        NetworkObject networkObject = other.GetComponent<NetworkObject>();
        Player player = other.GetComponent<Player>();
        if (player.IsClient || player.IsHost && networkObject != null && networkObject.IsOwner)
        {
            print("WeDOINGTHIS");
            ChangeColorServerRpc(networkObject.OwnerClientId);
        }
        else
        {
            print("CantDoThis");
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void ChangeColorServerRpc(ulong playerId)
    {
        // Simple team system: blue for even, red for odd
        Color newColor = (playerId % 2 == 0) ? new Color(0, 0, 1, 0.5f) : new Color(1, 0, 0, 0.5f);
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
        m_NetworkColor.Value = newColor;
        this.gameObject.SetActive(false);
    }
}
