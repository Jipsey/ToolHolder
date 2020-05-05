using System;
using System.Collections.Generic;
using System.Linq;
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
        private string _descriprion;

        //-------------All holder data
        private double _lowerDiameter;
        private double _sectionLength;
        private double _taperAngle;
        private double _cornerRadius;
        private double _offset;
        private List<string[]> sectionList;

        public string HolderLibraryReference
        {
            get { return _holderLibraryReference; }
        }

        public List<string[]> SectionList
        {
            get { return sectionList; }
        }

        public thNXToolHolder (string [] generalRec, List <string []> holderRec)
        {
            _holderLibraryReference = generalRec[1];
            _recordType = generalRec[2];
            _holderType = generalRec[3];
            _subtype = generalRec[4];
            _sectionNum = generalRec[5];
            _maxOFfset = generalRec[6];
            _minDia = generalRec[7];
            _maxDia = generalRec[8];
            _descriprion = generalRec[9];

            if ( holderRec.All(hr => Convert.ToInt32(hr[2]) == 2) 
                          && holderRec.All(i => i.Length == 8))
                      sectionList = holderRec;
        }
        public thNXToolHolder(thNXTool nxTool)
        {
            _parentTool = nxTool;
            _holderLibraryReference = nxTool.Builder.HolderLibraryReference;

            parseByLibRef();
        }



        private void parseByLibRef()
        {
          //  throw new NotImplementedException();
        }
    }
}