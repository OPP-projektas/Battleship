﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Strategy
{
    public interface IStrategy
    {
        object StrategicSort(object data);
    }
}
