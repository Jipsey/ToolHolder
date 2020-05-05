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
        private int _zOffset;
        private string _desc;
        private string _toolName;
        private ToolBuilder _toolBuilder;

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
                return null;
            }
        }

        public thNXTool(Tool tool)
        {
            Tool = tool;
            _toolNumber = setToolNumber();
            initToolBuilder(tool);
            if (Builder.HolderLibraryReference == null || Builder.HolderLibraryReference.Equals(string.Empty))
                nxToolHolder = null;

            else if ( thNXSession._toolHolderArrayFromLibrary.Any(holder => holder.HolderLibraryReference.Equals(Builder.HolderLibraryReference)))
                        nxToolHolder = thNXSession._toolHolderArrayFromLibrary
                               .FirstOrDefault(holder => holder.HolderLibraryReference.Equals(Builder.HolderLibraryReference));
            else nxToolHolder = new thNXToolHolder(this);
        }

        private void initToolBuilder(Tool tool)
        {
          _toolBuilder = thNXSession._workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool);
          
        }




        private int setToolNumber()
        {
            int value;
            thNXSession.
                Ufs.Param.AskIntValue(param_tag: Tool.GetMembers().FirstOrDefault().Tag, param_index: UFConstants.UF_PARAM_TL_NUMBER, value: out  value);
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