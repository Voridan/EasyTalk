using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Annotations
{
    public abstract class BaseEntityAnnotation<T> : IBaseEntityAnnotation where T : class
    {
        protected EntityTypeBuilder<T> ModelBuilder { get; }

        protected BaseEntityAnnotation(ModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder.Entity<T>();
        }

        public abstract void Annotate();
    }
}
