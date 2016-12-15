﻿/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   This program can be provided to you by Xceed Software Inc. under a
   proprietary commercial license agreement for use in non-Open Source
   projects. The commercial version of Extended WPF Toolkit also includes
   priority technical support, commercial updates, and many additional 
   useful WPF controls if you license Xceed Business Suite for WPF.

   Visit http://xceed.com and follow @datagrid on Twitter.

  **********************************************************************/

using Microsoft.Practices.Prism.Regions;
using Samples.Infrastructure.Controls;

namespace Samples.Modules.Text.Views
{
  /// <summary>
  /// Interaction logic for MaskedTextBoxView.xaml
  /// </summary>
  [RegionMemberLifetime( KeepAlive = false )]
  public partial class MaskedTextBoxView : DemoView
  {
    public MaskedTextBoxView()
    {
      InitializeComponent();
      _mask.TextChanged += new System.Windows.Controls.TextChangedEventHandler( _mask_TextChanged );
    }

    void _mask_TextChanged( object sender, System.Windows.Controls.TextChangedEventArgs e )
    {
      _maskedTextBox.Value = null;
    }
  }
}
