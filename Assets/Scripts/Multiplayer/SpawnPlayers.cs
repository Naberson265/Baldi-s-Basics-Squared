using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject multiPlayerPrefab;
    // All of these are variables in the player script that aren't auto set.
    public StandardDoorScript setOfficeDoor;
    public NavMeshAgent setSweepAgent;
    public NavMeshAgent set1PRAgent;
    public Playtime setPlaytime;
    public Transform set1PRTransform;
    public Slider setStaminaSlider;
    public TMP_Text setStamText;
    public GameObject setJRScreen;
    public GameObject setPtObj;
    public GameObject setnametagUI;
    public SpoopBaldi setBald;
    public Principal setPr;
    public TMP_Text setItemText;
    public RawImage[] setSlots;
    public RectTransform setItemSelect;
    private void Start()
    {
        GameObject newPlayer = PhotonNetwork.Instantiate(multiPlayerPrefab.name, transform.position, Quaternion.identity);
        PlayerController nps = newPlayer.GetComponent<PlayerController>();
        // I'm stupid so this is how all the variables get set.
        nps.officeDoor = setOfficeDoor;
        nps.gottaSweep = setSweepAgent;
        nps.firstPrize = set1PRAgent;
        nps.playtime = setPlaytime;
        nps.firstPrizeTransform = set1PRTransform;
        nps.staminaBar = setStaminaSlider;
        nps.StaminaText = setStamText;
        nps.jumpRopeScreen = setJRScreen;
        nps.playtimeObj = setPtObj;
        nps.nametagUI = setnametagUI;
        nps.spoopBaldiScript = setBald;
        nps.principalScript = setPr;
        nps.itemNameText = setItemText;
        nps.itemSlot[0] = setSlots[0];
        nps.itemSlot[1] = setSlots[1];
        nps.itemSlot[2] = setSlots[2];
        nps.itemSelectBox = setItemSelect;
    }
}
