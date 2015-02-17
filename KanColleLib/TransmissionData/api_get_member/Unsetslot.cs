using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Unsetslot
    {
        public Dictionary<string, List<int>> unsetslots;

        public static Unsetslot fromDynamic(dynamic json)
        {
            Unsetslot unsetslot = new Unsetslot();
            unsetslot.unsetslots = new Dictionary<string, List<int>>();

            foreach (KeyValuePair<string, dynamic> item in json)
            {
                if(item.Value is double)
                {
                    // -1
                    unsetslot.unsetslots.Add(item.Key, new List<int>());
                }
                else
                {
                    List<int> values = new List<int>();
                    foreach (var value in item.Value)
                        values.Add((int)value);
                    unsetslot.unsetslots.Add(item.Key, values);
                }
            }

            return unsetslot;
        }
    }
}
