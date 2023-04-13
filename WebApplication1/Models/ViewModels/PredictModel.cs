using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntexWinter.Models.ViewModels
{
    public class PredictModel
    {
        public string Prediction { get; set; }
        public string Display { get; set; }
        public int Northsouthsquare { get; set; }
        [MaxLength(1)]
        public string Northsouth { get; set; }
        public int Eastwestsquare { get; set; }
        [MaxLength(1)]
        public string Eastwest { get; set; }
        [MaxLength(2)]
        public string Area { get; set; }
        public double Westtohead { get; set; }
        public double Westtofeet { get; set; }
        public double Southtohead { get; set; }
        public double Southtofeet { get; set; }
        public double Depth { get; set; }
        public int Year { get; set; }
        public double Length { get; set; }
        [MaxLength(1)]
        public string Age { get; set; }
        [MaxLength(1)]
        public string Sex { get; set; }
        [MaxLength(1)]
        public string Haircolor { get; set; }
        [MaxLength(2)]
        public string Preservation { get; set; }
        [MaxLength(1)]
        public string Wrapping { get; set; }
        [MaxLength(1)]
        public string Facebundles { get; set; }
        [MaxLength(1)]
        public string Femur { get; set; }
        public double Femurheaddiameter { get; set; }
        public double Femurlength { get; set; }
        [MaxLength(1)]
        public string Humerus { get; set; }
        public double Humerusheaddiameter { get; set; }
        public double Humeruslength { get; set; }
        [MaxLength(1)]
        public string Robust { get; set; }
        [MaxLength(1)]
        public string Supraorbitalridges { get; set; }
        [MaxLength(1)]
        public string Orbitedge { get; set; }
        [MaxLength(1)]
        public string Parietalblossing { get; set; }
        [MaxLength(1)]
        public string Gonion { get; set; }
        [MaxLength(1)]
        public string Nuchalcrest { get; set; }
        [MaxLength(1)]
        public string Zygomaticcrest { get; set; }
        [MaxLength(1)]
        public string Ventralarc { get; set; }
        [MaxLength(1)]
        public string Subpubicangle { get; set; }
        [MaxLength(1)]
        public string Sciaticnotch { get; set; }
        [MaxLength(1)]
        public string Medicalipramus { get; set; }
        [MaxLength(2)]
        public string Function { get; set; }
        [MaxLength(2)]
        public string Thickness { get; set; }
        [MaxLength(1)]
        public string Angle { get; set; }
        [MaxLength(2)]
        public string Manipulation { get; set; }
        [MaxLength(1)]
        public string Material { get; set; }
        [MaxLength(1)]
        public string Yarndirection { get; set; }

        public string Headdirection { get; set; }

        public PredictModel()
        {
            Northsouthsquare = 200;
            Northsouth = "N";
            Eastwestsquare = 30;
            Eastwest = "E";
            Area = "NE";
            Westtohead = 1.6;
            Westtofeet = 1.6;
            Southtohead = 0.6;
            Length = 1.6;
            Year = 1998;
            Depth = 2.03;
            Southtofeet = 1.6;
            Prediction = "";
            Display = "None";
            Sex = "U";
            Age = "A";
            Facebundles = "Y";
            Wrapping = "B";
            Haircolor = "B";
            Function = "F";
            Thickness = "F";
            Angle = "U";
            Manipulation = "U";
            Material = "L";
            Yarndirection = "U";
            Preservation = "P";
            Facebundles = "U";




        }
    }
}
