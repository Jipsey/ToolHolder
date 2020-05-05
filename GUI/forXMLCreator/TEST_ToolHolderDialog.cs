﻿//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  F:\Store\SANEK\Volume\NXOpen\5_ToolHolder\GUI\forXMLCreator\TEST_ToolHolderDialog.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: Sanek
//              Version: NX 12
//              Date: 05-05-2020  (Format: mm-dd-yyyy)
//              Time: 01:39 (Format: hh-mm)
//
//==============================================================================

//==============================================================================
//  Purpose:  This TEMPLATE file contains C# source to guide you in the
//  construction of your Block application dialog. The generation of your
//  dialog file (.dlx extension) is the first step towards dialog construction
//  within NX.  You must now create a NX Open application that
//  utilizes this file (.dlx).
//
//  The information in this file provides you with the following:
//
//  1.  Help on how to load and display your Block UI Styler dialog in NX
//      using APIs provided in NXOpen.BlockStyler namespace
//  2.  The empty callback methods (stubs) associated with your dialog items
//      have also been placed in this file. These empty methods have been
//      created simply to start you along with your coding requirements.
//      The method name, argument list and possible return values have already
//      been provided for you.
//==============================================================================

//------------------------------------------------------------------------------
//These imports are needed for the following template code
//------------------------------------------------------------------------------
using System;
using NXOpen;
using NXOpen.BlockStyler;

