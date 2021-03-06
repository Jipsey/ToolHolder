// NX 12.0.2.9
// Journal created by Sanek on Sun Apr 26 13:27:06 2020 RTZ 2 (зима)

//
using System;
using NXOpen;

public class NXJournal
{
  public static void Main(string[] args)
  {
    NXOpen.Session theSession = NXOpen.Session.GetSession();
    NXOpen.Part workPart = theSession.Parts.Work;
    NXOpen.Part displayPart = theSession.Parts.Display;
    NXOpen.Session.UndoMarkId markId1;
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Edit MILLR_D16_Z5_12_37_120_S16_");
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.CAM.Tool tool1 = (NXOpen.CAM.Tool)workPart.CAMSetup.CAMGroupCollection.FindObject("MILLR_D16_Z5_12_37_120_S16_");
    NXOpen.CAM.MillToolBuilder millToolBuilder1;
    millToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool1);
    
    theSession.SetUndoMarkName(markId2, "Milling Tool-5 Parameters Dialog");
    
    // ----------------------------------------------
    //   Dialog Begin Milling Tool-5 Parameters
    // ----------------------------------------------
    NXOpen.NXObject nXObject1;
    nXObject1 = millToolBuilder1.Commit();
    
    millToolBuilder1.CutterExportBuilder.HolderSaveFlag = NXOpen.CAM.CutterExport.SaveFlags.CreateNew;
    
    millToolBuilder1.CutterExportBuilder.ExportHolder();
    
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters");
    
    theSession.DeleteUndoMark(markId3, null);
    
    NXOpen.Session.UndoMarkId markId4;
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters");
    
    theSession.DeleteUndoMark(markId4, null);
    
    theSession.SetUndoMarkName(markId2, "Milling Tool-5 Parameters");
    
    millToolBuilder1.Destroy();
    
    theSession.DeleteUndoMark(markId2, null);
    
    NXOpen.Session.UndoMarkId markId5;
    markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.CAM.Tool tool2 = (NXOpen.CAM.Tool)nXObject1;
    NXOpen.CAM.MillToolBuilder millToolBuilder2;
    millToolBuilder2 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool2);
    
    theSession.SetUndoMarkName(markId5, "Milling Tool-5 Parameters Dialog");
    
    // ----------------------------------------------
    //   Dialog Begin Milling Tool-5 Parameters
    // ----------------------------------------------
    NXOpen.NXObject nXObject2;
    nXObject2 = millToolBuilder2.Commit();
    
    // ----------------------------------------------
    //   Dialog Begin Library Class Selection
    // ----------------------------------------------
    // ----------------------------------------------
    //   Dialog Begin Search Criteria
    // ----------------------------------------------
    // ----------------------------------------------
    //   Dialog Begin Search Result
    // ----------------------------------------------
    NXOpen.CAM.Tool tool3 = (NXOpen.CAM.Tool)nXObject2;
    bool success1;
    success1 = tool3.RetrieveHolder("69871DIN_300480_SK40A70_ER32");
    
    NXOpen.Session.UndoMarkId markId6;
    markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters");
    
    theSession.DeleteUndoMark(markId6, null);
    
    NXOpen.Session.UndoMarkId markId7;
    markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters");
    
    theSession.DeleteUndoMark(markId7, null);
    
    theSession.SetUndoMarkName(markId5, "Milling Tool-5 Parameters");
    
    millToolBuilder2.Destroy();
    
    theSession.DeleteUndoMark(markId5, null);
    
    NXOpen.Session.UndoMarkId markId8;
    markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.CAM.MillToolBuilder millToolBuilder3;
    millToolBuilder3 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool3);
    
    theSession.SetUndoMarkName(markId8, "Milling Tool-5 Parameters Dialog");
    
    // ----------------------------------------------
    //   Dialog Begin Milling Tool-5 Parameters
    // ----------------------------------------------
    millToolBuilder3.HolderLibraryReference = "69871DIN_300480_SK40A70_ER32_get";
    
    NXOpen.Session.UndoMarkId markId9;
    markId9 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters");
    
    theSession.DeleteUndoMark(markId9, null);
    
    NXOpen.Session.UndoMarkId markId10;
    markId10 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters");
    
    NXOpen.NXObject nXObject3;
    nXObject3 = millToolBuilder3.Commit();
    
    theSession.DeleteUndoMark(markId10, null);
    
    theSession.SetUndoMarkName(markId8, "Milling Tool-5 Parameters");
    
    millToolBuilder3.Destroy();
    
    theSession.DeleteUndoMark(markId8, null);
    
    // ----------------------------------------------
    //   Menu: Tools->Journal->Stop Recording
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
