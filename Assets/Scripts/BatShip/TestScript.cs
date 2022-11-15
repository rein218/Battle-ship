using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject MyMap;

    void OnGUI()
    {
        Rect LocationButton;
        LocationButton = new Rect(new Vector2(10,10),new Vector2(200,40));
        if (GUI.Button(LocationButton, "Сгенерировать палубы")) MyMap.GetComponent<GamePole>().EnterRandomShip();

        LocationButton = new Rect(new Vector2(10, 50), new Vector2(200, 40));
        if (GUI.Button(LocationButton, "Сopy в 2 полe")) MyMap.GetComponent<GamePole>().CopyPole();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
