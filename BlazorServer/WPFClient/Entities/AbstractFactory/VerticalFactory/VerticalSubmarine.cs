﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.AbstractFactory.VerticalFactory
{
    public class VerticalSubmarine : Ship
    {
        public VerticalSubmarine(string username) : base(username)
        {
            Logger logger = Logger.GetInstance();
            logger.Log($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");

            Length = 3;
        }
    }
}
