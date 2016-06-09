public class GameNavItem : NavItem
{
    public GameMenu.Actions Action;
    public string[] Data;

    public override void OnSelect(Menu manager)
    {
        manager.OnNavItemSelected(this, Action, Data);
    }
}
