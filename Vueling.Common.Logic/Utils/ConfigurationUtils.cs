﻿using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using Vueling.Common.Logic.Utils;

namespace Vueling.Common.Logic
{
    public static class ConfigurationUtils
    {
        public static void SaveFormat(string format)
        {
            ConfigurationManager.AppSettings["ConfiguredAccessType"] = format;
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).Save();
        }

        public static string LoadFormat()
        {
            return ConfigurationManager.AppSettings["ConfiguredAccessType"];
        }

        public static IVuelingLogger LoadLogger(Type declaringType)
        {
            var type = Assembly.GetExecutingAssembly().GetType(new StringBuilder(Assembly.GetExecutingAssembly().GetName().Name).Append(".Utils").Append(".").Append(LoadVariable("LoggerFramework")).ToString());
            return (IVuelingLogger)Activator.CreateInstance(type,new object[] { declaringType });
        }

        private static string LoadVariable(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetConnecionString()
        {
            return LoadVariable("ConnectionString");
        }

        public static string GetFilePath<T>(DataTypeAccess dataTypeAccess)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), typeof(T).Name + "." + dataTypeAccess.ToString());
        }

    }
}