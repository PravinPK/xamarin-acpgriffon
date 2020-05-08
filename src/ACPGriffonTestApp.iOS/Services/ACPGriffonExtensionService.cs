﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using Com.Adobe.Marketing.Mobile;

namespace ACPGriffonTestApp.iOS
{
    public class ACPGriffonExtensionService : IACPGriffonExtensionService
    {
        TaskCompletionSource<string> stringOutput;
        private string sessionUrl = "";

        public ACPGriffonExtensionService()
        {
        }

        // ACPCore methods
        public TaskCompletionSource<string> GetExtensionVersionCore()
        {
            stringOutput = new TaskCompletionSource<string>();
            stringOutput.SetResult(ACPCore.ExtensionVersion);
            return stringOutput;
        }

        public TaskCompletionSource<string> GetPrivacyStatus()
        {
            stringOutput = new TaskCompletionSource<string>();
            Action<ACPMobilePrivacyStatus> callback = new Action<ACPMobilePrivacyStatus>(handleCallback);
            ACPCore.GetPrivacyStatus(callback);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SetAdvertisingIdentifier()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.SetAdvertisingIdentifier("testAdvertisingIdentifier");
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SetLogLevel()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.LogLevel = ACPMobileLogLevel.Verbose;
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> SetPrivacyStatus()
        {
            stringOutput = new TaskCompletionSource<string>();
            ACPCore.SetPrivacyStatus(ACPMobilePrivacyStatus.OptIn);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> TrackAction()
        {
            stringOutput = new TaskCompletionSource<string>();
            var data = new NSMutableDictionary<NSString, NSString>
            {
                ["key"] = new NSString("value")
            };
            ACPCore.TrackAction("action", data);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> TrackState()
        {
            stringOutput = new TaskCompletionSource<string>();
            var data = new NSMutableDictionary<NSString, NSString>
            {
                ["key"] = new NSString("value")
            };
            ACPCore.TrackState("state", data);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        public TaskCompletionSource<string> UpdateConfig()
        {
            stringOutput = new TaskCompletionSource<string>();
            var config = new NSMutableDictionary<NSString, NSObject>
            {
                ["someConfigKey"] = new NSString("configValue"),
                ["analytics.batchLimit"] = new NSNumber(5)
            };
            ACPCore.UpdateConfiguration(config);
            stringOutput.SetResult("completed");
            return stringOutput;
        }

        // ACPGriffon methods
        public TaskCompletionSource<string> GetExtensionVersionGriffon()
        {
            stringOutput = new TaskCompletionSource<string>();
            stringOutput.SetResult(ACPGriffon.ExtensionVersion);
            return stringOutput;
        }

        public TaskCompletionSource<string> StartSession()
        {
            stringOutput = new TaskCompletionSource<string>();
            if(sessionUrl.Length > 0)
            {
                NSUrl url = new NSUrl(sessionUrl);
                ACPGriffon.StartSession(url);
            }
            else
            {
                Console.WriteLine("Invalid session url");
            }
            stringOutput.SetResult("");
            return stringOutput;
        }

        // get text input from Entry
        public void GetSessionUrlFromEntry(string newSessionUrl)
        {
            sessionUrl = newSessionUrl;
        }

        // callbacks
        private void handleCallback(ACPMobilePrivacyStatus privacyStatus)
        {
            Console.WriteLine("Privacy status: " + privacyStatus.ToString());
        }

        private void handleCallback(NSString content)
        {
            if (content == null)
            {
                Console.WriteLine("String callback is null");
            }
            else
            {
                Console.WriteLine("String callback: " + content);
            }
        }

    }

}