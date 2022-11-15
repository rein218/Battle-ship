using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamePole : MonoBehaviour
{
    
    //основной скрипт
    public GameObject ePole, eBucvi, eNumbers;
    public bool HideShip = false;
    public GameObject MapDestination, GameMain, CM, swapDirection, swapDirection1, swapDirection2, swapDirection3;
    public Text TextSituation;
    GameObject[] Numbers;
    GameObject[] Liters;
    public GameObject[,] Pole;
    int Time=70, DeltaTime=0;
    public int DirectionSP=0;
    //задаем длину игрового поля
    int LongPole = 10;

    //Кол-во наших кораблей каждого типа, сколько их еще можно воткнуть.
    public int[] ShipsCount = {0, 4, 3, 2, 1 };

    //копирование поля
    public void CopyPole()
    {
        if (MapDestination != null)
        {
            for (int Y=0;Y<LongPole;Y++)
            {
                for (int X = 0; X < LongPole; X++)
                {
                    //Читаем что записано в нашем поле и записываем данные в другое поле
                    MapDestination.GetComponent<GamePole>().Pole[X, Y].GetComponent<Chanks>().Index = Pole[X, Y].GetComponent<Chanks>().Index;
                }
            }
            //Чистка списка от мусора
            MapDestination.GetComponent<GamePole>().ListShip.Clear();
            //записываем сгенерированные корабли
            MapDestination.GetComponent<GamePole>().ListShip.AddRange(ListShip);

        }
    }
    //проверяет остаток кораблей разных типов перед их установкой на поле
    //есть ли еще палубы в массиве  ShipsCount?
    bool CountShip()
    { 
        //переменная для подсчета кораблей
        int shipsLeft=0;
        //суммируем все значения
        foreach (int Ship in ShipsCount)
        {
            shipsLeft += Ship;
        }
        //если сумма не ноль, значит можно ставить дальше
        if (shipsLeft!=0)
        {
            return true;
        }
        //иначе нет

        return false;
    }

    public void ShipEnded()
    {
        if ((ShipsCount[1] == 0) && (CM.GetComponent<MainGame>().ChooseShip==1))
        {
            swapDirection.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection1.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection2.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection3.GetComponent<ObjectDragForMouse>().distance = 10;
        }
        if (ShipsCount[2] == 0 && (CM.GetComponent<MainGame>().ChooseShip == 2))
        {
            swapDirection.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection1.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection2.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection3.GetComponent<ObjectDragForMouse>().distance = 10;
        }
        if (ShipsCount[3] == 0&& (CM.GetComponent<MainGame>().ChooseShip == 3))
        {
            swapDirection.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection1.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection2.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection3.GetComponent<ObjectDragForMouse>().distance = 10;
        }
        if (ShipsCount[4] == 0  && (CM.GetComponent<MainGame>().ChooseShip == 4))
        {
            swapDirection.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection1.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection2.GetComponent<ObjectDragForMouse>().distance = 10;
            swapDirection3.GetComponent<ObjectDragForMouse>().distance = 10;
        }
    }

    //функция очищения поля
    public void Clearpole()
    {
        ShipsCount = new int[] {0, 4, 3, 2, 1 };
        ListShip.Clear();
        for (int Y =0; Y<LongPole;Y++)
        {
            for(int X = 0; X < LongPole; X++)
            {
                Pole[X, Y].GetComponent<Chanks>().Index = 0;
            }
        }
    }
    //расстановка кораблей игроком
    public void EnterPlayerShip(int X, int Y)
    {
        if (CM!=null)
        {
            int SelectShip = CM.GetComponent<MainGame>().ChooseShip;
            
            TestCoor[] P = TestEnterShip(SelectShip, DirectionSP, X, Y);
            if (P != null)
            {
                if (ShipsCount[SelectShip] > 0)
                {
                    EnterDesk(SelectShip, DirectionSP, X, Y);
                    ShipsCount[SelectShip]--;
                }
            }
        }
    }

    //случайная расстановка кораблей 
    public void EnterRandomShip()
    {
        Clearpole();
        //тип коробля: 0 - одно палубый, 3 - 4рех плубный
        int SelectShip = 4;

        //координаты по которым будет установлен корабль
        int X, Y;
        
        //положение корабля вертикаль или горизонталь
        int Direction;
        while (CountShip())
        {
            //генерирум координаты для постановки корабля
            X = Random.Range(0, 10);
            Y = Random.Range(0, 10);
            Direction = Random.Range(0, 2);
            if (EnterDesk(SelectShip, Direction, X, Y))
            {
               ShipsCount[SelectShip]--;
                if (ShipsCount[SelectShip]==0)
                {
                    SelectShip--;
                }
            }
        }
    }

    public struct TestCoor
    {
        public int X, Y;
    }

    public struct Ship
    {
        public TestCoor[] ShipCoord;
    }

    //Я знаю что это (список наших кораблей)
    public List<Ship> ListShip = new List<Ship>();
    
    void CreatePole()
    {
        //создает вокруг поля буквы и цифры 
        Vector3 StartPose = transform.position;
        float XX = StartPose.x+1;
        float YY = StartPose.y-1;
        Liters = new GameObject[LongPole];
        Numbers= new GameObject[LongPole];
        for (int i = 0; i< LongPole;i++)
        {
            Liters[i] = Instantiate(eBucvi);                       
            Liters[i].transform.position = new Vector3(XX, StartPose.y, StartPose.z);
            Liters[i].GetComponent<Chanks>().Index = i;
            XX++;
            Numbers[i] = Instantiate(eNumbers);
            Numbers[i].transform.position = new Vector3(StartPose.x, YY, StartPose.z);
            Numbers[i].GetComponent<Chanks>().Index = i;
            YY--;
        }
        XX = StartPose.x + 1;
        YY = StartPose.y - 1;
        //создает на поле клетки и задает им координаты
        //индекс 0 значит, что это пустая клетка
        //индекс 1 - клетка с кораблем
        Pole = new GameObject[LongPole, LongPole];
        for (int Y=0;Y<LongPole;Y++)
        {
            for (int X = 0; X < LongPole; X++)
            {
                Pole[X, Y] = Instantiate(ePole);
                Pole[X, Y].GetComponent<Chanks>().Index = 0;
                Pole[X, Y].GetComponent<Chanks>().HideChank = HideShip;

                Pole[X, Y].transform.position = new Vector3(XX,YY,StartPose.z);
                if (HideShip) Pole[X, Y].GetComponent<ClickPole>().WhoParent= this.gameObject; 
                if (CM != null) Pole[X, Y].GetComponent<ClickPole>().WhoParent = this.gameObject;

                Pole[X, Y].GetComponent<ClickPole>().CoorX = X;
                Pole[X, Y].GetComponent<ClickPole>().CoorY = Y;

                XX++;
            }
            XX = StartPose.x+1;
            YY--;
        }
    }

    bool TestEnterDesk(int X, int Y)
    {
        //жопа боль
        //при постановке корабля проверяет не мешает ли др. судно его мирному сосуществованию
        //между кораблями одна клетка должна быть свободна
        if ((X > -1) && (Y > -1) && (X < 10) && (Y < 10))
        {
            int[] XX = new int[9], YY = new int[9];
            //=================================================//
            //Верх                                             //
              XX[0] = X + 1;   XX[1] = X;       XX[2] = X - 1; //
              YY[0] = Y + 1;   YY[1] = Y + 1;   YY[2] = Y + 1; //
            //=================================================//
            //Середина                                         //
              XX[3] = X + 1;   XX[4] = X;       XX[5] = X - 1; //
              YY[3] = Y;       YY[4] = Y;       YY[5] = Y;     //
            //=================================================//
            //Низ                                              //
              XX[6] = X + 1;   XX[7] = X;       XX[8] = X - 1; //
              YY[6] = Y-1;     YY[7] = Y-1;     YY[8] = Y-1;   //
            //=================================================//
            //при постановке корабля проверяет не мешает ли что-нибудь его мирному сосуществованию(конец игрового поля)
            for (int i=0;i<9;i++)
            {
                if ( (XX[i] > -1) && (YY[i] > -1) && (XX[i] < 10) && (YY[i] < 10) )
                {
                    if (Pole[XX[i], YY[i]].GetComponent<Chanks>().Index != 0) return false;
                }               
            }
            
            return true;
        }
        return false;
    }
    
    //проверяем установку палуб в опред. направлении
    TestCoor[] TestEnterShipDirect(int ShipType, int XD, int YD, int X, int Y)
    {
        //массив для результата
        TestCoor[] ResultCoord = new TestCoor[ShipType];
        for (int i=0; i<ShipType;i++)
        {
            if (TestEnterDesk(X,Y))
            {
                //запоминаем значение координат
                ResultCoord[i].X = X; 
                ResultCoord[i].Y = Y;
            }
            else
            {
                return null;
            }
            X += XD;
            Y += YD;
        }
        return ResultCoord;
    }

    TestCoor[] TestEnterShip(int ShipType, int Direction, int X, int Y)
    {

        TestCoor[] ResultCoord = new TestCoor[ShipType];
        if (TestEnterDesk(X,Y))
        {
            switch (Direction)
            {
                case 0:
                    //пробуем установить палубы в положительном направлении Х
                    //ShipType - размер корабля потом передается в TestEnterShipDirect
                    ResultCoord = TestEnterShipDirect(ShipType, 1, 0, X, Y);
                    //Если не вышло поставить корабль
                    if (ResultCoord==null && CM == null) ResultCoord = TestEnterShipDirect(ShipType, -1, 0, X, Y);
                    break;
                case 1:
                    //пробуем посавить в положжительном направлении У
                    ResultCoord = TestEnterShipDirect(ShipType, 0,1, X, Y);
                    //если не получилось
                    if (ResultCoord == null && CM == null) ResultCoord = TestEnterShipDirect(ShipType, 0, -1, X, Y);
                    break;

            }
            return ResultCoord;
        }
        return null;
    }
    //если все условия сработали ставит корабль
    //индекс 0 значит, что это пустая клетка
    //индекс 1 - клетка с кораблем
    bool EnterDesk(int ShipType, int Direction, int X, int Y)
    {
        TestCoor[] P = TestEnterShip(ShipType, Direction, X, Y);
        if(P!=null)
        {
            foreach (TestCoor T in P)
            {
                Pole[T.X, T.Y].GetComponent<Chanks>().Index = 1;
            }
            Ship Desk;
            //сохраняем его координаты
            Desk.ShipCoord = P;
            //сохраняем корабль в список
            ListShip.Add(Desk);
            return true;
        }
        return false;
    }
    
    //очевидно 
    void Start()
    {
        
        CreatePole();
        if (HideShip)
        {
            TextSituation.text = "";
            EnterRandomShip();
        }
        
    }

    int swap;
    public int swap2=1;
    float swap3;
    void Update()
    {
        //поворачивает корабль
        if (Input.GetMouseButtonDown(1))
        {
            swap = GetComponent<GamePole>().DirectionSP;
            GetComponent<GamePole>().DirectionSP = swap2;
            swap2 = swap;

            if (swapDirection1!=null)
            {
                swap3 = swapDirection1.GetComponent<ObjectDragForMouse>().distationX;
                swapDirection1.GetComponent<ObjectDragForMouse>().distationX = -swapDirection1.GetComponent<ObjectDragForMouse>().distationY;
                swapDirection1.GetComponent<ObjectDragForMouse>().distationY = -swap3;
                if (swapDirection2 != null)
                {
                    swap3 = swapDirection2.GetComponent<ObjectDragForMouse>().distationX;
                    swapDirection2.GetComponent<ObjectDragForMouse>().distationX = -swapDirection2.GetComponent<ObjectDragForMouse>().distationY;
                    swapDirection2.GetComponent<ObjectDragForMouse>().distationY = -swap3;
                    if (swapDirection3 != null)
                    {
                        swap3 = swapDirection3.GetComponent<ObjectDragForMouse>().distationX;
                        swapDirection3.GetComponent<ObjectDragForMouse>().distationX = -swapDirection3.GetComponent<ObjectDragForMouse>().distationY;
                        swapDirection3.GetComponent<ObjectDragForMouse>().distationY = -swap3;
                    }
                }
            }

        }
        if ((CM!=null)&& CM.GetComponent<MainGame>().GameMode==3)
        {
            swapDirection1.GetComponent<ObjectDragForMouse>().distationX = 26.8f;
            swapDirection2.GetComponent<ObjectDragForMouse>().distationX = 53.6f;
            swapDirection3.GetComponent<ObjectDragForMouse>().distationX = 80.4f;
            swapDirection1.GetComponent<ObjectDragForMouse>().distationY = 0;
            swapDirection2.GetComponent<ObjectDragForMouse>().distationY = 0;
            swapDirection3.GetComponent<ObjectDragForMouse>().distationY = 0;
            GetComponent<GamePole>().DirectionSP = 0;
            swap2 = 1;
        }

        if (GameMain!=null&& GameMain.GetComponent<MainGame>().GameMode > 3)
        {
            TextSituation.text = "";
        }
        if (DeltaTime>Time)
        {
            TextSituation.text = "";
            DeltaTime = 0;
        }
        else if (TextSituation.text != "")
        {
            DeltaTime++;
        }
    }
    public void WhoClick(int X,int Y)
    {
        if (CM!=null&&CM.GetComponent<MainGame>().GameMode == 1)
        {
            EnterPlayerShip(X, Y);
        }
            

        if (GameMain != null)
            GameMain.GetComponent<MainGame>().UserClick(X, Y);
    } 

    public int GetindexBlock(int X,int Y)
    {
        return Pole[X, Y].GetComponent<Chanks>().Index;
    }
    //Выстел по клетке
    public bool Shoot(int X, int Y)
    {
        int PoleSelect = Pole[X, Y].GetComponent<Chanks>().Index;
        bool Result = false;
        switch (PoleSelect)
        {
            //промах
            case 0:
                Pole[X, Y].GetComponent<Chanks>().Index=2;
                Result = false;
                TextSituation.text = "MISS";
                break;
            //попал
            case 1:
                Pole[X, Y].GetComponent<Chanks>().Index = 3;
                Result = true;
                if (TestShoot(X,Y))
                {
                    TextSituation.text = "DESTOYED";
                }
                else
                {
                    TextSituation.text = "SHOT";
                }
                break;                
        }
        return Result;
    }

    //функция проверки попадания по кораблю
    bool TestShoot(int X, int Y)
    {
        bool Result = false;
        //перебираем корабли и смотрим в какой попали
        foreach (Ship Test in ListShip)
        {
            //перебираем палубы корабля и смотри попали ли мы в нее
            foreach (TestCoor Paluba in Test.ShipCoord)
            {
                //сравниваем координаты выстрела с координатами палубы 
                if ((Paluba.X==X) && (Paluba.Y==Y))
                {
                    int CountKills = 0;
                    //если попал по кобралю то сколько палуб у корабля разрушено?
                    foreach (TestCoor KillPaluba in Test.ShipCoord)
                    {
                        //проверяем, что записано в поле по данным координатам
                        int TestBlock = Pole[KillPaluba.X, KillPaluba.Y].GetComponent<Chanks>().Index;
                        //если цифра 3 то подсчитываем ранения
                        if (TestBlock == 3) CountKills++;
                    }
                    //если кол-во палуб равно кол-ву попаданий значит корабль все!
                    if (CountKills == Test.ShipCoord.Length)
                    {
                        deadShip(Paluba.X, Paluba.Y);
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                    }
                    return Result;
                }

            }
        }

        return Result;
    }

    void deadShip(int X, int Y)
    {
        foreach (Ship Test in ListShip)
        {
            foreach (TestCoor Paluba in Test.ShipCoord)
            {
                if ((Paluba.X == X) && (Paluba.Y == Y))
                {
                    foreach (TestCoor KillPaluba in Test.ShipCoord)
                    {
                        Pole[KillPaluba.X, KillPaluba.Y].GetComponent<Chanks>().Index=4;


                        int[] XX = new int[9], YY = new int[9];
                        XX[0] = KillPaluba.X + 1;    XX[1] = KillPaluba.X;       XX[2] = KillPaluba.X - 1;
                        YY[0] = KillPaluba.Y + 1;    YY[1] = KillPaluba.Y + 1;   YY[2] = KillPaluba.Y + 1;

                        XX[3] = KillPaluba.X + 1;    XX[4] = KillPaluba.X;       XX[5] = KillPaluba.X - 1;
                        YY[3] = KillPaluba.Y;        YY[4] = KillPaluba.Y;       YY[5] = KillPaluba.Y;

                        XX[6] = KillPaluba.X + 1;    XX[7] = KillPaluba.X;       XX[8] = KillPaluba.X - 1;
                        YY[6] = KillPaluba.Y - 1;    YY[7] = KillPaluba.Y - 1;   YY[8] = KillPaluba.Y - 1;
                        for (int i = 0; i < 9; i++)
                        {
                            if ((XX[i] > -1) && (YY[i] > -1) && (XX[i] < 10) && (YY[i] < 10))
                            {
                                if (Pole[XX[i], YY[i]].GetComponent<Chanks>().Index < 3)
                                {
                                    Pole[XX[i], YY[i]].GetComponent<Chanks>().Index = 2;
                                }
                            }
                        }
                    }
                }

            }
        }


        
    }
    //функция возвращает кол-во живых кораблей
    public int LifeShip()
    {
        //подсчет кол-во кораблей
        int CountLife = 0;

        foreach(Ship Test in ListShip)
        {
            foreach (TestCoor Paluba in Test.ShipCoord)
            {
                int TestBlock = Pole[Paluba.X, Paluba.Y].GetComponent<Chanks>().Index;
                if (TestBlock == 1) CountLife++;
            }
        }

        return CountLife;
    }
}
