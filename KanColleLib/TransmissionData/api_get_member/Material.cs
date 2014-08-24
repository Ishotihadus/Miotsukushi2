using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    /// <summary>
    /// 資材情報
    /// </summary>
    public class Material
    {
        public List<MaterialValue> materials;

        public static Material fromDynamic(dynamic json)
        {
            Material material = new Material();

            material.materials = new List<MaterialValue>();
            foreach (var data in json)
                material.materials.Add(MaterialValue.fromDynamic(data));

            return material;
        }

    }
}
