using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPole : MonoBehaviour
{
    public GameObject WhoParent = null;
    public int CoorX, CoorY;

    //этот скрип передает координаты объекта на который нажали мышью
    private void OnMouseDown()
    {
        if (WhoParent != null)
        {
            WhoParent.GetComponent<GamePole>().WhoClick(CoorX, CoorY);
        }
    }
}
