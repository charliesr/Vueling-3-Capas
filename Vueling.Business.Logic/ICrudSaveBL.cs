﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vueling.Business.Logic
{
    public interface ICrudSaveBL<T>
    {
        T Add(T entity);
    }
}
