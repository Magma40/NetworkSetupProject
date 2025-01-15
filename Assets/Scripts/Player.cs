using UnityEngine;
using Unity.Netcode;
using StarterAssets;
using Cinemachine;

public class Player : NetworkBehaviour
{
    public GameObject m_playerCamera;
    public GameObject m_cameraPrefab;

    public GameObject playerFollowCamera;
    public GameObject playerFollowCameraPrefab;

    ThirdPersonController m_thirdPersonController;

    private void WitnessMe()
    {
        if (!IsOwner) { return; }
        // this object has the player camera
        //_pCam = FindObjectOfType<PlayerCamera>();
        m_thirdPersonController = GetComponent<ThirdPersonController>();
        if (m_playerCamera == null)
        {
            m_playerCamera = Instantiate(m_cameraPrefab, this.transform);
            playerFollowCamera = Instantiate(playerFollowCameraPrefab, this.transform);
            m_thirdPersonController._mainCamera = m_playerCamera;
            m_playerCamera.transform.parent = this.transform;
            print("SpawnedCamera");
        }

        // and the player camera now targets this object's transform
        //m_playerCamera.GetComponent<CinemachineBrain>().IsLive
    }

    private void RemoveCamera()
    {
        if (m_playerCamera != null)
        {
            playerFollowCamera = null;
            m_playerCamera = null;
        }

    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            WitnessMe(); // this object shall now be witnessed.
        }
       
        Debug.Log("We have connected and spawned");
    }

    public override void OnGainedOwnership()
    {
        WitnessMe(); // we shall witness this upon gaining ownership of this.
    }

    public override void OnNetworkDespawn()
    {
        RemoveCamera(); // no longer witnessed upon despawning
    }

    public override void OnLostOwnership()
    {
        RemoveCamera(); // no longer witnessed if we no longer own it
    }
}
