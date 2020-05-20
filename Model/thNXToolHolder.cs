using System;
using System.Collections.Generic;
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
        private HolderToolMountType _holderToolMountSubType ;
        private ColletTypeSize _colletSize;

        public enum HolderToolMountType
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

        public HolderToolMountType HolderToolMountSubType => _holderToolMountSubType;

        public ColletTypeSize ColletSize => _colletSize;

        public thNXToolHolder (string [] generalRec, List <string []> holderRec)
        {
            _holderLibraryReference = generalRec[1];
            _recordType = generalRec[2].Trim();
            _holderType = generalRec[3].Trim();
            _subtype = generalRec[4].Trim();
            _sectionNum = generalRec[5].Trim();
            _maxOFfset = generalRec[6].Trim();
            _minDia = generalRec[7].Trim();
            _maxDia = generalRec[8].Trim();
            _description = enc(generalRec[9].Trim());

            if ( holderRec.All(hr => Convert.ToInt32(hr[2]) == 2) 
                          && holderRec.All(i => i.Length == 8))
                      sectionList = holderRec;

            initholderToolMountType();
            
        }

        private void initholderToolMountType()
        {
            if (_description.ToUpper().Contains("ЦАНГОВЫЙ"))
            {
                _holderToolMountSubType = HolderToolMountType.Collet;
                initColletSize();
                return;
            }


            if (_holderLibraryReference.ToUpper().Contains("THERMO") || _holderLibraryReference.ToUpper().Contains("TERM") || _holderLibraryReference.ToUpper().Contains("CELSIO") 
                                                                     || (_holderLibraryReference.ToUpper().Contains("GARANT") && _holderLibraryReference.Contains("ТЕРМОЗАЖИМНОЙ")))
            {
                _holderToolMountSubType = HolderToolMountType.Termo;
                return;
            }

            if (_holderLibraryReference.ToUpper().Contains("HYDRO") || _description.ToUpper().Contains("TENDOZERO"))
            {
                _holderToolMountSubType = HolderToolMountType.Hydro;
                return;
            }

            if ( _description.ToUpper().Contains("DATRON"))
            {
                _holderToolMountSubType = HolderToolMountType.DatronCollet;
                return;
            }

            if (_description.ToUpper().Contains("WELDON"))
            {
                _holderToolMountSubType = HolderToolMountType.Weldon;
                return;
            }

            _holderToolMountSubType = HolderToolMountType.Unknown;
        }

        private void initColletSize()
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