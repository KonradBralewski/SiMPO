using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Shop
{
    public partial class ProductCard
    {
        [Parameter]
        public string? Name { get; set; }
        [Parameter]
        public int? Price { get; set; }
        [Parameter]
        public string? Description { get; set; }

        [Parameter]
        public string? Image { get; set; }

        private bool _shouldDisableCard;
        protected override void OnInitialized()
        {
            Name ??= "Unknown product";
            Description ??= "Unknown description";

            if(Price is null)
            {
                _shouldDisableCard = true;
                Price = 0;
                return;
            }

            _shouldDisableCard = false;            
        }

    }
}
