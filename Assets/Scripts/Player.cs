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

    public GameObject emoteSelectUIPrefab;
    public GameObject emoteSelectUI;

    [SerializeField] GameObject canvasOBJ;

    [SerializeField] List<GameObject> m_emoteList;
    private Vector3 m_emotePosition;
    [SerializeField] Vector3 m_emotePositionOffset;

    private GameObject currentSelectedEmote;

    private void OnEnable()
    {
        if (emoteSelectUI == null)
        {
            canvasOBJ = GameObject.Find("Canvas");
            GameObject emoteSelectUIOBJ = Instantiate(emoteSelectUIPrefab, canvasOBJ.transform) as GameObject;
            emoteSelectUI = emoteSelectUIOBJ;
            emoteSelectUI.SetActive(false);
        }
    }

    private void Update()
    {
        if(currentSelectedEmote != null)
        {
            currentSelectedEmote.transform.position = transform.position + m_emotePositionOffset;
            currentSelectedEmote.transform.rotation = transform.rotation;
        }
        //if (IsOwner) { return;}
        if (Input.GetKey(KeyCode.G))
        {
            emoteSelectUI.gameObject.SetActive(true);
        }
        else
        {
            emoteSelectUI.gameObject.SetActive(false);
        }

        if (emoteSelectUI.activeSelf ) //This is a joke...
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentSelectedEmote = GameObject.Instantiate(m_emoteList[0]);
                currentSelectedEmote.transform.localPosition = m_emotePosition;
                currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentSelectedEmote = GameObject.Instantiate(m_emoteList[1]);
                currentSelectedEmote.transform.localPosition = m_emotePosition;
                currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentSelectedEmote = GameObject.Instantiate(m_emoteList[2]);
                currentSelectedEmote.transform.localPosition = m_emotePosition;
                currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentSelectedEmote = GameObject.Instantiate(m_emoteList[3]);
                currentSelectedEmote.transform.localPosition = m_emotePosition;
                currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                currentSelectedEmote = GameObject.Instantiate(m_emoteList[4]);
                currentSelectedEmote.transform.localPosition = m_emotePosition;
                currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                currentSelectedEmote = GameObject.Instantiate(m_emoteList[5]);
                currentSelectedEmote.transform.localPosition = m_emotePosition;
                currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
            }
        }
    }

    private void WitnessMe()
    {
        if (!IsOwner) { return; }

        m_thirdPersonController = GetComponent<ThirdPersonController>();
        if (m_playerCamera == null)
        {
            m_playerCamera = Instantiate(m_cameraPrefab, this.transform);
            playerFollowCamera = Instantiate(playerFollowCameraPrefab, this.transform);
            m_thirdPersonController._mainCamera = m_playerCamera;
            m_playerCamera.transform.parent = this.transform;
            print("SpawnedCamera");
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
