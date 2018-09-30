using Quanlybanhang.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Quanlybanhang.Scripts.Source.Utils
{
    public class MessageHelper
    {
        static private PlaceHolder _placeHolder;

        public static void SetPlaceHolder(PlaceHolder plc)
        {
            _placeHolder = plc;
        }

        public static void ShowSucessMessage(string message)
        {
            if (_placeHolder != null)
            {
                ValidatorHelper.showSucessMessage(_placeHolder, message);
            }
        }

        public static void ShowErrorMessage(string message)
        {
            if (_placeHolder != null)
            {
                ValidatorHelper.showErrorMessage(_placeHolder, message);
            }
        }
    }
}