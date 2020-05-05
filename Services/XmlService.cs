﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NXOpen;
using ToolHolder_NS.Model;

class XmlService
{
    private static string path;
            //"F:\\Store\\SANEK\\Volume\\NXOpen\\5_ToolHolder\\GUI\\forXMLCreator\\TEST_ToolHolderDialog.dlx";

        private static string _tempoXmlDocPath;
        //    "F:\\Store\\SANEK\\Volume\\NXOpen\\5_ToolHolder\\GUI\\forXMLCreator\\newTEST_ToolHolderDialog.dlx";

        private thNXSession _theSession;
        private thNXTool[] _nxTools;
        private int counter = 1;
        static Dictionary<string,int> dictionary = new Dictionary<string, int>();
        static string[] nameArrays = {
            "explorerNode",
            "stringLabelTool",
            "doubleToolDiam",
            "separator011",
            "offsetOfTool",
            "separator02",
            "multiline_string01",
            "separator03",
            "stringLabelHolder",
            "separator04",
            "stringLibRef1",
            "separator05",
            "multiline_string0",
            "separator06",
            "tree_control0",
        };


        public XmlService(thNXSession thSession, ToolHolderDialog dialogObject)
        {
            _nxTools = thSession.ToolArray;
            _theSession = thSession;
            path = dialogObject.TheDlxFileName;
            _tempoXmlDocPath = _getPathOfProject() + "\\doNotDeleteMe.dlx";
            
            if (!File.Exists(path))
                throw  new Exception("Файл диалога не существует!");
           

            buildDLXFile();


            
           // xmlDoc.Save(_tempoXmlDocPath);
        }


        private void buildDLXFile()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlDocument tempoXMLDocument = new XmlDocument();

            XmlElement xRoot = xmlDoc.DocumentElement;
            
            addNodes(xRoot);
           
            tempoXMLDocument.Save(_tempoXmlDocPath);
        }

        private void addNodes(XmlElement xRoot)
        {
            XmlNode seekTree = xRoot.ChildNodes.OfType<XmlNode>().ToArray()
                .Where(it => (it.Attributes.Count == 13 && it.Name.Equals("item")) &&
                             (it.Attributes.Item(0).Name.Equals("ContainerItems") &&
                              it.Attributes.Item(5).Name.Equals("containerBrowserName"))).FirstOrDefault()
                .FirstChild.FirstChild.FirstChild.FirstChild;

            XmlNode nodeDuplicate = seekTree.CloneNode(true);
            for (int i = 0; i < _nxTools.Length; i++)
            {
                nodeDuplicate = shiftAttribute(nodeDuplicate);
                seekTree.ParentNode.InsertAfter(nodeDuplicate, seekTree);
            }
        }

        /// <summary>
        /// метод к значениям аттрибутов входящих в массив nameArrays прибавляет значение переменной counter
        /// </summary>
        /// <param name="entryNode"></param>
        /// <returns></returns>
         XmlNode shiftAttribute(XmlNode entryNode)
        {
            foreach (XmlAttribute attribute in entryNode.Attributes)
            {
                if (nameArrays.Contains(attribute.Value))
                    attribute.Value += counter;
            }
             if(entryNode.HasChildNodes)
                 foreach (XmlNode child in entryNode.ChildNodes)
                 {
                     shiftAttribute(child);
                 }

            counter++;
            return entryNode;
        }


        private string _getPathOfProject()
        {
            string Part_RootPath = _theSession._innerSession.Parts.Display.FullPath;
            int endIndex = Part_RootPath.LastIndexOf(_theSession._innerSession.Parts.Display.Name + ".prt");

            return Part_RootPath.Substring(0, endIndex);
        }


         void compareXML(XmlNode primary, XmlNode secondary, string path)
        {

            int level = dictionary.Count;
            char separator = '/';

            if (!dictionary.ContainsKey(path))
            {
                dictionary.Add(path, counter++);

                Console.WriteLine(String.Format("{0} {1}------- уровень вложености {2}------- ", separator, path,
                    dictionary.Count));
            }

            else
                level = dictionary[path];

            Console.WriteLine(String.Format("{0} {1} vs {2} ",level, primary.Name, secondary.Name));


            if (primary.Attributes != null && secondary.Attributes != null)
                switch (primary.Attributes.Count.CompareTo(primary.Attributes.Count))
                {
                    case 0:
                        compareAttXmlNode(primary.Attributes.OfType<XmlAttribute>().ToArray(),
                                secondary.Attributes.OfType<XmlAttribute>().ToArray(), level); 
                        break;
                    case 1:
                        Console.WriteLine(String.Format("{0} в {1} атрибутов больше чем в {2}",level,primary.Name,secondary.Name));
                        break;
                    case -1:
                        Console.WriteLine(String.Format("{0} в {2} атрибутов больше чем в {1}",level, primary.Name, secondary.Name));
                        break;
                }
            else
            {
                Console.WriteLine(String.Format("{0} Объекты {1} и {2} без атрибутов",level, primary.NamespaceURI,secondary.NamespaceURI));
            }


            if (primary.HasChildNodes)
            {
                if (secondary.HasChildNodes)
                {
                    switch (primary.ChildNodes.Count.CompareTo(secondary.ChildNodes.Count))
                    {
                        case 0:
                            for (int i = 0; i < primary.ChildNodes.Count; i++)
                            {
                                compareXML(primary.ChildNodes[i], secondary.ChildNodes[i], path + separator + primary.ChildNodes[i].LocalName);
                            }

                            break;
                        case 1:
                            Console.WriteLine(String.Format("{0} уровень. В {1} вложеных объектов больше чем в {2}. На {3} штук",level,primary.Name, secondary.Name, primary.ChildNodes.Count - secondary.ChildNodes.Count));
                            break;
                        case -1:
                            Console.WriteLine(String.Format("{0} уровень. В {2} вложеных объектов больше чем в {1}. На {3} штук", level, primary.Name, secondary.Name, secondary.ChildNodes.Count - primary.ChildNodes.Count));
                            break;
                    }
                }
            }
        }


        static void compareAttXmlNode(XmlAttribute[] primaryArray, XmlAttribute[] secondaryArray, int level)
        {
            foreach (XmlAttribute primaryAtt in primaryArray)
            {
                if (secondaryArray.Contains(primaryAtt, new Comparer()))
                    foreach (var secondaryAttribute in secondaryArray)
                    {
                        if (secondaryAttribute.Name.Equals(primaryAtt.Name) &&
                            !secondaryAttribute.Value.Equals(primaryAtt.Value))
                        {
                            Console.WriteLine(String.Format("{0}атрибуы под именем {1} имеют разные значения",level,secondaryAttribute.Name));
                        }
                    }
                else
                  Console.WriteLine(string.Format("{0}для атрибута {1} не найден атрибут с таким же именем ",level, path + primaryAtt.Name));
            }
        }


        static string putIndent(int numberOfIndent)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < numberOfIndent; i++)
            {
                sb.Append('\t');
            }

            return sb.ToString();
        }
    }


    class Comparer : IEqualityComparer<XmlAttribute>
    {
        public bool Equals(XmlAttribute x, XmlAttribute y)
        {
            if (x.Name.Equals(y.Name))
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(XmlAttribute codeh)
        {
            return 0;
        }
    }

