using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cards.Equips.DriveDiscs;
using ZZZLib.Cards.Equips.Other;
using ZZZLib.Cards.Equips.WEngine;
using ZZZLib.Cards.Partner;

namespace Remielle.Model
{
    public class RemielleCardPool:CustomCardPoolModel
    {
        public override string Title => "Remielle";

        public override string EnergyColorName => "remielle";

        public override string CardFrameMaterialPath => "card_frame_pink";

        public override Color DeckEntryCardColor => Colors.Pink;

        public override Color EnergyOutlineColor => Colors.Red;

        public override bool IsColorless => false;

        /// <summary>
        /// 记录角色所有卡牌
        /// </summary>
        /// <returns></returns>
        protected override CardModel[] GenerateAllCards()
        {
            return new CardModel[]
            {
                ModelDb.Card<Norma>(),
                ModelDb.Card<StarlightBilly>(),
                ModelDb.Card<Cissia>(),
                ModelDb.Card<Pyrois>(),
                ModelDb.Card<Lucy>(),
                ModelDb.Card<Burnice>(),
                ModelDb.Card<Promeia>(),
                 ModelDb.Card<Sunbringer>(),
                 ModelDb.Card<Velina>(),
                 ModelDb.Card<Sigrid>(),
                 ModelDb.Card<FeatheredFate>(),
                 ModelDb.Card<ParadiseLost>(),
                 ModelDb.Card<OdeOfResurrectedWings>(),
                 ModelDb.Card<CovenantOfDayat>()
            };
        }

    }
}
