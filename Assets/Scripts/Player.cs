using UnityEngine;
using Unity.Netcode;
using StarterAssets;
using Cinemachine;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.tvOS;
using System.Collections;
using UnityEditor;

public class Player : NetworkBehaviour
{
    public GameObject m_playerCamera;
    public GameObject m_cameraPrefab;

    public GameObject playerFollowCamera;
    public GameObject playerFollowCameraPrefab;

    ThirdPersonController m_thirdPersonController;

    private void WitnessMe()
    {
        m_thirdPersonController = GetComponent<ThirdPersonController>();
        if (m_playerCamera == null)
        {
            m_playerCamera = Instantiate(m_cameraPrefab, this.transform);
            playerFollowCamera = Instantiate(playerFollowCameraPrefab, this.transform);
            m_thirdPersonController._mainCamera = m_playerCamera;
            m_playerCamera.transform.parent = this.transform;
            print("SpawnedPlayerCamera");
        }
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
        base.OnNetworkSpawn();
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
