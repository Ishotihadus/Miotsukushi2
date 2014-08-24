using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Unsetslot
    {
        Dictionary<string, List<int>> unsetslots;

        public static Unsetslot fromDynamic(dynamic json)
        {
            Unsetslot unsetslot = new Unsetslot();
            unsetslot.unsetslots = new Dictionary<string, List<int>>();

            foreach (KeyValuePair<string, dynamic> item in json)
            {
                try
                {
                    List<int> values = new List<int>();
                    foreach (var value in item.Value)
                        values.Add((int)value);
                    unsetslot.unsetslots.Add(item.Key, values);
                }
                catch
                {
                    // 明示的にnullを追加しておく
                    unsetslot.unsetslots.Add(item.Key, null);
                }
            }

            return unsetslot;
        }
    }
}
