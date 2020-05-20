using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        public Session InnerSession;
        public static Part WorkPart;
        public static UFSession Ufs;
        private static string UGII_CAM_LIBRARY_TOOL_DIR;
        private static string UGII_CAM_LIBRARY_TOOL_METRIC_DIR;
        private static Operation[] operations;
        private thNXTool [] toolArray;
        // public static thNXToolHolder [] _toolHolderArrayFromLibrary;
        public static Dictionary<string, thNXToolHolder>  _toolHolderDictionary ; 
        public static ListingWindow lw ;
        public static string Machine; 
        public static string ToolCarrier;


        public thNXSession(Session theSession, UI theUi)
        {
            if(theSession == null) throw new Exception("Сессия не запущена!");
            InnerSession = theSession;

            WorkPart = theSession.Parts.Work;
            if(WorkPart == null) throw new Exception("Не найден открытая деталь!");

            Ufs = UFSession.GetUFSession();
            if (Ufs == null) throw new Exception("GetUFSession!");

              UGII_CAM_LIBRARY_TOOL_DIR = theSession.GetEnvironmentVariableValue("UGII_CAM_LIBRARY_TOOL_DIR");
              if(UGII_CAM_LIBRARY_TOOL_DIR == null)
                  throw new Exception("Не удалось получить путь к библиотеке инструмента!");
              UGII_CAM_LIBRARY_TOOL_METRIC_DIR = theSession.GetEnvironmentVariableValue("UGII_CAM_LIBRARY_TOOL_METRIC_DIR");
              if (UGII_CAM_LIBRARY_TOOL_METRIC_DIR == null)
                  throw new Exception("Не удалось получить путь к библиотеке инструмента(metric)!");

              initilizeMachineType();

              lw = theSession.ListingWindow;

              lw.Open();
              var startTime = System.Diagnostics.Stopwatch.StartNew();

              lw.WriteLine("Начинаем отсчёт времени выоления метода");

             // initializeHolderfromLibrary();

            //  startTime.Stop();
          //    var result = startTime.Elapsed;


           //   lw.WriteLine(String.Format("Время работы initializeHolderfromLibrary() {0} секунд ", result.Seconds));

              startTime.Reset();
              startTime.Start();

              initHolderFromLibraryByStreamReader();

              startTime.Stop();
              var result = startTime.Elapsed;

              lw.WriteLine(String.Format("Время работы initHolderFromLibraryByStreamReader() {0} секунд ", result.Seconds));


              startTime.Reset();
              startTime.Start();

              initializeTools();

              startTime.Stop();
              result = startTime.Elapsed;

              lw.WriteLine(String.Format("Время работы initializeTools() {0} секунд ", result.Seconds));
        }

        private void initilizeMachineType()
        {
            // TODO: добавить реализацию метода определяющего тип станка, что будет определять работу утилиты
            
          
        }

        private void initHolderFromLibraryByStreamReader()
        {
            List<string> stringList = new List<string>();
            string holdersFileName = "holder_database.dat";
            string path = Path.Combine(UGII_CAM_LIBRARY_TOOL_METRIC_DIR, holdersFileName);
            if (!File.Exists(Path.Combine(path)))
                throw new Exception(string.Format("Файл с оправками не существует! \n путь: \"{0}\"", path));
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                string line;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if(!line.StartsWith("#") && line.StartsWith("DATA"))
                            stringList.Add(line);
                }
            }

            string[] strings = stringList.ToArray();

            var holderList = new List<thNXToolHolder>();
            _toolHolderDictionary = new Dictionary<string, thNXToolHolder>();

            var xy = new List<string[]>();
            foreach (var line in strings)
            {
                xy.Add(line.Split('|'));
            }
          

            foreach (var l in xy)
            {
                if (l.Length != 10)
                    continue;

                string libRef = l[1];
                holderList.Add(new thNXToolHolder(l, xy.FindAll(i => i.Length == 8 && i[1] == libRef)));
             
            }

            _toolHolderDictionary = holderList.ToArray().ToDictionary(k => k.HolderLibraryReference);
        }

        public thNXTool[] ToolArray
        {
            get { return toolArray; }
        }

        public Dictionary<string, thNXToolHolder> ToolHolderDictionary => _toolHolderDictionary;



        private void initializeHolderfromLibrary()
        {           
            string holdersFileName = "holder_database.dat";
            string path = Path.Combine(UGII_CAM_LIBRARY_TOOL_METRIC_DIR, holdersFileName);
  
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
                holderList.Add(new thNXToolHolder(l, xy.FindAll(i => i.Length == 8 && i[1] == libRef)));
                //ToolHolderDictionary.Add(libRef, new thNXToolHolder(l, xy.FindAll(i => i.Length == 8 && i[1] == libRef)));
            }

            _toolHolderDictionary = holderList.ToArray().ToDictionary(k => k.HolderLibraryReference);
        }

        private void initializeTools()
        {
            List <thNXTool> list = new  List <thNXTool> ();
            operations = WorkPart.CAMSetup.CAMOperationCollection.ToArray()
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

            initToolCarrier();

        }

        private void initToolCarrier()
        {
           //TODO: добавить реалтзацию метода, определяющего магазин(-ы)


        }
    }
}
