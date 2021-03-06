﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface ICrudService<Key, Item>
    {
        Item Create(Item item);
        Item Read(Key key);
        void Update(Item item);
        void Delete(Key key);
        ICollection<Item> FindAll();
    }
}
