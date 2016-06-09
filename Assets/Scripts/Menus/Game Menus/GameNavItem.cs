public class GameNavItem : NavItem
{
    public GameMenu.Actions Action;
    public string[] Data;

    public override void OnSelect(IMenu manager)
    {
        manager.OnNavItemSelected(this, Action, Data);
    }
}
