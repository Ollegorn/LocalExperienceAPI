namespace Ollegorn.LocalExperience.Persistence.Base;

using Kritikos.Configuration.Persistence.Contracts.Behavioral;

public abstract class LocalExperienceEntity<TKey> : IEntity<TKey>
  where TKey : IEquatable<TKey>, IComparable<TKey>
{
  public TKey Id { get; set; } = default!;
}
