using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WWWGame.UI.Controls
{
  public class GroupBox : ContentControl
  {
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof (UIElement), typeof (GroupBox), (PropertyMetadata) null);
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof (CornerRadius), typeof (GroupBox), (PropertyMetadata) null);
    public static readonly DependencyProperty HeaderCornerRadiusProperty = DependencyProperty.Register("HeaderCornerRadius", typeof (CornerRadius), typeof (GroupBox), (PropertyMetadata) null);
    public static readonly DependencyProperty HeaderBorderThicknessProperty = DependencyProperty.Register("HeaderBorderThickness", typeof (Thickness), typeof (GroupBox), (PropertyMetadata) null);
    public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register("HeaderBackground", typeof (Brush), typeof (GroupBox), (PropertyMetadata) null);
    public static readonly DependencyProperty HeaderBorderBrushProperty = DependencyProperty.Register("HeaderBorderBrush", typeof (Brush), typeof (GroupBox), (PropertyMetadata) null);
    public static readonly DependencyProperty HeaderMarginProperty = DependencyProperty.Register("HeaderMargin", typeof (Thickness), typeof (GroupBox), (PropertyMetadata) null);

    public UIElement Header
    {
      get
      {
        return (UIElement) ((DependencyObject) this).GetValue(GroupBox.HeaderProperty);
      }
      set
      {
        ((DependencyObject) this).SetValue(GroupBox.HeaderProperty, (object) value);
      }
    }

    public CornerRadius CornerRadius
    {
      get
      {
        return (CornerRadius) ((DependencyObject) this).GetValue(GroupBox.CornerRadiusProperty);
      }
      set
      {
        ((DependencyObject) this).SetValue(GroupBox.CornerRadiusProperty, (object) value);
      }
    }

    public CornerRadius HeaderCornerRadius
    {
      get
      {
        return (CornerRadius) ((DependencyObject) this).GetValue(GroupBox.HeaderCornerRadiusProperty);
      }
      set
      {
        ((DependencyObject) this).SetValue(GroupBox.HeaderCornerRadiusProperty, (object) value);
      }
    }

    public Thickness HeaderBorderThickness
    {
      get
      {
        return (Thickness) ((DependencyObject) this).GetValue(GroupBox.HeaderBorderThicknessProperty);
      }
      set
      {
        ((DependencyObject) this).SetValue(GroupBox.HeaderBorderThicknessProperty, (object) value);
      }
    }

    public Thickness HeaderMargin
    {
      get
      {
        return (Thickness) ((DependencyObject) this).GetValue(GroupBox.HeaderMarginProperty);
      }
      set
      {
        ((DependencyObject) this).SetValue(GroupBox.HeaderMarginProperty, (object) value);
      }
    }

    public Brush HeaderBackground
    {
      get
      {
        return (Brush) ((DependencyObject) this).GetValue(GroupBox.HeaderBackgroundProperty);
      }
      set
      {
        ((DependencyObject) this).SetValue(GroupBox.HeaderBackgroundProperty, (object) value);
      }
    }

    public Brush HeaderBorderBrush
    {
      get
      {
        return (Brush) ((DependencyObject) this).GetValue(GroupBox.HeaderBorderBrushProperty);
      }
      set
      {
        ((DependencyObject) this).SetValue(GroupBox.HeaderBorderBrushProperty, (object) value);
      }
    }

    public GroupBox()
    {
        DefaultStyleKey = typeof(GroupBox);
      //base.\u002Ector();
      //((Control) this).set_DefaultStyleKey((object) typeof (GroupBox));
    }

    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
    }
  }
}
