using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class Avto
    {
        protected int typeCar;
        private float ves = 2000f;
        protected string nom;
        protected double bak;
        protected double rasst;
        protected double rashod;
        protected double probeg;
        protected int beginX;
        protected int beginY;
        protected int endX;
        protected int endY;
        protected double km;
        protected int top;
        protected double maxBak = 40;
        protected List<string> coordinates = new List<string>();
        protected int speed;
        protected int minSpeed = 40;

        public int ClassType()
        {

            return this.typeCar;
        }
        public void nomProv(List<Avto> transport)
        {
            int count = 1;
            bool checkNom = true;
            
                for (int i = 0; i < transport.Count; i++)
                {
                    if (this.nom == transport[i].nom && i != transport.IndexOf(this))
                    {
                        Console.WriteLine("Такой номер уже есть. Введите заново");
                        this.nom = Console.ReadLine();
                        this.nomProv(transport);
                    }
                }
             
        }
        public void choose(int otvet, List<Avto> transport)
        {

            switch (otvet)
            {
                case 1:
                    info(this.maxBak, transport);
                    break;
                case 2:
                    move();
                    break;

                case 3:
                    razgon();
                    break;
                case 4:
                    tormoz();
                    break;
                case 5:
                    output();
                    break;
                case 6:

                    this.raschetKm();

                    break;



            }

        }
        public void choose(Avto car)
        {
            if (car.km == 0 || this.km == 0)
            {
                Console.WriteLine("Ошибка. Не спланирован маршрут");
            }
            else
            {
                this.coord(this.beginX, this.endX, this.coordinates);
                car.coord(car.beginX, car.endX, car.coordinates);
                this.crash(car.coordinates);
                this.coordinates.Clear();
                car.coordinates.Clear();
            }

        }
        protected virtual void info(double maxBak, List<Avto> transport)
        {
            
            Console.WriteLine("Введите номера машины: ");
            this.nom = Console.ReadLine();
            this.nomProv(transport);
            Console.WriteLine("Введите количество бензина в машине: ");
            this.bak = Convert.ToInt32(Console.ReadLine());
            if (this.bak > maxBak)
            {

                Console.WriteLine("В бак столько не вместится, заправляю " + maxBak);
                this.bak = maxBak;

            }
            Console.WriteLine("Введите расход топлива машины: ");
            this.rashod = float.Parse(Console.ReadLine());
            while (this.rashod > this.bak / 3)
            { 
                Console.WriteLine("Ошибка.Расход не может быть больше "+ this.bak/3 + ". Введите заново");
                this.rashod = float.Parse(Console.ReadLine());

            }
            Console.WriteLine("Введите текущую скорость: ");
            this.speed = Convert.ToInt32(Console.ReadLine());
            while (this.speed > 180 || this.speed <= 0)
            { 
            Console.WriteLine("Ошибка. Введите заново:");
                this.speed = Convert.ToInt32(Console.ReadLine());
            }

        }

        protected virtual void output()
        {
            Console.WriteLine("Тип машины: " + this.typeCar);
            Console.WriteLine("Номер машины: " + this.nom);
            Console.WriteLine("Количество бензина в машине: " + this.bak);
            Console.WriteLine("Расход топлива: " + this.rashod);
            Console.WriteLine("Пробег: " + this.probeg);
            Console.WriteLine("Скорость: " + this.speed);
            Console.WriteLine("Вес: " + this.ves);

        }
        
        protected virtual void zapravka(int top, double maxBak)
        {
           
            if (this.bak + top > maxBak)
            {
               
                top = (int)(maxBak - this.bak);
                this.bak += top;
               
            }
            else
            {
                this.bak += top;
            }
          //  Console.WriteLine("Топлива в баке " + this.bak);
        }
        protected  void speedChange(int gruz, int speed, int minSpeed)
        {
            if (gruz >= 100 && gruz <= 1000 && this.speed > this.minSpeed)
            {
                this.speed -= Convert.ToInt32(0.4 * this.speed);
                Console.WriteLine(" Теперь ваша скорость: " + this.speed);
            }
            else if (gruz > 1000 && gruz <= 2000 && this.speed > this.minSpeed)
            {
                this.speed -= Convert.ToInt32(0.8 * this.speed);
                Console.WriteLine(" Теперь ваша скорость: " + this.speed);
            }
            else if (gruz < 100 && gruz > 0 && this.speed > this.minSpeed)
            {
                Console.WriteLine("Груз небольшой, скорость не меняется");
            }
            else if (this.speed < this.minSpeed)
            {
            Console.WriteLine("Скорость и так меньше минимальной. Не меняется");
            }
        }

        protected  void rashodChange(int speed, double rashod)
        {
            if (this.speed > 0 && this.speed <= 45)
            {
                this.rashod = 12;
            }
            else if (this.speed > 45 && this.speed <= 100)
            {
                this.rashod = 9;
            }
            else if (this.speed > 100 && this.speed <= 180)
            {
                this.rashod = 12.5f;
            }
            Console.WriteLine(" Теперь ваш расход: " + this.rashod);
        }
        protected virtual void move()
        {
            if (this.km == 0)
            {
                Console.WriteLine("Вы не сплаировали маршрут");
            }
            else
            {
                this.rasst = 0;
                double proehali = 0;

                double put = Convert.ToDouble(this.km);
                if (this.bak - (put * this.rashod / 100) >= 0) //  если бензина хватает, чтобы доехать
                {
                    this.bak = this.bak - (put * this.rashod / 100);
                    raschetProbeg(put);
                    this.rasst += put;
                    Console.WriteLine("Вы успешно проехали заданное расстояние");
                }
                else //если бензина не хватит
                {
                    proehali = this.bak / (this.rashod / 100);
                    Console.WriteLine("Вы проехали " + proehali);
                    this.rasst += proehali;
                    this.bak = 0;
                    put -= proehali;
                    raschetProbeg(proehali);

                    Console.WriteLine("Чтобы проехать полное расстояние требуется заправка. Едем на заправку? да/нет");
                    string otvet = Console.ReadLine();

                    switch (otvet)
                    {
                        case "да":
                            top = 1;
                            while ((this.rashod / 100 * put) > this.bak && top != 0)
                            {
                                Console.WriteLine("Сколько бензина залить? Чтобы доехать  требуется " + (this.rashod / 100 * put) + " литров. " +
                                    "\nЕсли введете 0, движение прекратится." +
                                    " \nВведите -1, если хотите узнать информацию о машине");
                                top = Convert.ToInt32(Console.ReadLine());
                                if (top == 0)
                                {

                                    break;

                                }
                                else if (top == -1)
                                {
                                    this.output();

                                }

                                else
                                {
                                    zapravka(top, this.maxBak);
                                }

                                if (this.bak >= (this.rashod / 100 * put)) //если в баке  хватает топлива, чтобы доехать
                                {
                                    proehali = put;
                                    this.rasst += proehali;
                                    raschetProbeg(proehali);

                                    this.bak = this.bak - (this.rashod / 100 * put);
                                    put = 0;

                                }
                                else //если еще не хватает, чтобы доехать
                                {
                                    proehali = this.bak / (this.rashod / 100);
                                    put = put - proehali;
                                    this.bak = 0;
                                    this.rasst += proehali; //считает расстояние за эту поездку
                                    raschetProbeg(proehali);

                                }


                            }
                            break;

                        case "нет": //если на заправку ехать отказались


                            put = this.bak / (this.rashod / 100);
                            this.bak = 0;
                            this.rasst = put;
                            raschetProbeg(proehali);
                            break;

                    }


                }
                Console.WriteLine("Путь пройден");
            }


        }

        protected void tormoz()
        {
            
            Console.WriteLine("На сколько км/ч сбросить скорость? ");
            int sbros = Convert.ToInt32(Console.ReadLine());
            while (this.speed - sbros <= 0)
            {
                Console.WriteLine("Вы не можете обнулить скорость. Введите заново");
                sbros = Convert.ToInt32(Console.ReadLine());
            }
            
            this.speed -= sbros;
            Console.WriteLine("измененная скорость: " + this.speed);

        }

        protected void razgon()
        {
            
            Console.WriteLine("На сколько км/ч увеличить скорость? ");
            int razgonSpeed = Convert.ToInt32(Console.ReadLine());
            while (this.speed + razgonSpeed > 180)
            {
                Console.WriteLine("Скорость не может быть больше 180. Введите заново");
                razgonSpeed = Convert.ToInt32(Console.ReadLine());
            }
            this.speed += razgonSpeed;
            Console.WriteLine("измененная скорость: " + this.speed);

        }

        protected virtual void raschetKm()
        {
            Console.WriteLine("Начальная координата X: ");
            this.beginX = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Начальная координата Y: ");
            this.beginY = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Конечная координата X: ");
            this.endX = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Конечная координата Y: ");
            this.endY = Convert.ToInt32(Console.ReadLine());
           
            this.km = Convert.ToDouble(Math.Abs(Math.Sqrt(Math.Pow(this.beginX - this.endX, 2) + Math.Pow(this.beginY - this.endY, 2))));
            Console.WriteLine("Рассточние между начальной и конечной координатой " + this.km);
        }

        protected void raschetProbeg(double put)
        {
            this.probeg += put;
         
        }

        protected void crash(List<string> coordinates2)
        {
            List<string> crashCoordinates = new List<string>();
            bool crashCheck = false;
            string beginCrash = null;
            string endCrash = null;
            int k, l;
            int i = 0;
            int j = 0; ;
            for ( k = 0; k < this.coordinates.Count; k++) 
            {
                for ( l = 0; l < coordinates2.Count; l++)
                {
                    if (this.coordinates[k] == coordinates2[l])
                    {
                        crashCoordinates.Add(coordinates2[l]);
                        
                      
                    }

                }
               
            }
            bool checkEnd=false; 
          

            switch (crashCoordinates.Count)
            {
                case >0:
                    Console.WriteLine("Авария!");
                    Console.WriteLine("На промежутке с " + crashCoordinates[1] + " по " + crashCoordinates[crashCoordinates.Count-1] + "\nКоличество аварийных точек: " +  (crashCoordinates.Count-1));
                 
                    break;
                case 0:
                    Console.WriteLine("Аварии не случилось! :)");
                 
                    break;

            }
           

        }
        protected void coord( int beginX, int endX, List<string> coordinates)
        {
           

            int tempX = beginX;

            int count;

            if (endX > tempX)
            {
                count = 1;
            }
            else
            {
                count = -1;
                
            }
            while (tempX != endX)
            {
                coordinates.Add("" + tempX);
                tempX += count;


            }
        }

    }
}

