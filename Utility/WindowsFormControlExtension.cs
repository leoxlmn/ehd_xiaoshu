using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Utility {
    public static class WindowsFormControlExtension {

        public static void AdjustDropdownWidth(this ComboBox ctrl){
            int width = ctrl.DropDownWidth;
            Graphics g = ctrl.CreateGraphics();
            Font font = ctrl.Font;
            int vertScrollBarWidth = ctrl.Items.Count > ctrl.MaxDropDownItems ?
                SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (object item in ctrl.Items) {
                newWidth = (int)g.MeasureString(item.ToString(), font).Width + vertScrollBarWidth;
                if (width < newWidth) {
                    width = newWidth;
                }
            }
            ctrl.DropDownWidth = width;
        }

    }
}
