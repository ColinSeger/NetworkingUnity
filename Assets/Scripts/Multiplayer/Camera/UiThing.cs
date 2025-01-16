using Unity.Netcode;
using UnityEngine;

public class UiThing : MonoBehaviour
{
    public void StartHost(){
        NetworkManager.Singleton.StartHost();
        this.gameObject.SetActive(false);
    }
    public void StartClient(){
        NetworkManager.Singleton.StartClient();
        this.gameObject.SetActive(false);
    }
}
