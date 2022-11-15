using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDragForMouse : MonoBehaviour
{
    public float distance = 10f;
    public GameObject GameMain;//гл. объекст на котором гл. скрипт
    public float distationX=0, distationY=0;
    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x+distationX, Input.mousePosition.y+ distationY, distance); // переменной записываються координаты мыши по иксу и игрику
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
        transform.position = objPosition;
    }
    private void FixedUpdate()
    {
        OnMouseDrag();
    }
}
