﻿namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;

/// <summary>
/// Factory to typography custom section.
/// </summary>
public class TypographyCustomSectionFactory : BaseCustomSectionFactory<TypographyCustomSection, ITypographyProxy>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TypographyCustomSectionFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
