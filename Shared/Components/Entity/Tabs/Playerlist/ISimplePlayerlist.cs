using ForkCommon.Model.Entity.Pocos.Player;

namespace ForkFrontend.Shared.Components.Entity.Tabs.Playerlist;

public interface ISimplePlayerlist
{
    public Player? ActivePlayer { get; set; }
    public void SelectPlayer(PlayerComponent playerComponent);
}