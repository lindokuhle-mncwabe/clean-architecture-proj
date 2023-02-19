using System;
using System.Collections.Generic;
using GatheringEvents.Domain.Events;

namespace GatheringEvents.Domain.Types;

public abstract class Entity : IEquatable<Entity>
{
    #region ~Constructors
    protected Entity(Guid id)
    {
        Id = id;
    }
    #endregion
   
    #region ~Props
    public Guid Id { get; private init; }
    #endregion

    #region ~Operators
    public static bool operator ==(Entity? first, Entity? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }    

    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }
    #endregion

    #region ~MethodOverrides
    public override bool Equals(object? obj)
    {
        if (obj is null ) {
            return false;
        }

        if (obj.GetType() != GetType()){
            return false;
        }

        if (obj is not Entity entity){
            return false;
        }

        return entity.Id == Id;
    }

    public bool Equals(Entity? other)
    {
        if (other is null){
            return false;
        }

        if (other.GetType() != GetType()){
            return false;
        }

        return other.Id == Id;       
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
    #endregion

    #region ~DomainEvents
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    protected void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
    #endregion
}