﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFClient.Entities.Decorator
{
    public class UnderlineDecorator : Decorator
    {
        public UnderlineDecorator(TextComponent component) : base(component)
        {
        }

        public override Run GetFormattedText(Run run)
        {
            run.TextDecorations = TextDecorations.Underline;
            return base.GetFormattedText(run);
        }
    }
}
