﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.AbstractFactory.VerticalFactory
{
    public class HorizontalCarrier : Ship
    {
        public HorizontalCarrier(string username) : base(username)
        {
            Logger logger = Logger.GetInstance();
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message,4);

            Length = 4;
        }
    }
}
