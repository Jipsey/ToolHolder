// NX 12.0.2.9
// Journal created by Sanek on Wed Jun 17 22:56:25 2020 RTZ 2 (зима)

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
    
    // ----------------------------------------------
    //   Dialog Begin Milling Tool-5 Parameters
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters");
    
    millToolBuilder1.HolderSectionBuilder.TlHolderOffsetBuilder.Value = 777.0;
    
    theSession.DeleteUndoMark(markId3, null);
    
    NXOpen.Session.UndoMarkId markId4;
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters");
    
    NXOpen.NXObject nXObject1;
    nXObject1 = millToolBuilder1.Commit();
    
    theSession.DeleteUndoMark(markId4, null);
    
    theSession.SetUndoMarkName(markId2, "Milling Tool-5 Parameters");
    
    millToolBuilder1.Destroy();
    
    theSession.DeleteUndoMark(markId2, null);
    
    NXOpen.Session.UndoMarkId markId5;
    markId5 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Edit MILLR_D16_Z5_12_37_120_S16_");
    
    NXOpen.Session.UndoMarkId markId6;
    markId6 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Start");
    
    NXOpen.CAM.Tool tool2 = (NXOpen.CAM.Tool)nXObject1;
    NXOpen.CAM.MillToolBuilder millToolBuilder2;
    millToolBuilder2 = workPart.CAMSetup.CAMGroupCollection.CreateMillToolBuilder(tool2);
    
    theSession.SetUndoMarkName(markId6, "Milling Tool-5 Parameters Dialog");
    
    // ----------------------------------------------
    //   Dialog Begin Milling Tool-5 Parameters
    // ----------------------------------------------
    NXOpen.Point3d scaleAboutPoint1 = new NXOpen.Point3d(-286.70516201357901, 27.560920692940968, 0.0);
    NXOpen.Point3d viewCenter1 = new NXOpen.Point3d(286.7051620135797, -27.560920692940627, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint1, viewCenter1);
    
    NXOpen.Point3d scaleAboutPoint2 = new NXOpen.Point3d(-229.36412961086322, 22.048736554352818, 0.0);
    NXOpen.Point3d viewCenter2 = new NXOpen.Point3d(229.36412961086378, -22.048736554352505, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint2, viewCenter2);
    
    NXOpen.Point3d scaleAboutPoint3 = new NXOpen.Point3d(-183.49130368869046, 17.638989243482285, 0.0);
    NXOpen.Point3d viewCenter3 = new NXOpen.Point3d(183.49130368869115, -17.638989243481941, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint3, viewCenter3);
    
    NXOpen.Point3d scaleAboutPoint4 = new NXOpen.Point3d(-146.7930429509523, 14.477715846598475, 0.0);
    NXOpen.Point3d viewCenter4 = new NXOpen.Point3d(146.79304295095295, -14.47771584659815, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint4, viewCenter4);
    
    NXOpen.Point3d scaleAboutPoint5 = new NXOpen.Point3d(-117.43443436076178, 11.582172677278839, 0.0);
    NXOpen.Point3d viewCenter5 = new NXOpen.Point3d(117.43443436076245, -11.582172677278479, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint5, viewCenter5);
    
    NXOpen.Point3d scaleAboutPoint6 = new NXOpen.Point3d(-93.947547488609331, 9.2657381418231157, 0.0);
    NXOpen.Point3d viewCenter6 = new NXOpen.Point3d(93.94754748861007, -9.265738141822764, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint6, viewCenter6);
    
    NXOpen.Point3d scaleAboutPoint7 = new NXOpen.Point3d(-75.158037990887379, 7.4125905134584906, 0.0);
    NXOpen.Point3d viewCenter7 = new NXOpen.Point3d(75.158037990888076, -7.4125905134581593, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint7, viewCenter7);
    
    NXOpen.Point3d scaleAboutPoint8 = new NXOpen.Point3d(-60.12643039270985, 5.9300724107668339, 0.0);
    NXOpen.Point3d viewCenter8 = new NXOpen.Point3d(60.126430392710503, -5.9300724107665177, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint8, viewCenter8);
    
    NXOpen.Point3d scaleAboutPoint9 = new NXOpen.Point3d(-49.061966173127487, 2.9425169430641356, 0.0);
    NXOpen.Point3d viewCenter9 = new NXOpen.Point3d(49.061966173128155, -2.9425169430638092, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint9, viewCenter9);
    
    NXOpen.Point3d scaleAboutPoint10 = new NXOpen.Point3d(-39.24957293850192, 2.3540135544513348, 0.0);
    NXOpen.Point3d viewCenter10 = new NXOpen.Point3d(39.249572938502602, -2.3540135544510212, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint10, viewCenter10);
    
    NXOpen.Point3d scaleAboutPoint11 = new NXOpen.Point3d(-31.399658350801484, 1.8832108435610995, 0.0);
    NXOpen.Point3d viewCenter11 = new NXOpen.Point3d(31.399658350802142, -1.883210843560791, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint11, viewCenter11);
    
    NXOpen.Point3d origin1 = new NXOpen.Point3d(164.84120942379855, -231.96225566314746, 93.442374791060715);
    workPart.ModelingViews.WorkView.SetOrigin(origin1);
    
    NXOpen.Point3d origin2 = new NXOpen.Point3d(164.84120942379855, -231.96225566314746, 93.442374791060715);
    workPart.ModelingViews.WorkView.SetOrigin(origin2);
    
    NXOpen.Matrix3x3 rotMatrix1 = new NXOpen.Matrix3x3();
    rotMatrix1.Xx = -0.92455190915842644;
    rotMatrix1.Xy = 0.36837264888254806;
    rotMatrix1.Xz = -0.09749542977373428;
    rotMatrix1.Yx = -0.12480670128190648;
    rotMatrix1.Yy = -0.050994564468317953;
    rotMatrix1.Yz = 0.99086974003136052;
    rotMatrix1.Zx = 0.36003757385395102;
    rotMatrix1.Zy = 0.9282785928534294;
    rotMatrix1.Zz = 0.093122497085394787;
    NXOpen.Point3d translation1 = new NXOpen.Point3d(246.96300992949458, -83.844748234257665, 147.27497989586558);
    workPart.ModelingViews.WorkView.SetRotationTranslationScale(rotMatrix1, translation1, 4.3026858150475196);
    
    NXOpen.Point3d scaleAboutPoint12 = new NXOpen.Point3d(-27.88689363444497, 0.95313528408813641, 0.0);
    NXOpen.Point3d viewCenter12 = new NXOpen.Point3d(27.886893634445624, -0.9531352840878311, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint12, viewCenter12);
    
    NXOpen.Point3d scaleAboutPoint13 = new NXOpen.Point3d(-22.260320828377161, 0.81170230644928132, 0.0);
    NXOpen.Point3d viewCenter13 = new NXOpen.Point3d(22.260320828377843, -0.8117023064489669, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint13, viewCenter13);
    
    NXOpen.Point3d scaleAboutPoint14 = new NXOpen.Point3d(-17.729546136015692, 0.9248486885603705, 0.0);
    NXOpen.Point3d viewCenter14 = new NXOpen.Point3d(17.729546136016374, -0.92484868856005742, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint14, viewCenter14);
    
    NXOpen.Point3d scaleAboutPoint15 = new NXOpen.Point3d(-14.183636908812508, 1.2436263216385735, 0.0);
    NXOpen.Point3d viewCenter15 = new NXOpen.Point3d(14.183636908813163, -1.2436263216382608, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint15, viewCenter15);
    
    // ----------------------------------------------
    //   Menu: View->Operation->Fit
    // ----------------------------------------------
    workPart.ModelingViews.WorkView.Fit();
    
    NXOpen.Point3d scaleAboutPoint16 = new NXOpen.Point3d(15.504435415295911, 219.79817265213524, 0.0);
    NXOpen.Point3d viewCenter16 = new NXOpen.Point3d(-15.504435415295911, -219.79817265213524, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint16, viewCenter16);
    
    NXOpen.Point3d scaleAboutPoint17 = new NXOpen.Point3d(12.403548332236728, 175.83853812170818, 0.0);
    NXOpen.Point3d viewCenter17 = new NXOpen.Point3d(-12.403548332236529, -175.83853812170818, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint17, viewCenter17);
    
    NXOpen.Point3d scaleAboutPoint18 = new NXOpen.Point3d(9.9228386657894614, 140.67083049736655, 0.0);
    NXOpen.Point3d viewCenter18 = new NXOpen.Point3d(-9.9228386657891434, -140.67083049736655, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint18, viewCenter18);
    
    NXOpen.Point3d scaleAboutPoint19 = new NXOpen.Point3d(7.0043567052632767, 114.40449285263009, 0.0);
    NXOpen.Point3d viewCenter19 = new NXOpen.Point3d(-7.0043567052630227, -114.40449285263009, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint19, viewCenter19);
    
    NXOpen.Point3d scaleAboutPoint20 = new NXOpen.Point3d(2.6149598366315758, 93.764988427788197, 0.0);
    NXOpen.Point3d viewCenter20 = new NXOpen.Point3d(-2.6149598366314741, -93.764988427788239, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint20, viewCenter20);
    
    NXOpen.Point3d scaleAboutPoint21 = new NXOpen.Point3d(3.2686997957895967, 117.20623553473519, 0.0);
    NXOpen.Point3d viewCenter21 = new NXOpen.Point3d(-3.2686997957893427, -117.20623553473537, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint21, viewCenter21);
    
    NXOpen.Point3d scaleAboutPoint22 = new NXOpen.Point3d(4.0858747447369961, 146.507794418419, 0.0);
    NXOpen.Point3d viewCenter22 = new NXOpen.Point3d(-4.085874744736679, -146.50779441841922, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint22, viewCenter22);
    
    NXOpen.Point3d scaleAboutPoint23 = new NXOpen.Point3d(5.1073434309211461, 183.13474302302382, 0.0);
    NXOpen.Point3d viewCenter23 = new NXOpen.Point3d(-5.1073434309210466, -183.13474302302401, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint23, viewCenter23);
    
    NXOpen.Point3d scaleAboutPoint24 = new NXOpen.Point3d(-4.5601280633219918, 230.7424800041087, 0.0);
    NXOpen.Point3d viewCenter24 = new NXOpen.Point3d(4.5601280633224883, -230.74248000410896, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint24, viewCenter24);
    
    NXOpen.Point3d scaleAboutPoint25 = new NXOpen.Point3d(-5.7001600791526439, 288.42810000513583, 0.0);
    NXOpen.Point3d viewCenter25 = new NXOpen.Point3d(5.7001600791529539, -288.428100005136, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint25, viewCenter25);
    
    NXOpen.Point3d scaleAboutPoint26 = new NXOpen.Point3d(-7.125200098940998, 360.53512500641995, 0.0);
    NXOpen.Point3d viewCenter26 = new NXOpen.Point3d(7.1252000989413853, -360.53512500641995, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint26, viewCenter26);
    
    NXOpen.Point3d scaleAboutPoint27 = new NXOpen.Point3d(-8.9065001236762491, 450.668906258025, 0.0);
    NXOpen.Point3d viewCenter27 = new NXOpen.Point3d(8.9065001236762491, -450.668906258025, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint27, viewCenter27);
    
    NXOpen.Point3d scaleAboutPoint28 = new NXOpen.Point3d(-69.025375958491892, 469.81788152392915, 0.0);
    NXOpen.Point3d viewCenter28 = new NXOpen.Point3d(69.025375958491892, -469.81788152392915, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint28, viewCenter28);
    
    NXOpen.Matrix3x3 rotMatrix2 = new NXOpen.Matrix3x3();
    rotMatrix2.Xx = -0.32570673521222537;
    rotMatrix2.Xy = 0.93619419322519726;
    rotMatrix2.Xz = 0.13211947323849185;
    rotMatrix2.Yx = -0.46660733165419815;
    rotMatrix2.Yy = -0.28070137983368343;
    rotMatrix2.Yz = 0.83873972924025531;
    rotMatrix2.Zx = 0.82230938258294473;
    rotMatrix2.Zy = 0.21153526403625911;
    rotMatrix2.Zz = 0.52826140440615954;
    NXOpen.Point3d translation2 = new NXOpen.Point3d(160.31288587598124, 658.78312117697226, 212.07365751642178);
    workPart.ModelingViews.WorkView.SetRotationTranslationScale(rotMatrix2, translation2, 0.074266920131170408);
    
    NXOpen.Point3d scaleAboutPoint29 = new NXOpen.Point3d(194.16170269614508, 475.60710660431897, 0.0);
    NXOpen.Point3d viewCenter29 = new NXOpen.Point3d(-194.16170269614508, -475.60710660431897, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint29, viewCenter29);
    
    NXOpen.Point3d scaleAboutPoint30 = new NXOpen.Point3d(242.70212837018136, 594.5088832553987, 0.0);
    NXOpen.Point3d viewCenter30 = new NXOpen.Point3d(-242.70212837018136, -594.5088832553987, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint30, viewCenter30);
    
    NXOpen.Point3d scaleAboutPoint31 = new NXOpen.Point3d(303.37766046272668, 743.13610406924829, 0.0);
    NXOpen.Point3d viewCenter31 = new NXOpen.Point3d(-303.37766046272668, -743.13610406924829, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint31, viewCenter31);
    
    NXOpen.Point3d scaleAboutPoint32 = new NXOpen.Point3d(379.22207557840835, 901.08731720007154, 0.0);
    NXOpen.Point3d viewCenter32 = new NXOpen.Point3d(-379.22207557840835, -901.08731720007154, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint32, viewCenter32);
    
    NXOpen.Point3d scaleAboutPoint33 = new NXOpen.Point3d(465.32984044598311, -152.21069547298509, 0.0);
    NXOpen.Point3d viewCenter33 = new NXOpen.Point3d(-465.32984044598311, 152.21069547298509, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint33, viewCenter33);
    
    NXOpen.Point3d scaleAboutPoint34 = new NXOpen.Point3d(570.79010802369487, -201.13556187501464, 0.0);
    NXOpen.Point3d viewCenter34 = new NXOpen.Point3d(-570.79010802369487, 201.13556187501686, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint34, viewCenter34);
    
    NXOpen.Point3d scaleAboutPoint35 = new NXOpen.Point3d(482.45354368669388, 699.89739436238756, 0.0);
    NXOpen.Point3d viewCenter35 = new NXOpen.Point3d(-482.45354368669388, -699.89739436238574, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint35, viewCenter35);
    
    NXOpen.Point3d scaleAboutPoint36 = new NXOpen.Point3d(385.96283494935358, 559.91791548991068, 0.0);
    NXOpen.Point3d viewCenter36 = new NXOpen.Point3d(-385.96283494935653, -559.91791548990852, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint36, viewCenter36);
    
    NXOpen.Point3d scaleAboutPoint37 = new NXOpen.Point3d(317.46802198651068, 465.32984044598362, 0.0);
    NXOpen.Point3d viewCenter37 = new NXOpen.Point3d(-317.46802198651307, -465.32984044598186, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint37, viewCenter37);
    
    NXOpen.Point3d scaleAboutPoint38 = new NXOpen.Point3d(253.97441758920854, 407.05488846489862, 0.0);
    NXOpen.Point3d viewCenter38 = new NXOpen.Point3d(-253.97441758921042, -407.05488846489578, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint38, viewCenter38);
    
    NXOpen.Point3d scaleAboutPoint39 = new NXOpen.Point3d(203.17953407136687, 381.30953654489662, 0.0);
    NXOpen.Point3d viewCenter39 = new NXOpen.Point3d(-203.17953407136915, -381.309536544894, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint39, viewCenter39);
    
    millToolBuilder2.HolderSectionBuilder.TlHolderOffsetBuilder.Value = 5.0;
    
    NXOpen.Point3d scaleAboutPoint40 = new NXOpen.Point3d(-362.93988003981349, 723.6531350487071, 0.0);
    NXOpen.Point3d viewCenter40 = new NXOpen.Point3d(362.93988003981076, -723.65313504870437, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint40, viewCenter40);
    
    NXOpen.Point3d scaleAboutPoint41 = new NXOpen.Point3d(-347.35350482338032, 610.98590848420099, 0.0);
    NXOpen.Point3d viewCenter41 = new NXOpen.Point3d(347.35350482337736, -610.98590848419792, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint41, viewCenter41);
    
    NXOpen.Point3d scaleAboutPoint42 = new NXOpen.Point3d(-277.88280385870468, 491.63880682693821, 0.0);
    NXOpen.Point3d viewCenter42 = new NXOpen.Point3d(277.88280385870161, -491.63880682693451, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint42, viewCenter42);
    
    NXOpen.Point3d scaleAboutPoint43 = new NXOpen.Point3d(-222.30624308696406, 393.3110454615508, 0.0);
    NXOpen.Point3d viewCenter43 = new NXOpen.Point3d(222.30624308696093, -393.31104546154711, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(1.25, scaleAboutPoint43, viewCenter43);
    
    NXOpen.Point3d scaleAboutPoint44 = new NXOpen.Point3d(382.1387317064079, 413.1476025370028, 0.0);
    NXOpen.Point3d viewCenter44 = new NXOpen.Point3d(-382.13873170641091, -413.1476025369991, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint44, viewCenter44);
    
    NXOpen.Point3d scaleAboutPoint45 = new NXOpen.Point3d(466.27309447470458, 516.43450317125303, 0.0);
    NXOpen.Point3d viewCenter45 = new NXOpen.Point3d(-466.27309447470719, -516.43450317124928, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint45, viewCenter45);
    
    NXOpen.Point3d scaleAboutPoint46 = new NXOpen.Point3d(579.99128805380462, 645.54312896406589, 0.0);
    NXOpen.Point3d viewCenter46 = new NXOpen.Point3d(-579.99128805380735, -645.54312896406225, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint46, viewCenter46);
    
    NXOpen.Point3d origin3 = new NXOpen.Point3d(-456.82871085494554, 659.5905676803327, 45.534677646621525);
    workPart.ModelingViews.WorkView.SetOrigin(origin3);
    
    NXOpen.Point3d origin4 = new NXOpen.Point3d(-456.82871085494554, 659.5905676803327, 45.534677646621525);
    workPart.ModelingViews.WorkView.SetOrigin(origin4);
    
    NXOpen.Point3d scaleAboutPoint47 = new NXOpen.Point3d(379.41690526861242, 439.98110610961515, 0.0);
    NXOpen.Point3d viewCenter47 = new NXOpen.Point3d(-379.41690526861532, -439.98110610961106, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint47, viewCenter47);
    
    NXOpen.Point3d scaleAboutPoint48 = new NXOpen.Point3d(465.36463146208962, 549.97638263701833, 0.0);
    NXOpen.Point3d viewCenter48 = new NXOpen.Point3d(-465.36463146209201, -549.97638263701413, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint48, viewCenter48);
    
    NXOpen.Point3d scaleAboutPoint49 = new NXOpen.Point3d(548.30641386382592, 681.90391571897487, 0.0);
    NXOpen.Point3d viewCenter49 = new NXOpen.Point3d(-548.30641386382933, -681.90391571897032, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint49, viewCenter49);
    
    NXOpen.Point3d scaleAboutPoint50 = new NXOpen.Point3d(615.8009851135597, 831.50528498385142, 0.0);
    NXOpen.Point3d viewCenter50 = new NXOpen.Point3d(-615.80098511356357, -831.50528498384665, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint50, viewCenter50);
    
    // ----------------------------------------------
    //   Menu: View->Operation->Fit
    // ----------------------------------------------
    workPart.ModelingViews.WorkView.Fit();
    
    NXOpen.Matrix3x3 rotMatrix3 = new NXOpen.Matrix3x3();
    rotMatrix3.Xx = -0.074067505804252642;
    rotMatrix3.Xy = 0.99277609926014787;
    rotMatrix3.Xz = 0.094390790449827497;
    rotMatrix3.Yx = -0.0020149135722141304;
    rotMatrix3.Yy = -0.094799559997341881;
    rotMatrix3.Yz = 0.99549434129361458;
    rotMatrix3.Zx = 0.99725119438746967;
    rotMatrix3.Zy = 0.073543593617096698;
    rotMatrix3.Zz = 0.0090219249969129348;
    NXOpen.Point3d translation3 = new NXOpen.Point3d(3.1589136400735374, -105.14889998754266, 33.616845100090721);
    workPart.ModelingViews.WorkView.SetRotationTranslationScale(rotMatrix3, translation3, 0.70100925726828234);
    
    // ----------------------------------------------
    //   Menu: Snap View
    // ----------------------------------------------
    workPart.ModelingViews.WorkView.SnapToClosestCannedOrientation();
    
    NXOpen.Point3d scaleAboutPoint51 = new NXOpen.Point3d(-16.795724466199619, 122.4766873995907, 0.0);
    NXOpen.Point3d viewCenter51 = new NXOpen.Point3d(16.795724466199619, -122.4766873995907, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint51, viewCenter51);
    
    NXOpen.Point3d scaleAboutPoint52 = new NXOpen.Point3d(-20.994655582749555, 152.15227922329743, 0.0);
    NXOpen.Point3d viewCenter52 = new NXOpen.Point3d(20.994655582749555, -152.15227922329734, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint52, viewCenter52);
    
    NXOpen.Point3d scaleAboutPoint53 = new NXOpen.Point3d(-26.243319478436941, 188.42113648001362, 0.0);
    NXOpen.Point3d viewCenter53 = new NXOpen.Point3d(26.243319478436941, -188.42113648001362, 0.0);
    workPart.ModelingViews.WorkView.ZoomAboutPoint(0.80000000000000004, scaleAboutPoint53, viewCenter53);
    
    NXOpen.Session.UndoMarkId markId7;
    markId7 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters");
    
    theSession.DeleteUndoMark(markId7, null);
    
    NXOpen.Session.UndoMarkId markId8;
    markId8 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Milling Tool-5 Parameters");
    
    NXOpen.NXObject nXObject2;
    nXObject2 = millToolBuilder2.Commit();
    
    theSession.DeleteUndoMark(markId8, null);
    
    theSession.SetUndoMarkName(markId6, "Milling Tool-5 Parameters");
    
    millToolBuilder2.Destroy();
    
    theSession.DeleteUndoMark(markId6, null);
    
    // ----------------------------------------------
    //   Menu: Tools->Journal->Stop Recording
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)NXOpen.Session.LibraryUnloadOption.Immediately; }
}
