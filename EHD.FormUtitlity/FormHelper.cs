using System.Windows.Forms;
using System;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace EHD.FormUtitlity {
    public class FormHelper {

        #region Set Form Controls ready-only
        public void SetControlsReadonly(Control parentControl, bool AutoDisableButtons = false) {

            //Set textboxes to be readonly
            for(int i = parentControl.Controls.Count - 1; i >= 0; i--) {
                Control ctr = parentControl.Controls[i];
                if(ctr is TextBox) {
                    ((TextBox)ctr).ReadOnly = true;
                } else if(ctr is DateTimePicker || ctr is ComboBox || ctr is NumericUpDown) {
                    //Replace with Textboxs
                    TextBox tb = new TextBox();
                    tb.Text = getControlText(ctr);
                    tb.Name = "txt_" + ctr.Name;
                    tb.ReadOnly = true;
                    parentControl.Controls.Add(tb);
                    tb.Location = ctr.Location;
                    tb.Size = ctr.Size;

                    parentControl.Controls.Remove(ctr);
                    parentControl.Controls.SetChildIndex(tb, i);
                } else if(ctr is RadioButton) {
                    ((RadioButton)ctr).AutoCheck = false;
                }else if(ctr is CheckBox){
                    ((CheckBox)ctr).AutoCheck = false;
                }else if(ctr is Button){
                    if (AutoDisableButtons) {
                        string ctrText = ctr.Text.Trim().ToLower();
                        if (ctrText != "exit" && ctrText != "close" && ctrText != "cancel" && ctrText != "quit") {
                            (ctr as Button).Enabled = false;
                        }
                    }
                } else if(ctr.HasChildren) {
                    SetControlsReadonly(ctr);
                }
            }

        }

        private string getControlText(Control ctr) {
            string strCtrlText = string.Empty;

            if(ctr != null) {
                if(ctr is DateTimePicker) {
                    DateTimePicker dt = (DateTimePicker)ctr;
                    strCtrlText = (dt.Format == DateTimePickerFormat.Custom ?
                        dt.Value.ToString(dt.CustomFormat) :
                        (DateFormatString.Trim().Length > 0 ? dt.Value.ToString(DateFormatString) : dt.Value.ToString())
                        );
                } else if(ctr is ComboBox) {
                    strCtrlText = ((ComboBox)ctr).Text;
                } else if(ctr is NumericUpDown) {
                    strCtrlText = ((NumericUpDown)ctr).Text;
                } else {
                    //Ignore
                }
            }

            return strCtrlText;
        }
        #endregion

        #region Properties
        public string DateFormatString { get; set; }
        #endregion Properties

        #region Constructors
        public FormHelper() { }

        public FormHelper(string dateFormatString) {
            DateFormatString = dateFormatString;
        }
        #endregion Constructors
    }
}
