using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NXOpen.CAM;
using NXOpen.UF;
using System.Collections.Generic;

namespace ToolHolder_NS.Model
{
    public class thNXTool : IEquatable<thNXTool>, IComparable
    {
        private Tool _tool;
        public int _toolNumber;
        private thNXToolHolder nxToolHolder;
        private double _diam;
        private double _shankDiam;
        private int _zOffset;
        private string _desc;
        private string _toolName;
        private string _holderLibref;
        private MillToolBuilder _toolBuilder;
        private thNXToolHolder[] possibleChoice;

        public Tool Tool
        {
            get { return _tool; }
            set { _tool = value; }
        }

        public int ToolNumber
        {
            get { return _toolNumber; }
            set { _toolNumber = value; }
        }

        public ToolBuilder Builder
        {
            get
            {
                if (_toolBuilder != null)
                return _toolBuilder ;
                return createBuilder(_tool);
            }
        }

        public thNXToolHolder NxToolHolder
        {
            get { return nxToolHolder; }
        }

        public string HolderLibraryRef => _holderLibref;

        public thNXTool(Tool tool)
        {
            //createBuilder(tool);
            Tool = tool;
            _toolNumber = setToolNumber();
            _toolName = Tool.Name;

            
            thNXSession._ufs.Param.AskStrValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_DESCRIPTION, value: out _desc);
            thNXSession._ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_DIAMETER, value: out _diam);
            thNXSession._ufs.Param.AskStrValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_HOLDER_LIBREF, value: out _holderLibref);
            thNXSession._ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_SHANK_DIA, value: out _shankDiam);


            _zOffset = determinateZOffset(); 

            thNXSession._ufs.Param.AskIntValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_Z_OFFSET, value: out _zOffset);

            if (HolderLibraryRef == null || HolderLibraryRef.Equals(string.Empty))
                nxToolHolder = null;
            else if (thNXSession._toolHolderArrayFromLibrary.Any(holder => holder.HolderLibraryReference.Equals(HolderLibraryRef)))
                nxToolHolder = thNXSession._toolHolderArrayFromLibrary
                       .FirstOrDefault(holder => holder.HolderLibraryReference.Equals(HolderLibraryRef));


            definePossibleList();



            //else nxToolHolder = new thNXToolHolder(this);
            //UFConstants.UF_PARAM_TL_LIBREF;
            //_desc = _toolBuilder.TlDescription;
            //_diam = _toolBuilder.TlDiameterBuilder.Value;


            // if (Builder.HolderLibraryReference == null || Builder.HolderLibraryReference.Equals(string.Empty))
            //    nxToolHolder = null;

            //  else if ( thNXSession._toolHolderArrayFromLibrary.Any(holder => holder.HolderLibraryReference.Equals(Builder.HolderLibraryReference)))
            //              nxToolHolder = thNXSession._toolHolderArrayFromLibrary
            //                     .FirstOrDefault(holder => holder.HolderLibraryReference.Equals(Builder.HolderLibraryReference));
            //  else nxToolHolder = new thNXToolHolder(this);
        }

        private void definePossibleList()
        {
            List<string> tempList = new List<string>();
            var tempArr = thNXSession._toolHolderArrayFromLibrary
                       .Where(holder => holder.RecordType.Equals("1")).ToArray();


            string[] collets = determCollet();
            string[] termoHolder = determTermoHolder(tempArr);
            string[] hydroHolders = determHydroHolders();
            string[] weldonHolder = determWeldonHolders();

            foreach (var itemHolder in tempArr)
            {
                
             if(itemHolder.HolderLibraryReference.Contains("ER8".ToLower()))


            }

        }


        private string[]  determCollet()
        {
            List<string> tempList = new List<string>();

            if(5.0>=_shankDiam && _shankDiam >=1.0)
                  tempList.Add("ER8");
            if (7.0 >= _shankDiam && _shankDiam >= 1.0)
                  tempList.Add("ER11");
            if (10.0 >= _shankDiam && _shankDiam >= 1.0)
                  tempList.Add("ER16");
            if (16.0 >= _shankDiam && _shankDiam >= 1.0)
                  tempList.Add("ER25");
            if (20.0 >= _shankDiam && _shankDiam >= 1.0)
                  tempList.Add("ER32");
            if (26.0 >= _shankDiam && _shankDiam >= 1.0)
                  tempList.Add("ER40");
            return tempList.ToArray();

        }


        private string[] determTermoHolder(thNXToolHolder[] tempArr)
        {

            List<string> tempList = new List<string>();
           var x =  tempArr.Where(s => s.HolderLibraryReference.ToUpper().Contains("THERMO") 
                                          && s.HolderLibraryReference.ToUpper().Contains("D" + _shankDiam)).ToList();

           foreach (var thNxToolHolder in x)
           {
                tempList.Add(thNxToolHolder.HolderLibraryReference);
           }
            return tempList.ToArray();
        }


        private MillToolBuilder createBuilder(Tool tool)
        {
          return thNXSession._workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);          
        }

        private int determinateZOffset()
        {
            //double holderOffset = _toolBuilder.HolderSectionBuilder.TlHolderOffsetBuilder.Value;
            //double length = _toolBuilder.TlHeightBuilder.Value;
            double holderOffset;
            double length ;
            thNXSession._ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_Z_OFFSET, value: out holderOffset);
            thNXSession._ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_HEIGHT, value: out length);

            return (int) (length - holderOffset);
        }


        private int setToolNumber()
        {
            int value;
            thNXSession._ufs.Param.AskIntValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_NUMBER, value: out  value);
            return value;
        }



        /// <summary>
        /// переопределил метод чтобы сравнивать по полю Tool
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Equals(thNXTool obj)
        { 
            thNXTool t = obj;
            return (t.Tool == Tool);
        }

       
        public int CompareTo(object obj)
        {
            thNXTool nxTool = obj as thNXTool;

            if (nxTool != null)
                return ToolNumber.CompareTo(nxTool.ToolNumber);
          
                throw  new Exception("Невозможно сравнить два объекта!");
        }
    }
}