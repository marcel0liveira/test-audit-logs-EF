namespace JobApp.Domain
{
    namespace FCG.Users.Domain.Abstractions
    {
        public abstract class BaseEntity
        {
            public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
            public DateTimeOffset? UpdatedAt { get; set; } = null;

            public Guid Id { get; init; } = Guid.NewGuid();
            public bool IsActive { get; protected set; } = true;


            public void Activate()
            {
                IsActive = true;
                UpdatedAt = DateTime.UtcNow;
            }

            public void Deactivate()
            {
                IsActive = false;
                UpdatedAt = DateTime.UtcNow;
            }

            protected BaseEntity() { 
            }

            protected BaseEntity(Guid id)
            {
                Id = id;
            }
        }
    }
}
