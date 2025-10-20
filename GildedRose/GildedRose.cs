using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            UpdateItemQuality(item);
        }
    }
    
    private void UpdateItemQuality(Item item)
    {
        bool isConjured = item.Name.StartsWith("Conjured");
        bool isAgedBrie = item.Name == "Aged Brie";
        bool isBackstagePass = item.Name == "Backstage passes to a TAFKAL80ETC concert";
        bool isSulfuras = item.Name == "Sulfuras, Hand of Ragnaros";
        
        if (isSulfuras) return;
            
        item.SellIn -= 1;

        if (isAgedBrie)
            UpdateAgedBrie(item);
        else if (isBackstagePass)
            UpdateBackstagePass(item);
        else
            UpdateNormalOrConjuredItem(item, isConjured);
        
        item.Quality = Math.Clamp(item.Quality, 0, 50);
    }
    
    private void UpdateAgedBrie(Item item)
    {
        if (item.SellIn < 0)
            item.Quality += 2;
        else
            item.Quality += 1;
    }
    
    private void UpdateBackstagePass(Item item)
    {
        if (item.SellIn < 0)
        {
            item.Quality = 0;
            return;
        }

        if (item.Quality >= 50)
            return;

        int increase = 1;
        if (item.SellIn < 10) increase++;
        if (item.SellIn < 5) increase++;

        item.Quality += increase;
    }
    
    private void UpdateNormalOrConjuredItem(Item item, bool isConjured)
    {
        if (item.Quality <= 0) return;

        int degrade = (item.SellIn < 0 ? 2 : 1);

        item.Quality -= degrade;
    }
}