//------------------------------------------------------------------------------
//Represents Block Styler application class
//------------------------------------------------------------------------------
public class TEST_ToolHolderDialog
{
    //class members
    private static Session theSession = null;
    private static UI theUI = null;
    private string theDlxFileName;
    private NXOpen.BlockStyler.BlockDialog theDialog;
    private NXOpen.BlockStyler.Label label0;// Block type: Label
    private NXOpen.BlockStyler.Explorer explorer;// Block type: Explorer
    private NXOpen.BlockStyler.Group explorerNode;// Block type: Group
    private NXOpen.BlockStyler.Label stringLabelTool;// Block type: Label
    private NXOpen.BlockStyler.DoubleBlock doubleToolDiam;// Block type: Double
    private NXOpen.BlockStyler.Separator separator011;// Block type: Separator
    private NXOpen.BlockStyler.IntegerBlock offsetOfTool;// Block type: Integer
    private NXOpen.BlockStyler.Separator separator02;// Block type: Separator
    private NXOpen.BlockStyler.MultilineString multiline_string01;// Block type: Multiline String
    private NXOpen.BlockStyler.Separator separator03;// Block type: Separator
    private NXOpen.BlockStyler.Label stringLabelHolder;// Block type: Label
    private NXOpen.BlockStyler.Separator separator04;// Block type: Separator
    private NXOpen.BlockStyler.StringBlock stringLibRef1;// Block type: String
    private NXOpen.BlockStyler.Separator separator05;// Block type: Separator
    private NXOpen.BlockStyler.MultilineString multiline_string0;// Block type: Multiline String
    private NXOpen.BlockStyler.Separator separator06;// Block type: Separator
    private NXOpen.BlockStyler.Tree tree_control0;// Block type: Tree Control
    
    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------
    public TEST_ToolHolderDialog()
    {
        try
        {
            theSession = Session.GetSession();
            theUI = UI.GetUI();
            theDlxFileName = "TEST_ToolHolderDialog.dlx";
            theDialog = theUI.CreateDialog(theDlxFileName);
            theDialog.AddApplyHandler(new NXOpen.BlockStyler.BlockDialog.Apply(apply_cb));
            theDialog.AddOkHandler(new NXOpen.BlockStyler.BlockDialog.Ok(ok_cb));
            theDialog.AddUpdateHandler(new NXOpen.BlockStyler.BlockDialog.Update(update_cb));
            theDialog.AddCancelHandler(new NXOpen.BlockStyler.BlockDialog.Cancel(cancel_cb));
            theDialog.AddFilterHandler(new NXOpen.BlockStyler.BlockDialog.Filter(filter_cb));
            theDialog.AddInitializeHandler(new NXOpen.BlockStyler.BlockDialog.Initialize(initialize_cb));
            theDialog.AddFocusNotifyHandler(new NXOpen.BlockStyler.BlockDialog.FocusNotify(focusNotify_cb));
            theDialog.AddKeyboardFocusNotifyHandler(new NXOpen.BlockStyler.BlockDialog.KeyboardFocusNotify(keyboardFocusNotify_cb));
            theDialog.AddEnableOKButtonHandler(new NXOpen.BlockStyler.BlockDialog.EnableOKButton(enableOKButton_cb));
            theDialog.AddDialogShownHandler(new NXOpen.BlockStyler.BlockDialog.DialogShown(dialogShown_cb));
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            throw ex;
        }
    }
    //------------------------------- DIALOG LAUNCHING ---------------------------------
    //
    //    Before invoking this application one needs to open any part/empty part in NX
    //    because of the behavior of the blocks.
    //
    //    Make sure the dlx file is in one of the following locations:
    //        1.) From where NX session is launched
    //        2.) $UGII_USER_DIR/application
    //        3.) For released applications, using UGII_CUSTOM_DIRECTORY_FILE is highly
    //            recommended. This variable is set to a full directory path to a file 
    //            containing a list of root directories for all custom applications.
    //            e.g., UGII_CUSTOM_DIRECTORY_FILE=$UGII_BASE_DIR\ugii\menus\custom_dirs.dat
    //
    //    You can create the dialog using one of the following way:
    //
    //    1. Journal Replay
    //
    //        1) Replay this file through Tool->Journal->Play Menu.
    //
    //    2. USER EXIT
    //
    //        1) Remove the following conditional definitions:
    //                a) #if USER_EXIT_OR_MENU
    //                    #endif//USER_EXIT_OR_MENU
    //
    //                b) #if USER_EXIT
    //                    #endif//USER_EXIT
    //        2) Create the Shared Library -- Refer "Block UI Styler programmer's guide"
    //        3) Invoke the Shared Library through File->Execute->NX Open menu.
    //
    //    3. THROUGH CALLBACK OF ANOTHER DIALOG
    //
    //        1) Remove the following conditional definition:
    //             #if CALLBACK
    //             #endif//CALLBACK
    //        2) Call the following line of code from where ever you want to lauch this dialog.
    //             TEST_ToolHolderDialog.Show_TEST_ToolHolderDialog();
    //        3) Integrate this file with your main application file.
    //
    //    4. MENU BAR
    //    
    //        1) Remove the following conditional definition:
    //                a) #if USER_EXIT_OR_MENU
    //                   #endif//USER_EXIT_OR_MENU
    //        2) Add the following lines to your MenuScript file in order to
    //           associate a menu bar button with your dialog.  In this
    //           example, a cascade menu will be created and will be
    //           located just before the Help button on the main menubar.
    //           The button, SAMPLEVB_BTN is set up to launch your dialog and
    //           will be positioned as the first button on your pulldown menu.
    //           If you wish to add the button to an existing cascade, simply
    //           add the 3 lines between MENU LAUNCH_CASCADE and END_OF_MENU
    //           to your menuscript file.
    //           The MenuScript file requires an extension of ".men".
    //           Move the contents between the dashed lines to your Menuscript file.
    //        
    //           !-----------------------------------------------------------------------------
    //           VERSION 120
    //        
    //           EDIT UG_GATEWAY_MAIN_MENUBAR
    //        
    //           BEFORE UG_HELP
    //           CASCADE_BUTTON BLOCKSTYLER_DLX_CASCADE_BTN
    //           LABEL Dialog Launcher
    //           END_OF_BEFORE
    //        
    //           MENU BLOCKSTYLER_DLX_CASCADE_BTN
    //           BUTTON SAMPLEVB_BTN
    //           LABEL Display SampleVB dialog
    //           ACTIONS <path of Shared library> !For example: D:\temp\SampleVB.dll
    //           END_OF_MENU
    //           !-----------------------------------------------------------------------------
    //        
    //        3) Make sure the .men file is in one of the following locations:
    //        
    //           - $UGII_USER_DIR/startup   
    //           - For released applications, using UGII_CUSTOM_DIRECTORY_FILE is highly
    //             recommended. This variable is set to a full directory path to a file 
    //             containing a list of root directories for all custom applications.
    //             e.g., UGII_CUSTOM_DIRECTORY_FILE=$UGII_BASE_DIR\ugii\menus\custom_dirs.dat
    //    
    //------------------------------------------------------------------------------
#if USER_EXIT_OR_MENU
    public static void Main()
    {
        TEST_ToolHolderDialog theTEST_ToolHolderDialog = null;
        try
        {
            theTEST_ToolHolderDialog = new TEST_ToolHolderDialog();
            // The following method shows the dialog immediately
            theTEST_ToolHolderDialog.Show();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        finally
        {
            if(theTEST_ToolHolderDialog != null)
                theTEST_ToolHolderDialog.Dispose();
                theTEST_ToolHolderDialog = null;
        }
    }
#endif//USER_EXIT_OR_MENU
#if USER_EXIT
    //------------------------------------------------------------------------------
    // This method specifies how a shared image is unloaded from memory
    // within NX. This method gives you the capability to unload an
    // internal NX Open application or user  exit from NX. Specify any
    // one of the three constants as a return value to determine the type
    // of unload to perform:
    //
    //
    //    Immediately : unload the library as soon as the automation program has completed
    //    Explicitly  : unload the library from the "Unload Shared Image" dialog
    //    AtTermination : unload the library when the NX session terminates
    //
    //
    // NOTE:  A program which associates NX Open applications with the menubar
    // MUST NOT use this option since it will UNLOAD your NX Open application image
    // from the menubar.
    //------------------------------------------------------------------------------
     public static int GetUnloadOption(string arg)
    {
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
         return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
    }
    
    //------------------------------------------------------------------------------
    // Following method cleanup any housekeeping chores that may be needed.
    // This method is automatically called by NX.
    //------------------------------------------------------------------------------
    public static void UnloadLibrary(string arg)
    {
        try
        {
            //---- Enter your code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
#endif//USER_EXIT
    
    //------------------------------------------------------------------------------
    //This method shows the dialog on the screen
    //------------------------------------------------------------------------------
    public NXOpen.UIStyler.DialogResponse Show()
    {
        try
        {
            theDialog.Show();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Method Name: Dispose
    //------------------------------------------------------------------------------
    public void Dispose()
    {
        if(theDialog != null)
        {
            theDialog.Dispose();
            theDialog = null;
        }
    }
    
#if CALLBACK
    //------------------------------------------------------------------------------
    //Method name: Show_TEST_ToolHolderDialog
    //------------------------------------------------------------------------------
    public static void Show_TEST_ToolHolderDialog()
    {
        TEST_ToolHolderDialog theTEST_ToolHolderDialog = null;
        try
        {
            theTEST_ToolHolderDialog = new TEST_ToolHolderDialog();
            // The following method shows the dialog immediately
            theTEST_ToolHolderDialog.Show();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        finally
        {
            theTEST_ToolHolderDialog.Dispose();
            theTEST_ToolHolderDialog = null;
        }
    }
#endif//CALLBACK
    
    //------------------------------------------------------------------------------
    //---------------------Block UI Styler Callback Functions--------------------------
    //------------------------------------------------------------------------------
    
    //------------------------------------------------------------------------------
    //Callback Name: initialize_cb
    //------------------------------------------------------------------------------
    public void initialize_cb()
    {
        try
        {
            label0 = (NXOpen.BlockStyler.Label)theDialog.TopBlock.FindBlock("label0");
            explorer = (NXOpen.BlockStyler.Explorer)theDialog.TopBlock.FindBlock("explorer");
            explorerNode = (NXOpen.BlockStyler.Group)theDialog.TopBlock.FindBlock("explorerNode");
            stringLabelTool = (NXOpen.BlockStyler.Label)theDialog.TopBlock.FindBlock("stringLabelTool");
            doubleToolDiam = (NXOpen.BlockStyler.DoubleBlock)theDialog.TopBlock.FindBlock("doubleToolDiam");
            separator011 = (NXOpen.BlockStyler.Separator)theDialog.TopBlock.FindBlock("separator011");
            offsetOfTool = (NXOpen.BlockStyler.IntegerBlock)theDialog.TopBlock.FindBlock("offsetOfTool");
            separator02 = (NXOpen.BlockStyler.Separator)theDialog.TopBlock.FindBlock("separator02");
            multiline_string01 = (NXOpen.BlockStyler.MultilineString)theDialog.TopBlock.FindBlock("multiline_string01");
            separator03 = (NXOpen.BlockStyler.Separator)theDialog.TopBlock.FindBlock("separator03");
            stringLabelHolder = (NXOpen.BlockStyler.Label)theDialog.TopBlock.FindBlock("stringLabelHolder");
            separator04 = (NXOpen.BlockStyler.Separator)theDialog.TopBlock.FindBlock("separator04");
            stringLibRef1 = (NXOpen.BlockStyler.StringBlock)theDialog.TopBlock.FindBlock("stringLibRef1");
            separator05 = (NXOpen.BlockStyler.Separator)theDialog.TopBlock.FindBlock("separator05");
            multiline_string0 = (NXOpen.BlockStyler.MultilineString)theDialog.TopBlock.FindBlock("multiline_string0");
            separator06 = (NXOpen.BlockStyler.Separator)theDialog.TopBlock.FindBlock("separator06");
            tree_control0 = (NXOpen.BlockStyler.Tree)theDialog.TopBlock.FindBlock("tree_control0");
            //------------------------------------------------------------------------------
            //Registration of Treelist specific callbacks
            //------------------------------------------------------------------------------
            //tree_control0.SetOnExpandHandler(new NXOpen.BlockStyler.Tree.OnExpandCallback(OnExpandCallback));
            
            //tree_control0.SetOnInsertColumnHandler(new NXOpen.BlockStyler.Tree.OnInsertColumnCallback(OnInsertColumnCallback));
            
            //tree_control0.SetOnInsertNodeHandler(new NXOpen.BlockStyler.Tree.OnInsertNodeCallback(OnInsertNodecallback));
            
            //tree_control0.SetOnDeleteNodeHandler(new NXOpen.BlockStyler.Tree.OnDeleteNodeCallback(OnDeleteNodecallback));
            
            //tree_control0.SetOnPreSelectHandler(new NXOpen.BlockStyler.Tree.OnPreSelectCallback(OnPreSelectcallback));
            
            //tree_control0.SetOnSelectHandler(new NXOpen.BlockStyler.Tree.OnSelectCallback(OnSelectcallback));
            
            //tree_control0.SetOnStateChangeHandler(new NXOpen.BlockStyler.Tree.OnStateChangeCallback(OnStateChangecallback));
            
            //tree_control0.SetToolTipTextHandler(new NXOpen.BlockStyler.Tree.ToolTipTextCallback(ToolTipTextcallback));
            
            //tree_control0.SetColumnSortHandler(new NXOpen.BlockStyler.Tree.ColumnSortCallback(ColumnSortcallback));
            
            //tree_control0.SetStateIconNameHandler(new NXOpen.BlockStyler.Tree.StateIconNameCallback(StateIconNameCallback));
            
            //tree_control0.SetOnBeginLabelEditHandler(new NXOpen.BlockStyler.Tree.OnBeginLabelEditCallback(OnBeginLabelEditCallback));
            
            //tree_control0.SetOnEndLabelEditHandler(new NXOpen.BlockStyler.Tree.OnEndLabelEditCallback(OnEndLabelEditCallback));
            
            //tree_control0.SetOnEditOptionSelectedHandler(new NXOpen.BlockStyler.Tree.OnEditOptionSelectedCallback(OnEditOptionSelectedCallback));
            
            //tree_control0.SetAskEditControlHandler(new NXOpen.BlockStyler.Tree.AskEditControlCallback(AskEditControlCallback));
            
            //tree_control0.SetOnMenuHandler(new NXOpen.BlockStyler.Tree.OnMenuCallback(OnMenuCallback));;
            
            //tree_control0.SetOnMenuSelectionHandler(new NXOpen.BlockStyler.Tree.OnMenuSelectionCallback(OnMenuSelectionCallback));;
            
            //tree_control0.SetIsDropAllowedHandler(new NXOpen.BlockStyler.Tree.IsDropAllowedCallback(IsDropAllowedCallback));;
            
            //tree_control0.SetIsDragAllowedHandler(new NXOpen.BlockStyler.Tree.IsDragAllowedCallback(IsDragAllowedCallback));;
            
            //tree_control0.SetOnDropHandler(new NXOpen.BlockStyler.Tree.OnDropCallback(OnDropCallback));;
            
            //tree_control0.SetOnDropMenuHandler(new NXOpen.BlockStyler.Tree.OnDropMenuCallback(OnDropMenuCallback));
            
            //tree_control0.SetOnDefaultActionHandler(new NXOpen.BlockStyler.Tree.OnDefaultActionCallback(OnDefaultActionCallback));
            
            //------------------------------------------------------------------------------
            //------------------------------------------------------------------------------
            //Registration of StringBlock specific callbacks
            //------------------------------------------------------------------------------
            //stringLibRef1.SetKeystrokeCallback(new NXOpen.BlockStyler.StringBlock.KeystrokeCallback(KeystrokeCallback));
            
            //------------------------------------------------------------------------------
            //------------------------------------------------------------------------------
            //Registration of Explorer, explorer specific callbacks
            //------------------------------------------------------------------------------
            //explorer.SetNotifyNodeSelectedPreHandler(new NXOpen.BlockStyler.Explorer.NotifyNodeSelectedPreCallback(notifyNodeSelectedPreCallback));
            
            //explorer.SetNotifyNodeSelectedPostHandler(new NXOpen.BlockStyler.Explorer.NotifyNodeSelectedPostCallback(notifyNodeSelectedPostCallback));
            
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: dialogShown_cb
    //This callback is executed just before the dialog launch. Thus any value set 
    //here will take precedence and dialog will be launched showing that value. 
    //------------------------------------------------------------------------------
    public void dialogShown_cb()
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: apply_cb
    //------------------------------------------------------------------------------
    public int apply_cb()
    {
        int errorCode = 0;
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return errorCode;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: update_cb
    //------------------------------------------------------------------------------
    public int update_cb( NXOpen.BlockStyler.UIBlock block)
    {
        try
        {
            if(block == label0)
            {
            //---------Enter your code here-----------
            }
            else if(block == stringLabelTool)
            {
            //---------Enter your code here-----------
            }
            else if(block == doubleToolDiam)
            {
            //---------Enter your code here-----------
            }
            else if(block == separator011)
            {
            //---------Enter your code here-----------
            }
            else if(block == offsetOfTool)
            {
            //---------Enter your code here-----------
            }
            else if(block == separator02)
            {
            //---------Enter your code here-----------
            }
            else if(block == multiline_string01)
            {
            //---------Enter your code here-----------
            }
            else if(block == separator03)
            {
            //---------Enter your code here-----------
            }
            else if(block == stringLabelHolder)
            {
            //---------Enter your code here-----------
            }
            else if(block == separator04)
            {
            //---------Enter your code here-----------
            }
            else if(block == stringLibRef1)
            {
            //---------Enter your code here-----------
            }
            else if(block == separator05)
            {
            //---------Enter your code here-----------
            }
            else if(block == multiline_string0)
            {
            //---------Enter your code here-----------
            }
            else if(block == separator06)
            {
            //---------Enter your code here-----------
            }
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: ok_cb
    //------------------------------------------------------------------------------
    public int ok_cb()
    {
        int errorCode = 0;
        try
        {
            errorCode = apply_cb();
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return errorCode;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: cancel_cb
    //------------------------------------------------------------------------------
    public int cancel_cb()
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: filter_cb
    //------------------------------------------------------------------------------
    public int filter_cb(NXOpen.BlockStyler.UIBlock block, NXOpen.TaggedObject selectedObject)
    {
        return(NXOpen.UF.UFConstants.UF_UI_SEL_ACCEPT);
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: focusNotify_cb
    //This callback is executed when any block (except the ones which receive keyboard entry such as Integer block) receives focus.
    //------------------------------------------------------------------------------
    public void focusNotify_cb(NXOpen.BlockStyler.UIBlock block, bool focus)
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: keyboardFocusNotify_cb
    //This callback is executed when block which can receive keyboard entry, receives the focus.
    //------------------------------------------------------------------------------
    public void keyboardFocusNotify_cb(NXOpen.BlockStyler.UIBlock block, bool focus)
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: enableOKButton_cb
    //This callback allows the dialog to enable/disable the OK and Apply button.
    //------------------------------------------------------------------------------
    public bool enableOKButton_cb()
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return true;
    }
    //------------------------------------------------------------------------------
    //Treelist specific callbacks
    //------------------------------------------------------------------------------
    //public void OnExpandCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node)
    //{
    //}
    
    //public void OnInsertColumnCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID)
    //{
    //}
    
    //public void OnInsertNodecallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node)
    //{
    //}
    
    //public void OnDeleteNodecallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node)
    //{
    //}
    
    //public void OnPreSelectcallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID, bool Selected)
    //{
    //}
    
    //public void OnSelectcallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID, bool Selected)
    //{
    //}
    
    //public void OnStateChangecallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int State)
    //{
    //}
    
    //public string ToolTipTextcallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID)
    //{
    //}
    
    //public int ColumnSortcallback(NXOpen.BlockStyler.Tree tree, int columnID, NXOpen.BlockStyler.Node node1, NXOpen.BlockStyler.Node node2)
    //{
    //}
    
    //public string StateIconNameCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int state)
    //{
    //}
    
    //public Tree.BeginLabelEditState OnBeginLabelEditCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID)
    //{
    //}
    
    //public Tree.EndLabelEditState OnEndLabelEditCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID, string editedText)
    //{
    //}
    
    //public Tree.EditControlOption OnEditOptionSelectedCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID, int selectedOptionID, string selectedOptionText, Tree.ControlType type)
    //{
    //}
    
    //public Tree.ControlType AskEditControlCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID)
    //{
    //}
    
    //public void OnMenuCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID)
    //{
    //}
    
    //public void OnMenuSelectionCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int menuItemID)
    //{
    //}
    
    //public Node.DropType IsDropAllowedCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID, NXOpen.BlockStyler.Node targetNode, int targetColumnID)
    //{
    //}
    
    //public Node.DragType IsDragAllowedCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID)
    //{
    //}
    
    //public bool OnDropCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node[] node, int columnID, NXOpen.BlockStyler.Node targetNode, int targetColumnID, Node.DropType dropType, int dropMenuItemId)
    //{
    //}
    
    //public void OnDropMenuCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID, NXOpen.BlockStyler.Node targetNode, int targetColumnID)
    //{
    //}
    
    //public void OnDefaultActionCallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID)
    //{
    //}
    
    //------------------------------------------------------------------------------
    //StringBlock specific callbacks
    //------------------------------------------------------------------------------
    //public int KeystrokeCallback(NXOpen.BlockStyler.StringBlock string_block, string uncommitted_value)
    //{
    //}
    
    //------------------------------------------------------------------------------
    //public int  notifyNodeSelectedPreCallback(NXOpen.BlockStyler.Explorer explorer, int nextNode)
    //{
    //}
    
    //public void notifyNodeSelectedPostCallback(NXOpen.BlockStyler.Explorer explorer, int nextNode)
    //{
    //}
    
    
    //------------------------------------------------------------------------------
    //Function Name: GetBlockProperties
    //Returns the propertylist of the specified BlockID
    //------------------------------------------------------------------------------
    public PropertyList GetBlockProperties(string blockID)
    {
        PropertyList plist =null;
        try
        {
            plist = theDialog.GetBlockProperties(blockID);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return plist;
    }
    
}
