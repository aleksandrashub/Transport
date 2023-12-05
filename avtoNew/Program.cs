
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using avtoNew;
using Cars;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            string otvet1;
            int otvet = 1;
            int ind;
            int createOrChoose = 1;
            int otvetMenu = 7;
            int typeCar;
            List<Avto> transport = new List<Avto>();

            while (createOrChoose == 1 || createOrChoose == 0 || createOrChoose == -1)
            {
                Console.WriteLine("Введите \n0 - если хотите перейти в меню созданной машины  \n1 - чтобы создать новую \n-1 - проверить аварии");
                createOrChoose = Convert.ToInt32(Console.ReadLine());
                switch (createOrChoose)
                {
                    case 1:
                        Console.WriteLine("Введите \n 1 - создать обычную машину \n 2 - создать грузовую \n 3 - создать автобус");
                        typeCar = Convert.ToInt32(Console.ReadLine());

                        switch (typeCar)
                        {
                            case 1:
                                Avto avto = new Avto();
                                transport.Add(avto);
                                avto.choose(1, transport);


                            

                                otvetMenu = 1;
                                while (otvetMenu == 1 || otvetMenu == 2 || otvetMenu == 3 || otvetMenu == 4 ||
                                       otvetMenu == 5 || otvetMenu == 6)
                                {
                                    Console.WriteLine("Меню машины номер " + (transport.IndexOf(avto) + 1) +
                                                      "\n Нажмите:  \n 1 - чтобы начать движение \n 2 - чтобы разогнаться \n 3- чтобы притормозить " +
                                                      "\n 4 - чтобы вывести информацию о машине \n 5 - чтобы спланировать маршрут \n 0 - переключиться на другую машину/создать или проверить аварии");
                                    otvetMenu = Convert.ToInt32(Console.ReadLine());

                                    if (otvetMenu == 0)
                                    {
                                        break;


                                    }

                                    else if (otvetMenu == 2)
                                    {
                                        count += 1;
                                    }
                                    else if (otvetMenu == -1)
                                    { 
                                    
                                    
                                    }
                                    avto.choose(otvetMenu + 1, transport);
                                }

                                break;

                            case 2:
                                Gruzovaya gruzovaya = new Gruzovaya();
                                transport.Add(gruzovaya);
                                gruzovaya.choose(1, transport);

                                 

                                otvetMenu = 1;
                                while (otvetMenu == 1 || otvetMenu == 2 || otvetMenu == 3 || otvetMenu == 4 || otvetMenu == 5 || otvetMenu == 6)
                                {
                                    Console.WriteLine("Меню \n нажмите:  \n 1 - чтобы начать движение \n 2 - чтобы разогнаться \n 3 - чтобы притормозить " +
                               "\n 4 - чтобы вывести информацию о машине \n 5 - чтобы спланировать маршрут \n 0 - переключиться на другую машину/создать или проверить аварии");
                                    otvetMenu = Convert.ToInt32(Console.ReadLine());
                                    if (otvetMenu == 2)
                                    {
                                        count += 1;
                                    }
                                    if (otvetMenu == 0)
                                    {
                                        break;

                                    }

                                    gruzovaya.choose(otvetMenu + 1, transport);
                                }
                                break;

                            case 3:

                                Bus bus = new Bus();
                                transport.Add(bus);
                                bus.choose(1, transport);
                                otvetMenu = 1;
                                while (otvetMenu == 1 || otvetMenu == 2 || otvetMenu == 3 || otvetMenu == 4 || otvetMenu == 5 || otvetMenu == 6)
                                {
                                    Console.WriteLine("Меню \n нажмите:  \n 1 - чтобы начать движение \n 2 - чтобы разогнаться \n 3 - чтобы притормозить " +
                                 "\n 4 - чтобы вывести информацию о машине \n 5 - чтобы спланировать маршрут \n 0 - переключиться на другую машину/создать или проверить аварии");
                                    otvetMenu = Convert.ToInt32(Console.ReadLine());
                                    if (otvetMenu == 2)
                                    {
                                        count += 1;
                                    }
                                    if (otvetMenu == 0)
                                    {
                                        break;

                                    }
                                    bus.choose(otvetMenu + 1, transport);
                                }
                                break;



                        }
                        break;
                    case 0:
                        if (transport.Count == 0)
                        {
                            Console.WriteLine("У вас еще нет созданных машин.");
                            break;
                        }
                        Console.WriteLine("у вас машин " + transport.Count);
                        bool checkCar = false;
                        Console.WriteLine("Введите номер машины, в меню которой хотите перейти");
                        ind = Convert.ToInt32(Console.ReadLine());
                        for (int i = 0; i < transport.Count; i++)
                        {
                            if (ind == i + 1)
                            {
                                checkCar = true;
                                break;
                            }
                        }

                        if (checkCar == true)
                        { 
                           // Console.WriteLine("Машина нашлась");
                            otvetMenu = 1;
                            if (transport[ind - 1].ClassType() == 1 || transport[ind - 1].ClassType() == 2)
                            {
                                while (otvetMenu == 1 || otvetMenu == 2 || otvetMenu == 3 || otvetMenu == 4 ||
                                       otvetMenu == 5 || otvetMenu == 6)
                                {
                                    Console.WriteLine("Меню машины номер " + ind +
                                                      "\n 1 - чтобы начать движение \n 2 - чтобы разогнаться \n 3 - чтобы притормозить " +
                                                      "\n 4 - чтобы вывести информацию о машине \n 5 - чтобы спланировать маршрут \n 0 - переключиться на другую машину / создать или проверить аварии");
                                    otvetMenu = Convert.ToInt32(Console.ReadLine());
                                    if (otvetMenu == 2)
                                    {
                                        count += 1;
                                    }

                                    if (otvetMenu == 0 || otvetMenu == -1)
                                    {
                                        break;

                                    }
                                    transport[ind-1].choose(otvetMenu + 1, transport);
                                }
                            }
                            else
                            {
                                while (otvetMenu == 1 || otvetMenu == 2 || otvetMenu == 3 || otvetMenu == 4 ||
                                       otvetMenu == 5 || otvetMenu == 6)
                                {
                                    Console.WriteLine("Меню машины номер " + ind +
                                                      "\n 1 - чтобы начать движение \n 2 - чтобы разогнаться \n 3 - чтобы притормозить " +
                                                      "\n 4 - чтобы вывести информацию о машине \n 5 - чтобы спланировать маршрут \n 0 - переключиться на другую машину / создать или проверить аварии");
                                    otvetMenu = Convert.ToInt32(Console.ReadLine());
                                    if (otvetMenu == 2)
                                    {
                                        count += 1;
                                    }

                                    if (otvetMenu == 0 || otvetMenu == -1)
                                    {
                                        break;

                                    }
                                    transport[ind-1].choose(otvetMenu + 1, transport);
                                }
                            }

                           
                        }
                        else
                        {
                            
                            Console.WriteLine("Такой машины не нашлось");
                        }

                       break;
                    case -1:
                        if (transport.Count <= 1)
                        {
                            Console.WriteLine("У вас менее двух машин. Чтобы проверить аварии нужно больше");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Введите номер первой машины, которую хотите проверить");
                            int ind1 = Convert.ToInt32(Console.ReadLine());
                            checkCar = false;
                            for (int i = 0; i < transport.Count; i++)
                            {
                                if (ind1 == i + 1)
                                {
                                    checkCar = true;
                                    break;
                                }
                            }

                            if (checkCar == false)
                            {
                                Console.WriteLine("Машины не найдено");
                                break;
                            }
                            else
                            {
                                checkCar = false;
                                Console.WriteLine("Введите номер второй машины, которую хотите проверить");
                                int ind2 = Convert.ToInt32(Console.ReadLine());
                                for (int i = 0; i < transport.Count; i++)
                                {
                                    if (ind2 == i + 1)
                                    {
                                        checkCar = true;
                                        break;
                                    }
                                }

                                if (checkCar == false)
                                {
                                    Console.WriteLine("Машины не найдено");
                                    break;
                                }

                                else
                                {
                                    transport[ind1-1].choose(transport[ind2-1]);
                                
                                }
                            }
                             

                        }
                        break;

                }

            }
            








        }
    }
}
 