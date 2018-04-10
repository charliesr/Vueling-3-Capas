﻿using System;

namespace Vueling.Common.Logic
{
    public enum DataTypeAccess
    {
        txt = 1,
        json = 2,
        xml = 3,
        sqlDB = 4
    }

    public static class EnumsHelper
    {
        public static DataTypeAccess StringToDataTypeAccess(string from)
        {
            return (DataTypeAccess)Enum.Parse(typeof(DataTypeAccess), from);
        }
    }
}
