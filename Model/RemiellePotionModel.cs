using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Model
{
    [Pool(typeof(RemiellePotionPool))]
    public abstract class RemiellePotionModel : CustomPotionModel
    {

    }
}
