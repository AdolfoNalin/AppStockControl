using System;
using System.Collections.Generic;
using System.Text;

namespace AppStockControl.Helpers
{
    public class MessageException
    {
        public static string Message(Exception ex)
        {
            return $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}";
        }
    }
}
