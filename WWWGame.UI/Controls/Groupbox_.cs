using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WWWGame.UI.Controls
{
    /// <summary>
    /// Implements a GroupBox control
    /// </summary>
    /// <remarks>http://programmerpayback.com/2008/11/26/silverlight-groupbox-control/</remarks>
    public class GroupBox : ContentControl 
    { 
        
        private RectangleGeometry _fullRect; 
        private RectangleGeometry _headerRect; 
        private ContentControl _headerContainer; 
        
        public GroupBox() 
        { 
            DefaultStyleKey = typeof(GroupBox);
            this.SizeChanged += GroupBox_SizeChanged;
        } 
        
        public override void OnApplyTemplate() 
        { 
            base.OnApplyTemplate(); 
            
            _fullRect = (RectangleGeometry)GetTemplateChild("FullRect"); 
            _headerRect = (RectangleGeometry)GetTemplateChild("HeaderRect"); 
            _headerContainer = (ContentControl)GetTemplateChild("HeaderContainer");
            _headerContainer.SizeChanged += HeaderContainer_SizeChanged;
        } 
        
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(GroupBox), null); 
        public object Header { 
            get { return GetValue(HeaderProperty); } 
            set { SetValue(HeaderProperty, value); } 
        } 
        
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(GroupBox), null); 
        public DataTemplate HeaderTemplate { 
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); } 
            set { SetValue(HeaderTemplateProperty, value); } 
        }

        private void GroupBox_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            _fullRect.Rect = new Rect(new Point(), e.NewSize);
        }

        private void HeaderContainer_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            _headerRect.Rect = new Rect(new Point(_headerContainer.Margin.Left, 0), e.NewSize);
        } 
    } 
}
