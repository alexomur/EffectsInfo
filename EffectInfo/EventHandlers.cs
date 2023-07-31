using System.Linq;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace EffectInfo
{
    public class EventHandlers
    {
        private readonly Config _config = Plugin.Instance.Config;

        private bool IsPlaying(Player player)
        {
            if (player.Role == RoleTypeId.None || player.Role == RoleTypeId.Overwatch ||
                player.Role == RoleTypeId.Spectator)
            {
                return false;
            }

            if (!player.IsConnected || !player.IsVerified)
            {
                return false;
            }

            return true;
        }
        
        public void OnReceivingEffect(ReceivingEffectEventArgs ev)
        {

            if (!IsPlaying(ev.Player)) return;

            string effectName = EffectTypeExtension.GetEffectType(ev.Effect).ToString();
            
            // Creating HintText. Checking if there is text on the screen.
            string hintText = _config.HintText;
            if (ev.Player.CurrentHint != null && ev.Player.CurrentHint.Content != "")
            {
                hintText = ev.Player.CurrentHint.Content;
            }

            // Handling the case when the effect should be removed.
            if (ev.Intensity == 0 && ev.Player.ActiveEffects.Contains(ev.Effect))
            {
                if (ev.Player.ActiveEffects.Count() == 1)
                {
                    ev.Player.ShowHint("", 0);
                    return;
                }
                hintText = hintText.Replace("\n<align=left>          " + effectName + "</align>", "");
                ev.Player.ShowHint(hintText, _config.HintDuration);
                return;
            }
            
            if (ev.Player.ActiveEffects.Contains(ev.Effect)) return;
            
            hintText += "\n<align=left>          " + effectName + "</align>";
            ev.Player.ShowHint(hintText, _config.HintDuration);
        }
        
        // Cleansing Hint on death
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (!IsPlaying(ev.Player))
            {
                ev.Player.ShowHint("");
            }
        }

    }
}