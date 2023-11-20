using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PuntosTotales
    {
        get { return _puntosTotales; }
    }
    private int _puntosTotales;

    public void SumarPuntos(int puntosASumar)
    {
        _puntosTotales += puntosASumar;
    }
}
