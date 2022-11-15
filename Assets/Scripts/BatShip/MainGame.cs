using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    
    //режим игры
    public int GameMode = 0, ChooseShip = 0;
    public GameObject PlayerPole, ComputerPole, Player, 
        dragObject1, dragObject2, dragObject3, dragObject4;
    public Text OpisHC, OpisHP;
    public float distance = 10f;
    int starting = 0;
    int ShootCDTime, DeltaTime = 0;
    bool whoseMove = true; //определяет кто ходит тру- игрок
    void OnGUI()
    {
        float CentrScreenX = Screen.width  / 2;
        float CentrScreenY = Screen.height / 2;
        Rect LocationButton;
        //получаем игровое поле 
        GamePole PlayerPoleControl = PlayerPole.GetComponent<GamePole>();
        
        Camera cam;
        
        switch (GameMode)
        {
            case 0:
                dragObject2.GetComponent<ObjectDragForMouse>().distationX = 26.8f;
                dragObject3.GetComponent<ObjectDragForMouse>().distationX = 53.6f;
                dragObject4.GetComponent<ObjectDragForMouse>().distationX = 80.4f;
                //Выход
                //получаем компонент камера
                cam = GetComponent<Camera>();
                //задаем дальность обзора камеры
                cam.orthographicSize = 6.5f;
                //задаем коордикаты для камеры
                this.transform.position = new Vector3(0,0,-10);

                //создаем прямоугольник для задного фонаэ
                LocationButton = new Rect(new Vector2(CentrScreenX-150,CentrScreenY-50),new Vector2(300,200));
                GUI.Box(LocationButton, "");

                //Добавляем надпись что это меню игры
                LocationButton = new Rect(new Vector2(CentrScreenX-100 , CentrScreenY - 40), new Vector2(200, 30));
                GUI.Box(LocationButton, "MENU GAME");

                //Добавляем кнопку старта игры
                LocationButton = new Rect(new Vector2(CentrScreenX - 100, CentrScreenY ), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "START")) GameMode = 1; //Если игрок надал кнопку то режим меняется на 1
                //Кнопка выхода
                LocationButton = new Rect(new Vector2(CentrScreenX - 100, CentrScreenY +40), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "EXIT")) Application.Quit();
                break;
            case 1:
                //Подготовка

                if (starting == 0)
                {
                    activateObjects();
                    starting++;
                }
                cam = GetComponent<Camera>();
                cam.orthographicSize = 9;
                this.transform.position = new Vector3(-50, 0, -10);
                LocationButton = new Rect(new Vector2(CentrScreenX - 120, 0), new Vector2(240 , 160));
                GUI.Box(LocationButton, "");

                LocationButton = new Rect(new Vector2(CentrScreenX - 100, 0), new Vector2(200,30));
                GUI.Box(LocationButton, "ДОК");

                LocationButton = new Rect(new Vector2(CentrScreenX - 100, 40), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "BACK TO MENU"))
                {
                    
                    PlayerPoleControl.Clearpole();
                    GameMode = 0;
                }
                LocationButton = new Rect(new Vector2(CentrScreenX - 100, 80), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "RANDOM"))
                    PlayerPoleControl.EnterRandomShip();

                if (PlayerPoleControl.LifeShip()==20)
                {
                    LocationButton = new Rect(new Vector2(CentrScreenX - 100, 120), new Vector2(200, 30));
                    if (GUI.Button(LocationButton, "TO BATTLE"))
                    {
                        GameMode = 3;
                        PlayerPole.GetComponent<GamePole>().CopyPole();
                        ComputerPole.GetComponent<GamePole>().EnterRandomShip();
                    }
                }

                LocationButton = new Rect(new Vector2(CentrScreenX - 410, CentrScreenY - 70), new Vector2(140, 280));
                GUI.Box(LocationButton, "");
                LocationButton = new Rect(new Vector2(CentrScreenX - 400, CentrScreenY - 60), new Vector2(120, 60));
                if (GUI.Button(LocationButton, "CANCEL CHOOSE"))
                {
                    ChooseShip = 0;
                    dragObject1.GetComponent<ObjectDragForMouse>().distance = 10;
                    dragObject2.GetComponent<ObjectDragForMouse>().distance = 10;
                    dragObject3.GetComponent<ObjectDragForMouse>().distance = 10;
                    dragObject4.GetComponent<ObjectDragForMouse>().distance = 10;
                }
                    
                LocationButton = new Rect(new Vector2(CentrScreenX - 400, CentrScreenY + 10), new Vector2(120, 40));
                if (GUI.Button(LocationButton, "1"))
                {
                    ChooseShip = 1;
                    dragObject1.GetComponent<ObjectDragForMouse>().distance = 8;
                    dragObject2.GetComponent<ObjectDragForMouse>().distance = 10;
                    dragObject3.GetComponent<ObjectDragForMouse>().distance = 10;
                    dragObject4.GetComponent<ObjectDragForMouse>().distance = 10;

                }
                    
                LocationButton = new Rect(new Vector2(CentrScreenX - 400, CentrScreenY + 60), new Vector2(120, 40));
                if (GUI.Button(LocationButton, "2"))
                {
                    ChooseShip = 2;
                    dragObject1.GetComponent<ObjectDragForMouse>().distance = 8;
                    dragObject2.GetComponent<ObjectDragForMouse>().distance = 8;
                    dragObject3.GetComponent<ObjectDragForMouse>().distance = 10;
                    dragObject4.GetComponent<ObjectDragForMouse>().distance = 10;
                }
                    
                LocationButton = new Rect(new Vector2(CentrScreenX - 400, CentrScreenY + 110), new Vector2(120, 40));
                if (GUI.Button(LocationButton, "3"))
                {
                    ChooseShip = 3;
                    dragObject1.GetComponent<ObjectDragForMouse>().distance = 8;
                    dragObject2.GetComponent<ObjectDragForMouse>().distance = 8;
                    dragObject3.GetComponent<ObjectDragForMouse>().distance = 8;
                    dragObject4.GetComponent<ObjectDragForMouse>().distance = 10;
                }
                    
                LocationButton = new Rect(new Vector2(CentrScreenX - 400, CentrScreenY + 160), new Vector2(120, 40));
                if (GUI.Button(LocationButton, "4"))
                {
                    ChooseShip = 4;
                    dragObject1.GetComponent<ObjectDragForMouse>().distance = 8;
                    dragObject2.GetComponent<ObjectDragForMouse>().distance = 8;
                    dragObject3.GetComponent<ObjectDragForMouse>().distance = 8;
                    dragObject4.GetComponent<ObjectDragForMouse>().distance = 8;
                }
                    

                LocationButton = new Rect(new Vector2(CentrScreenX + 170, CentrScreenY -70), new Vector2(200, 250));
                GUI.Box(LocationButton, "");
                //Кнопка очистки поля
                LocationButton = new Rect(new Vector2(CentrScreenX + 180, CentrScreenY - 60), new Vector2(180, 120));
                if (GUI.Button(LocationButton, "CLEAR FIELD"))
                    PlayerPole.GetComponent<GamePole>().Clearpole();
                LocationButton = new Rect(new Vector2(CentrScreenX + 180, CentrScreenY + 70), new Vector2(180, 100));
                GUI.Box(LocationButton, "PRESS RMB TO " + System.Environment.NewLine+ 
                    "ROTATRE SHIP,"+ System.Environment.NewLine);

                break;
            case 3:
                //Битва
                dragObject1.SetActive(false);
                dragObject2.SetActive(false);
                dragObject3.SetActive(false);
                dragObject4.SetActive(false);
                dragObject1.GetComponent<ObjectDragForMouse>().distance = 10;
                dragObject2.GetComponent<ObjectDragForMouse>().distance = 10;
                dragObject3.GetComponent<ObjectDragForMouse>().distance = 10;
                dragObject4.GetComponent<ObjectDragForMouse>().distance = 10;
                starting = 0;
                this.transform.position = new Vector3(0, -25.5f, -10);
                cam = GetComponent<Camera>();
                cam.orthographicSize = 11;
                OpisHC.text = "OPPONENT'S MOVES:";
                OpisHP.text = "YOURS MOVES:";
                
                break;
            case 4:
                ComputerPole.GetComponent<GamePole>().TextSituation.text = "";
                this.transform.position = new Vector3(25, 0, -10);
                cam = GetComponent<Camera>();
                cam.orthographicSize = 6.5f;
                LocationButton = new Rect(new Vector2(CentrScreenX - 100, CentrScreenY - 10), new Vector2(200, 30));
                GUI.Box(LocationButton, "YOU WIN!");
                LocationButton = new Rect(new Vector2(CentrScreenX - 100, CentrScreenY + 20), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "BACK TO MENU"))
                {
                    PlayerPoleControl.Clearpole();
                    GameMode = 0;
                }
                OpisHC.text = "";
                OpisHP.text = "";
                break;
            case 5:
                ComputerPole.GetComponent<GamePole>().TextSituation.text = "";
                this.transform.position = new Vector3(50, 0, -10);
                cam = GetComponent<Camera>();
                cam.orthographicSize = 6.5f;
                LocationButton = new Rect(new Vector2(CentrScreenX - 100, CentrScreenY - 10), new Vector2(200, 30));
                GUI.Box(LocationButton, "YOU LOSE!");

                LocationButton = new Rect(new Vector2(CentrScreenX - 100, CentrScreenY + 20), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "BACK TO MENU"))
                {
                    PlayerPoleControl.Clearpole();
                    GameMode = 0;
                }
                OpisHC.text = "";
                OpisHP.text = "";
                break;
                
        }
    }
    int ShootCount = 0;
    
    void activateObjects()
    {
        dragObject1.GetComponent<ObjectDragForMouse>().distance = 10;
        dragObject2.GetComponent<ObjectDragForMouse>().distance = 10;
        dragObject3.GetComponent<ObjectDragForMouse>().distance = 10;
        dragObject4.GetComponent<ObjectDragForMouse>().distance = 10;
        dragObject1.SetActive(true);
        dragObject2.SetActive(true);
        dragObject3.SetActive(true);
        dragObject4.SetActive(true);
        starting++;
    }
    //ИИ
    void ArtificialIntelligence()
    {
        //проверяем можно ли ходить
        if (!whoseMove)
        {
            ShootCDTime = Random.Range(71,200);
            if (ShootCDTime< DeltaTime)
            {
                //если палуб больше половины стреляем наугад
                int ShotX = Random.Range(0, 9);
                int ShotY = Random.Range(0, 9);
               // Random rnd = new Random();
                int PC_Ship = ComputerPole.GetComponent<GamePole>().LifeShip();
                if (PC_Ship < 10)
                {
                    if (ShootCount == 0)
                    {
                        //стреляем по палубе 
                        GamePole.TestCoor XY = Homming();
                        if (XY.X >= 0 && XY.Y >= 0)
                        {
                            ShotX = XY.X;
                            ShotY = XY.Y;
                        }
                        ShootCount++;
                    }
                    else
                    {
                        ShootCount = 0;
                    }
                }
                if (Player.GetComponent<GamePole>().GetindexBlock(ShotX, ShotY) > 2)
                {
                    ArtificialIntelligence();
                }
                whoseMove = !Player.GetComponent<GamePole>().Shoot(ShotX, ShotY);
                DeltaTime = 0;
            }
            


        }
    }
    //добивание
    GamePole.TestCoor Homming()
    {
        GamePole.TestCoor XY;
        XY.X = -1;
        XY.Y = -1;
        //перебираем корабли и смотрим в каком из них есть палубы
        foreach (GamePole.Ship Test in Player.GetComponent<GamePole>().ListShip)
        {
            //перебираем палубы и проверяем попали ли мы в нее
            foreach (GamePole.TestCoor Paluba in Test.ShipCoord)
            {
                //смотрим какой номер у палубы
                int Index = Player.GetComponent<GamePole>().GetindexBlock(Paluba.X, Paluba.Y);
                if (Index == 1)
                {
                    return Paluba;
                }
            }
        }
        return XY;
    }

    void TestWhoWin()
    {
        //проверяем сколько палуб сталось у пк
        int PC_Ship = ComputerPole.GetComponent<GamePole>().LifeShip();
        int Player_Ship = Player.GetComponent<GamePole>().LifeShip();

        //луз ПК
        if (PC_Ship == 0)
        {
            GameMode = 4;
        }
        //Луз игрока
        if (Player_Ship == 0)
        {
            GameMode = 5;
        }
    }
    public void UserClick(int X,int Y)
    {
        if (whoseMove && ComputerPole.GetComponent<GamePole>().GetindexBlock(X, Y)< 2)
        {
            //ходит игрок
            //если он попал то функция вернет тру и ход останется за ним
            //а если промах то ход передается пк

            whoseMove = ComputerPole.GetComponent<GamePole>().Shoot(X, Y);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPole.GetComponent<GamePole>().ShipEnded();
        if (!whoseMove)
        {
            DeltaTime++;
        }
        if (GameMode==3)
        {
            TestWhoWin();
            ArtificialIntelligence();
        }
        
    }
}
