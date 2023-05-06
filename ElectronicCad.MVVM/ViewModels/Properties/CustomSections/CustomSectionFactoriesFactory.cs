﻿using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Colors;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.MVVM.Properties.Implementation.CustomSections;

/// <inheritdoc cref="ICustomSectionFactoriesFactory">
public class CustomSectionFactoriesFactory : ICustomSectionFactoriesFactory
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CustomSectionFactoriesFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public ICustomSectionFactory CreateFactory(Type customSectionType)
    {
        if (customSectionType == typeof(TransformationCustomSection))
        {
            var factory = ActivatorUtilities.CreateInstance(serviceProvider, typeof(TransformationCustomSectionFactory));
            return (ICustomSectionFactory)factory;
        }
        else if(customSectionType == typeof(ColorsCustomSection))
        {
            var factory = ActivatorUtilities.CreateInstance(serviceProvider, typeof(ColorsCustomSectionFactory));
            return (ICustomSectionFactory)factory;
        }

        throw new NotSupportedException($"{customSectionType} is not supported.");
    }
}