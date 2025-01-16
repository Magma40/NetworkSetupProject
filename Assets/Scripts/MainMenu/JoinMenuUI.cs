using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class JoinMenuUI : MonoBehaviour
{
    [SerializeField] Button hostButton;

    [SerializeField] Button clientButton;

    [SerializeField] GameObject cam;

    

    void Start()
    {
        cam.gameObject.SetActive(true);
        hostButton.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); hide(); });
        clientButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); hide(); });
    }

    void hide()
    {
        hostButton.gameObject.SetActive(false);
        clientButton.gameObject.SetActive(false);
        cam.gameObject.SetActive(false);
    }
}
