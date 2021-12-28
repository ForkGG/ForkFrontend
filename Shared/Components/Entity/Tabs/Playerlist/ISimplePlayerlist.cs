using ProjectAveryCommon.Model.Entity.Pocos.Player;

namespace ProjectAveryFrontend.Shared.Components.Entity.Tabs.Playerlist;

public interface ISimplePlayerlist
{
    public Player? ActivePlayer { get; set; }
    public void SelectPlayer(PlayerComponent playerComponent);
}