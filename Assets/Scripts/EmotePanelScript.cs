using UnityEngine;

public class EmotePanelScript : MonoBehaviour
{
    [SerializeField] public Transform m_emotePos;

    private void Update()
    {
        if(m_emotePos == null)
        {
            var emotePosOnPlayer = GameObject.Find("PlayerArmature(Clone)").transform.GetChild(3).gameObject;
            m_emotePos = emotePosOnPlayer.transform;
        }
        else
        {
            this.gameObject.transform.position = m_emotePos.position;
            this.gameObject.transform.rotation = m_emotePos.rotation;
        }
    }
}
