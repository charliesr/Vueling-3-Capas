﻿using System.Collections.Generic;
using Vueling.Common.Logic;

namespace Vueling.DataAccess.DAO
{
    public interface ISelectDAO<T> : IFormatFactory
    {
        List<T> GetAll(DataTypeAccess dataTypeAccess);
    }
}
