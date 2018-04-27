using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SelfControlDiary.Models;

namespace SelfControlDiary.ViewModels
{
    public class IndicatorsListViewModel
    {
        public Student Stud { get; set; }
        public int Age { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Semestr { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int JEL { get; set; }
        public int BaseForce { get; set; }
        public int LeftWrist { get; set; }
        public int RightWrist { get; set; }
        public int Pause { get; set; }
        public int Breath { get; set; }
        public int Exhalation { get; set; }
        public int ChSS { get; set; }
        public int ADS { get; set; }
        public int ADD { get; set; }
        public int Genchi { get; set; }
        public int Shtange { get; set; }
        public int CCC { get; set; }
        public int Stat { get; set; }

        public double GetVRIndex()
        {
            return Math.Round((double)(Weight * 1000 / Height), 2);
        }

        public double GetLifeIndex()
        {
            return Math.Round(((double)JEL / Weight), 2);
        }

        public double GetLeftWristPowerIndex()
        {
            return Math.Round(((double)LeftWrist / Weight * 100), 2);
        }

        public double GetRightWristPowerIndex()
        {
            return Math.Round(((double)RightWrist / Weight * 100), 2);
        }

        public double GetBaseForceIndex()
        {
            return Math.Round(((double)BaseForce / Weight * 100), 2);
        }

        public double GetAPSKIndex()
        {
            return Math.Round((0.011 * ChSS + 0.014 * (ADS + Age) + 
                0.008 * ADD + 0.009 * (Weight - Height) - 0.27), 2);
        }

        public double GetKremptonIndex()
        {
            return Math.Round((3.15 + ADS - (double)ChSS / 20), 2);
        }

        public double GetKerdoIndex()
        {
            return Math.Round(((1 - (double)ADD/ChSS) * 100), 2);
        }

        public double GetStaminaCoef()
        {
            return Math.Round(((double)ChSS * 10 / (ADS - ADD)), 2);
        }

        public double GetUFS()
        {
            return Math.Round(((700 - 3 * ChSS - 2.5 * ((double)(ADS - ADD) / 3 + ADD) - 2.7 * Age
                + 0.28 * Weight)/(350 + 0.21 * Height - 2.6 * Age)), 2);
        }

        public double GetRobinsonIndex()
        {
            return ((double)ChSS * ADS / 100);
        }

        public double GetWeightIndex()
        {
            return Math.Round((Weight / ((double)Height * Height / 10000)), 2);
        }

        public string GetVRVerdict()
        {
            return IndicatorRatesNorms.GetVRRes(GetVRIndex(), Stud.Sex);
        }

        public string GetLifeVerdict()
        {
            return IndicatorRatesNorms.GetLifeRes(GetLifeIndex(), Stud.Sex);
        }

        public string GetLeftWristPowerVerdict()
        {
            return IndicatorRatesNorms.GetPowerRes(GetLeftWristPowerIndex(), Stud.Sex);
        }

        public string GetRightWristPowerVerdict()
        {
            return IndicatorRatesNorms.GetPowerRes(GetRightWristPowerIndex(), Stud.Sex);
        }

        public string GetBaseForceVerdict()
        {
            return IndicatorRatesNorms.GetBaseForceRes(GetBaseForceIndex(), Stud.Sex);
        }

        public string GetAPSKVerdict()
        {
            return IndicatorRatesNorms.GetAPSKRes(GetAPSKIndex());
        }

        public string GetKremptonVerdict()
        {
            return IndicatorRatesNorms.GetKrempInd(GetKremptonIndex());
        }

        public string GetKerdoVerdict()
        {
            return IndicatorRatesNorms.GetVIK(GetKerdoIndex());
        }

        public string GetStaminaCoefVerdict()
        {
            return IndicatorRatesNorms.GetStaminaCoefRes(GetStaminaCoef());
        }

        public string GetUFSVerdict()
        {
            return IndicatorRatesNorms.GetUFSRes(GetUFS());
        }

        public string GetRobinsonVerdict()
        {
            return IndicatorRatesNorms.GetPDPRes(GetRobinsonIndex());
        }

        public string GetWeightVerdict()
        {
            return IndicatorRatesNorms.GetIMTRes(GetWeightIndex());
        }

        public string GetGenchiVerdict()
        {
            return IndicatorRatesNorms.GetGenchiRes(Genchi);
        }

        public string GetShtangeVerdict()
        {
            return IndicatorRatesNorms.GetShtangeRes(Shtange);
        }

        public string GetCCCVerdict()
        {
            return IndicatorRatesNorms.GetCCCRes(CCC);
        }

        public string GetStatVerdict()
        {
            return IndicatorRatesNorms.GetStatRes(Stat);
        }
    }
}
