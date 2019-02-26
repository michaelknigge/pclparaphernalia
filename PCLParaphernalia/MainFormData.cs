using System;

namespace PCLParaphernalia
{
    /// <summary>
    /// 
    /// Class manages temporary storage of common data for the main form.
    /// 
    /// © Chris Hutchinson 2011
    /// 
    /// </summary>

    static class MainFormData
    {
        //--------------------------------------------------------------------//
        //                                                        F i e l d s //
        // Fields (class variables).                                          //
        //                                                                    //
        //--------------------------------------------------------------------//

        private static Double _windowScale = 1.0;

        private static Boolean _versionChange = false;

        private static Int32 _versionMajorOld = -1;
        private static Int32 _versionMinorOld = -1;
        private static Int32 _versionBuildOld = -1;
        private static Int32 _versionRevisionOld = -1;

        private static Int32 _versionMajorCrnt = -1;
        private static Int32 _versionMinorCrnt = -1;
        private static Int32 _versionBuildCrnt = -1;
        private static Int32 _versionRevisionCrnt = -1;

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // c h e c k O l d V e r s i o n                                      //
        //--------------------------------------------------------------------//
        //                                                                    //
        // This function checks if this is the first run of a new version,    //
        // and if so, if the old version was the one specified by the         //
        // supplied paprameters.                                              //
        //                                                                    //
        // Version data was first used after version 2.5.0.0                  //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean checkOldVersion (Int32 major,
                                               Int32 minor,
                                               Int32 build,
                                               Int32 revision)
        {
            Boolean oldMatch = false;

            if (VersionChange)
            {
                if ((major == _versionMajorOld) &&
                    (minor == _versionMinorOld) &&
                    (build == _versionBuildOld) &&
                    (revision == _versionRevisionOld))
                {
                    oldMatch = true;
                }
            }

            return oldMatch;
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // g e t V e r s i o n D a t a                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Retrieve stored version data; first used after version 2.5.0.0     //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void getVersionData (Boolean crnt,
                                           ref Int32 major,
                                           ref Int32 minor,
                                           ref Int32 build,
                                           ref Int32 revision)
        {
            if (crnt)
            { 
                major    = _versionMajorCrnt;
                minor    = _versionMinorCrnt;
                build    = _versionBuildCrnt;
                revision = _versionRevisionCrnt;
            }
            else
            { 
                major    = _versionMajorOld;
                minor    = _versionMinorOld;
                build    = _versionBuildOld;
                revision = _versionRevisionOld;
            }
        }

        //--------------------------------------------------------------------//
        //                                                        M e t h o d //
        // s e t V e r s i o n D a t a                                        //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Set version data; first used after version 2.5.0.0                 //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static void setVersionData (Boolean crnt,
                                           Int32 major,
                                           Int32 minor,
                                           Int32 build,
                                           Int32 revision)
        {
            if (crnt)
            {
                _versionMajorCrnt    = major;
                _versionMinorCrnt    = minor;
                _versionBuildCrnt    = build;
                _versionRevisionCrnt = revision;
            }
            else
            {
                _versionMajorOld    = major;
                _versionMinorOld    = minor;
                _versionBuildOld    = build;
                _versionRevisionOld = revision;
            }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // V e r s i o n C h a n g e                                          //
        //--------------------------------------------------------------------//
        //                                                                    //
        // Return or set boolean indicating if version has changed.           //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Boolean VersionChange
        {
            get { return _versionChange; }
            set
            {
                _versionChange = value;
            }
        }

        //--------------------------------------------------------------------//
        //                                                    P r o p e r t y //
        // W i n d o w S c a l e                                              //
        //                                                                    //
        //--------------------------------------------------------------------//

        public static Double WindowScale
        {
            get { return _windowScale; }
            set
            {
                _windowScale = value;
            }
        }
    }
}
