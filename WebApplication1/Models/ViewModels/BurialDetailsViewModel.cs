using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexWinter.Models.ViewModels
{
    public class BurialDetailsViewModel
    {
        public long Id { get; set; }
        public string SquareNorthSouth { get; set; }
        public string HeadDirection { get; set; }
        public string Sex { get; set; }
        public string NorthSouth { get; set; }
        public string Depth { get; set; }
        public string EastWest { get; set; }
        public string AdultSubadult { get; set; }
        public string FaceBundles { get; set; }
        public string SouthToHead { get; set; }
        public string Preservation { get; set; }
        public string FieldBookPage { get; set; }
        public string SquareEastWest { get; set; }
        public string Goods { get; set; }
        public string Text { get; set; }
        public string Wrapping { get; set; }
        public string HairColor { get; set; }
        public string WestToHead { get; set; }
        public string SamplesCollected { get; set; }
        public string Area { get; set; }
        public string Length { get; set; }
        public string BurialNumber { get; set; }
        public string DataExpertInitials { get; set; }
        public string WestToFeet { get; set; }
        public string AgeAtDeath { get; set; }
        public string SouthToFeet { get; set; }
        public string ExcavationRecorder { get; set; }
        public string Photos { get; set; }
        public string Hair { get; set; }
        public string BurialMaterials { get; set; }
        public DateTime? DateOfExcavation { get; set; }
        public string FieldBookExcavationYear { get; set; }
        public string ClusterNumber { get; set; }
        public string ShaftNumber { get; set; }

        public BurialDetailsViewModel(Burialmain burial)
        {
            Id = burial.Id;
            SquareNorthSouth = burial.Squarenorthsouth;
            HeadDirection = burial.Headdirection;
            Sex = burial.Sex;
            NorthSouth = burial.Northsouth;
            Depth = burial.Depth;
            EastWest = burial.Eastwest;
            AdultSubadult = burial.Adultsubadult;
            FaceBundles = burial.Facebundles;
            SouthToHead = burial.Southtohead;
            Preservation = burial.Preservation;
            FieldBookPage = burial.Fieldbookpage;
            SquareEastWest = burial.Squareeastwest;
            Goods = burial.Goods;
            Text = burial.Text;
            Wrapping = burial.Wrapping;
            HairColor = burial.Haircolor;
            WestToHead = burial.Westtohead;
            SamplesCollected = burial.Samplescollected;
            Area = burial.Area;
            Length = burial.Length;
            BurialNumber = burial.Burialnumber;
            DataExpertInitials = burial.Dataexpertinitials;
            WestToFeet = burial.Westtofeet;
            AgeAtDeath = burial.Ageatdeath;
            SouthToFeet = burial.Southtofeet;
            ExcavationRecorder = burial.Excavationrecorder;
            Photos = burial.Photos;
            Hair = burial.Hair;
            BurialMaterials = burial.Burialmaterials;
            DateOfExcavation = burial.Dateofexcavation;
            FieldBookExcavationYear = burial.Fieldbookexcavationyear;
        }
    }
}
