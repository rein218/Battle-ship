using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chanks : MonoBehaviour
{
    /*вкраце, этот скрип меняет картинку в зависимости  от значения индекса обекта, который можно изменять*/
    public Sprite[] imgs; //ссылка на объект
    public int Index = 0; //индекс объекта
    public bool HideChank = false; //будем прятать чужое поле

    void ChangeImgs()
    {
        /*изменяет картинку объекта, если индекс не превышает кол-во используемых картинок одного объекта*/
        if (imgs.Length> Index)
        {
            //если поле нужно спрятать и индексм единица, то скрываем клетку
            if (HideChank && Index == 1) GetComponent<SpriteRenderer>().sprite = imgs[0];
            else  GetComponent<SpriteRenderer>().sprite = imgs[Index]; //если нет, то все как обычно
        }
    }
    void Start()
    { 
        
    }

    void Update()
    {
        ChangeImgs();
    }
}
