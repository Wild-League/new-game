using Fusion;

namespace _Project.Scripts.Networking
{
    public class NetworkGameplayHandler : NetworkBehaviour
    {
        public static NetworkGameplayHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public static void UseSpell()
        {
            
        }
    }
}