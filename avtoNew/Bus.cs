using Cars;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace avtoNew
{
    internal class Bus : Avto
    {
        protected int kolvoOst;
        protected int vseLyudi;
        protected int maxBak = 60;
        protected int kg;
        protected int minSpeed = 40;

        
        protected override void raschetKm()
        {
            base.raschetKm();
            Console.WriteLine("Введите количество остановок на пути");
            this.kolvoOst = Convert.ToInt32(Console.ReadLine());
                
        }
        protected void passazhiri(int vseLyudi)
        {
            Console.WriteLine("Введите кол-во вышедших людей");
            int vyshli = Convert.ToInt32(Console.ReadLine());
            while (this.vseLyudi - vyshli < 0)
            {
                Console.WriteLine("Ошибка. Вышедших не может быть больше, чем есть в автобусе. Введите заново:");
                vyshli = Convert.ToInt32(Console.ReadLine());

            }
            this.vseLyudi -= vyshli;
            
            Console.WriteLine("Введите кол-во вошедших людей");
            int voshli = Convert.ToInt32(Console.ReadLine());
            while (this.vseLyudi + voshli > 30)
            {
                Console.WriteLine("Ошибка. Автобус столько не вмещает. Выгоните кого-нибудь и введите новое количество вошедших");
                voshli = Convert.ToInt32(Console.ReadLine());
            }
           
            this.vseLyudi += voshli;
            this.kg = this.vseLyudi * 70;
            this.speedChange(this.kg, this.speed, this.minSpeed);
            this.rashodChange(this.kg, this.rashod);
        }

        protected override void info(double maxBak, List <Avto> transport)
        {
            base.info(this.maxBak, transport);
            
            this.typeCar = 2;

        }
        
        protected override void output()
        {
            base.output();
            Console.WriteLine("Людей в автобусе: " + this.vseLyudi);
            Console.WriteLine("Кг пассажиров в машине :" + this.kg);
            Console.WriteLine("Остановок: " + this.kolvoOst);
            Console.WriteLine("Весь путь: " + this.km);
        }


        protected override void move()
        {
            if (this.km == 0)
            {

                Console.WriteLine("Вы не спланировали маршрут");
            }
            else
            {
                int countOst = 0;
                double ostKm = (this.km / this.kolvoOst); //расстояние между остановками
                Console.WriteLine("Расстояние между остановками: " + ostKm);
                double proehali = 0;

                while (this.kolvoOst > countOst)
                {

                    if (countOst == 0)//первая остановка
                    {
                        if (this.bak - (ostKm * this.rashod / 100) >= 0) // хватает топлива, чтобы доехать до первой остановки
                        {
                            this.bak = this.bak - (ostKm * this.rashod / 100);
                            raschetProbeg(ostKm);
                            Console.WriteLine("Вы успешно доехали до остановки. Сколько людей вошло?");
                            int voshli = Convert.ToInt32(Console.ReadLine());
                            while (voshli > 30)
                            {
                                Console.WriteLine("Столько не поместится. Введите заново: ");
                                voshli = Convert.ToInt32(Console.ReadLine());
                            }
                            this.vseLyudi += voshli;
                            this.kg = this.vseLyudi * 70;
                            this.speedChange(this.kg, this.speed, this.minSpeed);
                            this.rashodChange(this.speed, this.rashod);
                            countOst += 1;
                            proehali = 0;
                        }
                        else
                        {
                            Console.WriteLine("в баке: " + this.bak);
                            proehali = this.bak / (this.rashod / 100);
                            ostKm -= proehali;
                            Console.WriteLine("На имеющемся топливе вы преодолели " + proehali + " км до новой остановки");
                            raschetProbeg(proehali);
                            Console.WriteLine("Чтобы проехать до остановки требуется заправка");
                            proehali = 0;
                            this.bak = 0;
                            top = 1;
                            while ((this.rashod / 100 * ostKm) > this.bak)
                            {
                                Console.WriteLine("Сколько бензина залить? Чтобы доехать  требуется " + (this.rashod / 100 * ostKm) + " литров. " +
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

                                if (this.bak >= (this.rashod / 100 * ostKm)) //если в баке  хватает топлива, чтобы доехать
                                {
                                    countOst += 1;
                                    proehali = ostKm;
                                    raschetProbeg(proehali);
                                    this.bak = this.bak - (this.rashod / 100 * ostKm);
                                    Console.WriteLine("Вы успешно доехали до остановки. Сколько людей вошло?");
                                    int voshli = Convert.ToInt32(Console.ReadLine());
                                    while (voshli > 30)
                                    {
                                        Console.WriteLine("Столько не поместится. Введите заново: ");
                                        voshli = Convert.ToInt32(Console.ReadLine());
                                    }
                                    this.vseLyudi += voshli;
                                    this.kg = this.vseLyudi * 70;
                                    this.speedChange(this.kg, this.speed, this.minSpeed);
                                    this.rashodChange(this.speed, this.rashod);
                                    proehali = 0;
                                    break;

                                }
                                else //если еще не хватает, чтобы доехать
                                {
                                    proehali = this.bak / (this.rashod / 100);
                                    ostKm -= proehali;
                                    raschetProbeg(proehali);
                                    proehali = 0;
                                    this.bak = 0;

                                }
                            }
                            ostKm = this.km / this.kolvoOst;

                        }
                    }
                    else if (countOst != 0)
                    {
                        if (this.bak - (ostKm * this.rashod / 100) >= 0) //  если бензина хватает, чтобы доехать до любой другой остановки
                        {
                            this.bak = this.bak - ostKm * this.rashod / 100;
                            raschetProbeg(ostKm);
                            Console.WriteLine("Вы успешно доехали до очередной остановки. Пассажиров сейчас: " + this.vseLyudi);
                            countOst++;
                            proehali = 0;
                            this.passazhiri(this.vseLyudi);
                        }
                        else //если бензина не хватит
                        {
                            Console.WriteLine("в баке: " + this.bak);
                            proehali = this.bak / (this.rashod / 100);
                            ostKm -= proehali;
                            Console.WriteLine("На имеющемся топливе вы преодолели " + proehali + " км до новой остановки");
                            raschetProbeg(proehali);
                            Console.WriteLine("Чтобы проехать до остановки требуется заправка");
                            this.bak = 0;

                            if (this.bak - (ostKm * this.rashod / 100) < 0)
                            {

                                top = 1;
                                while ((this.rashod / 100 * ostKm) > this.bak)
                                {
                                    Console.WriteLine("Сколько бензина залить? Чтобы доехать  требуется " + (this.rashod / 100 * ostKm) + " литров. " +
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

                                    if (this.bak >= (this.rashod / 100 * ostKm)) //если в баке  хватает топлива, чтобы доехать
                                    {
                                        proehali = ostKm;
                                        this.bak = this.bak - (this.rashod / 100 * ostKm);

                                        Console.WriteLine("Вы успешно доехали до очередной остановки. Пассажиров сейчас: " + this.vseLyudi);
                                        Console.WriteLine("в баке осталось: " + this.bak);
                                        countOst++;
                                        raschetProbeg(proehali);
                                        proehali = 0;
                                        this.passazhiri(this.vseLyudi);
                                        break;
                                    }
                                    else //если еще не хватает, чтобы доехать
                                    {
                                        proehali = this.bak / (this.rashod / 100);
                                        ostKm -= proehali;
                                        Console.WriteLine("На имеющемся топливе вы преодолели " + proehali + " км до новой остановки");
                                        raschetProbeg(proehali);
                                        proehali = 0;
                                        this.bak = 0;

                                    }
                                }

                            }
                            ostKm = this.km / this.kolvoOst;
                        }



                    }
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
                    Console.WriteLine("Остановок пройдено: " + countOst);

                }
                ostKm = this.km / kolvoOst;
                proehali = 0;
                Console.WriteLine("Пробег: " + this.probeg);
                Console.WriteLine("Возвращаемся.");



                while (this.kolvoOst * 2 > countOst)
                {

                    if (this.bak - (ostKm * this.rashod / 100) >= 0) //  если бензина хватает, чтобы доехать до любой другой остановки
                    {
                        this.bak = this.bak - ostKm * this.rashod / 100;
                        raschetProbeg(ostKm);
                        Console.WriteLine("Вы успешно доехали до очередной остановки. Пассажиров сейчас: " + this.vseLyudi);
                        countOst++;
                        proehali = 0;
                        this.passazhiri(this.vseLyudi);
                    }
                    else //если бензина не хватит
                    {
                        Console.WriteLine("в баке: " + this.bak);
                        proehali = this.bak / (this.rashod / 100);
                        ostKm -= proehali;
                        Console.WriteLine("На имеющемся топливе вы преодолели " + proehali + " км до новой остановки");
                        raschetProbeg(proehali);
                        Console.WriteLine("Чтобы проехать до остановки требуется заправка");
                        this.bak = 0;

                        if (this.bak - (ostKm * this.rashod / 100) < 0)
                        {

                            top = 1;
                            while ((this.rashod / 100 * ostKm) > this.bak)
                            {
                                Console.WriteLine("Сколько бензина залить? Чтобы доехать  требуется " + (this.rashod / 100 * ostKm) + " литров. " +
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

                                if (this.bak >= (this.rashod / 100 * ostKm)) //если в баке  хватает топлива, чтобы доехать
                                {
                                    proehali = ostKm;
                                    this.bak = this.bak - (this.rashod / 100 * ostKm);

                                    Console.WriteLine("Вы успешно доехали до очередной остановки. Пассажиров сейчас: " + this.vseLyudi);
                                    Console.WriteLine("в баке осталось: " + this.bak);
                                    countOst++;
                                    raschetProbeg(proehali);
                                    proehali = 0;
                                    this.passazhiri(this.vseLyudi);
                                    break;
                                }
                                else //если еще не хватает, чтобы доехать
                                {
                                    Console.WriteLine("в баке: " + this.bak);
                                    proehali = this.bak / (this.rashod / 100);
                                    ostKm -= proehali;
                                    Console.WriteLine("На имеющемся топливе вы преодолели " + proehali + " км до новой остановки");
                                    raschetProbeg(proehali);
                                    proehali = 0;
                                    this.bak = 0;

                                }
                            }

                        }
                        ostKm = this.km / this.kolvoOst;
                    }


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
                    Console.WriteLine("Пробег: " + this.probeg);
                    Console.WriteLine("Остановок пройдено: " + countOst);

                }


                countOst = 0;
                this.vseLyudi = 0;
                Console.WriteLine("Вы вернулись к базе");

            }
        }
    }

}
