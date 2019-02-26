using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class defines a class which helps with focus quirks.
    ///
    /// © Chris Hutchinson 2013
    /// 
    /// </summary>

    public static class Helper_WPFFocusFix
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Class variables.                                                   //
        //                                                                    //
        //--------------------------------------------------------------------//

        private delegate void MethodInvoker ();

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // f o c u s                                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Focus in a callback to run on another thread, ensuring that the    //
        // main UI thread is initialised by the time focus is set.            //
        // header.                                                            //
        // Obtained via:                                                      //
        //  http://apocryph.org/2006/09/10/wtf_is_wrong_with_wpf_focus/       //
        //                                                                    //
        // I don't really understand why this is necessary, or how it works,  //
        // but it does!                                                       //
        // Without it, the Lost_Focus code, for certain text boxes in some of //
        // the tools, which tries to return focus (to the textbox which has   //
        // just lost focus) if it detects that that the textbox contains an   //
        // invalid value, doesn't work: odd things happen; e.g.:              //
        //                                                                    //
        //  -   Symbol Set Generate tool: codepoint text boxes:               //
        //      The old textbox regains the focus, but then something         //
        //      mysteriously returns focus to the new element that got the    //
        //      focus when the Lost_Focus event was triggered.                // 
        //                                                                    //
        //  -   Form Sample: form file path text boxes:                       //
        //      The old textbox regains the focus, but then something         //
        //      mysteriously retriggers the Lost_Focus event, so the          //
        //      validation is repeated ad infinitum.                          // 
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void Focus (UIElement element)
        {
            ThreadPool.QueueUserWorkItem (delegate (Object objName)
            {
                UIElement elem = (UIElement) objName;

                elem.Dispatcher.Invoke (DispatcherPriority.Normal,

                    (MethodInvoker) delegate ()
                    {
                        elem.Focus ();
                        Keyboard.Focus (elem);
                    });

            }, element );
        }
    }
}
