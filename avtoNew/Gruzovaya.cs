using Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace avtoNew
{
    internal class Gruzovaya : Avto
    {
      
        private float ves = 4000f;
        protected int gruz;
        protected int firstStop;
        private int secondStop;
     //   private List<double> ostanovki = new List<double>();
        protected double maxBak = 80;
        protected int minSpeed = 70;
        protected override void info(double maxBak, List<Avto> transport)
        {
            base.info(this.maxBak, transport);
            
            this.typeCar = 1;
        }
        protected override void output()
        {
            base.output();
            Console.WriteLine("Груза в машине :" + this.gruz);
            Console.WriteLine("Путь в км: " + this.km);
        }
        protected override void raschetKm()
        {
            base.raschetKm();
           
            Console.WriteLine("Введите через сколько км от базы будет остановка для погрузки");
            this.firstStop = Convert.ToInt32(Console.ReadLine());
            while (this.firstStop >= this.km)
            {
                Console.WriteLine("Ошибка. Введите заново");
                this.firstStop = Convert.ToInt32(Console.ReadLine());
            }
            

        }
        
        private void pogruzka()
        {
            Console.WriteLine("Сколько погружаем? Введите в кг");
            int pogruz = Convert.ToInt32(Console.ReadLine());
            while (this.gruz + pogruz > 2000)
            {
                Console.WriteLine("Превышает максимальный вес груза. Введите заново");
                pogruz = Convert.ToInt32(Console.ReadLine());
            }
            this.gruz += pogruz;
            speedChange(this.gruz, this.speed, this.minSpeed);
            rashodChange(this.speed, this.rashod);
        }
        private void razgruzka()
        {
            Console.WriteLine("Сколько разгружаем? Введите в кг");
            int razgruz = Convert.ToInt32(Console.ReadLine());
            while (this.gruz - razgruz < 0)
            {
                Console.WriteLine("Ошибка. Введите заново");
                razgruz = Convert.ToInt32(Console.ReadLine());
            }
            this.gruz -= razgruz;
            this.speedChange(this.gruz, this.speed, this.minSpeed);
            this.rashodChange(this.speed, this.rashod);
        }
        
        protected override void move()
        {
            if (this.km == 0)
            {
                Console.WriteLine("Вы не спланировали маршрут");

            }
            else 
            {

                this.rasst = 0;
                double proehali;

                double km1 = this.firstStop;
                double km2 = this.km - this.firstStop;
                double km3 = this.km;
                while (this.rasst != km1)
                {
                    if (this.bak - (km1 * this.rashod / 100) >= 0) //  если бензина хватает, чтобы доехать
                    {
                        this.bak = this.bak - (km1 * this.rashod / 100);
                        raschetProbeg(km1);
                        this.rasst += km1;
                        Console.WriteLine("Вы успешно проехали до погрузки");

                        this.pogruzka();
                        Console.WriteLine("Введите:" +
                                            "\n 1 - увеличить скорость \n 2 - уменьшить скорость \n 0 - не менять");
                        int otvet = Convert.ToInt32(Console.ReadLine());
                        switch (otvet)
                        {
                            case 1:
                                this.razgon();
                                this.rashodChange(this.speed, this.rashod);
                                break;
                            case 2:
                                this.tormoz();
                                this.rashodChange(this.speed, this.rashod);
                                break;
                            case 0:
                                break;
                        }
                        break;

                    }
                    else //если бензина не хватит
                    {
                        proehali = this.bak / (this.rashod / 100);
                        Console.WriteLine("Вы проехали " + proehali + " км на имеющемся топливе");
                        this.rasst += proehali;
                        this.bak = 0;
                        km1 -= proehali;
                        raschetProbeg(proehali);

                        Console.WriteLine("Чтобы проехать полное расстояние требуется заправка. Едем на заправку");

                        top = 1;
                        while ((this.rashod / 100 * km1) > this.bak)
                        {
                            Console.WriteLine("Сколько бензина залить? Чтобы доехать  требуется " + (this.rashod / 100 * km1) + " литров. " +
                                " \nВведите -1, если хотите узнать информацию о машине");
                            top = Convert.ToInt32(Console.ReadLine());
                            if (top == -1)
                            {
                                this.output();
                            }
                            else
                            {
                                this.zapravka(top, this.maxBak);
                            }

                            if (this.bak >= (this.rashod / 100 * km1)) //если в баке  хватает топлива, чтобы доехать
                            {
                                proehali = km1;
                                this.rasst += proehali;
                                raschetProbeg(proehali);
                                this.bak = this.bak - (this.rashod / 100 * km1);
                                km1 = 0;
                                //  this.pogruzka();

                                break;
                            }
                            else //если еще не хватает, чтобы доехать
                            {
                                proehali = this.bak / (this.rashod / 100);
                                km1 = km1 - proehali;
                                this.bak = 0;
                                this.rasst += proehali; //считает расстояние за эту поездку
                                raschetProbeg(proehali);

                            }


                        }
                    }

                }
                Console.WriteLine("Пробег: " + this.probeg);
                Console.WriteLine("Едем на разгрузку");
                proehali = 0;
                this.rasst = 0;
                while (this.rasst != km2)
                {
                    if (this.bak - (km2 * this.rashod / 100) >= 0) //  если бензина хватает, чтобы доехать
                    {
                        this.bak = this.bak - (km2 * this.rashod / 100);
                        raschetProbeg(km2);
                        this.rasst += km2;
                        Console.WriteLine("Вы успешно проехали до разгрузки");

                        this.razgruzka();
                        Console.WriteLine("Введите:" +
                                            "\n 1 - увеличить скорость \n 2 - уменьшить скорость \n 0 - не менять");
                        int otvet = Convert.ToInt32(Console.ReadLine());
                        switch (otvet)
                        {
                            case 1:
                                this.razgon();
                                this.rashodChange(this.speed, this.rashod);
                                break;
                            case 2:
                                this.tormoz();
                                this.rashodChange(this.speed, this.rashod);
                                break;
                            case 0:
                                break;
                        }
                        break;
                    }
                    else //если бензина не хватит
                    {
                        proehali = this.bak / (this.rashod / 100);
                        Console.WriteLine("Вы проехали " + proehali + " км на имеющемся топливе");
                        this.rasst += proehali;
                        this.bak = 0;
                        km2 -= proehali;
                        raschetProbeg(proehali);

                        Console.WriteLine("Чтобы проехать полное расстояние требуется заправка. Едем на заправку");


                        while ((this.rashod / 100 * km2) > this.bak)
                        {
                            Console.WriteLine("Сколько бензина залить? Чтобы доехать  требуется " + (this.rashod / 100 * km2) + " литров. " +
                                " \nВведите -1, если хотите узнать информацию о машине");
                            top = Convert.ToInt32(Console.ReadLine());
                            if (top == -1)
                            {
                                this.output();
                            }
                            else
                            {
                                this.zapravka(top, this.maxBak);
                            }

                            if (this.bak >= (this.rashod / 100 * km2)) //если в баке  хватает топлива, чтобы доехать
                            {
                                proehali = km2;
                                this.rasst += proehali;
                                raschetProbeg(proehali);
                                this.bak = this.bak - (this.rashod / 100 * km2);
                                km2 = 0;


                                break;
                            }
                            else //если еще не хватает, чтобы доехать
                            {
                                proehali = this.bak / (this.rashod / 100);
                                km2 -= proehali;
                                this.bak = 0;
                                this.rasst += proehali; //считает расстояние за эту поездку
                                raschetProbeg(proehali);

                            }


                        }
                    }

                }
                Console.WriteLine("Пробег: " + this.probeg);
                Console.WriteLine("Возвращаемся на базу.");
                proehali = 0;
                this.rasst = 0;
                while (this.rasst != km3)
                {
                    if (this.bak - (km3 * this.rashod / 100) >= 0) //  если бензина хватает, чтобы доехать
                    {
                        this.bak = this.bak - (km3 * this.rashod / 100);
                        raschetProbeg(km3);
                        this.rasst += km3;
                        Console.WriteLine("Вы успешно проехали до базы");

                        break;
                    }
                    else //если бензина не хватит
                    {
                        proehali = this.bak / (this.rashod / 100);
                        Console.WriteLine("Вы проехали " + proehali + " км на имеющемся топливе");
                        this.rasst += proehali;
                        this.bak = 0;
                        km3 -= proehali;
                        raschetProbeg(proehali);

                        Console.WriteLine("Чтобы проехать полное расстояние требуется заправка. Едем на заправку");


                        while ((this.rashod / 100 * km3) > this.bak)
                        {
                            Console.WriteLine("Сколько бензина залить? Чтобы доехать  требуется " + (this.rashod / 100 * km3) + " литров. " +
                                " \nВведите -1, если хотите узнать информацию о машине");
                            top = Convert.ToInt32(Console.ReadLine());
                            if (top == -1)
                            {
                                this.output();
                            }
                            else
                            {
                                this.zapravka(top, this.maxBak);
                            }

                            if (this.bak >= (this.rashod / 100 * km3)) //если в баке  хватает топлива, чтобы доехать
                            {
                                proehali = km3;
                                this.rasst += proehali;
                                raschetProbeg(proehali);
                                this.bak = this.bak - (this.rashod / 100 * km3);
                                km3 = 0;

                                break;
                            }
                            else //если еще не хватает, чтобы доехать
                            {
                                proehali = this.bak / (this.rashod / 100);
                                km3 -= proehali;
                                this.bak = 0;
                                this.rasst += proehali; //считает расстояние за эту поездку
                                raschetProbeg(proehali);

                            }


                        }
                    }
                    this.gruz = 0;
                    Console.WriteLine("Пробег: " + this.probeg);
                    Console.WriteLine("Вы достигли конца пути");
                }

            }
           
        }
           


        }


    }


