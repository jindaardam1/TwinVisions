using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;

    public TextMeshProUGUI almas;

    // Update is called once per frame
    void Update()
    {
        almas.text = " Has recogido " + gameManager.PuntosTotales + "/15 almas";
    }
}
