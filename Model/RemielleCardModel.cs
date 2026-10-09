using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
using Remielle.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Model
{
    [Pool(typeof(RemielleCardPool))]
    public abstract class RemielleCardModel
        (int baseCost, CardType type, CardRarity rarity, TargetType target, bool showInCardLibrary = true, bool autoAdd = true) :
        CustomCardModel(baseCost, type, rarity, target, showInCardLibrary, autoAdd)
    {
        protected virtual string PortraitName => Id.Entry.RemovePrefix().ToSnakeCase();

        public override string? CustomPortraitPath =>
            PathHelper.GetCardImageBigPath(PortraitName);

        public override string PortraitPath =>
            PathHelper.GetCardImageBigPath(PortraitName);


    }
}
