// NX 12.0.2.9
// Journal created by Sanek on Thu Jun 11 22:09:49 2020 RTZ 2 (зима)

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
    NXOpen.CAM.PlanarMilling planarMilling1 = (NXOpen.CAM.PlanarMilling)workPart.CAMSetup.CAMOperationCollection.FindObject("PLANAR_MILL");
    theSession.CAMSession.PathDisplay.ShowToolPath(planarMilling1);
    
    theSession.CAMSession.PathDisplay.HideToolPath(planarMilling1);
    
    NXOpen.Session.UndoMarkId markId1;
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Edit MILLR_D16_Z5_12_37_120_S16_");
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.CAM.Tool tool1 = (NXOpen.CAM.Tool)workPart.CAMSetup.CAMGroupCollection.FindObject("MILLR_D16_Z5_12_37_120_S16_");
    NXOpen.CAM.MillToolBuilder millToolBuilder1;
    millToolBuilder1 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool1);
    
    theSession.SetUndoMarkName(markId2, "Milling Tool-5 Parameters Dialog");
    
    millToolBuilder1.HolderLibraryReference = "69871DIN_300480_SK40A70_ER25";
    
    NXOpen.Point3d scaleAboutPoint1 = new NXOpen.Point3d(-63.376899373644299, 102.03231317600999, 0.0);
    NXOpen.Point3d viewCenter1 = new NXOpen.Point3d(63.376899373645401, -102.03231317600964, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint1, viewCenter1);
    
    NXOpen.Point3d scaleAboutPoint2 = new NXOpen.Point3d(-81.468531996262811, 132.03520702842695, 0.0);
    NXOpen.Point3d viewCenter2 = new NXOpen.Point3d(81.468531996263877, -132.03520702842664, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint2, viewCenter2);
    
    NXOpen.Point3d scaleAboutPoint3 = new NXOpen.Point3d(-103.24029485733317, 167.85326850954266, 0.0);
    NXOpen.Point3d viewCenter3 = new NXOpen.Point3d(103.24029485733431, -167.85326850954237, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint3, viewCenter3);
    
    NXOpen.Point3d scaleAboutPoint4 = new NXOpen.Point3d(-151.8756038292405, 225.61867158447942, 0.0);
    NXOpen.Point3d viewCenter4 = new NXOpen.Point3d(151.87560382924121, -225.61867158447919, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint4, viewCenter4);
    
    NXOpen.Point3d scaleAboutPoint5 = new NXOpen.Point3d(-115.88196361537399, 180.49493726758354, 0.0);
    NXOpen.Point3d viewCenter5 = new NXOpen.Point3d(115.88196361537494, -180.49493726758325, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint5, viewCenter5);
    
    NXOpen.Point3d scaleAboutPoint6 = new NXOpen.Point3d(-89.334459223488324, 144.39594981406682, 0.0);
    NXOpen.Point3d viewCenter6 = new NXOpen.Point3d(89.334459223489233, -144.39594981406657, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint6, viewCenter6);
    
    NXOpen.Point3d scaleAboutPoint7 = new NXOpen.Point3d(-67.871714932058936, 115.51675985125351, 0.0);
    NXOpen.Point3d viewCenter7 = new NXOpen.Point3d(67.871714932059788, -115.51675985125327, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint7, viewCenter7);
    
    NXOpen.NXObject nXObject1;
    nXObject1 = millToolBuilder1.Commit();
    
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
    millToolBuilder2.HolderLibraryReference = "69871DIN_300480_SK40A70_ER32";
    
    NXOpen.NXObject nXObject2;
    nXObject2 = millToolBuilder2.Commit();
    
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
    
    NXOpen.CAM.Tool tool3 = (NXOpen.CAM.Tool)nXObject2;
    NXOpen.CAM.MillToolBuilder millToolBuilder3;
    millToolBuilder3 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool3);
    
    theSession.SetUndoMarkName(markId8, "Milling Tool-5 Parameters Dialog");
    
    // ----------------------------------------------
    //   Dialog Begin Milling Tool-5 Parameters
    // ----------------------------------------------
    NXOpen.Point3d scaleAboutPoint8 = new NXOpen.Point3d(-5.3937786700969621, 106.07764717858296, 0.0);
    NXOpen.Point3d viewCenter8 = new NXOpen.Point3d(5.3937786700978423, -106.07764717858277, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint8, viewCenter8);
    
    NXOpen.Point3d scaleAboutPoint9 = new NXOpen.Point3d(-7.6411864493042421, 132.59705897322871, 0.0);
    NXOpen.Point3d viewCenter9 = new NXOpen.Point3d(7.6411864493050974, -132.59705897322849, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint9, viewCenter9);
    
    NXOpen.Point3d scaleAboutPoint10 = new NXOpen.Point3d(-9.5514830616303783, 164.62261982693221, 0.0);
    NXOpen.Point3d viewCenter10 = new NXOpen.Point3d(9.5514830616312949, -164.62261982693198, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint10, viewCenter10);
    
    NXOpen.Point3d scaleAboutPoint11 = new NXOpen.Point3d(-69.529178169224238, 209.99216436967887, 0.0);
    NXOpen.Point3d viewCenter11 = new NXOpen.Point3d(69.529178169225105, -209.99216436967876, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint11, viewCenter11);
    
    NXOpen.Point3d scaleAboutPoint12 = new NXOpen.Point3d(-55.623342535379315, 167.99373149574299, 0.0);
    NXOpen.Point3d viewCenter12 = new NXOpen.Point3d(55.623342535380232, -167.99373149574294, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint12, viewCenter12);
    
    NXOpen.Point3d scaleAboutPoint13 = new NXOpen.Point3d(-44.498674028303391, 134.3949851965944, 0.0);
    NXOpen.Point3d viewCenter13 = new NXOpen.Point3d(44.498674028304244, -134.3949851965944, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint13, viewCenter13);
    
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
