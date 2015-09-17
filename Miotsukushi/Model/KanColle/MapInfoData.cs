using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model.KanColle
{
    class MapInfoData
    {

        public string Name;
        public int AreaId;
        public int MapId;

        public enum MapDefeatType
        {
            Normal, CountOfDefeat, MaxHp
        }
        
        public int MapLevel;
        public string Opename;
        public string OpeInfo;
        public MapDefeatType DefeatType;
        public int DefeatCount;
    }
}
