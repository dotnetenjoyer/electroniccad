﻿using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.Properties.Helpers;
using System.Linq.Expressions;

namespace ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

/// <summary>
/// Builds property object configuration with primitive properties.
/// </summary>
public class PrimitivePropertyObjectConfigurationBuilder<SELF, TPropertiesProxy> : PropertyObjectConfigurationBuilder<SELF, TPropertiesProxy>
    where SELF : PrimitivePropertyObjectConfigurationBuilder<SELF, TPropertiesProxy>
{
    /// <summary>
    /// Adds primitive property to configuration.
    /// </summary>
    /// <param name="name">Name of primitive property.</param>
    public SELF HasPrimitive<TValue>(Expression<Func<TPropertiesProxy, TValue>> propertySelector)
    {
        var property = ReflectionHelper.FindPropertyInfo(propertySelector);

        var primitive = new PrimitivePropertyConfiguration() 
        { 
            Name = property.Name,
            SourceProperty = property,
        };

        PropertyConfigurations.Add(primitive);
        
        return (SELF)this;
    }
}