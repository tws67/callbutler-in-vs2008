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
using System.Text;
using CallButler.Service.Plugin;

namespace T2.CallButler.Service.Plugin.ExternalDialtone
{
    public class ExternalDialtoneModule : CallButlerAddonModulePlugin
    {
        private const string productID = "CB-M-ED-1";

        public ExternalDialtoneModule()
        {
        }

        public override bool IsLicensed
        {
            get
            {
                return true;
            }
        }

        public override Guid PluginID
        {
            get
            {
                return new Guid("D473A5A9-444B-4155-8565-454C155AE871");
            }
        }

        public override string PluginName
        {
            get
            {
                return "CallButler External Dialtone Module";
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }

        public override void OnExternalAction(CallButlerScriptContext scriptContext, string command, string commandData)
        {
            if (command == "ValidatePasscode")
            {
                // Check on the passcode
                foreach (string passcode in Properties.Settings.Default.Passcodes)
                {
                    if (passcode == commandData)
                        scriptContext.SignalExternalScriptEvent("ValidPasscode");
                }
                
                scriptContext.SignalExternalScriptEvent("InvalidPasscode");
            }
        }

        public override object ExchangePluginData(string method, object data)
        {
            if (method == "GetPasscodes")
            {
                List<string> passcodes = new List<string>();

                foreach(string passcode in Properties.Settings.Default.Passcodes)
                {
                    passcodes.Add(passcode);
                }

                return passcodes.ToArray();
            }
            else if (method == "SetPasscodes")
            {
                Properties.Settings.Default.Passcodes.Clear();
                Properties.Settings.Default.Passcodes.AddRange((string[])data);
                Properties.Settings.Default.Save();
            }
            else if (method == "GetLicenseInfo")
            {
                return "";
            }
            else if (method == "SetLicenseName")
            {
                Properties.Settings.Default.LicenseName = (string)data;
                Properties.Settings.Default.Save();
            }
            else if (method == "SetLicenseKey")
            {
                Properties.Settings.Default.LicenseKey = (string)data;
                Properties.Settings.Default.Save();
            }

            return null;
        }

        public override bool ModuleReturnsScriptPath
        {
            get
            {
                return false;
            }
        }

        public override string OnStartScript(CallButlerScriptContext scriptContext)
        {
            return Properties.Resources.ExternalDialtoneModuleScript;
        }
    }
}
