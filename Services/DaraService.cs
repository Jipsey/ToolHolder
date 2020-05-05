
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NXOpen;
using Microsoft.Win32;
using NXOpen.CAM;
using ToolHolder_NS.Model;
using Operation = NXOpen.CAM.Operation;

//------------------------------------------------------------------------------
//Represents Block Styler application class
//------------------------------------------------------------------------------
public class DataService
{
    private static UI theUI = null;
    static public ToolHolderDialog _dialog;
    private static Session _theSession = null;
    static thNXSession data;
    private string _path = string.Empty;
    private static Dictionary<NCGroup, List<Operation>> _programmCNC = null;
    private static Operation[] operations;
    private static UI _ui;
    private XmlService xmlService;

    private ListingWindow _lw;

    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------

    public DataService(ToolHolderDialog dialogObject) // конструктор
    {
        checkR();

        _theSession = ToolHolderDialog.theSession;
        _ui = ToolHolderDialog.theUI;
        _dialog = dialogObject;
        _lw = _theSession.ListingWindow;
        _dialog.SetVisibleNodes(5);
        data = new thNXSession(_theSession,_ui);
        xmlService = new XmlService(data, _dialog);
    }
    /// <summary>
    /// стоит далее этот метод удалить
    /// </summary>
    void Build_programmCncDict()
        {
            _programmCNC = new Dictionary<NCGroup, List<Operation>>();
            operations = _theSession.Parts.Work.CAMSetup.CAMOperationCollection.ToArray()
                .Where(op => op.GetParent(CAMSetup.View.ProgramOrder).GetType().Name.Equals("NCGroup")).ToArray();

            NCGroup cncGroup = null;
            foreach (var item in operations)
            {
                if (cncGroup == null
                    || !cncGroup.Equals(item.GetParent(CAMSetup.View.ProgramOrder)))
                    cncGroup = item.GetParent(CAMSetup.View.ProgramOrder);
                    
                    if(_programmCNC == null)
                          _programmCNC = new Dictionary<NCGroup, List<Operation>>();

                       if (!_programmCNC.ContainsKey(cncGroup))
                       {
                           _programmCNC.Add(cncGroup, new List<Operation> { item });
                           continue;
                       }
                       
                              var tempoList = _programmCNC[cncGroup];

                          tempoList.Add(item);
                          _programmCNC[cncGroup] = tempoList;
             }
            }

    void checkWCSinDict()
    {
        var lw = _theSession.ListingWindow;
        lw.Open();

        foreach (var key in _programmCNC.Keys)
        {
            string msg = String.Format("        В УП {0} все оперциии с одной WCS!", key.Name);
            var flag = false;
            OrientGeometry wcs = null;
            foreach (var op in _programmCNC[key])
            {
                var parent = op.GetParent(CAMSetup.View.Geometry);
                while (parent.GetType() != typeof(OrientGeometry) && !parent.Name.Equals("NONE"))
                {
                    parent = parent.GetParent();
                }

                if(wcs == null)
                     wcs = (OrientGeometry)parent;
                if (parent != wcs)
                {
                    msg = String.Format("-->ВНИМАНИЕ<-- В УП {0} используются разные WCS !  проверь операцию {1} ", key.Name, op.Name);
                    lw.WriteFullline(String.Format(msg));
                    flag = true;
                }
            }
                    if(!flag)
                    lw.WriteFullline(String.Format(msg));
        }
    }

    //public static void Main()
    //{
    //    sw = new Stopwatch();
    //    sw.Start();
    //    //DataService theDataService = null;
               
    //    try
    //    {
    //        ToolHolderDialog = new ToolHolderDialog();
            
    //        //theDataService._dataService = new DataService(theDataService);
    //    }
    //    catch (Exception ex)
    //    {
    //        theUI.NXMessageBox.Show("Ошибка построения диалога!", NXMessageBox.DialogType.Error, ex.Message);
    //    }
    //    finally
    //    {
    //        if (ToolHolderDialog != null)
    //        {
    //            ToolHolderDialog.Dispose();
    //        }
    //        ToolHolderDialog = null;
    //    }
    //}
    public void Dispose()
    {
        data = null;
        _ui = null;
        operations = null;
        _theSession = null;
        _programmCNC = null;
        theUI = null;
    }




    private void checkR()
    {
        string doe = "day of Exception".GetHashCode().ToString();
        string year = "startLicense".GetHashCode().ToString();

        //дата начала лицензии 10 января 2020г
        DateTime startLicense = new DateTime(2020, 1, 10);


        if (Registry.CurrentUser.OpenSubKey(@"Software\Abstraction") != null)
            using (RegistryKey checkKey = Registry.CurrentUser.OpenSubKey(@"Software\Abstraction"))
            {
                if (checkKey != null && checkKey.GetValue(doe).ToString().Equals("1"))
                    throw new Exception("Internal Exception ;)");
            }


        //если в регистре нет раздела с указаным именем, то создаём его
        if (Registry.CurrentUser.OpenSubKey(@"Software\Intelligence for exist") == null)
            using (RegistryKey createKey = Registry.CurrentUser.CreateSubKey(@"Software\Intelligence for exist"))
            {
                if (createKey != null)
                {
                    createKey.SetValue(year, startLicense.Ticks, RegistryValueKind.QWord);
                }
            }


        using (RegistryKey openKey = Registry.CurrentUser.OpenSubKey(@"Software\Intelligence for exist", false))
        {
            if (openKey != null)
            {
                var yearGot = openKey.GetValue(year).ToString();
                long millisecond;
                long.TryParse(yearGot, out millisecond);

                int diff = DateTime.Today.Subtract(new DateTime(millisecond)).Days;

                if (diff > 220)
                {

                    using (RegistryKey createNewKey = Registry.CurrentUser.CreateSubKey(@"Software\Abstraction"))
                    {
                        if (createNewKey != null)
                            createNewKey.SetValue(doe, true,
                                RegistryValueKind.QWord);
                    }

                    throw new Exception("Internal Exception! ");
                }
            }
        }
    }



#if CALLBACK
    //------------------------------------------------------------------------------
    //Method name: Show_TechDoc
    //------------------------------------------------------------------------------
    public static void Show_TechDoc()
    {
        TechDoc theTechDoc = null;
        try
        {
            theTechDoc = new TechDoc();
            // The following method shows the dialog immediately
            theTechDoc.Show();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        finally
        {
            theTechDoc.Dispose();
            theTechDoc = null;
        }
    }
#endif//CALLBACK
}
