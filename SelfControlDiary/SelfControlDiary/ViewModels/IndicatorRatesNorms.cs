using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public static class IndicatorRatesNorms
    {
        static int MinManVRIndex = 350;
        static int MaxManVRIndex = 400;
        static int MinGirlVRIndex = 325;
        static int MaxGirlVRIndex = 375;
        static int MinManLifeIndex = 65;
        static int MaxManLifeIndex = 70;
        static int MinGirlLifeIndex = 55;
        static int MaxGirlLifeIndex = 60;
        static int MinManPowerIndex = 70;
        static int MaxManPowerIndex = 75;
        static int MinGirlPowerIndex = 50;
        static int MaxGirlPowerIndex = 60;
        static int MinManBaseForceIndex = 200;
        static int MaxManBaseForceIndex = 220;
        static int MinGirlBaseForceIndex = 135;
        static int MaxGirlBaseForceIndex = 150;
        static int[] BoysRun1 = new int[]
        {
            1500, 1450, 1400, 1350, 1300, 1250, 1200, 1150, 1100, 1000
        };
        static int[] BoysRun2 = new int[]
        {
            1600, 1550, 1500, 1450, 1400, 1350, 1300, 1250, 1200, 1150
        };
        static int[] GirlsRun1 = new int[]
        {
            1300, 1250, 1200, 1170, 1150, 1100, 1050, 1000, 900, 800
        };
        static int[] GirlsRun2 = new int[]
        {
            1350, 1300, 1250, 1200, 1170, 1150, 1100, 1050, 1000, 900
        };
        static int[] BoysIncline1 = new int[]
        {
            20, 18, 15, 13, 11, 9, 7, 5, 3, 1
        };
        static int[] BoysIncline2 = new int[]
        {
            22, 20, 18, 16, 14, 12, 10, 8, 6, 3
        };
        static int[] GirlsIncline1 = new int[]
        {
            23, 20, 18, 15, 12, 10, 8, 6, 3, 1
        };
        static int[] GirlsIncline2 = new int[]
        {
            25, 22, 20, 18, 15, 12, 10, 8, 6, 3
        };
        static int[] BoysBending1 = new int[]
        {
            45, 40, 38, 33, 30, 28, 25, 22, 18, 15
        };
        static int[] BoysBending2 = new int[]
        {
            50, 46, 42, 38, 36, 32, 28, 25, 22, 18
        };
        static int[] GirlsBending1 = new int[]
        {
            25, 23, 20, 18, 15, 13, 11, 9, 7, 3
        };
        static int[] GirlsBending2 = new int[]
        {
            30, 25, 23, 20, 18, 15, 12, 10, 8, 5
        };
        static int[] BoysPulling1 = new int[]
        {
            15, 14, 12, 10, 8, 6, 5, 3, 2, 1
        };
        static int[] BoysPulling2 = new int[]
        {
            16, 15, 14, 12, 10, 8, 7, 5, 3, 2
        };
        static int[] GirlsPress1 = new int[]
        {
            80, 70, 65, 60, 55, 50, 45, 40, 35, 30
        };
        static int[] GirlsPress2 = new int[]
        {
            100, 90, 80, 70, 65, 60, 55, 50, 45, 40
        };
        static int[] BoysSquatting1 = new int[]
        {
            90, 80, 70, 60, 55, 50, 45, 40, 35, 30
        };
        static int[] BoysSquatting2 = new int[]
        {
            99, 95, 90, 80, 70, 60, 55, 50, 45, 40
        };
        static int[] GirlsSquatting1 = new int[]
        {
            80, 70, 60, 50, 45, 40, 35, 30, 25, 20
        };
        static int[] GirlsSquatting2 = new int[]
        {
            100, 90, 80, 70, 60, 50, 45, 40, 35, 30
        };


        public static string GetAPSKRes(double val)
        {
            if (val >= 1.5 && val <= 2.59)
                return "Удовлетворительная адаптация";
            if (val >=2.6 && val <=3.09)
                return "Напряжение механизмов адаптации";
            if (val >= 3.1 && val <= 3.49)
                return "Неудовлетворительная адаптация";
            if (val >= 3.5)
                return "Срыв адаптации";
            return "Аномальный показатель";
        }

        public static string GetKrempInd(double val)
        {
            if (val > 100)
                return "Высокий";
            if (val >= 76 && val <= 100)
                return "Средний";
            if (val >= 50 && val <= 75)
                return "Слабый";
            if (val < 50)
                return "Недостаточный";
            return "Аномальный показатель";
        }

        public static string GetVIK(double val)
        {
            if (val <= -30)
                return "Выраженная парасимпатикотония";
            if (val > -30 && val < -15)
                return "Преобладают парасимпатические влияния";
            if (val >= -15 && val <= 15)
                return "Уравновешены симпатические и парасимпатические влияния";
            if (val > 15 && val < 30)
                return "Преобладают симпатические влияния";
            return "Выраженная симпатикотония";
        }

        public static string GetStaminaCoefRes(double val)
        {
            if (val >= 9 && val < 16)
                return "Спортсмены";
            if (val >= 16 && val < 20)
                return "Взрослые, занимающиеся ФК";
            if (val >= 20 && val <= 25)
                return "Здоровые нетренированные";
            return "Аномальный показатель";
        }

        public static string GetUFSRes(double val)
        {
            if (val > 0.82)
                return "Отличное";
            if (val >= 0.67 && val <= 0.82)
                return "Хорошее";
            if (val >= 0.52 && val <= 0.66)
                return "Удовлетворительное";
            if (val >= 0.37 && val <= 0.51)
                return "Низкое";
            if (val < 0.37)
                return "Очень низкое";
            return "Аномальный показатель";
        }

        public static string GetPDPRes(double val)
        {
            if (val <= 75)
                return "Выше среднего";
            if (val >= 76 && val <= 89)
                return "Среднее";
            if (val >= 90)
                return "Ниже среднего";
            return "Аномальный показатель";
        }

        public static string GetGenchiRes(int val)
        {
            if (val >= 40)
                return "<p style='color: green'>Отлично</p>";
            if (val >=25)
                return "<p style='color: green'>Хорошо</p>";
            if (val >= 20)
                return "<p style='color: yellow'>Посредственно</p>";
            return "<p style='color: red'>Неудовлетворительно</p>";
        }

        public static string GetShtangeRes(int val)
        {
            if (val >= 56)
                return "<p style='color: green'>Отлично</p>";
            if (val >= 40)
                return "<p style='color: green'>Хорошо</p>";
            if (val >= 30)
                return "<p style='color: yellow'>Посредственно</p>";
            return "<p style='color: red'>Неудовлетворительно</p>";
        }

        public static string GetCCCRes(int val)
        {
            if (val <= 60)
                return "<p style='color: green'>Высокий</p>";
            if (val <= 90)
                return "<p style='color: green'>Выше среднего</p>";
            if (val <= 120)
                return "<p style='color: yellow'>Средний</p>";
            if (val <= 180)
                return "<p style='color: red'>Ниже среднего</p>";
            return "<p style='color: red'>Низкий</p>";
        }

        public static string GetStatRes(int val)
        {
            if (val <= 10 && val >= 0)
                return "<p style='color: green'>Отлично</p>";
            if (val <= 16 && val >= 11)
                return "<p style='color: green'>Хорошо</p>";
            if (val <= 22 && val >= 17)
                return "<p style='color: yellow'>Удовлетворительно</p>";
            return "<p style='color: red'>Неудовлетворительно</p>";
        }

        public static string GetIMTRes(double val)
        {
            if (val < 18.5)
                return "<p style='color: orange'>Дефицит массы</p>";
            if (val >= 18.5 && val <= 24.9)
                return "<p style='color: green'>Норма</p>";
            if (val >= 0.25 && val <= 29.9)
                return "<p style='color: orange'>Избыток массы</p>";
            if (val >= 30 && val <= 34.9)
                return "<p style='color: red'>Ожирение 1-ой степени</p>";
            if (val >= 35 && val <= 39.9)
                return "<p style='color: red'>Ожирение 2-ой степени</p>";
            return "<p style='color: red'>Ожирение 3-ой степени</p>";
        }

        static int GetMark(int[] marks, int res)
        {
            int i = 0;
            while (i < marks.Length && marks[i] > res)
                i++;
            return (10 - i);
        }

        public static int GetRunRes(int val, string sex, int course)
        {
            if (course <= 2)
            {
                return sex == "Мужской" ? GetMark(BoysRun1, val) : GetMark(GirlsRun1, val);
            }
            else
            {
                return sex == "Мужской" ? GetMark(BoysRun2, val) : GetMark(GirlsRun2, val);
            }
        }

        public static int GetInclineRes(int val, string sex, int course)
        {
            if (course <= 2)
            {
                return sex == "Мужской" ? GetMark(BoysIncline1, val) : GetMark(GirlsIncline1, val);
            }
            else
            {
                return sex == "Мужской" ? GetMark(BoysIncline2, val) : GetMark(GirlsIncline2, val);
            }
        }

        public static int GetBendingRes(int val, string sex, int course)
        {
            if (course <= 2)
            {
                return sex == "Мужской" ? GetMark(BoysBending1, val) : GetMark(GirlsBending1, val);
            }
            else
            {
                return sex == "Мужской" ? GetMark(BoysBending2, val) : GetMark(GirlsBending2, val);
            }
        }

        public static int GetSquattingRes(int val, string sex, int course)
        {
            if (course <= 2)
            {
                return sex == "Мужской" ? GetMark(BoysSquatting1, val) : GetMark(GirlsSquatting1, val);
            }
            else
            {
                return sex == "Мужской" ? GetMark(BoysSquatting2, val) : GetMark(GirlsSquatting2, val);
            }
        }

        public static int GetPullingRes(int val, int course)
        {
            if (course <= 2)
            {
                return GetMark(BoysPulling1, val);
            }
            else
            {
                return GetMark(BoysPulling2, val);
            }
        }

        public static int GetPressRes(int val, int course)
        {
            if (course <= 2)
            {
                return GetMark(GirlsPress1, val);
            }
            else
            {
                return GetMark(GirlsPress2, val);
            }
        }

        static string GetRes(int min, int max, double val)
        {
            if (val >= min && val <= max)
            {
                return "<p style='color: green'>В норме</p>";
            }
            else
            {
                double ras = val < min ? val / min * 100 - 100 : 100 - max / val * 100;
                return $"<p style='color: " + (ras > 0 ? "red" : "blue") + $"'>Расхождение с нормой: {Math.Round(ras, 2)}%</p>";
                //if (Math.Abs(ras) <= 10)
                //{
                //    return $"<p style='color: " + (ras > 0 ? "red" : "blue") + $"'>Расхождение с нормой: {Math.Round(ras, 2)}%</p>";
                //}
                //if (Math.Abs(ras) <= 20)
                //{
                //    return $"<p style='color: " + (ras > 0 ? "red" : "blue") + $"'>Расхождение с нормой: {Math.Round(ras, 2)}%</p>";
                //}
                //return $"<p style='color: red'>Расхождение с нормой: {Math.Round(ras, 2)}%</p>";
            }
        }

        public static string GetVRRes(double val, string sex)
        {
            return sex == "Мужской" ? GetRes(MinManVRIndex, MaxManVRIndex, val) : 
                GetRes(MinGirlVRIndex, MaxGirlVRIndex, val);
        }

        public static string GetLifeRes(double val, string sex)
        {
            return sex == "Мужской" ? GetRes(MinManLifeIndex, MaxManLifeIndex, val) :
                GetRes(MinGirlLifeIndex, MaxGirlLifeIndex, val);
        }

        public static string GetPowerRes(double val, string sex)
        {
            return sex == "Мужской" ? GetRes(MinManPowerIndex, MaxManPowerIndex, val) :
                GetRes(MinGirlPowerIndex, MaxGirlPowerIndex, val);
        }

        public static string GetBaseForceRes(double val, string sex)
        {
            return sex == "Мужской" ? GetRes(MinManBaseForceIndex, MaxManBaseForceIndex, val) :
                GetRes(MinGirlBaseForceIndex, MaxGirlBaseForceIndex, val);
        }
    }
}
