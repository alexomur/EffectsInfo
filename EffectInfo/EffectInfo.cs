using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;

namespace EffectInfo
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Author => "DrBright";

        public override string Name => "EffectsInfo";

        public override string Prefix => Name;

        public static Plugin Instance;

        private EventHandlers _handlers;

        public override void OnEnabled()
        {
            Instance = this;

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            Instance = null;

            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _handlers = new EventHandlers();
            Player.ChangingRole += _handlers.OnChangingRole;
            Player.ReceivingEffect += _handlers.OnReceivingEffect;
        }

        private void UnregisterEvents()
        {
            _handlers = null;
        }
    }
}