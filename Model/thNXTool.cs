using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NXOpen.CAM;
using NXOpen.UF;
using System.Collections.Generic;
using System.Text;
using colletTypeSize = ToolHolder_NS.Model.thNXToolHolder.ColletTypeSize;

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

        public thNXToolHolder[] PossibleChoice => possibleChoice;

        /// <summary>
        /// подумать над тем чтобы конструктор не вызывался на каждой операции, а только если лист не содержит данный инструмент
        /// </summary>
        /// <param name="tool"></param>
        public thNXTool(Tool tool)
        {

           // GetRequiredParams(tool.Tag, operation);

            Tool = tool;
            _toolNumber = setToolNumber();
            _toolName = Tool.Name;
            thNXSession.Ufs.Param.AskStrValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_DESCRIPTION, value: out var value );
            thNXSession.Ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_DIAMETER, value: out _diam);
            thNXSession.Ufs.Param.AskStrValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_HOLDER_LIBREF, value: out _holderLibref);
            thNXSession.Ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: 7358, value: out _shankDiam);
            if (_shankDiam == 0)
                _shankDiam = _diam;

            _zOffset = determinateZOffset(); 

            thNXSession.Ufs.Param.AskIntValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_Z_OFFSET, value: out _zOffset);

            if (HolderLibraryRef == null || HolderLibraryRef.Equals(string.Empty))
                nxToolHolder = null;
            else if (thNXSession._toolHolderDictionary.Values.Any(holder => holder.HolderLibraryReference.Equals(HolderLibraryRef)))
                nxToolHolder = thNXSession._toolHolderDictionary.Values
                       .FirstOrDefault(holder => holder.HolderLibraryReference.Equals(HolderLibraryRef));

            _desc = enc(value);

            //definePossibleList();
        }

        public void definePossibleList()
        {//TODO стоит переделать логику, чтобы не парсить временный список с основной инофой оправок, а сразу сделать его в объекте сессии
            List<string> tempList = new List<string>();
            var tempArr = thNXSession._toolHolderDictionary.Values
                       .Where(holder => holder.RecordType.Equals("1")).ToArray();

            //thNXToolHolder[] collets = determCollet(tempArr);
            //thNXToolHolder[] termoHolder = determTermoHolder(tempArr);
            //thNXToolHolder[] hydroHolders = determHydroHolders(tempArr);
            //thNXToolHolder[] weldonHolders = determWeldonHolders(tempArr);
            //thNXToolHolder[] datronCollets = determDatronCollets(tempArr);
            if(PossibleChoice is null)
                 possibleChoice =  parseChoice(tempArr);

        }

        private thNXToolHolder[] parseChoice(thNXToolHolder[] tempArr)
        {

            List<thNXToolHolder> answerArray = new List<thNXToolHolder>();

            if (5.0>=_shankDiam && _shankDiam >=1.0)
                answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER8).ToArray());
            if (7.0 >= _shankDiam && _shankDiam >= 1.0)
                answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER11).ToArray());
            if (10.0 >= _shankDiam && _shankDiam >= 1.0)
                answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER16).ToArray());
            if (13.0 >= _shankDiam && _shankDiam >= 1.0)
                answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER20).ToArray());
            if (16.0 >= _shankDiam && _shankDiam >= 1.0)
                answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER25).ToArray());
            if (20.0 >= _shankDiam && _shankDiam >= 2.0)
                answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER32).ToArray());
            if (26.0 >= _shankDiam && _shankDiam >= 3.0)
                answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER40).ToArray());


            answerArray.AddRange(tempArr.Where(h => (h.HolderSubType == thNXToolHolder.HolderType.Termo || h.HolderSubType == thNXToolHolder.HolderType.Weldon
                                                                                                        || h.HolderSubType == thNXToolHolder.HolderType.Hydro || h.HolderSubType == thNXToolHolder.HolderType.DatronCollet)
                                                    && (h.HolderLibraryReference.ToUpper().Contains("D" + _shankDiam) || h.HolderLibraryReference.ToUpper().Contains("_" + _shankDiam))).ToList());

            return answerArray.ToArray();
        }



        private void GetRequiredParams(NXOpen.Tag tag, Operation operation)
        {
            if (thNXSession.Ufs == null) return;
            int cnt;
            int[] indices;

            thNXSession.Ufs.Param.AskRequiredParams(tag, out cnt, out indices);

            foreach (var index in indices)
            {
                UFParam.IndexAttribute indexAttribute;
                UFParam.Status paramStatus;
                
                thNXSession.Ufs.Param.AskParamAttributes(index,out indexAttribute);
                thNXSession.lw.WriteLine(String.Format("номер параметра  {0}; имя атрибута  {1} ", index, indexAttribute.name));
            }

            thNXSession.lw.WriteLine(new string('*',80));

            thNXSession.Ufs.Param.AskRequiredParams(operation.Tag, out cnt, out indices);

            foreach (var index in indices)
            {
                UFParam.IndexAttribute indexAttribute;
                UFParam.Status paramStatus;

                thNXSession.Ufs.Param.AskParamAttributes(index, out indexAttribute);
                thNXSession.lw.WriteLine(String.Format("номер параметра  {0}; имя атрибута  {1} ", index, indexAttribute.name));
            }
        }


        private thNXToolHolder[]  determCollet(thNXToolHolder[] tempArr)
        {
            List<thNXToolHolder> answerArray = new List<thNXToolHolder>();


            if (5.0>=_shankDiam && _shankDiam >=1.0)
                  answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER8).ToArray());
            if (7.0 >= _shankDiam && _shankDiam >= 1.0)
                  answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER11).ToArray());
            if (10.0 >= _shankDiam && _shankDiam >= 1.0)
                  answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER16).ToArray());
            if (13.0 >= _shankDiam && _shankDiam >= 1.0)
                answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER20).ToArray());
            if (16.0 >= _shankDiam && _shankDiam >= 1.0)
                  answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER25).ToArray());
            if (20.0 >= _shankDiam && _shankDiam >= 2.0)
                  answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER32).ToArray());
            if (26.0 >= _shankDiam && _shankDiam >= 3.0)
                  answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Collet && h.ColletSize == colletTypeSize.ER40).ToArray());

            return answerArray.ToArray();
        }


        private thNXToolHolder[] determTermoHolder(thNXToolHolder[] tempArr)
        {
            List<thNXToolHolder> answerArray = new List<thNXToolHolder>();
           
           answerArray.AddRange( tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Termo 
                                                                && (h.HolderLibraryReference.ToUpper().Contains("D" + _shankDiam) || h.HolderLibraryReference.ToUpper().Contains("_" + _shankDiam))).ToList());

           return answerArray.ToArray();
        }

        private thNXToolHolder[] determHydroHolders(thNXToolHolder[] tempArr)
        {
            List<thNXToolHolder> answerArray = new List<thNXToolHolder>();
            answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Hydro
                                                    && (h.Description.ToUpper().Contains("D" + _shankDiam) || h.Description.ToUpper().Contains("_" + _shankDiam))).ToList());

            return answerArray.ToArray();
        }

        private thNXToolHolder[] determWeldonHolders(thNXToolHolder[] tempArr)
        {
            List<thNXToolHolder> answerArray = new List<thNXToolHolder>();
            answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.Weldon && (h.HolderLibraryReference.ToUpper().Contains("D" + _shankDiam) 
                                                                                                            || h.HolderLibraryReference.ToUpper().Contains("_" + _shankDiam))).ToList());

            return answerArray.ToArray();
        }


        private thNXToolHolder[] determDatronCollets(thNXToolHolder[] tempArr)
        {

            List<thNXToolHolder> answerArray = new List<thNXToolHolder>();
            answerArray.AddRange(tempArr.Where(h => h.HolderSubType == thNXToolHolder.HolderType.DatronCollet && (h.HolderLibraryReference.ToUpper().Contains("D" + _shankDiam)
                                                                                                                  || h.Description.ToUpper().Contains("D" + _shankDiam))).ToList());

            return answerArray.ToArray();
        }


        private MillToolBuilder createBuilder(Tool tool)
        {
          return thNXSession.WorkPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);          
        }

        private int determinateZOffset()
        {
            double holderOffset;
            double length ;
            thNXSession.Ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_Z_OFFSET, value: out holderOffset);
            thNXSession.Ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_HEIGHT, value: out length);

            return (int) (length - holderOffset);
        }


        private int setToolNumber()
        {
            int value;
            thNXSession.Ufs.Param.AskIntValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_NUMBER, value: out  value);
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


        private string enc(string entryDesc)
        {
             return Encoding.UTF8.GetString(Encoding.Default.GetBytes(entryDesc));
            //return Encoding.Default.GetString(Encoding.GetEncoding(Encoding.Default.CodePage).GetBytes(entryDesc));
        }
    }
}