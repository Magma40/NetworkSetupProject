using UnityEngine;
using Unity.Netcode;
using StarterAssets;
using Cinemachine;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;

public class Player : NetworkBehaviour
{
    public GameObject m_playerCamera;
    public GameObject m_cameraPrefab;

    public GameObject playerFollowCamera;
    public GameObject playerFollowCameraPrefab;

    ThirdPersonController m_thirdPersonController;

    public GameObject emoteSelectUIPrefab;
    public GameObject emoteSelectUI;

    [SerializeField] public List<GameObject> m_emoteList;

    public Transform emotePos;

    [SerializeField] GameObject canvasOBJ;


    private void OnEnable()
    {
    }
    //public void OnEmote(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        emoteSelectUI.gameObject.SetActive(true);
    //    }
    //    else if (context.canceled)
    //    {
    //        emoteSelectUI.gameObject.SetActive(false);
    //    }
    //}

    private  void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
          emoteSelectUI.gameObject.SetActive(true);
        }
        else
        {
            emoteSelectUI.gameObject.SetActive(false);
        }

        if (emoteSelectUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameObject emote3 = Instantiate(m_emoteList[0], emotePos) as GameObject;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GameObject emote3 = Instantiate(m_emoteList[1], emotePos) as GameObject;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                GameObject emote3 = Instantiate(m_emoteList[2], emotePos) as GameObject;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                GameObject emote3 = Instantiate(m_emoteList[3], emotePos) as GameObject;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                GameObject emote3 = Instantiate(m_emoteList[4], emotePos) as GameObject;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                GameObject emote3 = Instantiate(m_emoteList[5], emotePos) as GameObject;
            }
        }

    }
    private void WitnessMe()
    {
        if (!IsOwner) { return; }
        canvasOBJ = GameObject.Find("Canvas");
        emoteSelectUI = Instantiate(emoteSelectUIPrefab, canvasOBJ.transform) as GameObject;

        //emoteSelectUI.transform.localPosition = Vector3.zero;
        //emoteSelectUI.SetActive(false);
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
