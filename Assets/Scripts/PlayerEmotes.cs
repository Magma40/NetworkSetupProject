using UnityEngine;
using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;

public class PlayerEmotes : NetworkBehaviour
{
    public GameObject emoteSelectUIPrefab;
    public GameObject emoteSelectUI;

    [SerializeField] GameObject canvasOBJ;

    [SerializeField] List<GameObject> m_emoteList;
    [SerializeField] Vector3 m_emotePositionOffset;
    public GameObject m_currentSelectedEmote;
    public bool m_usingEmote;

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
        if (m_currentSelectedEmote != null)
        {
            m_currentSelectedEmote.transform.position = transform.position + m_emotePositionOffset;
            m_currentSelectedEmote.transform.rotation = transform.rotation;
        }
        else { m_usingEmote = false; }

        if (Input.GetKey(KeyCode.G))
        {
            emoteSelectUI.gameObject.SetActive(true);
            print("Step1");
            if (m_usingEmote) return;
            print("Step2");
            m_usingEmote = true;
            print("Step3");
            if (IsOwner)
            {
                print("Step4");
                print("Step5");
                if (emoteSelectUI.activeSelf) //This is a joke...
                {
                    print("We are Here!");
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {                    
                        SendEmoteServerRpc("Alpha1");
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        SendEmoteServerRpc("Alpha2");
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        SendEmoteServerRpc("Alpha3");
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        SendEmoteServerRpc("Alpha4");
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        SendEmoteServerRpc("Alpha4");
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        SendEmoteServerRpc("Alpha5");
                    }
                }
            }
        }
        else
        {
            emoteSelectUI.gameObject.SetActive(false);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendEmoteServerRpc(string pressedButton)
    {
        switch(pressedButton)  //The joke continues
        {
            case ("Alpha1"):
                m_currentSelectedEmote = GameObject.Instantiate(m_emoteList[0]);
                m_currentSelectedEmote.transform.localPosition = transform.position + m_emotePositionOffset;
                m_currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
                break;

            case ("Alpha2"):
                m_currentSelectedEmote = GameObject.Instantiate(m_emoteList[1]);
                m_currentSelectedEmote.transform.localPosition = transform.position + m_emotePositionOffset;
                m_currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
                break;

            case ("Alpha3"):
                m_currentSelectedEmote = GameObject.Instantiate(m_emoteList[2]);
                m_currentSelectedEmote.transform.localPosition = transform.position + m_emotePositionOffset;
                m_currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
                break;

            case ("Alpha4"):
                m_currentSelectedEmote = GameObject.Instantiate(m_emoteList[3]);
                m_currentSelectedEmote.transform.localPosition = transform.position + m_emotePositionOffset;
                m_currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
                break;

            case ("Alpha5"):
                m_currentSelectedEmote = GameObject.Instantiate(m_emoteList[4]);
                m_currentSelectedEmote.transform.localPosition = transform.position + m_emotePositionOffset;
                m_currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
                break;
            case ("Alpha6"):
                m_currentSelectedEmote = GameObject.Instantiate(m_emoteList[5]);
                m_currentSelectedEmote.transform.localPosition = transform.position + m_emotePositionOffset;
                m_currentSelectedEmote.GetComponent<NetworkObject>().Spawn();
                break;

            default: break;
        }      
    }
}
