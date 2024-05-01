﻿using Microsoft.AspNetCore.HttpLogging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            //Имя таблицы
            Type?.GetCustomAttributes(true).ToList().ForEach(attr => 
            {
                if (attr is AlternativeName name)
                    TableName = name.Name;
            });
            TableName ??= Type?.Name;

            //Значения
            Type?.GetProperties().ToList().ForEach(prop => 
            {
                bool isHaveAttr = false;

                var propertyAttrs = prop.GetCustomAttributes(true).ToList();

                foreach (var attr in propertyAttrs) 
                {
                    //Если есть аттрибут InclusionInHeader:
                    //Include - будет в шапке
                    //NotInclude - не будет в шапке
                    if (attr is InclusionInHeader inclusion)
                    {
                        if (inclusion.HeaderInclusion == HeaderInclusion.NotInclude)
                        {   
                            if (PropName.Any(pn => pn.Key == prop.Name))
                                PropName.Remove(prop.Name);

                            isHaveAttr = true;

                            break;
                        }
                    }

                    //Если есть атрибут AlternativeName, то ИМЯ по значению атрибута
                    if (attr is AlternativeName name)
                    {
                        PropName.Add(prop.Name, name.Name);
                        isHaveAttr = true;
                    }
                }

                //Если нет атрибута AlternativeName, то ИМЯ по названию свойства
                if (!isHaveAttr)
                    PropName.Add(prop.Name, prop.Name);

                isHaveAttr = false;
            });
        }

        public TableValueResult? GetValues<ConT>(ConT convObj, bool withoutCheck = false, bool withNotInclude = false) 
        {
            if(!withoutCheck)
                if (convObj?.GetType().Name != Type?.Name)
                    return null;

            var properties = convObj?.GetType()
                .GetProperties()
                .ToList();

            TableValueResult includedValues = new();

            int i = 0;

            foreach (var prop in properties ?? []) 
            {
                var propertyAttrs = prop.GetCustomAttributes(true).ToList();

                bool include = true;

                foreach (var attr in propertyAttrs)
                {
                    if (attr is InclusionInHeader inclusion)
                    {
                        if (inclusion.HeaderInclusion == HeaderInclusion.NotInclude)
                        {
                            include = false;
                            break;
                        }
                    }
                }

                if (include || withNotInclude)
                    includedValues[prop.Name] = prop.GetValue(convObj);

                i++;
            }

            return includedValues;
        }

        public bool HasAttribute<AttrT>(string name) 
            where AttrT : Attribute 
        {
            var property = Type?
                .GetProperty(name);

            if (property != null) 
            {
                var propertyAttrs = property.GetCustomAttributes(true).ToList();

                foreach (var attr in propertyAttrs)
                {
                    if (attr is AttrT)
                        return true;
                }
            }

            return false;   
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
