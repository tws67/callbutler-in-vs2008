///////////////////////////////////////////////////////////////////////////////////////////////
//
//    This File is Part of the CallButler Open Source PBX (http://www.codeplex.com/callbutler
//
//    Copyright (c) 2005-2008, Jim Heising
//    All rights reserved.
//
//    Redistribution and use in source and binary forms, with or without modification,
//    are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice,
//      this list of conditions and the following disclaimer.
//
//    * Redistributions in binary form must reproduce the above copyright notice,
//      this list of conditions and the following disclaimer in the documentation and/or
//      other materials provided with the distribution.
//
//    * Neither the name of Jim Heising nor the names of its contributors may be
//      used to endorse or promote products derived from this software without specific prior
//      written permission.
//
//    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
//    IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
//    INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
//    NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//    PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
//    WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
//    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
//    POSSIBILITY OF SUCH DAMAGE.
//
///////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace CallButler.ExceptionManagement.Dialogs
{
    public partial class ShowErrorDialog : Form
    {
        public ShowErrorDialog()
        {
            InitializeComponent();
        }

        public ShowErrorDialog(ErrorPacket ePack)
        {
            InitializeComponent();
            LoadErrorContent(ePack);
        }

        private void LoadErrorContent(ErrorPacket ePack)
        {
            FieldInfo [] fis = typeof(ErrorPacket).GetFields();

            foreach (FieldInfo fi in fis)
            {
                object[] atts = fi.GetCustomAttributes( typeof(ShowOnErrorDialog), false);

                if (atts.Length > 0)
                {
                    ShowOnErrorDialog s = (ShowOnErrorDialog)atts[0];

                    txtErrorInfo.Text += s.FriendlyName + ": " + fi.GetValue(ePack) + "\r\n\r\n";
                    //DataRow row = tbl.NewRow();
                    //row[0] = s.FriendlyName;
                    //row[1] = fi.GetValue(ePack);
                    //tbl.Rows.Add(row);
                }
            }
        }
    }
}