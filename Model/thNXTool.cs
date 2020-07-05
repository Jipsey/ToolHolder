using System;
using System.CodeDom;
using System.Collections;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NXOpen.CAM;
using NXOpen.UF;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NXOpen;
using static System.Double;
using colletTypeSize = ToolHolder_NS.Model.thNXToolHolder.ColletTypeSize;
using Operation = NXOpen.CAM.Operation;

namespace ToolHolder_NS.Model
{
    public class thNXTool : IEquatable<thNXTool>, IComparable
    {
        private Tool _tool;
        private int _toolNumber;
        private thNXToolHolder nxToolHolder;
        private double _diam;
        private double _shankDiam;
        private int _zOffset;
        private string _desc;
        private string _toolName;
        private string _holderLibref;
        private string _currentToolHolderDescr;
        private MillToolBuilder _toolBuilder;
        private thNXToolHolder[] _possibleChoice;
        private bool _hasHolder;

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
                return createBuilder();
            }
        }

        public thNXToolHolder NxToolHolder
        {
            get { return nxToolHolder; }
        }

        public string HolderLibraryRef => _holderLibref;

        public thNXToolHolder[] PossibleChoice => _possibleChoice;

        public double Diam
        {
            get
            {
                return _diam;
            }
        }

        public int ZOffset
        {
            get => _zOffset;
        }

        public string Desc
        {
            get { return _desc; }
        }

        public string CurrentToolHolderDescr => _currentToolHolderDescr;

        /// <summary>
        /// подумать над тем чтобы конструктор не вызывался на каждой операции, а только если лист не содержит данный инструмент
        /// </summary>
        /// <param name="tool"></param>
        public thNXTool(Tool tool, Operation operation)
        {
            //TODO переделать реализацию инициализации поля nxToolHolder
            //GetRequiredParams(tool.Tag, operation);

            Tool = tool;
            _toolNumber = setToolNumber();
            _toolName = Tool.Name;
            thNXSession.Ufs.Param.AskStrValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_DESCRIPTION, value: out var value );
            _desc = enc(value);
            thNXSession.Ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_DIAMETER, value: out _diam);
            thNXSession.Ufs.Param.AskStrValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_HOLDER_LIBREF, value: out _holderLibref);
            thNXSession.Ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: 7358, value: out _shankDiam);
            thNXSession.Ufs.Param.AskStrValue(Tool.GetMembers().FirstOrDefault().Tag, UFConstants.UF_PARAM_TL_HOLDER_DESCRIPTION , out _currentToolHolderDescr);
            _hasHolder = IsToolHolderAppointedTo(tool);

            if (_shankDiam == 0)
                _shankDiam = Diam;

            _zOffset = determinateZOffset();

            //thNXSession.Ufs.Param.AskIntValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_Z_OFFSET, value: out _zOffset);


            nxToolHolder = _hasHolder ? IsToolHolderFromLibrary(tool) ?  setThNxToolHolder() :  null :  null;

            //if (HolderLibraryRef == null || HolderLibraryRef.Equals(string.Empty))
            //    nxToolHolder = null;
            //else if (thNXSession._toolHolderDictionary.Values.Any(holder => holder.HolderLibraryReference.Equals(HolderLibraryRef)))
            //    nxToolHolder = setThNxToolHolder();



            //definePossibleList();
        }

        /// <summary>
        /// возвращет булево значение назначена ли оправка
        /// </summary>
        /// <param name="tool"></param>
        /// <returns></returns>
        private bool IsToolHolderAppointedTo(Tool tool)
        {
            int value;
            thNXSession.Ufs.Param.AskIntValue(tool.Tag,1156,out value);

            int localCnt;
            thNXSession.Ufs.Cutter.AskSectionCount(tool.Tag,out localCnt);
        
            return localCnt != 0;
        }
        /// <summary>
        /// возвращает булеов значение назначенная оправка из библиотеки или внешняя
        /// </summary>
        /// <returns></returns>
        private bool IsToolHolderFromLibrary(Tool tool)
        {
            bool answer = thNXSession._toolHolderDictionary.Values.Any(holder =>
                holder.HolderLibraryReference.Equals(HolderLibraryRef));

            return answer;
        }

        public void definePossibleList()
        {//TODO стоит переделать логику, чтобы не парсить временный список с основной инофой оправок, а сразу сделать его в объекте сессии
            List<string> tempList = new List<string>();
            var tempArr = thNXSession._toolHolderDictionary.Values
                       .Where(holder => holder.RecordType.Equals("1")).ToArray();

            if(PossibleChoice is null)
                 _possibleChoice =  parseChoice(tempArr);
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
                thNXSession.lw.WriteLine(String.Format("номер параметра  {0}; имя атрибута  {1}; тип {2}; ключ {3} ", index, indexAttribute.name, indexAttribute.type, indexAttribute.key));
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


        private MillToolBuilder createBuilder()
        {
           

          return thNXSession.WorkPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(_tool);          
        }

        private int determinateZOffset()
        {
            double holderOffset;
            double length ;
            bool useTaperShank;
            double shankLengh;

            thNXSession.Ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_Z_OFFSET, value: out holderOffset);
            thNXSession.Ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_HEIGHT, value: out length);
            thNXSession.Ufs.Param.AskLogicalValue(Tool.GetMembers().FirstOrDefault().Tag, 7361,out useTaperShank); //метод возвращает булево значение указывающее есть ли хвостовик у инструмента

            if (useTaperShank)
            { thNXSession.Ufs.Param.AskDoubleValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: 7359, value: out shankLengh); // возвращает значение длины хвостовика
                length += shankLengh;
            }

            return (int) (length - holderOffset);
        }


        private int setToolNumber()
        {
            int value;
            thNXSession.Ufs.Param.AskIntValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_NUMBER, value: out  value);
            return value;
        }

        public void setToolHolder(string entryLibRef)
        {
            //int cnt;
            //thNXSession.Ufs.Cutter.AskSectionCount(_tool.Tag, out cnt);
            thNXSession.Ufs.Param.SetStrValue(_tool.Tag, UFConstants.UF_PARAM_TL_HOLDER_LIBREF, entryLibRef);


            if (IsToolHolderAppointedTo(_tool))
                removeHolderSections();

            var possibleHolder = PossibleChoice.Where(th => th.HolderLibraryReference.Equals(entryLibRef)).ToList();
            if (possibleHolder.Count > 0)
                buildHolderSections(possibleHolder.FirstOrDefault(), entryLibRef);

            else new Exception("В массиве возможных державок не найдено ссылочное имя державки!");
        }

        private void removeHolderSections()
        {
            //TODO далее удалить
            int localCnt;
            UFCutter.HolderSection[] hs;
            thNXSession.Ufs.Cutter.AskHolderData(_tool.Tag, out localCnt, out hs);

            for (int i = 0; i < localCnt; i++)
            {
                thNXSession.Ufs.Cutter.DeleteHolderSection(_tool.Tag,0);
            }
        }

        /// <summary>
        /// метод создаёт секции державки
        /// </summary>
        /// <param name="nxToolHolder"></param>
        private void buildHolderSections(thNXToolHolder nxToolHolder, string entryLibRef)
        {
            var shorterSectionList =  parseShorterSectionList(nxToolHolder.SectionList);

 //         UFCutter.HolderSection[] hsArr;
         
          // DIAM LENGTH TAPER CRAD

            foreach (var section in shorterSectionList)
            {
                double length = Parse(section[1], CultureInfo.InvariantCulture);
                if(length <=0)
                    continue;
                UFCutter.HolderSection hs = new UFCutter.HolderSection
                {
                    diameter = Parse(section[0], CultureInfo.InvariantCulture),
                    length = length,
                    taper = Parse(section[2], CultureInfo.InvariantCulture),
                    corner = Parse(section[3], CultureInfo.InvariantCulture)
                };

                thNXSession.Ufs.Cutter.CreateHolderSection(_tool.Tag, ref hs);
            }

            thNXSession.Ufs.Param.SetDoubleValue(_tool.Tag, 1049, 5);
            refreshHolderInfo(entryLibRef);
        }

        /// <summary>
        /// метод назначает ТОЛЬКО объект ThNxToolHolder
        /// </summary>
        /// <returns></returns>
        private thNXToolHolder setThNxToolHolder()
        {
            var localHolder = thNXSession._toolHolderDictionary.Values
                .FirstOrDefault(holder => holder.HolderLibraryReference.Equals(HolderLibraryRef));
            //if( localHolder != null)
            //{
            //    _holderLibref = localHolder.HolderLibraryReference;
            //    _currentToolHolderDescr = localHolder.Description;
            //}
            return localHolder;
        }
        /// <summary>
        /// обновляет поля в объекте thNXTool
        /// </summary>
        /// <param name="entryLibRef"></param>
        private void refreshHolderInfo(string entryLibRef)
        {
            _holderLibref = entryLibRef;
            nxToolHolder = setThNxToolHolder();
            _currentToolHolderDescr = nxToolHolder.Description;
            thNXSession.Ufs.Param.SetStrValue(_tool.Tag, 1064, nxToolHolder.Description); // Назначает описание оправки
        }

        private List<String[]> parseShorterSectionList(List<string[]> sectionList)
        {
            var answer = new List<String[]>();

            if(sectionList.Count > 0 && sectionList != null)
                foreach (var section in sectionList)
                {
                    var valueArray = new String[4];
                    Array.Copy(section,4,valueArray,0,4);
                    answer.Add(valueArray);
                }

            return answer;
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