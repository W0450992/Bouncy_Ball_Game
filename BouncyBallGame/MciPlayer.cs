using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
namespace BouncyBallGame
{
    internal class MciPlayer
    {
        [DllImport("winmm.dll")]
        private static extern int mciSendString(String strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        [DllImport("winmm.dll")]
        public static extern int mciGetErrorString(int errCode, StringBuilder errMsg, int buflen);
        [DllImport("winmm.dll")]
        public static extern int mciGetDeviceID(string lpszDevice);

        public MciPlayer()
        {

        }

        public MciPlayer(string filename, string alias)
        {
            _medialocation = filename;
            _alias = alias;
            LoadMediaFile(_medialocation, _alias);
        }

        int _deviceid = 0;

        public int Deviceid
        {
            get { return _deviceid; }
        }

        private bool _isloaded = false;

        public bool Isloaded
        {
            get { return _isloaded; }
            set { _isloaded = value; }
        }

        private string _medialocation = "";

        public string MediaLocation
        {
            get { return _medialocation; }
            set { _medialocation = value; }
        }
        private string _alias = "";

        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }

        public bool LoadMediaFile(string filename, string alias)
        {
            _medialocation = filename;
            _alias = alias;
            StopPlaying();
            CloseMediaFile();
            string Pcommand = "open \"" + filename + "\" alias " + alias;
            int ret = mciSendString(Pcommand, null, 0, IntPtr.Zero);
            _isloaded = (ret == 0) ? true : false;
            if (_isloaded)
                _deviceid = mciGetDeviceID(_alias);
            return _isloaded;
        }

        public void PlayFromStart()
        {
            if (_isloaded)
            {
                string Pcommand = "play " + Alias + " from 0";
                int ret = mciSendString(Pcommand, null, 0, IntPtr.Zero);
            }
        }

        public void PlayFromStart(IntPtr callback)
        {
            if (_isloaded)
            {
                string Pcommand = "play " + Alias + " from 0 notify";
                int ret = mciSendString(Pcommand, null, 0, callback);
            }
        }

        public void PlayLoop()
        {
            if (_isloaded)
            {
                string Pcommand = "play " + Alias + " repeat";
                int ret = mciSendString(Pcommand, null, 0, IntPtr.Zero);
            }
        }

        public void CloseMediaFile()
        {
            string Pcommand = "close " + Alias;
            int ret = mciSendString(Pcommand, null, 0, IntPtr.Zero);
            _isloaded = false;

        }

        public void StopPlaying()
        {
            string Pcommand = "stop " + Alias;
            int ret = mciSendString(Pcommand, null, 0, IntPtr.Zero);
        }
    }
}
