using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel.DetailInfoPanel.Battle
{
    class BattleShipViewModel : ViewModelBase
    {
        public string ShipName { get; set; }
        public int ShipLevel { get; set; }
        public int AfterHP { get; set; }
        public int MaxHP { get; set; }
        public int SumAttack { get; set; }
        public int SumDamage { get; set; }
        public int Firepower { get; set; }
        public int Torpedo { get; set; }
        public int AntiAir { get; set; }
        public int Armor { get; set; }
        public int Speed { get; set; }
        public bool IsFriend { get; set; }
        public List<BattleShipSlotViewModel> Slot { get; set; }

        public class BattleShipSlotViewModel
        {
            public System.Windows.Media.Color ItemTypeColor { get; set; }
            public string ItemName { get; set; }
        }
    }
}
