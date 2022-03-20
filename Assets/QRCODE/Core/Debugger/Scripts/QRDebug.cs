using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using QRCode.Extensions;
using UnityEditor;
using UnityEngine;

namespace QRCode.Debugging
{
    public static class QRDebug
    {
        private static bool isIinit = false;
        
        private static string folderPath = Application.dataPath + "/DebugFolder";
        private static int logIndex = 0;
        private static string filePath = folderPath + QRDebugSettings.Instance.debugFileName + " " + logIndex + ".txt";

        
        //--<Properties>
        public static DebuggingWindow GetDebuggingWindow { get; private set; }

        //---<CORE>----------------------------------------------------------------------------------------------------<

        public static void DebugText(object title, FrenchPallet color, object message)
        {
            if (IsInit())
            {
                GetDebuggingWindow.Debug($"<b><color={ColorPallets.GetFrenchPalletColor(color).ToHtlmColor()}>{title} " +
                $"-> </color></b> {message}");
            }
        }
        
        public static bool IsInit()
        {
            if (!Application.isPlaying)
            {
                return false;
            }
            
            if (!isIinit)
            {
                isIinit = true;
                GetDebuggingWindow = DebuggingWindow.CreateInstance();

                if (PlayerPrefs.HasKey("logIndex") && PlayerPrefs.HasKey("logIndexLast"))
                {
                    logIndex++;
                    PlayerPrefs.SetInt("logIndex", logIndex);
                }
                else
                {
                    PlayerPrefs.SetInt("logIndex", 0);
                }
            }

            return isIinit;
        }

        private static void UpdateLogFile(object title, object message)
        {
            if (!Application.isPlaying)
            {
                return;
            }
            
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);


            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, $"Log of {DateTime.UtcNow.ToLocalTime()} \n\n");
            }
            else
            {
                File.AppendAllText(filePath, $"{Time.time}: {title} - {message} \n");
            }
        }
        
        //---<DEBUG EXTENSIONS>----------------------------------------------------------------------------------------<
        public static void Log(object title, FrenchPallet color, object message)
        {
            IsInit();
            
            Debug.Log($"<b><color={ColorPallets.GetFrenchPalletColor(color).ToHtlmColor()}>{title} " +
                      $"-> </color></b> {message}");
            
            UpdateLogFile(title, message);
        }
        
        public static void LogError(object title, FrenchPallet color, object message)
        {
            IsInit();
            
            Debug.LogError($"<b><color={ColorPallets.GetFrenchPalletColor(color).ToHtlmColor()}>{title} " +
                           $"-> </color></b> {message}");
            
            UpdateLogFile(title, message);
        }
        
        public static void LogWarning(object title, FrenchPallet color, object message)
        {
            IsInit();
            
            Debug.LogWarning($"<b><color={ColorPallets.GetFrenchPalletColor(color).ToHtlmColor()}>{title} " +
                             $"-> </color></b> {message}");
            
            UpdateLogFile(title, message);
        }
    }
}
