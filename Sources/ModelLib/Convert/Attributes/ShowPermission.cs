﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.Convert.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Interface)]
    public class ShowPermission : Attribute
    {
        public List<string> Permissions { get; private set; }

        public ShowPermission(string permissionName) 
        {
            Permissions = permissionName.Split([ " ", ","], StringSplitOptions.None).ToList();
        }
    }
}