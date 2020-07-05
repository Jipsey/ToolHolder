using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NXOpen;
using NXOpen.CAM;

namespace ToolHolder_NS.Model
{
    public class thNXToolHolder
    {

        private thNXTool _parentTool;
        //------------- General holder data
        private string _holderLibraryReference; // 1-general holder info; 2-all holder data
        private string _recordType;
        private string _holderType;
        private string _subtype;  // for _holderType == 1 0-all milling and drilling holders; _holderType == 2 turning subtypes not defined 
        private string _sectionNum;
        private string _maxOFfset;
        private string _minDia;
        private string _maxDia;        
        private string _description; // TODO: проверить на диаконтовской версии как работает мой энкодинг описания инструмента 

        //-------------All holder data
        private double _lowerDiameter;
        private double _sectionLength;
        private double _taperAngle;
        private double _cornerRadius;
        private double _offset;
        private List<string[]> sectionList;
        private HolderType _holderSubType ;
        private HolderSpindelMountType _holderSpindelMount;
        private ColletTypeSize _colletSize;
        private double _length;

        public enum HolderType
        {  Collet = 0,
           Termo = 1,
           DatronCollet = 2,
           Weldon = 3,
           Hydro = 4,
           Unknown = 10
        }

        public enum ColletTypeSize 
        {
            ER8 =  0,
            ER11 = 1,
            ER16 = 2,
            ER20 = 3,
            ER25 = 4,
            ER32 = 5,
            ER40 = 6,
            unknown = 10
        }

        public enum HolderSpindelMountType
        {
            SK40 = 0,
            HSK63A = 1,
            DatronSpindel = 2,
            Capto4 = 3,
            Capto6 = 4,
            Unknown = 5
        }

        public string HolderLibraryReference
        {
            get { return _holderLibraryReference; }
        }

        public List<string[]> SectionList
        {
            get { return sectionList; }
        }

        public string RecordType => _recordType;

        public string Description => _description;

        public HolderType HolderSubType => _holderSubType;

        public ColletTypeSize ColletSize => _colletSize;

        public double Length => _length;

        public HolderSpindelMountType HolderSpindelMount => _holderSpindelMount;

        public thNXToolHolder (string [] generalRec, List <string []> holderRec)
        {
            _holderLibraryReference = generalRec[1].Trim();
            _recordType = generalRec[2].Trim();
            _holderType = generalRec[3].Trim();
            _subtype = generalRec[4].Trim();
            _sectionNum = generalRec[5].Trim();
            _maxOFfset = generalRec[6].Trim();
            _minDia = generalRec[7].Trim();
            _maxDia = generalRec[8].Trim();
            _description = generalRec[9].Trim();                    //enc(generalRec[9].Trim());

            if ( holderRec.All(hr => Convert.ToInt32(hr[2]) == 2) 
                          && holderRec.All(i => i.Length == 8))
                      sectionList = holderRec;

            InitHolderType();
            InitSpindelMountType();
            CalcLength();
        }

        private void InitHolderType()
        {
            if (_description.ToUpper().Contains("ЦАНГОВЫЙ"))
            {
                _holderSubType = HolderType.Collet;
                InitColletSize();
                return;
            }


            if (_holderLibraryReference.ToUpper().Contains("THERMO") || _holderLibraryReference.ToUpper().Contains("TERM") || _holderLibraryReference.ToUpper().Contains("CELSIO") 
                                                                     || (_holderLibraryReference.ToUpper().Contains("GARANT") && _holderLibraryReference.Contains("ТЕРМОЗАЖИМНОЙ")))
            {
                _holderSubType = HolderType.Termo;
                return;
            }

            if (_holderLibraryReference.ToUpper().Contains("HYDRO") || _description.ToUpper().Contains("TENDOZERO"))
            {
                _holderSubType = HolderType.Hydro;
                return;
            }

            if ( _description.ToUpper().Contains("DATRON"))
            {
                _holderSubType = HolderType.DatronCollet;
                return;
            }

            if (_description.ToUpper().Contains("WELDON"))
            {
                _holderSubType = HolderType.Weldon;
                return;
            }

            _holderSubType = HolderType.Unknown;
        }

        private void InitColletSize()
        {
            int startIndex;

            if (Description.Contains("ER"))
                startIndex = Description.ToUpper().LastIndexOf("ER");
            else if(Description.Contains("-"))
                startIndex = Description.ToUpper().LastIndexOf("-")- 1;
            else
                startIndex = Description.ToUpper().IndexOf("_") - 1;

            string answer = "ER" + Description.Substring(startIndex + 2, 2).Trim();

            switch (answer)
            {
                case "ER8":
                    _colletSize = ColletTypeSize.ER8;
                    break;

                case "ER11":
                    _colletSize = ColletTypeSize.ER11;
                    break;

                case "ER16":
                    _colletSize = ColletTypeSize.ER16;
                    break;

                case "ER20":
                    _colletSize = ColletTypeSize.ER20;
                    break;

                case "ER25":
                    _colletSize = ColletTypeSize.ER25;
                    break;

                case "ER32":
                    _colletSize = ColletTypeSize.ER32;
                    break;

                case "ER40":
                    _colletSize = ColletTypeSize.ER40;
                    break;

                default:
                    _colletSize = ColletTypeSize.unknown;
                    break;
            }
        }

        private void InitSpindelMountType()
        {
            string refer = _holderLibraryReference.ToUpper();
            _holderSpindelMount = refer.Contains("SK40") || _description.Contains("SK40") || _description.Contains("D'ANDREA")  ? HolderSpindelMountType.SK40 :
                refer.Contains("HSK-A_63") || _description.Contains("HSK-A_63") ?  HolderSpindelMountType.SK40 :
                    _description.Contains("DATRON") || _description.Contains("DATRON") ? HolderSpindelMountType.DatronSpindel :
                         refer.Contains("C4") || _description.Contains("C4") ? HolderSpindelMountType.Capto4 :
                              refer.Contains("C6") || _description.Contains("C6") ?  HolderSpindelMountType.Capto6 :
                                  HolderSpindelMountType.Unknown;
        }

        private void CalcLength()
        {
            if (SectionList is null)
                return;

            double answer = 0;
            foreach (var item in SectionList)
            {
                string s = item[5].Trim();
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                answer += Double.Parse(s, formatter);
            }

            _length = answer;
        }


        /// <summary>
        /// метод меняет кодировку в описании инструмента 
        /// </summary>
        /// <param name="entryDesc"></param>
        /// <returns></returns>
        private string enc(string entryDesc)
        {
            return Encoding.Default.GetString(Encoding.GetEncoding(Encoding.Default.CodePage).GetBytes(entryDesc));
        }
    }
}