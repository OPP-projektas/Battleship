﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFClient.Entities.ChainOfResponsability
{
    public class RedHandler : AbstractHandler
    {
        public RedHandler(int level) : base(level)
        {

        }

        public override TextBlock Handle(TextBlock request, int toCheckLevel)
        {
            if (level == toCheckLevel)
            {
                request.Foreground = Brushes.Red;
                return request;
            }
            else
            {
                return base.Handle(request, toCheckLevel);
            }
        }
    }
}
