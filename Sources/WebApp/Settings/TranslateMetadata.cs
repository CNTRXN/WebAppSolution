﻿using Microsoft.AspNetCore.HttpLogging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebApp.Attributes;
using WebApp.Models;

namespace WebApp.Settings
{


    public class TranslateMetadata<T>
    {
        public string? TableName { get; private set; }

        public Dictionary<string,string> PropName { get; private set; } = [];

        public int PropsCount => PropName.Count;

        private Type? _type;
        public Type? Type 
        {
            get 
            {
                Type type = typeof(T);

                if(_type != null)
                    type = _type;

                return type;
            }

            private set 
            {
                _type = value;
            } 
        } 

        public TranslateMetadata()
        {

        }

        public TranslateMetadata(string lang)
        {

        }

        private void Translate() 
        {
            Type?.GetCustomAttributes(true).ToList().ForEach(attr => 
            {
                if (attr is AlternativeName name)
                    TableName = name.Name;
            });
            TableName ??= Type?.Name;

            Type?.GetProperties().ToList().ForEach(prop => 
            {
                bool isHaveAttr = false;

                prop.GetCustomAttributes(true).ToList().ForEach(attr => 
                {
                    if (attr is AlternativeName name) 
                    {
                        PropName.Add(name.Name, name.Name);
                        isHaveAttr = true;
                    }  
                });

                if (!isHaveAttr)
                    PropName.Add(prop.Name, prop.Name);

                isHaveAttr = false;
            });
        }

        public void Assign<B>()
        {
            bool isSubclass = typeof(B).IsSubclassOf(typeof(T)) || typeof(B).GetInterfaces().Contains(typeof(T));

            if (isSubclass)
                Type = typeof(B);
            else
                Type = typeof(T);

            Translate();
        }

        public List<ConType?> Converter<ConType>(IList list) where ConType : class
        {
            Assign<ConType>();

            List<ConType?> items = [];

            foreach (var item in list)
            {
                try
                {
                    items.Add(item as ConType);
                }
                catch 
                {

                }
            }

            return items;
        }
    }
}
