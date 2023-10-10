﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Facotries
{
    public class OccupiedCell : IFactory
    {
        public void Place()
        {
            Logger logger = Logger.GetInstance();
            logger.Log($"Class = {GetType().Name}, method = Place");
        }
    }
}