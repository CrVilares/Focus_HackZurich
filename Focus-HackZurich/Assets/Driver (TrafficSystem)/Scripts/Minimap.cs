using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour
{

    public GameObject playerIcon;
    public Transform miniMapCamera;

    private Transform player;
    private GameObject InstantiatePlayerIcon;

    public GameObject gamePanels, mapUI, mapCamera;

    public void ShowMap(bool show)
    {
        gamePanels.SetActive(!show);
        mapUI.SetActive(show);
        mapCamera.SetActive(show);
    }


    void Start()
    {
        player = AIContoller.manager.playerCamera.transform;
        InstantiatePlayerIcon = Instantiate(playerIcon, new Vector3(player.transform.position.x, -250.5f, player.transform.position.z), Quaternion.Euler(90, player.eulerAngles.y, 0)) as GameObject;
    }


    void Update()
    {

        if (!player) return;

        InstantiatePlayerIcon.transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
        InstantiatePlayerIcon.transform.position = new Vector3(player.transform.position.x, -250.5f, player.transform.position.z);

        miniMapCamera.position = new Vector3(player.position.x, -250, player.position.z);
        miniMapCamera.rotation = Quaternion.Euler(0, player.eulerAngles.y+180, 0);

    }

}
