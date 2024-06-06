﻿using System.Collections;
using ModelLib.Convert.Attributes;

namespace ModelLib.Convert.Table
{
    public class TranslateMetadata<T>
    {
        public string? TableName { get; private set; }

        //private Dictionary<string, string> PropName { get; set; } = [];

        public int PropsCount { get; private set; }

        private Type? _type;
        public Type? Type
        {
            get
            {
                Type type = typeof(T);

                if (_type != null)
                    type = _type;

                return type;
            }

            private set
            {
                _type = value;
            }
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
            /*Type?.GetProperties().ToList().ForEach(prop =>
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
                        if (inclusion.HeaderInclusion == PropertyInclusion.NotInclude)
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
            });*/
        }


        public TableValueResult? GetValues<ConT>(ConT convObj, bool withoutCheck = false, bool withNotInclude = false)
        {
            if (!withoutCheck)
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
                        if (inclusion.HeaderInclusion == PropertyInclusion.NotInclude)
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

        public Dictionary<string, string> GetPropName(PropertyInclusion headerInclusion, bool includeAll = false)
        {
            Dictionary<string, string> foundedProps = [];

            Type?.GetProperties().ToList().ForEach(prop => 
            {
                bool isHaveAttr = false;

                var propertyAttrs = prop.GetCustomAttributes(true).ToList();

                foreach (var attr in propertyAttrs)
                {
                    if (attr is InclusionInHeader inclusion)
                    {
                        if(!includeAll)
                        if (inclusion.HeaderInclusion == PropertyInclusion.NotInclude)
                        {
                            if (foundedProps.Any(pn => pn.Key == prop.Name))
                                foundedProps.Remove(prop.Name);

                            isHaveAttr = true;

                            break;
                        }
                    }

                    //Если есть атрибут AlternativeName, то ИМЯ по значению атрибута
                    if (attr is AlternativeName name)
                    {
                        foundedProps.Add(prop.Name, name.Name);
                        isHaveAttr = true;
                    }
                }

                //Если нет атрибута AlternativeName, то ИМЯ по названию свойства
                if (!isHaveAttr)
                    foundedProps.Add(prop.Name, prop.Name);

                isHaveAttr = false;
            });

            return foundedProps;
        }


        public Dictionary<string, string> GetFormPropName() 
        {
            Dictionary<string, string> formProperties = [];

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
                    if (attr is InclusionInForm inclusion)
                    {
                        if (inclusion.HeaderInclusion == PropertyInclusion.NotInclude)
                        {
                            if (formProperties.Any(pn => pn.Key == prop.Name))
                                formProperties.Remove(prop.Name);

                            isHaveAttr = true;

                            break;
                        }
                    }

                    //Если есть атрибут AlternativeName, то ИМЯ по значению атрибута
                    if (attr is AlternativeName name)
                    {
                        formProperties.Add(prop.Name, name.Name);
                        isHaveAttr = true;
                    }
                }

                //Если нет атрибута AlternativeName, то ИМЯ по названию свойства
                if (!isHaveAttr)
                    formProperties.Add(prop.Name, prop.Name);

                isHaveAttr = false;
            });

            PropsCount = formProperties.Count;

            return formProperties;
        }

        public Dictionary<string, string> GetTablePropName() 
        {
            Dictionary<string, string> tableProperties = [];

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
                        if (inclusion.HeaderInclusion == PropertyInclusion.NotInclude)
                        {
                            if (tableProperties.Any(pn => pn.Key == prop.Name))
                                tableProperties.Remove(prop.Name);

                            isHaveAttr = true;

                            break;
                        }
                    }

                    //Если есть атрибут AlternativeName, то ИМЯ по значению атрибута
                    if (attr is AlternativeName name)
                    {
                        tableProperties.Add(prop.Name, name.Name);
                        isHaveAttr = true;
                    }
                }

                //Если нет атрибута AlternativeName, то ИМЯ по названию свойства
                if (!isHaveAttr)
                    tableProperties.Add(prop.Name, prop.Name);

                isHaveAttr = false;
            });

            PropsCount = tableProperties.Count;

            return tableProperties;
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

        public object? GetAttributeValue<AttrT>(string name, string attrPropertyName)
            where AttrT : Attribute
        {
            var property = Type?
                .GetProperty(name);

            object? foundedValue = null;

            if (property != null) 
            {
                var propertyAttrs = property.GetCustomAttributes(true).ToList();

                foreach (var attr in propertyAttrs) 
                {
                    if (attr is AttrT) 
                    {
                        var attrProperties = attr.GetType().GetProperties();

                        foreach (var attrProp in attrProperties) 
                        {
                            if (attrProp.Name == attrPropertyName) 
                            {
                                foundedValue = attrProp.GetValue(attr, null);
                            }
                        }

                        break;
                    }
                }
            }

            return foundedValue;
        }

        public bool CompareAttributesValues<AttrT>(string name, string attrPropertyName, object value)
            where AttrT : Attribute
        {
            var foundResult = GetAttributeValue<AttrT>(name, attrPropertyName);

            bool isEquals = false;

            if (foundResult != null) 
            {
                if (foundResult is IEnumerable<object> listResult) 
                {
                    /*foreach (var item in listResult) 
                    {
                        if (item.ToString() == value.ToString())
                            isEquals = true;

                        break;
                    }*/
                    isEquals = listResult.Contains(value);
                }
            }

            return isEquals;
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
