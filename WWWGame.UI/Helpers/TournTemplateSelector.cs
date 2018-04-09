using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WWWGame.UI.Model;

namespace WWWGame.UI.Helpers
{
    public class TournTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Tourn
        {
            get;
            set;
        }

        public DataTemplate SubLevel
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            TournRow tournRow = item as TournRow;
            if (tournRow != null)
            {
                if (tournRow.HasChildTourns)
                {
                    return SubLevel;
                }
                else
                {
                    return Tourn;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
