using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alma : MonoBehaviour
{
    public const int ValorAlma = 1;
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.SumarPuntos(ValorAlma);
            Destroy(gameObject);
        }
    }
}
