using System;
using System.Collections.Generic;


namespace Rikkonbi.Core.SharedKernel
{
    public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        
        public TKey Id { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}