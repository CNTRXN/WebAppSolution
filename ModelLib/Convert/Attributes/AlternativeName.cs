﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Interface)]
    public class AlternativeName : Attribute
    {
        public string Name { get; private set; }

        public AlternativeName() 
        {
            Name = string.Empty;
        }

        public AlternativeName(string name) 
        {
            Name = name;
        }

        /*public AlternativeName(string name, string lang)
        {
            Name = name;
        }*/
    }
}