﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Facotries
{
    public class FreeCell : IFactory
    {
        public void Place()
        {
            Logger logger = Logger.GetInstance();
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
        }
    }
}
