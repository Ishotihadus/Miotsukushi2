using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle.BattleModels
{
    class MapModel : NotifyModelBase
    {
        public int MapareaId;
        public string MapareaName;

        public int MapNo;
        public string MapName;

        public int Level;

        public string OpeTitle;
        public string OpeInfo;

        public bool IsCleared;

        public int? RequiredDefeatCount;
        public int? NowDefeatCount;

        public int? MaxHp;
        public int? NowHp;
        public int? SelectedRank;
    }
}
