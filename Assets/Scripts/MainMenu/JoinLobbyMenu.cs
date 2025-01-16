using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    //[SerializeField] private NetworkManager networkManager = null;

    //[Header("UI")]
    //[SerializeField] private GameObject landingPagePanel = null;
    //[SerializeField] private TMP_InputField ipAddressInputField = null;
    //[SerializeField] private Button joinButton = null;

    //private void OnEnable()
    //{
    //    NetworkManagerLobby.OnClientConnected += HandleClientConnected;
    //    NetworkManager.DisconnectClient += HandleClientDisconnected;
    //}
    //private void OnDisable()
    //{
    //    NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
    //    NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
    //}

    //public void JoinLobby()
    //{
    //    string ipAdress = ipAddressInputField.text;

    //    networkManager.networkAdress = ipAdress;
    //    networkManager.StartClient();

    //    joinButton.interactable = false;
    //}

    //public void HandleClientConnected()
    //{
    //    joinButton.interactable = true;

    //    gameObject.SetActive(false);
    //    landingPagePanel.SetActive(false);
    //}

    //private void HandleClientDisconnected()
    //{
    //    joinButton.interactable = true;
    //}
}
