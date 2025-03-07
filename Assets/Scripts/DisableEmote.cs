using Unity.Netcode;
using UnityEngine;

public class DisableEmote : NetworkBehaviour
{
    bool m_turnedOn;
    float m_turnedOnTimer;
    private void OnEnable()
    {
        m_turnedOn = true;
    }
    private void Update()
    {
        if (m_turnedOn)
        {
            m_turnedOnTimer += Time.deltaTime;
            if (m_turnedOnTimer >= 3)
            {
                m_turnedOnTimer = 0;
                RemoveEmoteServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void RemoveEmoteServerRpc()
    {
        this.gameObject.GetComponent<NetworkObject>().Despawn();
    }


}
