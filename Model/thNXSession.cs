using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NXOpen;
using NXOpen.CAM;
using NXOpen.UF;
using Operation = NXOpen.CAM.Operation;
using Path = System.IO.Path;

namespace ToolHolder_NS.Model
{
    public class thNXSession
    {
        public Session _innerSession;
        public static Part _workPart;
        public static UFSession Ufs;
        private static string _UGII_CAM_LIBRARY_TOOL_DIR;
        private static string _UGII_CAM_LIBRARY_TOOL_METRIC_DIR;
        private static Operation[] operations;
        private thNXTool [] toolArray;
        public static thNXToolHolder [] _toolHolderArrayFromLibrary;
        private Dictionary<string, thNXToolHolder>  _toolHolderDictionary ; 
       

        public thNXSession(Session theSession, UI theUi)
        {
            if(theSession == null) throw new Exception("Сессия не запущена!");
            _innerSession = theSession;

            _workPart = theSession.Parts.Work;
            if(_workPart == null) throw new Exception("Не найден открытая деталь!");

            Ufs = UFSession.GetUFSession();
            if (Ufs == null) throw new Exception("GetUFSession!");

              _UGII_CAM_LIBRARY_TOOL_DIR = theSession.GetEnvironmentVariableValue("UGII_CAM_LIBRARY_TOOL_DIR");
              if(_UGII_CAM_LIBRARY_TOOL_DIR == null)
                  throw new Exception("Не удалось получить путь к библиотеке инструмента!");
              _UGII_CAM_LIBRARY_TOOL_METRIC_DIR = theSession.GetEnvironmentVariableValue("UGII_CAM_LIBRARY_TOOL_METRIC_DIR");
              if (_UGII_CAM_LIBRARY_TOOL_METRIC_DIR == null)
                  throw new Exception("Не удалось получить путь к библиотеке инструмента(metric)!");


              initializeHolderfromLibrary();

              initializeTools();
        }

        public thNXTool[] ToolArray
        {
            get { return toolArray; }
        }

        public Session InnerSession
        {
            get { return _innerSession; }
        }


        private void initializeHolderfromLibrary()
        {           
            string holdersFileName = "holder_database.dat";
            string path = Path.Combine(_UGII_CAM_LIBRARY_TOOL_METRIC_DIR, holdersFileName);
  
            if(! File.Exists(Path.Combine(path)))
                throw new Exception(string.Format("Файл с оправками не существует! \n путь: \"{0}\"", path));
           string[] strings = File.ReadAllLines(path).Where(l => !l.StartsWith("#") && l.StartsWith("DATA")).ToArray();
           var holderList = new List<thNXToolHolder>();
            _toolHolderDictionary = new Dictionary<string, thNXToolHolder>();
               
            var xy = new List<string[]>();
            foreach (var line in strings)
            {
               xy.Add( line.Split('|'));
            }

            foreach (var l in xy)
            {
                if(l.Length != 10)
                    continue;

                 string libRef = l[1];
                 _toolHolderDictionary.Add(libRef, new thNXToolHolder(l, xy.FindAll(i => i.Length == 8 && i[1] == libRef)));
            }

            _toolHolderDictionary = holderList.ToArray().ToDictionary(k => k.HolderLibraryReference);
        }

        private void initializeTools()
        {
            List <thNXTool> list = new  List <thNXTool> ();
            operations = _workPart.CAMSetup.CAMOperationCollection.ToArray()
                .Where(op => op.GetParent(CAMSetup.View.ProgramOrder).GetType().Name.Equals("NCGroup")).ToArray();

            foreach (var operation in operations)
            {
               var tool = operation.GetParent(CAMSetup.View.MachineTool);
               if (tool != null && tool is Tool)
               {
                   thNXTool thNxThNxTool = new thNXTool((Tool)tool);

                   if (!list.Contains(thNxThNxTool))
                       list.Add(thNxThNxTool);
               }
            }
           
            list.Sort();
            toolArray = list.ToArray();
        }

    }
}
