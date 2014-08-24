using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib.TransmissionData.api_get_member.values;

namespace KanColleLib.TransmissionData.api_get_member
{
    public class Furniture
    {
        List<FurnitureValue> furnitures;

        public static Furniture fromDynamic(dynamic json)
        {
            Furniture furniture = new Furniture();
            furniture.furnitures = new List<FurnitureValue>();
            foreach (var data in json)
                furniture.furnitures.Add(FurnitureValue.fromDynamic(data));
            return furniture;
        }
    }
}
