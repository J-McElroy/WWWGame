namespace WWWGame.UI
{
    public class NavigateToPageMessage
    {
        public NavigateToPageMessage()
        {

        }

        public string PageName { get; set; }
        public object Param { get; set; }
    }
}